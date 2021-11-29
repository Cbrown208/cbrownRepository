using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQApiCalls.Models;

namespace RabbitMQApiCalls
{
	public class RabbitMqApiGateway
	{
		private readonly string _baseUrl;
		private const string UserName = "PAS";
		private const string UserPassword = "PAS";

		public RabbitMqApiGateway(string baseUrl)
		{
			_baseUrl = baseUrl;
		}
		public async Task<bool> GetRmqUsersList()
		{
			try
			{
				// Instantiate HttpClient passing in the HttpClientHandler
				using (HttpClient httpClient = GetClient())
				{
					// Get the response from the API endpoint.
					var httpResponseMessage = httpClient.GetAsync(_baseUrl + "/api/users/").Result;

					HttpContent httpContent = httpResponseMessage.Content;

					using (StreamReader streamReader = new StreamReader(httpContent.ReadAsStreamAsync().Result))
					{

						// Get the output string.
						string returnedJsonString = await streamReader.ReadToEndAsync();

						// Instantiate a list to loop through.
						List<string> mqAccountNames = new List<string>();

						if (returnedJsonString != "")
						{
							// Deserialize into object
							dynamic dynamicJson = JsonConvert.DeserializeObject(returnedJsonString);
							if (dynamicJson != null)
							{
								foreach (dynamic item in dynamicJson)
								{
									mqAccountNames.Add(item.name.ToString());
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			return true;
		}

		public async Task<List<RmqConnection>> GetRmqConnectionsList(string vHost)
		{
			try
			{
				var endpoint = _baseUrl + "/api/connections";

				if (!string.IsNullOrWhiteSpace(vHost))
				{
					endpoint = _baseUrl + "/api/vhosts/" + vHost + "/connections";
				}

				// Instantiate HttpClient passing in the HttpClientHandler
				using (var httpClient = GetClient())
				{
					// Get the response from the API endpoint.
					var httpResponseMessage = httpClient.GetAsync(endpoint).Result;

					var httpContent = httpResponseMessage.Content;

					using (var streamReader = new StreamReader(httpContent.ReadAsStreamAsync().Result))
					{

						// Get the output string.
						var returnedJsonString = await streamReader.ReadToEndAsync();

						// Instantiate a list to loop through.
						var results = new List<RmqConnection>();

						if (returnedJsonString != "")
						{
							results = JsonConvert.DeserializeObject<List<RmqConnection>>(returnedJsonString);
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

		public async Task<List<RmqQueueProperties>> GetRmqQueueList(string vHost = null)
		{
			try
			{
				var endpoint = _baseUrl + "/api/queues";

				if (!string.IsNullOrWhiteSpace(vHost))
				{
					endpoint = _baseUrl + "/api/queues/" + vHost;
				}

				// Instantiate HttpClient passing in the HttpClientHandler
				using (var httpClient = GetClient())
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

		public async Task<List<RmqConsumerProperties>> GetRmqConsumerList(string vHost = null)
		{
			try
			{
				var consumerEndpoint = "/api/consumers";
				var endpoint = _baseUrl + consumerEndpoint;

				if (!string.IsNullOrWhiteSpace(vHost))
				{
					endpoint = _baseUrl + consumerEndpoint + vHost;
				}

				// Instantiate HttpClient passing in the HttpClientHandler
				using (var httpClient = GetClient())
				{
					// Get the response from the API endpoint.
					var httpResponseMessage = httpClient.GetAsync(endpoint).Result;
					var httpContent = httpResponseMessage.Content;

					using (var streamReader = new StreamReader(httpContent.ReadAsStreamAsync().Result))
					{
						// Get the output string.
						var returnedJsonString = await streamReader.ReadToEndAsync();
						var results = new List<RmqConsumerProperties>();

						if (returnedJsonString != "")
						{
							results = JsonConvert.DeserializeObject<List<RmqConsumerProperties>>(returnedJsonString);
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

		public async Task<List<RmqExchangeProperties>> GetRmqExchangeList(string vHost = null)
		{
			try
			{
				var consumerEndpoint = "/api/exchanges";
				var endpoint = _baseUrl + consumerEndpoint;

				if (!string.IsNullOrWhiteSpace(vHost))
				{
					endpoint = _baseUrl + consumerEndpoint + vHost;
				}

				// Instantiate HttpClient passing in the HttpClientHandler
				using (var httpClient = GetClient())
				{
					// Get the response from the API endpoint.
					var httpResponseMessage = httpClient.GetAsync(endpoint).Result;
					var httpContent = httpResponseMessage.Content;

					using (var streamReader = new StreamReader(httpContent.ReadAsStreamAsync().Result))
					{
						// Get the output string.
						var returnedJsonString = await streamReader.ReadToEndAsync();
						var results = new List<RmqExchangeProperties>();

						if (returnedJsonString != "")
						{
							results = JsonConvert.DeserializeObject<List<RmqExchangeProperties>>(returnedJsonString);
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

		private HttpClient GetClient()
		{
			var httpClientHandler = new HttpClientHandler { Credentials = new NetworkCredential(UserName, UserPassword) };
			var httpClient = new HttpClient(httpClientHandler);
			return httpClient;
		}
	}
}
