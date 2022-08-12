using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FirewallConnectionChecker
{
	public class ConnectionCheckerManager
	{
		public const string OutputFileName = "Output.txt";
		public ConcurrentBag<string> OutputValues = new ConcurrentBag<string>();

		public void CheckConnections()
		{
			CheckWebsites();
			Console.ReadLine();
		}

		public async void CheckWebsites()
		{
			var fileWebsiteDetails = new List<WebsiteDetails>();
			var path = @"SiteList.txt";
			//path = @"RcPfeWebSiteList.txt";

			WriteValueToFile("------------------------------------------------------------------------------------------------------------------");
			WriteValueToFile("Start Time: " + DateTime.Now.ToLocalTime());
			WriteValueToFile("Input File: " + path);
			WriteValueToFile("Output File: " + OutputFileName + Environment.NewLine);


			var lines = File.ReadLines(path);
			foreach (var line in lines)
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					fileWebsiteDetails.Add(new WebsiteDetails { Url = line.Trim() });
				}
			}

			var checkingSitesHeader = "----------------- Checking " + fileWebsiteDetails.Count + " Sites from SiteList.txt ----------------------------------------------------";
			OutputValues.Add(checkingSitesHeader);
			WriteValueToFile(checkingSitesHeader);

			await RunConnectionCheckAsync(fileWebsiteDetails);

			WriteValueToFile(Environment.NewLine);

			Console.WriteLine(Environment.NewLine + "Connection Test Finished... Press any key to exit.");
		}

		public async Task<bool> RunConnectionCheckAsync(List<WebsiteDetails> websiteList)
		{
			var resultList = new List<WebsiteDetails>();
			List<Task<WebsiteDetails>> tasks = new List<Task<WebsiteDetails>>();

			foreach (var site in websiteList)
			{
				tasks.Add(Task.Run(() => CheckWebsite(site)));
			}

			await Task.WhenAll(tasks);

			foreach (var task in tasks)
			{
				resultList.Add(task.Result);
			}

			//WriteSiteListToFile(resultList);

			var failedSites = resultList.Where(x => !x.Result).ToList();

			if (failedSites.Any())
			{
				WriteValueToFile(Environment.NewLine + "----------------- " + failedSites.Count + " - Sites Failed Connection Test -------------------------------------------------------");
				WriteSiteListToFile(failedSites);
			}

			return true;
		}

		public WebsiteDetails CheckWebsite(WebsiteDetails siteDetails)
		{
			if (siteDetails == null)
			{
				throw new ApplicationException("Site is null");
			}
			siteDetails.SiteName = siteDetails.Url.Split('/')[2];

			try
			{
				var result = CheckWebConnection(siteDetails.Url);
				siteDetails.Result = result;
				var msg = siteDetails.Result + "--" + siteDetails.Url;
				WriteValueToFile(msg);
				//Console.WriteLine(msg);
			}
			catch (Exception ex)
			{
				if ((siteDetails.Url.ToLower().Contains("ecash") || siteDetails.Url.ToLower().Contains("transunion")) && ex.Message.Contains("(403) Forbidden"))
				{
					siteDetails.Result = true;
					var msg = siteDetails.Result + "--" + siteDetails.Url;
					WriteValueToFile(msg);
					//Console.WriteLine(msg);
				}
				else if (siteDetails.Url.ToLower().Contains("patientco") && ex.Message.Contains("(404) Not Found"))
				{
					siteDetails.Result = true;
					var msg = siteDetails.Result + "--" + siteDetails.Url;
					WriteValueToFile(msg);
					//Console.WriteLine(msg);
				}
				else
				{
					siteDetails.Result = false;
					siteDetails.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
					//Console.WriteLine(false + "-" + siteDetails.SiteName + "-" + siteDetails.ErrorMessage);
					var msg = false + "-" + siteDetails.SiteName;
					WriteValueToFile(msg);
					//Console.WriteLine(msg);
				}
			}

			return siteDetails;
		}

		public void WriteSiteListToFile(List<WebsiteDetails> results)
		{
			foreach (var result in results)
			{
				var msg = result.SiteName + "-" + result.ErrorMessage;
				WriteValueToFile(msg);
			}
		}

		public void WriteValueToFile(string value)
		{
			try
			{
				using (var file = new StreamWriter(OutputFileName, true))
				{
					file.WriteLineAsync(value);
				}

				Console.WriteLine(value);
				OutputValues.Add(value);
			}
			catch (Exception ex)
			{
				Console.WriteLine(value);
				OutputValues.Add(value);
			}
		}

		public bool CheckWebConnection(string requestedUrl)
		{

			WebRequest request = WebRequest.Create(requestedUrl);
			request.Timeout = 10000;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			if (response.StatusCode != HttpStatusCode.OK)
			{
				Console.WriteLine(response.StatusDescription);
				return false;
			}

			//Console.WriteLine(response.StatusDescription);
			response.Close();
			return true;
		}
	}
}
