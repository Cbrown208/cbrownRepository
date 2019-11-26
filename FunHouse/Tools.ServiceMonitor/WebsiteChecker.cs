using System;
using System.Text;
using Microsoft.Web.Administration;

namespace Tools.ServiceMonitor
{
	public class WebsiteChecker
	{
		public void GetLocalAppPoolDetails()
		{
			var server = new ServerManager();
			var appPools = server.ApplicationPools;

			var sb = new StringBuilder();
			sb.Append("AutoStart").Append(",");
			sb.Append("ManagedRuntimeVersion").Append(",");
			sb.Append("Name").Append(",");
			sb.Append("IdentityType").Append(",");
			sb.Append("UserName").Append(",");
			sb.Append("Password").Append(",");
			sb.AppendLine();

			foreach (var pool in appPools)
			{
				bool autoStart = pool.AutoStart;
				var runtime = pool.ManagedRuntimeVersion;
				var appPoolName = pool.Name;
				var identityType = pool.ProcessModel.IdentityType;
				var username = pool.ProcessModel.UserName;
				var password = pool.ProcessModel.Password;

				sb.Append(autoStart).Append(",");
				sb.Append(runtime).Append(",");
				sb.Append(appPoolName).Append(",");
				sb.Append(identityType).Append(",");
				sb.Append(username).Append(",");
				sb.Append(password).Append(",");
				sb.AppendLine();
			}
			Console.WriteLine(sb.ToString());

		}
	}
}
