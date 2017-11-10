using System;
using System.Collections.Generic;
using System.Linq;
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
				var dbContext = new DbContext();
				var excelImport = new ExcelReaderSvc();
				var path = @"C:\MyScripts\Documents\DI\InformationForChrisBrown.xlsx";
				var excelValueList = excelImport.GetExcelFile(path);
				var tableName = "Hl7XmlMappings";
				var mappingsList = new List<Hl7XmlMappings>();
				foreach (var excelObject in excelValueList)
				{
					mappingsList.Add(new Hl7XmlMappings
					{
						ClientId = Convert.ToInt32(excelObject.ClientId),
						FacilityId = new Guid(excelObject.FacilityId),
						MappingFileName = excelObject.MappingFileName
					});
				}
				
					dbContext.AddXmlMapping(mappingsList, tableName);
				var healthChecks = dbContext.GetAllXmlMappings();

				var firstHealthCheck = healthChecks.FirstOrDefault();
				if (firstHealthCheck != null)
				{
					//Console.WriteLine(firstHealthCheck.SiteName);
				}
				Console.ReadLine();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
