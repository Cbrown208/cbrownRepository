using System;
using ClosedXML.Excel;

namespace ExcelTesting.Models
{
	public class ExcelTestingObject
	{
		public int Id { get; set; }
		public string ClientId { get; set; }
		public string FacilityId { get; set; }
		public string MappingFileName { get; set; }
		public bool IsAwesome { get; set; }
		public DateTime CreatedBy { get; set; }
	}
}
