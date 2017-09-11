using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebCallsExample
{
	public class CheckWebsite
	{
		public bool HealthCheck(string requestedUrl)
		{

			WebRequest request = WebRequest.Create(requestedUrl);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			if (response.StatusCode != HttpStatusCode.OK)
			{
				return false;
			}
			Console.WriteLine(response.StatusDescription);
			response.Close();

			return true;
		}

		public bool AsyncHealthCheck(string requestedUrl)
		{
			using (var client = GetHttpClient(requestedUrl,new MediaTypeWithQualityHeaderValue("application/json")))
			{
				//var task= client.PostAsJsonAsync(requestedUrl, request);
				var task = client.GetAsync(requestedUrl);
				var response = task.Result.Content.ReadAsStringAsync().Result;
				Console.WriteLine(response);
				string currentDate = DateTime.Now.Year+"-"+DateTime.Now.Month+"-"+DateTime.Now.Day;
				if (response.Contains(currentDate))
				{
					return true;
				}
				return false;
			}
		}

		protected HttpClient GetHttpClient(string baseUrl,params MediaTypeWithQualityHeaderValue[] mediaTypes)
		{
			int TimeoutInSeconds = 100;
			var client = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(TimeoutInSeconds) };
			client.DefaultRequestHeaders.Accept.Clear();

			foreach (var mediaType in mediaTypes)
			{
				client.DefaultRequestHeaders.Accept.Add(mediaType);
			}
			return client;
		}
	}
}