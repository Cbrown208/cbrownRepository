using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebCallsExample
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Press Enter to make the web call");
			CheckWebsite webCheck = new CheckWebsite();
			string input;
			input = "http://RCM41VHESTSOA02:10666/api/v1/systems";
			//webCheck.HealthCheck(input);

			webCheck.AsyncHealthCheck(input);

			Console.ReadLine();
		}
	}
}
