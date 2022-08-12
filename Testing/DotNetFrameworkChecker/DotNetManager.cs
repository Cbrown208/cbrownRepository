using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Reflection;

namespace DotNetFrameworkChecker
{
	public class DotNetManager
	{
		public const string OutputFileName = "DotNetCheckLog.txt";

		public void CheckDotNetVersionForServers()
		{
			var resultList = new List<string>();
			var serverList = new List<string>();

			CleanOutputFile(OutputFileName);

			serverList = GetServerList("WEB",18);
			serverList.Add("LEWVPPASWEB19.nthrive.nthcrp.com");
			serverList.Add("LEWVPPASWEB20.nthrive.nthcrp.com");
			serverList.AddRange(GetServerList("APP", 32));
			serverList.AddRange(GetServerList("TSK", 52));

			foreach (var server in serverList)
			{
				var version = DotNetFrameworkVersion(server);
				var result = CheckFor45PlusVersion((int) version);
				var output = server + " : " + result;
				resultList.Add(output);
				WriteValueToFile(output);
			}

			var outputPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)) +"\\" + OutputFileName;
			Console.WriteLine("FilePath: " + outputPath);
		}

		private List<string> GetServerList(string serverType, int serverCount)
		{
			var serverList = new List<string>();
			var baseServerName = "LEWVPPAS" + serverType;

			var serverDomain = ".nthrive.nthcrp.com";
			if (serverType.ToLower().Contains("web"))
			{
				serverDomain = ".nthext.com";
			}

			for (int i = 1; i < serverCount + 1; i++)
			{
				var sName = baseServerName + String.Format("{0:00}", i) + serverDomain;
				serverList.Add(sName);
			}
			return serverList;
		}

		private static UInt32 DotNetFrameworkVersion(string machineName)
		{
			try
			{
				string wmiPath = $@"\\{machineName}\root\CIMV2";
				string className = "CCM_ClientUtilities";
				string methodName = "DetermineIfRebootPending";
				
				ConnectionOptions options = new ConnectionOptions();
				ManagementScope scope = new ManagementScope(wmiPath, options);
				scope.Connect();
				
				string softwareRegLoc = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";
				
				ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);
				ManagementBaseObject inParams = registry.GetMethodParameters("GetDWORDValue");

				inParams["hDefKey"] = 0x80000002; //HKEY_LOCAL_MACHINE
				inParams["sSubKeyName"] = softwareRegLoc;
				inParams["sValueName"] = "Release";
				
				ManagementBaseObject outParams = registry.InvokeMethod("GetDWORDValue", inParams, null);
				return (UInt32)outParams["uValue"];
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: "+ ex.Message);
				return 0;
			}
		}

		static string CheckFor45PlusVersion(int releaseKey)
		{
			if (releaseKey >= 528040)
				return "4.8 or later";
			if (releaseKey >= 461808)
				return "4.7.2";
			if (releaseKey >= 461308)
				return "4.7.1";
			if (releaseKey >= 460798)
				return "4.7";
			if (releaseKey >= 394802)
				return "4.6.2";
			if (releaseKey >= 394254)
				return "4.6.1";
			if (releaseKey >= 393295)
				return "4.6";
			if (releaseKey >= 379893)
				return "4.5.2";
			if (releaseKey >= 378675)
				return "4.5.1";
			if (releaseKey >= 378389)
				return "4.5";

			// This code should never execute. A non-null release key should mean
			// that 4.5 or later is installed.
			return "No 4.5 or later version detected" + releaseKey;
		}

		private void WriteValueToFile(string value)
		{
			try
			{
				using (var file = new StreamWriter(OutputFileName, true))
				{
					file.WriteLineAsync(value);
				}
				Console.WriteLine(value);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(value);
			}
		}

		private void CleanOutputFile(string outputFileName)
		{
			File.WriteAllText(outputFileName, string.Empty);
		}
	}
}
