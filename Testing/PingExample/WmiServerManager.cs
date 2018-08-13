using System.Linq;

namespace PingExample
{
	using System.Management;

	public class WmiServerManager
	{
		public string GetServerInfo(string server)
		{
			var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
			            select x.GetPropertyValue("Caption")).FirstOrDefault();
			return name != null ? name.ToString() : "Unknown";
		}
	}
}
