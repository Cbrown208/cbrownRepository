using System;

namespace DotNetFrameworkChecker
{
	class Program
	{
		private static readonly DotNetManager DotNetManager = new DotNetManager();

		static void Main(string[] args)
		{
			Console.WriteLine("Getting Dot Net Version ...");

			DotNetManager.CheckDotNetVersionForServers();
			Console.ReadLine();
		}
	}
}
