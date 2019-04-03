using System.Linq;
using Common.Formatters.Converters;
using FluentAssertions;
using NUnit.Framework;
using Pas.ExcelExporterTests.Models;

namespace ExcelTesting.UnitTests
{
	public class ExcelReaderTests
	{
		[TestFixture]
		public class ExcelTestingUnitTests
		{
			private readonly DataTableConverter _dtConverter = new DataTableConverter();
			private readonly ExcelReader _excelReader = new ExcelReader();
			private readonly ExcelTestingManager _manager = new ExcelTestingManager();
			private readonly ExcelTestData _testData = new ExcelTestData();

			[Test]
			public void ReadExcelTestClass1()
			{
				var path = _testData.GetTestFilePath() + @"ExcelTestFile.xlsx";
				var result = _manager.ReadExcelFile<ExcelExporterTestClass1>(path);

				result.Count.Should().BeGreaterThan(1);
				var itemResult = result.First();
				itemResult.Id.Should().Be(1);
			}

			[Test]
			public void ReadExcelTestClass2()
			{
				var path = _testData.GetTestFilePath() + @"ExcelTestFile2.xlsx";
				var result = _manager.ReadExcelFile<ExcelExporterTestClass2>(path);

				result.Count.Should().BeGreaterThan(1);
				var itemResult = result.First();
				itemResult.Id.Should().Be(1);
			}

			[Test]
			public void ReadExcelWithMissMatchedHeaderNames()
			{
				var path = _testData.GetTestFilePath() + @"ExcelTestFileMissMatched.xlsx";
				var result = _manager.ReadExcelFile<ExcelExporterTestClass2>(path);

				result.Count.Should().BeGreaterThan(1);
				var itemResult = result.First();
				itemResult.Id.Should().Be(1);
				itemResult.FacilityId.Should().BeNullOrWhiteSpace();
			}

			[Test]
			public void HeaderTest()
			{
				var fileTempName = "ExcelTestFile";
				var path = _testData.GetTestFilePath();
				var resultFilePath = path + fileTempName + ".xlsx";
				var dt = _excelReader.ReadExcelDataFromFile(resultFilePath);

				dt.Columns[0].ColumnName.Should().Be("Id");
				dt.Columns[1].ColumnName.Should().Be("FacilityId");
				dt.Columns[2].ColumnName.Should().Be("Code");
				dt.Columns[3].ColumnName.Should().Be("Description");
				dt.Columns[4].ColumnName.Should().Be("Price");
				dt.Columns[5].ColumnName.Should().Be("Notes");
				dt.Columns[6].ColumnName.Should().Be("LastTouchedDate");

				dt.Rows[0].ItemArray[2].ToString().Should().Be("1234");
				dt.Rows[1].ItemArray[2].ToString().Should().Be("5678");
			}

			[Test]
			public void SecondObjectTest()
			{
				var fileTempName = "ExcelTestFile2";
				var path = _testData.GetTestFilePath();
				var resultFilePath = path + fileTempName+".xlsx";
				var dt = _excelReader.ReadExcelDataFromFile(resultFilePath);

				dt.Columns[0].ColumnName.Should().Be("Id");
				dt.Columns[1].ColumnName.Should().Be("ClientId");
				dt.Columns[2].ColumnName.Should().Be("FacilityId");
				dt.Columns[3].ColumnName.Should().Be("MappingFileName");

				dt.Rows[0].ItemArray[1].ToString().Should().Be("1234");
				dt.Rows[1].ItemArray[1].ToString().Should().Be("8888");
				dt.Rows[2].ItemArray[1].ToString().Should().Be("9999");
			}

			[Test]
			public void ConvertToListTest()
			{
				var pricingList = _testData.GetExcelTestingData();
				var data = _dtConverter.ConvertToDatatable(pricingList);

				var result = _dtConverter.ConvertDatatableToList<ExcelExporterTestClass2>(data);

				result.Count.Should().BeGreaterThan(2);
				var itemResult = result.First();
				itemResult.Id.Should().Be(1);
			}
		}
	}
}
