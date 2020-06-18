using Newtonsoft.Json;

namespace Common.Formatters
{
	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}

	public class PersonJsonAttributes
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("first")]
		public string FirstName { get; set; }
		[JsonProperty("last")]
		public string LastName { get; set; }
		[JsonProperty("TheWall")]
		public string Email { get; set; }
	}
}
