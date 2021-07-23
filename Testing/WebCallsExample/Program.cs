using System;
using System.Windows.Forms;

namespace WebCallsExample
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Press Enter to make the web call");
			//CheckWebsite webCheck = new CheckWebsite();
			//SoapRequestExample soapExample = new SoapRequestExample();
			var websiteManager = new TestWebsiteManager();

			var inputUrl = "http://RCM41VHESTSOA02:10666/api/v1/systems";

			//webCheck.HealthCheck(inputUrl);
			//webCheck.AsyncHealthCheck(inputUrl);
			var result = websiteManager.RunApiCallTest();

			//var result = soapExample.GeneratePreAuthCheckStatusRequestXML("ISA...Requesting String", "usernameTest", "PasswordTest");
			//var httpRequest = soapExample.RequestMessageValues(result);
			Clipboard.SetText(result);
			Console.ReadLine();
		}
	}
}
