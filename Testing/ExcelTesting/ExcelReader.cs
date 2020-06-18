using System;
using System.IO;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using Exception = System.Exception;

namespace ExcelTesting
{
	public class ExcelReader
	{
		// Using OpenXML
		public System.Data.DataTable ReadExcelDataFromFile(string filePath)
		{
			var workbook = new XLWorkbook(filePath);
			var dataTable = ReadExcelData(workbook);
			return dataTable;
		}

		// Using OpenXML
		public System.Data.DataTable ReadExcelDataStream(Stream input)
		{
			var workbook = new XLWorkbook(input);
			var dataTable = ReadExcelData(workbook);
			return dataTable;
		}

		public void ReadExcelData(string path)
		{
			_Workbook wb;
			_Worksheet ws;
			var excel = new Application();
			try
			{
				wb = excel.Workbooks.Open(path);
				ws = wb.Worksheets[1];

				var last = ws.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing);
				var lastUsedRow = last.Row;
				var lastUsedColumn = last.Column;

				for (var i = 1; i < lastUsedRow+1; i++)
				{
					for (var j = 1; j <= lastUsedColumn; j++)
					{
						Console.WriteLine(i+","+j);
						ReadCell(ws,i, j);
					}
				}
				excel.Workbooks.Close();
			}
			catch (Exception ex)
			{
				excel.Workbooks.Close();
				Console.WriteLine(ex);
			}
		}

		public string ReadCell(_Worksheet ws, int row, int column)
		{
			var results = "";
			if (ws.Cells[row, column].Value != null)
			{
				results = ws.Cells[row, column].Value.ToString();
				Console.WriteLine(ws.Cells[row, column].Value.ToString());
				return ws.Cells[row, column].Value.ToString();
			}

			return results;
		}

		private System.Data.DataTable ReadExcelData(XLWorkbook workbook)
		{
			var dataTable = new System.Data.DataTable();
			var xlWorksheet = workbook.Worksheet(1);
			var range = xlWorksheet.Range(xlWorksheet.FirstCellUsed(), xlWorksheet.LastCellUsed());

			var col = range.ColumnCount();

			//if a data table already exists, clear the existing table
			dataTable.Clear();
			for (var i = 1; i <= col; i++)
			{
				var column = xlWorksheet.Cell(1, i);
				dataTable.Columns.Add(column.Value.ToString());
			}

			var firstHeadRow = 0;
			foreach (var item in range.Rows())
			{
				if (firstHeadRow != 0)
				{
					var array = new object[col];
					for (var y = 1; y <= col; y++)
					{
						array[y - 1] = item.Cell(y).Value;
					}
					dataTable.Rows.Add(array);
				}
				firstHeadRow++;
			}
			return dataTable;
		}
	}
}
