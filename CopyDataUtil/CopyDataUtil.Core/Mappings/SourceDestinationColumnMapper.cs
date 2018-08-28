using System.Collections.Generic;
using System.IO;
using CopyDataUtil.Core.Models.DbModels;
using Newtonsoft.Json;

namespace CopyDataUtil.Core.Mappings
{
	public class SourceDestinationColumnMapper
	{
		public static RootObject GetMappings()
		{
			string path = Directory.GetCurrentDirectory() + "\\Mappings\\SourceDestinationColumnMappings.json";

			var sourceDestinationMappings = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(path));

			return sourceDestinationMappings;
		}

		public static RootObject GetTempMappings()
		{
			string path = Directory.GetCurrentDirectory() + "\\Mappings\\TempMappings.json";
			//path = @"C:\Dev\cbrownRepository\CopyDataUtil\CopyDataUtil.Core\Mappings\TempMappings.json";

			var sourceDestinationMappings = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(path));

			return sourceDestinationMappings;
		}

		public static List<DataFactoryTableSchema> GetServiceCategoryMappings()
		{
			string path = @"C:\Dev\PAS_ServiceCategory\src\Pas.ServiceCategory.Processor\Pas.DataFactory.Pipeline.Template\ScSchemaMappings.json";

			var sourceDestinationMappings = JsonConvert.DeserializeObject<List<DataFactoryTableSchema>>(File.ReadAllText(path));

			return sourceDestinationMappings;
		}
	}
}
