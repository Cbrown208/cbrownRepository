using System;

namespace FirewallConnectionChecker
{
	class Program
	{
		private static readonly ConnectionCheckerManager ConnectionCheckerManager = new ConnectionCheckerManager();
		private static readonly DbConnectionManager DbConnectionManager = new DbConnectionManager();

		static void Main(string[] args)
		{
			Console.WriteLine("Please choose a Connection to Check ");
			Console.WriteLine("1 = Websites ");
			Console.WriteLine("2 = Db Connection ");
			
			var input = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(input))
			{
				Console.WriteLine("Please Enter a valid input");
			}

			if (!string.IsNullOrWhiteSpace(input) && input.Contains("1"))
			{
				ConnectionCheckerManager.CheckConnections();
			}
			else if (!string.IsNullOrWhiteSpace(input) && input.Contains("2"))
			{
				DbConnectionManager.CheckDbConnectionList();
			}

			Console.WriteLine("Press any key to exit");
			Console.ReadLine();
		}
	}
}
