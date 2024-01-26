using System;

namespace DbQueryMultipleServers
{
	class Program
	{
		private static readonly QueryManager Manager = new QueryManager();
		private static readonly CiqQueryManager CiqManager = new CiqQueryManager();
		static void Main(string[] args)
		{
			//Manager.RunMultipleDbQuery();

			CiqManager.CiqRunMulitpleDbQuery();

			//Console.ReadLine();
		}
	}
}
