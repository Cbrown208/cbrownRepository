using System;
using System.IO;
using Newtonsoft.Json;
using Tools.Core.Models;
using Tools.DataAccess;

namespace Tools.FileGenerator
{
	public class FileGeneratorManager
	{
		private const string OutputPath = @"C:\MyScripts\Temp\";
		private const string EiConnectionString = @"Server = LEWVQPASDBGL01.nthrivenp.nthcrpnp.com\IV; Database = EligibilityIntegrity; Trusted_Connection = true;";
		private const string LocalConnectionString = @"Server = .\localdb; Database = EbTest; Trusted_Connection = true;";

		public void RunFileGenerator()
		{
			Console.WriteLine("Starting to Generated Files");

			GenerateEbPortalMappingConfigJsonFile("QA");

			Console.WriteLine("File Generated Successful at: " + OutputPath);
		}

		private void GenerateEbPortalMappingConfigJsonFile(string env)
		{
			var eiDbContext = new DbContext(LocalConnectionString);
			var query = "SELECT * FROM EbPortalMapping";
			var eiPortalMappingList = eiDbContext.GetAllList<EbPortalMapping>(query);

			using (StreamWriter file = File.CreateText(OutputPath + @"EbPortalMapping."+env+".json"))
			{
				var serializer = new JsonSerializer { Formatting = Formatting.Indented };
				serializer.Serialize(file, eiPortalMappingList);
			}
		}
	}
}
