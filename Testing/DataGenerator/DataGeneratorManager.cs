using System;
using System.Linq;
using Console = System.Console;

namespace DataGenerator
{
	public class DataGeneratorManager
	{
		private readonly BogusDataGenerator _bogusDataGenerator = new BogusDataGenerator();
		public void RunBogusTests()
		{
			Console.WriteLine(FunctionTextHeader("Using_The_Faker_Facade"));
			_bogusDataGenerator.Using_The_Faker_Facade();
			WriteNewLine();

			Console.WriteLine(FunctionTextHeader("Using_DataSets_Directly"));
			_bogusDataGenerator.Using_DataSets_Directly();
			WriteNewLine();

			Console.WriteLine(FunctionTextHeader("Using_FakerT_Inheritance"));
			_bogusDataGenerator.Using_FakerT_Inheritance();
			Console.ReadKey();

		}

		public string FunctionTextHeader(string functionName)
		{
			var headerStart = "-----------------------";
			var totalLength = 71;

			var partialHeader = headerStart + functionName;
			var remainingLength = totalLength - partialHeader.Length;

			var endHeader = string.Concat(Enumerable.Repeat("-", remainingLength));
			var fullHeader = partialHeader + endHeader;
			return fullHeader;
		}

		public void WriteNewLine()
		{
			Console.WriteLine(Environment.NewLine);
		}
	}
}
