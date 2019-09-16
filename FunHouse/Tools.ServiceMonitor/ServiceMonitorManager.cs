using System;

namespace Tools.ServiceMonitor
{
	public class ServiceMonitorManager
	{
		private readonly WebsiteChecker _websiteChecker = new WebsiteChecker();
		public void RunMonitorTasks()
		{
			_websiteChecker.GetLocalAppPoolDetails();

			Console.ReadLine();
		}
	}
}
