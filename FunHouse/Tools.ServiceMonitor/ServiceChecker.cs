using System;
using System.Management;
using System.ServiceProcess;
using NLog;
using Tools.Core.Models;

namespace Tools.ServiceMonitor
{
	public interface IServiceChecker
	{
		string ScanServices(string server, int env);
		bool StartWindowsService(Services service);
		bool StopWindowsService(Services service);
	}

	public class ServiceChecker : IServiceChecker
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public string ScanServices(string server, int env)
		{
			try
			{
				ScanServerServices(server, env);
			}
			catch (Exception ex)
			{
				var errorMsg = string.Format(
					"Scan Services Error Occurred Attempting to Access Server {0}. Error: {1}", server, ex.Message);
				Logger.Error(errorMsg);
			}
			return "";
		}

		public bool StartWindowsService(Services service)
		{
			var svc = service.ServiceName;
			var server = service.ServerName;

			var op = new ConnectionOptions();
			var scope = new ManagementScope(@"\\" + server + @"\root\cimv2", op);

			scope.Connect();
			var sc = new ServiceController(svc, server);
			if (sc.Status != ServiceControllerStatus.Running)
			{
				try
				{
					sc.Start();
					var t = TimeSpan.FromSeconds(10);
					sc.WaitForStatus(
						ServiceControllerStatus.Running, t);
					service.Status = true;
				}
				catch (Exception ex)
				{
					var errorMsg = "Start Windows Service Error: " + ex.Message;
					Logger.Error(errorMsg);
					throw new ApplicationException(errorMsg);
				}
			}
			return true;
		}

		public bool StopWindowsService(Services service)
		{
			var svc = service.ServiceName;
			var server = service.ServerName;
			var op = new ConnectionOptions();
			var scope = new ManagementScope(@"\\" + server + @"\root\cimv2", op);
			scope.Connect();
			var sc = new ServiceController(svc, server);
			if (sc.Status != ServiceControllerStatus.Stopped)
			{
				try
				{
					sc.Stop();
					var t = TimeSpan.FromSeconds(10);
					sc.WaitForStatus(
						ServiceControllerStatus.Stopped, t);
					service.Status = false;
				}
				catch (Exception ex)
				{
					var errorMsg = "Stop Windows Service Error: " + ex.Message;
					Logger.Error(errorMsg);
					throw new ApplicationException(errorMsg);
				}
			}
			return true;
		}

		public bool ScanServerServices(string server, int env)
		{
			var svcStatus = new Services { ServerName = server };
			var connectionOptions = new ConnectionOptions();

			Logger.Info("Starting Scan");
			var scope = new ManagementScope(@"\\" + server + @"\root\cimv2", connectionOptions);

			scope.Connect();
			Logger.Info("Connect Successful");

			var path = new ManagementPath("Win32_Service");
			var services = new ManagementClass(scope, path, null);

			foreach (var manageObject in services.GetInstances())
			{
				var service = (ManagementObject)manageObject;
				if (!service.GetPropertyValue("DisplayName").ToString().ToUpper().Contains("NTHR") &&
					!service.GetPropertyValue("DisplayName").ToString().ToUpper().Contains("RABBITMQ") &&
					!service.GetPropertyValue("DisplayName").ToString().ToUpper().Contains("INTEGRATION RUNTIME SERVICE"))
					continue;

				var runAsUser = service.GetPropertyValue("StartName").ToString().Trim();
				Console.WriteLine(runAsUser);


				var state = service.GetPropertyValue("State").ToString().Trim();
				if (state.ToLower() == "up" || state.ToLower() == "running")
					svcStatus.Status = true;
				else
					svcStatus.Status = false;

				svcStatus.ServiceName = service.GetPropertyValue("DisplayName").ToString().Trim();
				try
				{
					svcStatus.ServiceDescription = service.GetPropertyValue("Description").ToString().Trim();
				}
				catch (Exception)
				{
					svcStatus.ServiceDescription = "";
				}
				svcStatus.StartMode = service.GetPropertyValue("StartMode").ToString().Trim();
				svcStatus.Environment = env;
			}
			return true;
		}

		public void SetServiceStatusToDown(string pingTarget)
		{
			var svc = new Services
			{
				ServerName = pingTarget,
				Status = false
			};
		}
	}
}