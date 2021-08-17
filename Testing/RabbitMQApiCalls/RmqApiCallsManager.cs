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
		private const string RcRmqUrl = "http://rc-rmq-pas.nthrive.com:15672";
		private const string ProdRmqUrl = "http://rmq-pas.nthrive.com:15672";
		private const string OldProdRmqUrl = "http://rmq2-pas.nthrive.com:15672";
		private readonly RabbitMqApiGateway _rmqApiGateway;

		public RmqApiCallsManager()
		{

			//_rmqApiGateway = new RabbitMqApiGateway(OldProdRmqUrl);
			_rmqApiGateway = new RabbitMqApiGateway(QaRmqUrl);
		}

		public void RunApiCallsTest()
		{
			//ReadRmqConnectionsFromFile();
			//ReadRmqConnectionsFromServer();
			//GetMonitoredQueueListDetails();
			//GetQueuesWithConsumersFromServer();
			GetConsumersListFromServer();
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

		private void GetMonitoredQueueListDetails()
		{
			var rmqQueueList = _rmqApiGateway.GetRmqQueueList().Result;
			var monitoredQueueList = new List<RmqQueueProperties>();

			var importantQueueList = new List<string> { "PAS_ADT_HL7_INGRESS", "Ei_Audit", "_CMD_", "_QUERY_REQUEST", "EB_Processor", "STATUS" , "RegQA", "Worklist", "Service_Category", "_AUTOMATION", "Simulator_Cmd" };

			foreach (var queue in rmqQueueList)
			{
				foreach (var queueName in importantQueueList)
				{
					if (queue.name.Contains(queueName) && !queue.name.Contains("bus-") && !queue.name.Contains("_error") && !queue.name.Contains("_skipped"))
					{
						monitoredQueueList.Add(queue);
					}
				}
			}

			monitoredQueueList.OrderBy(x => x.name);
			foreach (var queue in monitoredQueueList)
			{
				Console.WriteLine("Queue Name: " + queue.name);
				if (queue.consumers == 0)
				{
					Console.WriteLine("Consumers : " + queue.consumers +" <------ No Consumers issue!"+ Environment.NewLine);
				}
				else
				{
					Console.WriteLine("Consumers : " + queue.consumers + Environment.NewLine);
				}
			}
		}

		private void GetQueuesWithConsumersFromServer()
		{
			var rmqQueueList = _rmqApiGateway.GetRmqQueueList().Result;

			var allQueueList = new List<RmqQueueProperties>();
			var consumerCount = 0;

			foreach (var queue in rmqQueueList)
			{
				if (!queue.name.Contains("bus-") && !queue.name.Contains("_error") && !queue.name.Contains("_skipped"))
				{
					consumerCount = consumerCount + queue.consumers;
					allQueueList.Add(queue);
				}
			}

			var consumerQueues = allQueueList.Where(x => x.consumers > 0).OrderBy(x => x.name);
			//consumerQueues.OrderBy(x => x.name);

			foreach (var queue in consumerQueues)
			{
				if (queue.consumers > 0)
				{
					Console.WriteLine("Queue Name: " + queue.name);
					Console.WriteLine("Consumers : " + queue.consumers + Environment.NewLine);
				}
			}
			Console.WriteLine("Total Consumer Count: "+consumerCount);
		}

		private void GetConsumersListFromServer()
		{
			var rmqConsumerList = _rmqApiGateway.GetRmqConsumerList().Result;
			var consumerList = new List<RmqQueueProperties>();

			foreach (var consumer in rmqConsumerList)
			{
				if (!consumer.queue.name.Contains("bus-") && !consumer.queue.name.Contains("INGRESS_CLIENT") && !consumer.queue.name.Contains("ADT_WORKER_"))
				{
					consumerList.Add(consumer.queue);
				}
			}

			var sortedList = consumerList.GroupBy(i => i.name).OrderBy(x => x.Key);

			foreach (var grp in sortedList)
			{
				Console.WriteLine("| {1} | {0}", grp.Key, grp.Count());
			}

			Console.WriteLine("Total Consumer Count: " + consumerList.Count);
		}
	}
}
