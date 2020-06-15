using System;
using System.Linq;

namespace PingExample
{
	using System.Management;

	public class WmiServerManager
	{
		public DateTime GetLastBootTime()
		{
			string remoteServer = "RCM41VQPASWEB01.npce.com";

			Console.WriteLine("GetLastBootUpTimeViaWMI");
			var wmiBootTime = GetLastBootUpTimeViaWMI(remoteServer);
			Console.WriteLine("original: " + wmiBootTime);

			wmiBootTime = GetLastBootUpTime(remoteServer);
			Console.WriteLine("original: " + wmiBootTime);

			return wmiBootTime;
			//using System.Management
		}

		private DateTime GetLastBootUpTimeViaWMI(string computerName = ".")
		{
			//https://docs.microsoft.com/en-us/dotnet/api/system.management.managementscope?view=netframework-4.7.2
			var scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", computerName));
			scope.Connect();
			//https://docs.microsoft.com/en-us/dotnet/api/system.data.objects.objectquery?view=netframework-4.7.2
			var query = new ObjectQuery("SELECT LastBootUpTime FROM Win32_OperatingSystem");
			//https://docs.microsoft.com/en-us/dotnet/api/system.management.managementobjectsearcher?view=netframework-4.7.2
			var searcher = new ManagementObjectSearcher(scope, query);
			var firstResult = searcher.Get().OfType<ManagementObject>().FirstOrDefault();
			if (firstResult != null)
			{
				return ManagementDateTimeConverter.ToDateTime(firstResult["LastBootUpTime"].ToString());
			}
			else
			{
				return DateTime.MinValue;
			}
		}

		private DateTime GetLastBootUpTime(string computerName = ".")
		{
			var connectionOptions = new ConnectionOptions();

			var path = new ManagementPath("Win32_OperatingSystem");
			var scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", computerName));
			scope.Connect();

			var services = new ManagementClass(scope, path, null);

			foreach (var svc in services.GetInstances())
			{
				var prop = (ManagementObject)svc;
				var bootPropertyValue = prop.GetPropertyValue("LastBootUpTime");
				var lastBootTime = ManagementDateTimeConverter.ToDateTime(bootPropertyValue.ToString());
				Console.WriteLine(lastBootTime);
			}
			
			var query = new ObjectQuery("SELECT LastBootUpTime FROM Win32_OperatingSystem");
			//https://docs.microsoft.com/en-us/dotnet/api/system.management.managementobjectsearcher?view=netframework-4.7.2
			var searcher = new ManagementObjectSearcher(scope, query);
			var firstResult = searcher.Get().OfType<ManagementObject>().FirstOrDefault();
			if (firstResult != null)
			{
				return ManagementDateTimeConverter.ToDateTime(firstResult["LastBootUpTime"].ToString());
			}
			else
			{
				return DateTime.MinValue;
			}
		}

		public string GetServerInfo(string server)
		{
			var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
						select x.GetPropertyValue("Caption")).FirstOrDefault();
			return name != null ? name.ToString() : "Unknown";
		}

		public void GetDiskspace(string computerName = ".")
		{
			computerName = "RCM41VQPASWEB01.npce.com";
			ConnectionOptions options = new ConnectionOptions();
			ManagementScope scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", computerName), options);
			scope.Connect();
			ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
			SelectQuery query1 = new SelectQuery("Select * from Win32_LogicalDisk");

			ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
			ManagementObjectCollection queryCollection = searcher.Get();
			ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(scope, query1);
			ManagementObjectCollection queryCollection1 = searcher1.Get();

			foreach (ManagementObject wmiObject in queryCollection)
			{
				// Display the remote computer information

				Console.WriteLine("Computer Name : {0}", wmiObject["csname"]);
				Console.WriteLine("Windows Directory : {0}", wmiObject["WindowsDirectory"]);
				Console.WriteLine("Operating System: {0}", wmiObject["Caption"]);
				Console.WriteLine("Version: {0}", wmiObject["Version"]);
				Console.WriteLine("Manufacturer : {0}", wmiObject["Manufacturer"]);
				Console.WriteLine();
			}

			foreach (ManagementObject wmiObject in queryCollection1)
			{
				// Display Logical Disks information

				Console.WriteLine("              Disk Name : {0}", wmiObject["Name"]);
				Console.WriteLine("              Disk Size : {0}", wmiObject["Size"]);
				Console.WriteLine("              FreeSpace : {0}", wmiObject["FreeSpace"]);
				Console.WriteLine("          Disk DeviceID : {0}", wmiObject["DeviceID"]);
				Console.WriteLine("        Disk VolumeName : {0}", wmiObject["VolumeName"]);
				Console.WriteLine("        Disk SystemName : {0}", wmiObject["SystemName"]);
				Console.WriteLine("Disk VolumeSerialNumber : {0}", wmiObject["VolumeSerialNumber"]);
				Console.WriteLine();
			}
			string line;
			line = Console.ReadLine();
		}

		public void GetServerDiskSpace(string computerName = ".")
		{
			computerName = "RCM41VQPASWEB01.npce.com";
			//computerName = "localhost";
			ConnectionOptions options = new ConnectionOptions();
			ManagementScope scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", computerName), options);
			scope.Connect();
			
			ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
			ManagementObjectSearcher osSearcher = new ManagementObjectSearcher(scope, query);

			foreach (var wmiObject in osSearcher.Get())
			{
				// Display the remote computer information

				Console.WriteLine("Computer Name : {0}", wmiObject["csname"]);
				Console.WriteLine("Windows Directory : {0}", wmiObject["WindowsDirectory"]);
				Console.WriteLine("Operating System: {0}", wmiObject["Caption"]);
				Console.WriteLine("Version: {0}", wmiObject["Version"]);
				Console.WriteLine("Manufacturer : {0}", wmiObject["Manufacturer"]);

				// Get Memory Usage
				var freeMemory = double.Parse(wmiObject["FreePhysicalMemory"].ToString());
				var totalMemory = double.Parse(wmiObject["TotalVisibleMemorySize"].ToString());
				Console.WriteLine("Free Memory: "+ FormatKiloBytes(freeMemory));
				Console.WriteLine("Total Memory: " + FormatKiloBytes(totalMemory));
				Console.WriteLine("Percentage used: {0}%", Math.Round(((totalMemory - freeMemory) / totalMemory * 100), 2));
			}

			//CPU Usage and Memory
			var processorSearcher = new ObjectQuery("select * from Win32_Processor");
			ManagementObjectSearcher cpuSearcher = new ManagementObjectSearcher(scope, processorSearcher);
			foreach (var wmiObject in cpuSearcher.Get())
			{
				var usage = wmiObject["LoadPercentage"];
				Console.WriteLine("CPU Usage : " + usage + "%");
			}

			// Disk Size 
			SelectQuery logicalDiskQuery = new SelectQuery("Select * from Win32_LogicalDisk");
			ManagementObjectSearcher logicalDiskSearcher = new ManagementObjectSearcher(scope, logicalDiskQuery);

			foreach (var wmiObject in logicalDiskSearcher.Get())
			{
				// Display Logical Disks information
				var diskName = wmiObject["Name"].ToString();
				if (!diskName.Contains("X"))
				{
					var totalDiskSize = Convert.ToInt64(wmiObject["Size"]);
					var freeDiskSpace = Convert.ToInt64(wmiObject["FreeSpace"]);

					var totalDisk = double.Parse(wmiObject["Size"].ToString());
					var freeDisk = double.Parse(wmiObject["FreeSpace"].ToString());

					var formattedDiskSize = FormatBytes(totalDiskSize);
					var formattedFreeSize = FormatBytes(freeDiskSpace);


					var formattedDiskSpaceUsed = FormatBytes(totalDiskSize - freeDiskSpace);

					Console.WriteLine("              Disk Name : {0}", diskName);
					Console.WriteLine("    Formatted Disk Size : {0}", totalDiskSize);
					Console.WriteLine("    Formatted Disk Size : {0}", totalDisk);
					Console.WriteLine("    Formatted FreeSpace : {0}", formattedFreeSize);


					Console.WriteLine("                       {0} {1}/{2}", diskName, formattedDiskSpaceUsed, formattedDiskSize);
					Console.WriteLine("Percentage used: {0}%", (100 - Math.Round(((totalDisk - freeDisk) / totalDisk * 100), 2)));
					Console.WriteLine(Environment.NewLine);
				}
			}
		}

		public string FormatBytes(double bytes)
		{
			string[] suffix = { "B", "KB", "MB", "GB", "TB" };
			int i;
			double dblSByte = bytes;
			for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
			{
				dblSByte = bytes / 1024.0;
			}

			return $"{dblSByte:0.##} {suffix[i]}";
		}

		public string FormatKiloBytes(double kiloBytes)
		{
			string[] suffix = { "KB", "MB", "GB", "TB" };
			int i;
			double dblSByte = kiloBytes;
			for (i = 0; i < suffix.Length && kiloBytes >= 1024; i++, kiloBytes /= 1024)
			{
				dblSByte = kiloBytes / 1024.0;
			}

			return $"{dblSByte:0.##} {suffix[i]}";
		}
	}
}
