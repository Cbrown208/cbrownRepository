using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQApiCalls.Models;

namespace RabbitMQApiCalls
{
	class ExampleRmqQueueList
	{
		private void ForJesus()
		{
			var rabbitMqUrl = "http://iv-rmq-pas.nthrive.com:15672";
			var rmqQueueList = GetRmqQueueList(rabbitMqUrl, null).Result;

		}

		private async Task<List<RmqQueueProperties>> GetRmqQueueList(string baseUrl, string vHost)
		{
			try
			{
				var endpoint = baseUrl + "/api/queues";

				if (!string.IsNullOrWhiteSpace(vHost))
				{
					endpoint = baseUrl + "/api/queues/" + vHost;
				}

				var userName = "PAS";
				var password = "PAS";
				// Instantiate HttpClient passing in the HttpClientHandler
				using (var httpClient = GetClient(userName, password))
				{
					// Get the response from the API endpoint.
					var httpResponseMessage = httpClient.GetAsync(endpoint).Result;
					var httpContent = httpResponseMessage.Content;

					using (var streamReader = new StreamReader(httpContent.ReadAsStreamAsync().Result))
					{
						// Get the output string.
						var returnedJsonString = await streamReader.ReadToEndAsync();
						var results = new List<RmqQueueProperties>();

						if (returnedJsonString != "")
						{
							results = JsonConvert.DeserializeObject<List<RmqQueueProperties>>(returnedJsonString);
							return results;
						}

						return results;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		private HttpClient GetClient(string userName, string password)
		{
			var httpClientHandler = new HttpClientHandler { Credentials = new NetworkCredential(userName, password) };
			var httpClient = new HttpClient(httpClientHandler);
			return httpClient;
		}
	}
}
