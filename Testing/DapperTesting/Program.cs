using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DapperTesting.Models;
using ExcelReader;

namespace DapperTesting
{
	class Program
	{
		[STAThreadAttribute]
		static void Main(string[] args)
		{
			try
			{
				BuildPccInsertScript();

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
			var connectionString = @"Server = LEWVHPASDB01.nthrivenp.nthcrpnp.com\RC; Database = AMS_CHSQ; Trusted_Connection = true;";

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
			var centuraConnectionString = @"Server = RCM41VQPASDB01.medassets.com\PASDEVIV; Database = AMS_QA_CHC; Trusted_Connection = true;";
			var serviceCategoryCenturaConnectionString = @"Server = RCM41VSPASDB02.medassets.com; Database = SC_Centura; Trusted_Connection = true;";
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
				var sName = tskServerName + String.Format("{0:00}", i)+ ".nthrive.nthcrp.com";
				//Console.WriteLine(sName);
				results = results +  queryBuilder.BuildPccInsertStatement(sName, sDescription);
			}
			Console.WriteLine(results);
			Clipboard.SetText(results);

		}
	}
}
