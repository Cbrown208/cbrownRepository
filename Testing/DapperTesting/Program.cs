using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperTesting.Models;
using ExcelReader;

namespace DapperTesting
{
	class Program
	{
		
		static void Main(string[] args)
		{
			try
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
				Console.ReadLine();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.ReadLine();
			}
		}
	}
}
