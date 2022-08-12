using System;

namespace DotNetFrameworkChecker
{
	class Program
	{
		private static readonly DotNetManager DotNetManager = new DotNetManager();
		private static readonly RegistryManager RegistryManager = new RegistryManager();

		static void Main(string[] args)
		{
			//Console.WriteLine("Getting Dot Net Version ...");
			//DotNetManager.CheckDotNetVersionForServers();

			Console.WriteLine("Getting TLS Version ...");
			RegistryManager.CheckTlsVersionForServers();

			Console.Write(Environment.NewLine + "DotNet Check Finished! Press Enter to exit.");
			Console.ReadLine();
		}
	}
}
