using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Dapper;
using DapperTesting.Models;
using ExcelReader;

namespace DapperTesting
{
	class Program
	{
		private const string OutputFileName = "LaneInsPlanCompare.csv";

		private static readonly string _outputPath =
			Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)) +
			"\\" + OutputFileName;

		[STAThreadAttribute]
		static void Main(string[] args)
		{
			try
			{
				//BuildPccInsertScript();
				TestInsPlanCompare();
				//RunMain();

				Console.ReadLine();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.ReadLine();
			}
		}

		private static void RunMain()
		{
			var connectionString =
				@"Server = LEWVHPASDB01.nthrivenp.nthcrpnp.com\RC; Database = AMS_CHSQ; Trusted_Connection = true;";

			using (var db = new SqlConnection(connectionString))
			{
				db.Open();
				for (int i = 25; i <= 1000; i++)
				{
					try
					{
						var query = "Kill " + i;
						Console.WriteLine(query);
						db.Execute(query);
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}
			}


			var queryBuilder = new QueryBuilder();
			var centuraConnectionString =
				@"Server = RCM41VQPASDB01.medassets.com\PASDEVIV; Database = AMS_QA_CHC; Trusted_Connection = true;";
			var serviceCategoryCenturaConnectionString =
				@"Server = RCM41VSPASDB02.medassets.com; Database = SC_Centura; Trusted_Connection = true;";
			var dbContext = new DbContext(serviceCategoryCenturaConnectionString);

			var tableNameList = dbContext.GetListofTableNames().ToList();

			var results = dbContext.BuildQueryCheckStatementForSc(tableNameList);

			//var excelImport = new ExcelReaderSvc();
			//var path = @"C:\MyScripts\Documents\DI\InformationForChrisBrown.xlsx";
			//var excelValueList = excelImport.GetExcelFile(path);
			//var tableName = "Hl7XmlMappings";
			//var mappingsList = new List<Hl7XmlMappings>();
			//foreach (var excelObject in excelValueList)
			//{
			//	mappingsList.Add(new Hl7XmlMappings
			//	{
			//		ClientId = Convert.ToInt32(excelObject.ClientId),
			//		FacilityId = new Guid(excelObject.FacilityId),
			//		MappingFileName = excelObject.MappingFileName
			//	});
			//}

			//	dbContext.AddXmlMapping(mappingsList, tableName);
			//var healthChecks = dbContext.GetAllXmlMappings();

			//var firstHealthCheck = healthChecks.FirstOrDefault();
			//if (firstHealthCheck != null)
			//{
			//	//Console.WriteLine(firstHealthCheck.SiteName);
			//}
		}

		private static void BuildPccInsertScript()
		{
			var queryBuilder = new QueryBuilder();
			var baseServerName = "LEWVPPAS";
			var sDescription = "Task Server";
			var results = "";

			for (int i = 1; i < 21; i++)
			{
				var webServerName = baseServerName + "WEB";
				sDescription = "Web Server";
				var sName = webServerName + String.Format("{0:00}", i) + ".nthext.com";
				//Console.WriteLine(sName);
				results = results + queryBuilder.BuildPccInsertStatement(sName, sDescription);
			}

			for (int i = 1; i < 33; i++)
			{
				var appServerName = baseServerName + "APP";
				sDescription = "App Server";
				var sName = appServerName + String.Format("{0:00}", i) + ".nthrive.nthcrp.com";
				//Console.WriteLine(sName);
				results = results + queryBuilder.BuildPccInsertStatement(sName, sDescription);
			}


			for (int i = 1; i < 53; i++)
			{
				var tskServerName = baseServerName + "TSK";
				sDescription = "Task Server";
				var sName = tskServerName + String.Format("{0:00}", i) + ".nthrive.nthcrp.com";
				//Console.WriteLine(sName);
				results = results + queryBuilder.BuildPccInsertStatement(sName, sDescription);
			}

			Console.WriteLine(results);
			Clipboard.SetText(results);
		}

		private static void TestInsPlanCompare()
		{
			CleanOutputFile(OutputFileName);
			var cmDbConnection =
				"Server=LEWVPCMGDB39.nthrive.nthcrp.com;Database=RMT_LANC_Contract; Trusted_Connection=true;";

			var CmPlanList = new List<CMPlanDetails>();

			using (var db = new SqlConnection(cmDbConnection))
			{
				db.Open();
				var query = "exec usp_PAS_GetActivePlansForFacility 632, '20220110', '20230123', 'I'";
				var queryResults = db.Query<CMPlanDetails>(query).ToList();
				CmPlanList.AddRange(queryResults);
				db.Close();
			}

			foreach (var result in CmPlanList)
			{
				Console.Write(result.PlanCode + Environment.NewLine);
			}

			var pasPlanList = new List<PasPlanDetails>();
			var pasDbServerConnection =
				"Server=LEWVPPASDB01.nthrive.nthcrp.com;Database=AMS_LANC; Trusted_Connection=true;";
			using (var db = new SqlConnection(pasDbServerConnection))
			{
				db.Open();
				var query = GetPasInsurancePlanQuery();
				var queryResults = db.Query<PasPlanDetails>(query).ToList();
				pasPlanList.AddRange(queryResults);
				db.Close();
			}

			var resultList = new List<CombinedPlanDetails>();
			foreach (var pasPlan in pasPlanList)
			{
				var plan = new CombinedPlanDetails
				{
					PasPlan = pasPlan
				};
				foreach (var cmPlan in CmPlanList)
				{
					if (pasPlan.PlanCode.ToLower() == cmPlan.PlanCode.ToLower())
					{
						plan.CmPlan = cmPlan;
					}
				}

				resultList.Add(plan);
			}

			WriteValueToFile("FacilityName,nThriveId,PasPlanCode,PasPayorName,PasPayorMasterId,CmPlanCode,CmContractPlanName");
			foreach (var result in resultList)
			{
				var outputResult = "";
				var pasPayorName = result.PasPlan?.PayorName?.Replace(",", "");
				if (result.CmPlan != null)
				{
					outputResult = result.PasPlan.FacilityName + "," + result.PasPlan.nThriveId + "," +
								   result.PasPlan.PlanCode + "," + pasPayorName + "," + result.PasPlan.PayorMasterId + "," +
								   result.CmPlan.PlanCode + "," + result.CmPlan.PlanName;
				}
				else
				{
					outputResult = result.PasPlan.FacilityName + "," + result.PasPlan.nThriveId + "," +
									   result.PasPlan.PlanCode + "," + pasPayorName + "," + result.PasPlan.PayorMasterId + "," +
									   "N/A" + "," + "N/A";
				}

				WriteValueToFile(outputResult);
			}

			Process.Start(_outputPath);
		}

		private static void CleanOutputFile(string outputFileName)
		{
			File.WriteAllText(outputFileName, string.Empty);
		}

		public static void WriteValueToFile(string value)
		{
			try
			{
				using (var file = new StreamWriter(OutputFileName, true))
				{
					file.WriteLineAsync(value);
				}

				Console.WriteLine(value);
			}
			catch (Exception ex)
			{
				Console.WriteLine(value);
			}
		}

		private static string GetPasInsurancePlanQuery()
		{
			return @"Select fac.Name as FacilityName, fac.medassetsId as nThriveId,
ip.PlanCode as PlanCode,
ip.PlanTypeId as PlanTypeId, 
pt.Name as PlanType,
cp.CompanyName as 'PayorName',
pm.CompanyName as 'PasPayorName',
pm.PayorMasterId as PayorMasterId
From InsurancePlan ip (nolock)
LEFT OUTER JOIN PlanType pt (nolock) ON ip.PlanTypeId = pt.PlanTypeId
LEFT OUTER JOIN ClientPayor cp (nolock) on ip.ClientPayorId = cp.ClientPayorId 
LEFT OUTER JOIN PayorMaster pm (nolock) on cp.PayorMasterId = pm.PayorMasterId
LEFT OUTER JOIN aaafacilities fac (nolock) ON fac.FacilityId = ip.FacilityId
Where pm.PayerRefId IS NOT NULL
AND ip.IsActive = 1";
		}
	}

	public class CMPlanDetails
	{
		public string PlanCode { get; set; }
		public string PlanName { get; set; }
		public string PlanDescription { get; set; }
	}

	public class PasPlanDetails
	{
		public string FacilityName { get; set; }
		public int nThriveId { get; set; }
		public string PlanCode { get; set; }
		public string PlanType { get; set; }
		public string PayorName { get; set; }
		public string PasPayorName { get; set; }
		public string PayorMasterId { get; set; }
	}

	public class CombinedPlanDetails
	{
		public CMPlanDetails CmPlan { get; set; }
		public PasPlanDetails PasPlan { get; set; }
	}
}
