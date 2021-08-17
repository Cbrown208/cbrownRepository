using System;

namespace ServerCheckerTool
{
	class Program
	{
		private static readonly ServerManager ServerManager = new ServerManager();
		static void Main(string[] args)
		{
			Console.WriteLine("Running Server Manager Tools..." + Environment.NewLine);
			ServerManager.GetServerDetails();

			Console.ReadLine();
		}
	}
}
