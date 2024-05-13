using System;

namespace DbQueryMultipleServers
{
	class Program
	{
		private static readonly QueryManager Manager = new QueryManager();
		private static readonly CiqQueryManager CiqManager = new CiqQueryManager();
		static void Main(string[] args)
		{
			Console.WriteLine("Please choose a Project to Run Query For ");
			Console.WriteLine("1 = Pas ");
			Console.WriteLine("2 = ClearIQ ");

			var input = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(input))
			{
				Console.WriteLine("Please Enter a valid input");
			}

			if (!string.IsNullOrWhiteSpace(input) && input.Contains("1"))
			{
				Manager.RunMultipleDbQuery();
			}
			else if (!string.IsNullOrWhiteSpace(input) && input.Contains("2"))
			{
				CiqManager.CiqRunMulitpleDbQuery();
			}

			//Console.ReadLine();
		}
	}
}
