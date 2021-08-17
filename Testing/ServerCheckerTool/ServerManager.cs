using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace ServerCheckerTool
{
	public class ServerManager
	{
		private readonly DotNetManager _dotNetManager = new DotNetManager();

		public void GetServerDetails()
		{
			var serverList = GetServerList();

			foreach (var server in serverList)
			{
				var isPendingReboot = GetPendingReboot(server);
				var lastBootTime = GetLastBootUpTime(server);
				var dotNetVersion = _dotNetManager.CheckDotNetVersionForServer(server);

				Console.WriteLine("Server: " + server);
				Console.WriteLine("Dot Net Version   : " + dotNetVersion);
				Console.WriteLine("Is Reboot Pending : " + isPendingReboot);
				Console.WriteLine("Last Boot Time    : " + lastBootTime);

				Console.WriteLine(Environment.NewLine);
			}
		}

		public List<string> GetServerList()
		{
			var serverList = new List<string>();
			serverList.Add("LEWVQPASWEB06.nthrivenp.nthcrpnp.com");

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


	}
}
