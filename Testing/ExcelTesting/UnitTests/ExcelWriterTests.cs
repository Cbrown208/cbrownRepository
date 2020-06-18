using FluentAssertions;
using NUnit.Framework;
using Pas.ExcelExporterTests.Models;

namespace ExcelTesting.UnitTests
{
	[TestFixture]
	public class ExcelWriterTests
	{
		private readonly ExcelTestingManager _manager = new ExcelTestingManager();
		private readonly ExcelTestData _testData = new ExcelTestData();

		[Test]
		public void ExportToStream()
		{
			var pricingList = _testData.GetMinValueTestingData();
			var result = _manager.ExportToExcelFile(pricingList, "TestingSheet");
			result.Should().NotBeNull();
		}

		[Test]
		public void ExportToStreamNoSheetName()
		{
			var pricingList = _testData.GetMinValueTestingData();
			var result = _manager.ExportToExcelFile(pricingList, "");
			result.Should().NotBeNull();
		}

		[Test]
		public void ExportToStreamAndReadBack()
		{
			var pricingList = _testData.GetMinValueTestingData();
			var data = _manager.ExportToExcelFile(pricingList, "TestingSheet");
			var result = _manager.ReadExcelFile<ExcelExporterTestClass2>(data);

			result.Count.Should().Be(3);
			result.Should().NotBeNull();
		}
	}
}
