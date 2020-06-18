using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Formatters
{
	public class JsonFormatter
	{
		/// <summary>
		/// Converts to ALL lower case Json Object
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string ToJson(object obj)
		{
			var settings = new JsonSerializerSettings { ContractResolver = new LowercaseContractResolver() };
			return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
		}

		/// <summary>
		/// Coverts Object to Camel Case Json
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string ToCamelCaseJson(object obj)
		{
			var settings = new JsonSerializerSettings{ContractResolver = new CamelCasePropertyNamesContractResolver()};
			return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
		}
	}

	public class LowercaseContractResolver : DefaultContractResolver
	{
		protected override string ResolvePropertyName(string propertyName)
		{
			return propertyName.ToLower();
		}
	}
}
