using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ExcelReader.Models;
using Excel = Microsoft.Office.Interop.Excel; //microsoft Excel 14 object in references-> COM tab

namespace ExcelReader
{
	public class ExcelReaderSvc
	{
		public List<ExcelObject> GetExcelFile(string path)
		{
			//Create COM Objects. Create a COM object for everything that is referenced
			Excel.Application xlApp = new Excel.Application();
			Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
			Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
			Excel.Range xlRange = xlWorksheet.UsedRange;

			int rowCount = xlRange.Rows.Count;
			int colCount = xlRange.Columns.Count;
			var excelObjectList = new List<ExcelObject>();

			//iterate over the rows and columns and print to the console as it appears in the file
			//excel is not zero based!!
			for (int i = 1; i <= rowCount; i++)
			{
				var excelobj = new ExcelObject();
				for (int j = 1; j <= colCount; j++)
				{
					//new line
					if (j == 1)
					{
						//Console.Write("\r\n");
						excelobj.Id = xlRange.Cells[i, j].Value2.ToString();
					}
					if (j == 2)
					{
						excelobj.ClientId = xlRange.Cells[i, j].Value2.ToString();
					}
					if (j == 3)
					{
						excelobj.FacilityId = xlRange.Cells[i, j].Value2.ToString();
					}
					if (j == 4)
					{
						if (xlRange.Cells[i, j].Value2.ToString() != null)
						{
							excelobj.MappingFileName = xlRange.Cells[i, j].Value2.ToString();
						}
					}
					//write the value to the console
					if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
					{

						//Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
					}
				}
				excelObjectList.Add(excelobj);
			}
			Console.WriteLine("Excel Reading Done");

			//cleanup
			GC.Collect();
			GC.WaitForPendingFinalizers();

			//rule of thumb for releasing com objects:
			//  never use two dots, all COM objects must be referenced and released individually
			//  ex: [somthing].[something].[something] is bad

			//release com objects to fully kill excel process from running in the background
			Marshal.ReleaseComObject(xlRange);
			Marshal.ReleaseComObject(xlWorksheet);

			//close and release
			xlWorkbook.Close();
			Marshal.ReleaseComObject(xlWorkbook);

			//quit and release
			xlApp.Quit();
			Marshal.ReleaseComObject(xlApp);
			return excelObjectList;
		}
	}
}
