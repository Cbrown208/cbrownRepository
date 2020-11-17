using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RabbitMQApiCalls.Models;

namespace RabbitMQApiCalls
{
	public class RmqApiCallsManager
	{
		private const string LocalRmqUrl = "http://localhost:15672";
		private const string DevRmqUrl = "http://dev-rmq-pas.nthrive.com:15672";
		private const string QaRmqUrl = "http://iv-rmq-pas.nthrive.com:15672";
		private const string RcRmqUrl = "http://rc-rmq2-pas.nthrive.com:15672";
		private const string ProdRmqUrl = "http://rmq2-pas.nthrive.com:15672";
		private readonly RabbitMqApiGateway _rmqApiGateway;

		public RmqApiCallsManager()
		{
			_rmqApiGateway = new RabbitMqApiGateway(QaRmqUrl);
		}

		public void RunApiCallsTest()
		{
			ReadRmqConnectionsFromFile();
			ReadRmqConnectionsFromServer();
			Console.ReadLine();
		}

		private void ReadRmqConnectionsFromFile()
		{
			var filePath = @"C:\MyScripts\Temp\iv-RmqConnections.json";

			var json = File.ReadAllText(filePath);
			var connectionList = JsonConvert.DeserializeObject<List<RmqConnection>>(json);

			if (connectionList.Any())
			{
				//Method 2
				var processList = new List<string>();
				foreach (var connection in connectionList)
				{
					if (!string.IsNullOrWhiteSpace(connection.client_properties.process_name))
					{
						processList.Add(connection.client_properties.process_name);
						Console.WriteLine("Connected: " + connection.client_properties.connected);
					}
				}

				var distinctProcess = processList.Distinct().ToList();
				distinctProcess.Sort();

				foreach (var process in distinctProcess)
				{
					Console.WriteLine("Process : " + process);
				}

			}
		}

		private void ReadRmqConnectionsFromServer()
		{
			var vHost = "PAS";
			var connectionList = _rmqApiGateway.GetRmqConnectionsList(vHost).Result;

			var processList = new List<RmqClientProperties>();
			foreach (var connection in connectionList)
			{
				if (!string.IsNullOrWhiteSpace(connection.client_properties.process_name) && !connection.client_properties.process_name.Contains("w3wp"))
				{
					processList.Add(connection.client_properties);
				}
			}

			processList = processList.OrderBy(x => x.process_name).ToList();

			foreach (var details in processList)
			{
				Console.WriteLine("Process:   " + details.process_name);
				Console.WriteLine("Host:      " + details.hostname);
				Console.WriteLine("Connected: " + details.connected + Environment.NewLine);
			}

			Console.WriteLine("Total Connection Count: " + processList.Count);
		}
	}
}
