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

		private const string ProdDiRmq = "http://dataingresshl7msgq.nthrive.com:15672";
		private readonly RabbitMqApiGateway _rmqApiGateway;

		public RmqApiCallsManager()
		{

			//_rmqApiGateway = new RabbitMqApiGateway(QaRmqUrl);
			_rmqApiGateway = new RabbitMqApiGateway(ProdRmqUrl);
		}

		public void RunApiCallsTest()
		{
			//GetSpecificQueueDetails();
			//ReadRmqConnectionsFromFile();
			//ReadRmqConnectionsFromServer();
			//GetMonitoredQueueListDetails();
			//GetQueuesWithConsumersFromServer();
			//GetConsumersListFromServer();

			//GetExchangeList();

			GetOrphanedQueues();
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

		private void GetSpecificQueueDetails()
		{
			var tempUri = new Uri("rabbitmq://localhost/PAS");



			var queueDetails = _rmqApiGateway.GetRmqQueueDetails("MedAssets.Estimation.Worker_error", "PAS").Result;

		}

		private void GetMonitoredQueueListDetails()
		{
			var rmqQueueList = _rmqApiGateway.GetRmqQueueList("PAS").Result;
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
					Console.WriteLine("Idel Since : " + queue.IdleSinceLocalDateTime);
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
				if (queue.backing_queue_status.avg_egress_rate <= 0 && queue.messages_ready > 0 && !queue.name.ToLower().Contains("_error"))
				{
					Console.WriteLine("Possible Orphan Queue: " + queue.name + "-" + queue.messages_ready + "-" + queue.backing_queue_status.avg_ingress_rate);
				}

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
					//Console.WriteLine("Queue Name: " + queue.name);
					//Console.WriteLine("Consumers : " + queue.consumers + Environment.NewLine);
				}
			}
			Console.WriteLine("Total Consumer Count: "+consumerCount);
		}

		private void GetConsumersListFromServer()
		{
			var temp = _rmqApiGateway.GetRmqQueueDetails("MedAssets.Estimation.Worker_error", "PAS").Result;

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

		private void GetOrphanedQueues()
		{
			var rmqQueueList = _rmqApiGateway.GetRmqQueueList().Result;

			var orphanedQueueList = new List<RmqQueueProperties>();
			var orphanedRouterList = new List<string>();
			// avg_ingress_rate seems to get reset to 0 after about 10-15 min or so 

			foreach (var queue in rmqQueueList)
			{
				if (queue.backing_queue_status.avg_ingress_rate * 100 <= 0 && queue.messages_ready > 0 && !queue.name.ToLower().Contains("_error") && queue.name.ToLower().Contains("adt_worker_"))
				{
					orphanedQueueList.Add(queue);
					//Console.WriteLine("Possible Orphan Queue: " + queue.name + "-" + queue.messages_ready + "-" + queue.backing_queue_status.avg_ingress_rate);
				}
				
			}

			if (orphanedQueueList.Any())
			{
				Console.WriteLine("PossibleOrphaned Queues: ");
				foreach (var queue in orphanedQueueList)
				{
					Console.WriteLine(queue.name+ "-" + queue.messages_ready);
					var queueParts = queue.name.Split('_');
					
					var isRouterInList = orphanedRouterList.FirstOrDefault(x => x == queueParts[3]);
					if (isRouterInList == null)
					{
						orphanedRouterList.Add(queueParts[3]);
					}
					//Console.WriteLine("Possible Orphan Queue: " + queue.name + "-" + queue.messages_ready + "-" + queue.backing_queue_status.avg_ingress_rate);
				}
				Console.WriteLine("Total PossibleOrphaned Queues: "+ orphanedQueueList.Count);
			}

			if (orphanedRouterList.Any())
			{
				Console.WriteLine("Routers below have orphaned Queues: ");
				foreach (var router in orphanedRouterList)
				{
					Console.WriteLine(router);
				}
			}

			else
			{
				Console.WriteLine("No Orphaned Queues found.");
			}
		}

		private void GetExchangeList()
		{
			var rmqExchangeList = _rmqApiGateway.GetRmqExchangeList().Result;
			var serverlist = new List<string>();

			foreach (var exchange in rmqExchangeList)
			{

				if (!exchange.auto_delete && exchange.name.Contains("bus-"))
				{
					var subExchangeName = exchange.name.Split('-');
					if (subExchangeName.Length > 1)
					{
						var serverValue = subExchangeName[1] + "-" + subExchangeName[2];
						serverlist.Add(serverValue);
					}
				}
			}

			var distinctList = serverlist
				.GroupBy(l => l)
				.Select(g => new
				{
					Date = g.Key,
					Count = g.Select(l => l).Count()
				});

			distinctList = distinctList.OrderBy(x => x.Count);
			foreach (var server in distinctList)
			{
				Console.WriteLine(server.Count+ " - " + server.Date);
			}

		}
	}
}
