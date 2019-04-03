using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Pas.ExcelExporterTests.Models;

namespace ExcelTesting.UnitTests
{
	public class ExcelTestData
	{
		public List<ExcelExporterTestClass2> GetExcelTestingData()
		{
			return new List<ExcelExporterTestClass2>{new ExcelExporterTestClass2{ClientId = "1234", FacilityId = "TestingFac", Id = 1, MappingFileName = "TestMap",IsAwesome = true, CreatedOn = DateTime.Now},
				new ExcelExporterTestClass2{ClientId = "8888", FacilityId = "TestingFac", Id = 1, MappingFileName = "TestMap",IsAwesome = true, CreatedOn = DateTime.Now},
				new ExcelExporterTestClass2{ClientId = "9999", FacilityId = null, Id = 0, MappingFileName = "TestMap",IsAwesome = false, CreatedOn = DateTime.MaxValue}
			};
		}
		public List<ExcelExporterTestClass2> GetMinValueTestingData()
		{
			return new List<ExcelExporterTestClass2>{new ExcelExporterTestClass2{ClientId = "1234", FacilityId = "TestingFac", Id = 1, MappingFileName = "TestMap",IsAwesome = true, CreatedOn = DateTime.Now},
				new ExcelExporterTestClass2{ClientId = "8888", FacilityId = "TestingFac", Id = 1, MappingFileName = "TestMap",IsAwesome = true, CreatedOn = DateTime.MaxValue},
				new ExcelExporterTestClass2{ClientId = "9999", FacilityId = null, Id = 0, MappingFileName = "TestMap",IsAwesome = false, CreatedOn = DateTime.MinValue}
			};
		}

		public string GetTestFilePath()
		{
			return Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)) + @"\TestFiles\";

		}
	}
}
