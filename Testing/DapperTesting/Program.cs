using System;
using System.Linq;

namespace DapperTesting
{
	class Program
	{
		static void Main(string[] args)
		{
			var dbContext = new DbContext();
			var healthChecks = dbContext.GetAll();
			var firstHealthCheck = healthChecks.FirstOrDefault();
			if (firstHealthCheck != null)
			{
				Console.WriteLine(firstHealthCheck.SiteName);
			}
			Console.ReadLine();
		}
	}
}
