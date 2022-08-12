using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebResponseChecker
{
	public class WebResponseManager
	{
		public const string OutputFileName = "Output.txt";
		public ConcurrentBag<string> OutputValues = new ConcurrentBag<string>();

		public void CheckConnections()
		{
			CheckWebsites();
			Console.ReadLine();
		}

		private void CheckWebsites()
		{
			var fileSiteDetails = new List<SiteDetails>();
			var path = @"SiteList.txt";

			WriteValueToFile("------------------------------------------------------------------------------------------------------------------");
			WriteValueToFile("Start Time: " + DateTime.Now.ToLocalTime());
			WriteValueToFile("Input File: " + path);
			WriteValueToFile("Output File: " + OutputFileName + Environment.NewLine);


			var lines = File.ReadLines(path);
			foreach (var line in lines)
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					fileSiteDetails.Add(new SiteDetails { Url = line.Trim() });
				}
			}
			fileSiteDetails.Add(new SiteDetails { Url = "http://RCM40VPPASTSK13.medassets.com:80", SiteName = "eCash Server 1" });
			fileSiteDetails.Add(new SiteDetails { Url = "http://RCM40VPPASTSK14.medassets.com:80", SiteName = "eCash Server 2" });

			fileSiteDetails.Add(new SiteDetails { Url = "http://LEWVPPASWEB21:10888/SystemHealth", SiteName = "24/7 Server 1" });
			fileSiteDetails.Add(new SiteDetails { Url = "http://LEWVPPASWEB22:10888/SystemHealth", SiteName = "24/7 Server 2" });

			fileSiteDetails.Add(new SiteDetails { Url = "http://RCM40VPPASAPP01:61789/patientbalanceservice/PatientBalanceService.svc?wsdl", SiteName = "svc-pas.nthrive.com Server 1" });
			fileSiteDetails.Add(new SiteDetails { Url = "http://RCM40VPPASAPP02:61789/patientbalanceservice/PatientBalanceService.svc?wsdl", SiteName = "svc-pas.nthrive.com Server 2" });

			fileSiteDetails.Add(new SiteDetails { Url = "http://RCM40VPPASWEB15:42300/api/system/HealthCheck", SiteName = "RegQa UI Server 1" });
			fileSiteDetails.Add(new SiteDetails { Url = "http://RCM40VPPASWEB16:42300/api/system/HealthCheck", SiteName = "RegQa UI Server 2" });


			fileSiteDetails = fileSiteDetails.OrderBy(x => x.Url).ToList();

			var checkingSitesHeader = "----------------- Checking " + fileSiteDetails.Count + " Sites from SiteList.txt ----------------------------------------------------";
			WriteValueToFile(checkingSitesHeader);

			RunConnectionCheckSync(fileSiteDetails);

			WriteValueToFile(Environment.NewLine);

			Console.WriteLine(Environment.NewLine + "Connection Test Finished... Press any key to exit.");
		}

		private bool RunConnectionCheckSync(List<SiteDetails> websiteList)
		{
			var resultList = new List<SiteDetails>();

			foreach (var site in websiteList)
			{
				var result = CheckWebsite(site);
				resultList.Add(result);
			}

			//WriteSiteListToFile(resultList);

			var rcmSites = resultList.Where(x => x.Result != null && x.Result.ToLower().Contains("rcm")).ToList();
			var lewSites = resultList.Where(x => x.Result != null && x.Result.ToLower().Contains("lew")).ToList();

			if (rcmSites.Any() || lewSites.Any())
			{
				WriteValueToFile(Environment.NewLine + "----------------- " + rcmSites.Count + " - RCM Sites  " + lewSites.Count + "  - LEW Sites  -------------------------------------------------------" + Environment.NewLine);
				//WriteSiteListToFile(rcmSites);
			}

			return true;
		}

		private SiteDetails CheckWebsite(SiteDetails siteDetails)
		{
			var rcmSiteResult = "RCM";
			if (siteDetails == null)
			{
				throw new ApplicationException("Site is null");
			}

			if (siteDetails.Url.Contains("/") && string.IsNullOrWhiteSpace(siteDetails.SiteName))
			{
				siteDetails.SiteName = siteDetails.Url.Split('/')[2];
			}

			try
			{
				var result = CheckWebConnection(siteDetails.Url);
				siteDetails.Result = result;



				if (siteDetails.SiteName.ToLower().Contains("nthrive.com") || siteDetails.Url.Contains("42300"))
				{
					if (result.ToLower().Contains("rcm4") || result.ToLower().Contains("IPatientBalanceFeedService"))
					{
						siteDetails.Result = "RCM";
					}

					if (result.ToLower().Contains("lew") || result.ToLower().Contains("IPatientBalanceFeedService"))
					{
						siteDetails.Result = "LEW";
					}
				}
				else if (siteDetails.SiteName == "24/7")
				{
					if (result.ToLower().Contains("lewvppasweb21") || result.ToLower().Contains("lewvppasweb22"))
					{
						siteDetails.Result = rcmSiteResult;
					}
				}

				// POS Service
				else if (siteDetails.Url.Contains("80") && result.Contains("406"))
				{
					siteDetails.Result = "80/406 " + rcmSiteResult;
				}
				else if (result.Contains("404"))
				{
					siteDetails.Result = "404 LEW";
				}

				var msg = siteDetails.Result + "--" + siteDetails.SiteName;
				WriteValueToFile(msg);
				//Console.WriteLine(msg);
			}
			catch (Exception ex)
			{
				if ((siteDetails.SiteName.ToLower().Contains("ecash")))
				{
					siteDetails.Result = "Error LEW";
					var msg = siteDetails.Result + "--" + siteDetails.SiteName;
					WriteValueToFile(msg);
					//Console.WriteLine(msg);
				}
				else
				{
					siteDetails.Result = "false";
					siteDetails.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
					//Console.WriteLine(false + "-" + siteDetails.SiteName + "-" + siteDetails.ErrorMessage);
					var msg = "LEW-" + siteDetails.SiteName;
					WriteValueToFile(msg);
					//Console.WriteLine(msg);
				}
			}
			return siteDetails;
		}

		private void WriteSiteListToFile(List<SiteDetails> results)
		{
			foreach (var result in results)
			{
				var msg = result.SiteName + "-" + result.Result;
				WriteValueToFile(msg);
			}
		}

		private void WriteValueToFile(string value)
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

		private string CheckWebConnection(string requestedUrl)
		{
			string response;

			using (var client = GetHttpClient(requestedUrl, new MediaTypeWithQualityHeaderValue("application/json")))
			{
				Task<HttpResponseMessage> task = client.GetAsync(requestedUrl);
				var urlContents = task.Result;
				response = urlContents.Content.ReadAsStringAsync().Result;
			}
			return response;
		}

		protected HttpClient GetHttpClient(string baseUrl, params MediaTypeWithQualityHeaderValue[] mediaTypes)
		{
			int timeoutInSeconds = 100;
			var client = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(timeoutInSeconds) };
			client.DefaultRequestHeaders.Accept.Clear();

			foreach (var mediaType in mediaTypes)
			{
				client.DefaultRequestHeaders.Accept.Add(mediaType);
			}
			return client;
		}
	}

	public class SiteDetails
	{
		public string SiteName { get; set; }
		public string Url { get; set; }
		public string Result { get; set; }
		public string ErrorMessage { get; set; }
	}
}
