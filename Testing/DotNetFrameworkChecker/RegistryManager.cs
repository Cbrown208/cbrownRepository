using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Reflection;

namespace DotNetFrameworkChecker
{
	public class RegistryManager
	{
		private const string OutputFileName = "TlsCheckLog.txt";
		public void CheckTlsVersionForServers()
		{
			var resultList = new List<string>();
			var serverList = new List<string>();

			CleanOutputFile(OutputFileName);

			serverList.AddRange(GetServerList("LEWVQCPAPP", 4));
			serverList.AddRange(GetServerList("LEWVQCPWEB", 2));


			// Pas 2019 Servers 
			//serverList = GetServerList("WEB", 18);
			//serverList.Add("LEWVPPASWEB19.nthrive.nthcrp.com");
			//serverList.Add("LEWVPPASWEB20.nthrive.nthcrp.com");
			//serverList.AddRange(GetServerList("APP", 32));
			//serverList.AddRange(GetServerList("TSK", 52));

			//serverList.AddRange(GetServerList("LEWVPPASAPP", 32));

			foreach (var server in serverList)
			{
				var isTlsEnabled = CheckTlsVersion(server);

				var output = server + " : " + isTlsEnabled;
				resultList.Add(output);
				WriteValueToFile(output);
			}


			WriteValueToFile("--------------------------- Servers Missing TLS1.2 Reg Keys ---------------------------");
			foreach (var result in resultList)
			{
				Console.WriteLine(Environment.NewLine);
				if (result.ToLower().Contains("missing"))
				{
					Console.WriteLine(result);
					WriteValueToFile(result);
				}
			}

			var outputPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)) + "\\" + OutputFileName;
			Console.WriteLine("FilePath: " + outputPath);
		}

		private List<string> GetServerList(string baseServerName, int serverCount)
		{
			var serverList = new List<string>();
			var isProd = baseServerName.ToLower().Contains("VP");

			var serverDomain = isProd ? ".nthrive.nthcrp.com" : ".nthrivenp.nthcrpnp.com";

			if (baseServerName.ToLower().Contains("web"))
			{
				serverDomain = isProd ? ".nthext.com" : ".nthextnp.com";
			}

			for (int i = 1; i < serverCount + 1; i++)
			{
				var sName = baseServerName + String.Format("{0:00}", i) + serverDomain;
				serverList.Add(sName);
			}
			return serverList;
		}

		private string CheckTlsVersion(string machineName)
		{
			//Computer\HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Server
			try
			{
				var results = "";
				string wmiPath = $@"\\{machineName}\root\CIMV2";
				string className = "CCM_ClientUtilities";
				string methodName = "DetermineIfRebootPending";

				ConnectionOptions options = new ConnectionOptions();
				ManagementScope scope = new ManagementScope(wmiPath, options);
				scope.Connect();

				ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);
				ManagementBaseObject inParams = registry.GetMethodParameters("GetDWORDValue");

				// Standard TLS Server Location
				string regLocation = @"SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Server";

				inParams["hDefKey"] = 0x80000002; //HKEY_LOCAL_MACHINE
				inParams["sSubKeyName"] = regLocation;
				inParams["sValueName"] = "Enabled";

				ManagementBaseObject outParams = registry.InvokeMethod("GetDWORDValue", inParams, null);
				var protocolResults = outParams["uValue"];

				if (protocolResults == null)
				{
					results = "Protocol- Missing!! | ";
				}

				if (protocolResults != null && !string.IsNullOrWhiteSpace(protocolResults.ToString()))
				{
					results = "Protocol- Good | ";
				}

				// Group Policy Reg Key Location
				string gpRegLocation64 = @"SOFTWARE\WOW6432Node\Microsoft\.NETFramework\v4.0.30319";

				//inParams["hDefKey"] = 0x80000002; //HKEY_LOCAL_MACHINE
				inParams["sSubKeyName"] = gpRegLocation64;
				inParams["sValueName"] = "SchUseStrongCrypto";

				ManagementBaseObject outParam2 = registry.InvokeMethod("GetDWORDValue", inParams, null);
				var gpresults = outParam2["uValue"];

				if (protocolResults == null)
				{
					results += "SchUseStrongCrypto64- Missing!!";
				}

				if (protocolResults != null && gpresults.ToString() == "1")
				{
					results += "SchUseStrongCrypto64- Good";
				}

				// Group Policy Reg Key Location
				string gpRegLocation32 = @"SOFTWARE\Microsoft\.NETFramework\v4.0.30319";

				//inParams["hDefKey"] = 0x80000002; //HKEY_LOCAL_MACHINE
				inParams["sSubKeyName"] = gpRegLocation32;
				inParams["sValueName"] = "SchUseStrongCrypto";

				ManagementBaseObject outParam3 = registry.InvokeMethod("GetDWORDValue", inParams, null);
				var gpresults2 = outParam2["uValue"];

				if (protocolResults == null)
				{
					results += "SchUseStrongCrypto32- Missing!!";
				}

				if (protocolResults != null && gpresults.ToString() == "1")
				{
					results += "SchUseStrongCrypto32- Good";
				}

				return results;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
				return "Entry does not Exist.";
			}
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
