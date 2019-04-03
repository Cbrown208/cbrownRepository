using System;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace ExcelTesting
{
	public class ExcelWriter
	{
		private const string FileType = ".xlsx";
		public void SaveExcelFileLocally(string fileName, DataTable data,  string worksheetName)
		{
			try
			{
				var wsName = worksheetName;
				var path = @"C:\Dev\cbrownRepository\Testing\ExcelTesting\bin\Debug\";
				var excelFileName = path+ fileName + FileType;
				if (string.IsNullOrWhiteSpace(fileName))
				{
					excelFileName = "DataTableTest" + FileType;
				}
				
				var workbook = new XLWorkbook();

				if (string.IsNullOrWhiteSpace(worksheetName))
				{
					wsName = fileName;
				}
				workbook.Worksheets.Add(data, wsName);
				workbook.SaveAs(excelFileName);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public MemoryStream GenerateExcelFileStream(DataTable data, string worksheetName)
		{
			var wsName = worksheetName;
			var workbook = new XLWorkbook();

			if (string.IsNullOrWhiteSpace(worksheetName))
			{
				wsName = "Data";
			}
			workbook.Worksheets.Add(data, wsName);

			var fs = new MemoryStream();
			workbook.SaveAs(fs);
			fs.Position = 0;
			return fs;
		}
	}
}
