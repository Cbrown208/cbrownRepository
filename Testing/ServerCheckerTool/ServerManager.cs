using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;

namespace ServerCheckerTool
{
	public class ServerManager
	{
		private readonly DotNetManager _dotNetManager = new DotNetManager();
		private string OutputFileName = "ServerResults.txt";

		public void GetServerDetails()
		{
			CleanOutputFile(OutputFileName);

			var serverList = new List<string>();
			serverList = GetServerList("WEB", 18);
			serverList.Add("LEWVPPASWEB19.nthrive.nthcrp.com");
			serverList.Add("LEWVPPASWEB20.nthrive.nthcrp.com");
			serverList.AddRange(GetServerList("APP", 32));
			serverList.AddRange(GetServerList("TSK", 52));

			serverList.RemoveAll(x => x.Contains("APP25") || x.Contains("APP26") || x.Contains("APP27") || x.Contains("APP28"));

			foreach (var server in serverList)
			{
				//var isPendingReboot = GetPendingReboot(server);
				//var lastBootTime = GetLastBootUpTime(server);
				//var dotNetVersion = _dotNetManager.CheckDotNetVersionForServer(server);
				var maxMemoryAllowed = GetMaxMemoryAllowed(server);
				var prettyMaxMemAllowed = server + "- Could not get Value";
				if (maxMemoryAllowed != 0)
				{
					prettyMaxMemAllowed = server + "- " + FormatMegaByte(maxMemoryAllowed);
				}
				
				//Console.WriteLine("Server: " + server);
				//Console.WriteLine("Dot Net Version   : " + dotNetVersion);
				//Console.WriteLine("Is Reboot Pending : " + isPendingReboot);
				//Console.WriteLine("Last Boot Time    : " + lastBootTime);
				//Console.WriteLine("MaxMemoryAllowed  : " + maxMemoryAllowed);

				WriteValueToFile(prettyMaxMemAllowed);

				//Console.WriteLine(Environment.NewLine);
			}
			var outputPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)) + "\\" + OutputFileName;
			Console.WriteLine("FilePath: " + outputPath);
		}

		private List<string> GetServerList(string serverType, int serverCount)
		{
			var serverList = new List<string>();
			var baseServerName = "LEWVPPAS" + serverType;

			var serverDomain = ".nthrive.nthcrp.com";
			//serverDomain = ".nthrivenp.nthcrpnp.com";
			if (serverType.ToLower().Contains("web"))
			{
				serverDomain = ".nthext.com";
				//serverDomain = ".nthextnp.com";
			}

			for (int i = 1; i < serverCount + 1; i++)
			{
				var sName = baseServerName + String.Format("{0:00}", i) + serverDomain;
				serverList.Add(sName);
			}
			return serverList;
		}

		private static bool? GetPendingReboot(string serverName)
		{
			bool? pending = null;
			string wmiPath = $@"\\{serverName}\root\ccm\ClientSDK";
			string className = "CCM_ClientUtilities";
			string methodName = "DetermineIfRebootPending";

			ConnectionOptions options = new ConnectionOptions();
			ManagementScope scope = new ManagementScope(wmiPath, options);
			scope.Connect();

			using (ManagementClass managementClass = new ManagementClass(scope.Path.Path, className, null))
			{
				//This .InvokeMethod throws the exception, but like a I said, only after machine restart and only for first time call
				using (ManagementBaseObject outParams = managementClass.InvokeMethod(methodName, null, null))
				{
					//foreach (PropertyData data in outParams.Properties)
					//{
					//	Console.WriteLine($"{data.Name} : {data.Value.ToString()}");
					//}

					pending = ((bool)outParams.Properties["IsHardRebootPending"].Value ||
							   (bool)outParams.Properties["RebootPending"].Value);
				}
			}
			//Console.WriteLine(serverName + " : " + pending);
			return pending;
		}

		private DateTime GetLastBootUpTime(string computerName = ".")
		{

			var scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", computerName));
			scope.Connect();
			var query = new ObjectQuery("SELECT LastBootUpTime FROM Win32_OperatingSystem");

			var searcher = new ManagementObjectSearcher(scope, query);
			var firstResult = searcher.Get().OfType<ManagementObject>().FirstOrDefault();
			if (firstResult != null)
			{
				return ManagementDateTimeConverter.ToDateTime(firstResult["LastBootUpTime"].ToString());
			}

			return DateTime.MinValue;
		}

		private double GetMaxMemoryAllowed(string serverName)
		{
			double maxMemoryAmount = 0;
			try
			{
				ConnectionOptions options = new ConnectionOptions();
				ManagementScope scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", serverName), options);
				scope.Connect();

				ObjectQuery query =
					new ObjectQuery("SELECT * FROM Win32_PerfRawData_Counters_HyperVDynamicMemoryIntegrationService");
				ManagementObjectSearcher maxMemResults = new ManagementObjectSearcher(scope, query);

				foreach (var wmiObject in maxMemResults.Get())
				{
					maxMemoryAmount = double.Parse(wmiObject["MaximumMemoryMBytes"].ToString());
				}
			}
			catch (Exception ex)
			{
				var temp = ex.Message;
				Console.WriteLine(ex.Message);
				maxMemoryAmount = 0;
			}
			return maxMemoryAmount;
		}

		private string FormatMegaByte(double megaByte)
		{
			string[] suffix = { "MB", "GB", "TB" };
				int i;
				double dblSByte = megaByte;
				for (i = 0; i < suffix.Length && megaByte >= 1024; i++, megaByte /= 1024)
				{
					dblSByte = megaByte / 1024.0;
				}
				return $"{dblSByte:0.##}{suffix[i]}";
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
