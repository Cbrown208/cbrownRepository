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
	}
}
