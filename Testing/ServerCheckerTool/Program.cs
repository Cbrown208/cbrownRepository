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

			Console.WriteLine("Done Running Server Manager Tools... Press any key to quit." + Environment.NewLine);
			Console.ReadLine();
		}
	}
}