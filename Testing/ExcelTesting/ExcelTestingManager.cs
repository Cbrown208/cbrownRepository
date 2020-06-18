using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Common.Formatters.Converters;
using ExcelTesting.Models;

namespace ExcelTesting
{
	public class ExcelTestingManager
	{
		private readonly CsvWriter _csvWriter = new CsvWriter();
		private readonly ExcelWriter _excelWriter = new ExcelWriter();
		private readonly DataTableConverter _dtConverter = new DataTableConverter();
		private readonly ExcelReader _excelReader = new ExcelReader();
		public void RunExcelTests()
		{
			//RunCsvWriterTesting();
			RunExcelWriterTesting();
			RunExcelReaderTesting();

			Console.WriteLine("Done");
			Console.ReadKey();
		}

		private void RunExcelReaderTesting()
		{
			var path = @"C:\Dev\cbrownRepository\Testing\ExcelTesting\bin\Debug\DataTableTest.xlsx";
			var _dt = new DataTable();

			_dt = _excelReader.ReadExcelDataFromFile(path);
			_excelReader.ReadExcelData(path);
		}

		public void RunCsvWriterTesting()
		{
			var pricingList = GetTestData();
			_csvWriter.WriteToCsv(pricingList);
		}

		public void RunExcelWriterTesting()
		{
			var pricingList = GetTestData();
			var fileTempName = "DtTesting";

			var data = _dtConverter.ConvertToDatatable(pricingList);
			_excelWriter.SaveExcelFileLocally(fileTempName, data, null);
		}

		/// <summary>
		/// This will generate a Excel file based on the Object being pasted in and use the property names as headers.
		/// Note: If you are returning the Memory stream from a controller, use ToArray to return the results in a FileContents instead of GetBuffer because the GetBuffer will cause the Excel file to error when the user opens the file.
		/// </summary>
		public MemoryStream ExportToExcelFile<T>(List<T> data, string sheetName)
		{
			var dt = _dtConverter.ConvertToDatatable(data);
			var cleanData = CleanData(dt);
			var ms = new MemoryStream();
			ms = _excelWriter.GenerateExcelFileStream(cleanData, sheetName);
			return ms;
		}

		public List<T> ReadExcelFile<T>(string filePath)
		{
			var data = _excelReader.ReadExcelDataFromFile(filePath);
			var result = _dtConverter.ConvertDatatableToList<T>(data);
			return result;
		}

		public List<T> ReadExcelFile<T>(Stream filePath)
		{
			var data = _excelReader.ReadExcelDataStream(filePath);
			var result = _dtConverter.ConvertDatatableToList<T>(data);
			return result;
		}

		private static DataTable CleanData(DataTable dt)
		{
			var dateTypeList = new List<int>();
			for (var i = 0; i < dt.Columns.Count; i++)
			{
				if (dt.Columns[i].DataType == typeof(DateTime))
				{
					dateTypeList.Add(i);
				}
			}

			if (dateTypeList.Count > 0)
			{
				foreach (DataRow item in dt.Rows)
				{
					foreach (var dateColumn in dateTypeList)
					{
						var val = DateTime.Parse(item[dateColumn].ToString());
						if (val == DateTime.MinValue)
						{
							item[dateColumn] = DateTime.Parse("01/01/1901 00:00:00");
						}
					}
				}
			}
			return dt;
		}

		private List<CmsPricing> GetTestData()
		{
			return new List<CmsPricing>
			{
				new CmsPricing
				{
					Code = "1234", Description = "Test Description", Price = 150M, Notes = "Test Notes", LastTouchedDate = DateTime.Now,FacilityId = Guid.Empty
				},
				new CmsPricing
				{
					Code = "5678", Description = "Test Description2", Price = 200M, Notes = "Test Notes2",LastTouchedDate = DateTime.Now,FacilityId = Guid.Empty
				}
			};
		}
	}
}
