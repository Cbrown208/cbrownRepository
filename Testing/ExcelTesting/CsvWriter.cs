using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ExcelTesting.Models;

namespace ExcelTesting
{
	public class CsvWriter
	{
		public void WriteToCsv(List<CmsPricing> pricingList)
		{
			var rootPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath));
			var filePath = rootPath+ @"\Output.csv";

			var csv = new StringBuilder();

			foreach (var pricing in pricingList)
			{
				//csvRow = csvRow + pricing.Code + "," + pricing.Description + "," + pricing.Price + "," + pricing.Notes + Environment.NewLine;
				var csvRow = string.Format("{0},{1},{2},{3}", pricing.Code, pricing.Description, pricing.Price, pricing.Notes);
				csv.AppendLine(csvRow);
			}

			File.WriteAllText(filePath, csv.ToString());


			//string csv = string.Format("{0},{1},{2},{3}\n", first, second);
			//File.WriteAllText(filePath, csv);

			Console.WriteLine("Done");
			
		}

		protected string CleanCSVString(string input)
		{
			string output = "\"" + input.Replace("\"", "\"\"").Replace("\r\n", " ").Replace("\r", " ").Replace("\n", "") + "\"";
			return output;
		}
	}
}
