using System;

namespace FirewallConnectionChecker
{
	class Program
	{
		private static readonly ConnectionCheckerManager ConnectionCheckerManager = new ConnectionCheckerManager();
		private static readonly DbConnectionManager DbConnectionManager = new DbConnectionManager();

		static void Main(string[] args)
		{
			//Console.WriteLine("Please choose a Connection to Check ");
			//Console.WriteLine("1 = Websites ");
			//Console.WriteLine("2 = Db Connection ");
			//ConnectionCheckerManager.CheckConnections();

			DbConnectionManager.CheckDbConnectionList();

			Console.WriteLine("Press any key to exit");
			Console.ReadLine();
		}
	}
}
