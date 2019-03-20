using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Tools.WebRequests
{
	public class WebRequestManager
	{
		private readonly AiWebRequests _aiWebRequests = new AiWebRequests();
		public void MakeWebRequests()
		{
			//_aiWebRequests.PurgeAzureAppInsightsData();
		}

		private HttpClient GetHttpClient()
		{
			var baseAddress = "https://management.azure.com";
			var client = new HttpClient { BaseAddress = new Uri(baseAddress) };
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}
	}
}
