using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Tools.Core.Models;

namespace Tools.WebRequests
{
	public class AiWebRequests
	{
		public string PurgeAzureAppInsightsData()
		{
			var results = "";
			try
			{
				// //POST https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/purge?api-version=2015-03-20
				using (var client = GetAzureHttpClient())
				{
					var subscriptionId = "";
					var resourceGroupName = "";
					var workspaceName = "";

					var request = GetAiPurgeRequest();

					var appInsightsPurgeEndpoint = FormatAppInsightsPurgeUrl(subscriptionId, resourceGroupName, workspaceName);

					var response = client.PostAsync(appInsightsPurgeEndpoint, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")).Result;

					results = "";

					if (!response.IsSuccessStatusCode)
					{
						var resp = response.Content.ReadAsStringAsync();
						throw new ApplicationException(resp.Result.ToString());
					}
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
			return results;
		}

		private string GetAiPurgeRequest()
		{
			var purgeRequest = new AppInsightsPurgeParams { table = "Request" };
			purgeRequest.filters.Add(new AiPurgeParamFilters { column = "TimeGenerated", Operator = ">", value = "2017-09-01T00:00:00" });
			var request = JsonConvert.SerializeObject(purgeRequest);
			request = request.Replace("Operator", "operator");
			return request;
		}

		private string FormatAppInsightsPurgeUrl(string subscriptionId, string resourceGroupName, string workspaceName)
		{
			var endpoint = string.Format(
				"/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.OperationalInsights/workspaces/{2}/purge?api-version=2015-03-20",
				subscriptionId, resourceGroupName, workspaceName);
			return endpoint;
		}

		private HttpClient GetAzureHttpClient()
		{
			var baseAddress = "https://management.azure.com";
			var client = new HttpClient { BaseAddress = new Uri(baseAddress) };
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}
	}
}
