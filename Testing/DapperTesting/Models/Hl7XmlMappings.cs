using System;

namespace DapperTesting.Models
{
	public class Hl7XmlMappings
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public Guid FacilityId { get; set; }
		public string MappingFileName { get; set; }
	}
}
