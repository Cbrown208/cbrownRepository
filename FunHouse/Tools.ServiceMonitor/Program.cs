using System;

namespace Tools.ServiceMonitor
{
	class Program
	{
		private static readonly ServiceMonitorManager Manager = new ServiceMonitorManager();
		static void Main(string[] args)
		{
			Manager.RunMonitorTasks();
		}
	}
}
