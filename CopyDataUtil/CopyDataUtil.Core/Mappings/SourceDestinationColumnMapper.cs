using System.IO;
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

		public static string SaveToMappingFile(RootObject configForFile)
		{
			string path = Directory.GetCurrentDirectory() + "\\Mappings\\SourceDestinationColumnMappings.json";
			using (StreamWriter file = File.CreateText(path))
			{
				JsonSerializer serializer = new JsonSerializer();
				//serialize object directly into file stream
				serializer.Serialize(file, configForFile);
			}
			return Directory.GetCurrentDirectory() + "\\Mappings\\SourceDestinationColumnMappings.json";
		}
	}
}
