using System;
using System.Collections.Generic;
using ExcelTesting.Models;

namespace ExcelTesting
{
	public class ExcelTestingManager
	{
		private readonly CsvWriter _csvWriter = new CsvWriter();
		public void RunExcelTests()
		{
			RunCsvWriterTesting();

			Console.ReadKey();
		}

		public void RunCsvWriterTesting()
		{
			var pricingList = new List<CmsPricing>();
			pricingList.Add(new CmsPricing{Code = "1234",Description = "Test Description",Price = 150M, Notes = "Test Notes"});
			pricingList.Add(new CmsPricing { Code = "5678", Description = "Test Description2", Price = 200M, Notes = "Test Notes2" });
			_csvWriter.WriteToCsv(pricingList);
		}
	}
}
