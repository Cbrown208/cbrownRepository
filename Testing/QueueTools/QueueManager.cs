using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using QueueTools.RabbitMQ;

namespace QueueTools
{
	public class QueueManager
	{
		private RabbitMqTransportManager _manager;
		private const int DefaultRabbitPort = 5672;
		private readonly BusSettings _busSettings;

		public QueueManager(BusSettings settings)
		{
			_busSettings = settings;
		}

		private RabbitMqTransportManager RabbitMqTransportManager
		{
			get
			{
				if (_manager == null)
				{
					var uri = new Uri(_busSettings.OutgoingBusSettings.BaseUriString);
					if (uri.Segments.Last() == "PAS")
					{
						_manager = new RabbitMqTransportManager(uri.Host, DefaultRabbitPort, _busSettings.Username, _busSettings.Password,
							"PAS");
					}
					else
					{
						_manager = new RabbitMqTransportManager(uri.Host, DefaultRabbitPort, _busSettings.Username, _busSettings.Password, null);
					}
				}
				
				return _manager;
			}
		}


		public List<RabbitMQ.QueueInfo> CreateQueues(string format, int numberOfQueues, List<RabbitMQ.QueueInfo> queueList)
		{
			int startingNumber = GetStartingNumber(queueList);

			var list = new List<RabbitMQ.QueueInfo>();
			for (int i = 0; i < numberOfQueues; i++)
			{
				var number = startingNumber + i;

				RabbitMqTransportManager.EnsureQueue(string.Format(format, number));
				list.Add(new RabbitMQ.QueueInfo { Name = string.Format(format, number), Durable = true });
			}
			return list;
		}

		public List<RabbitMQ.QueueInfo> GetQueueList(string queuePattern)
		{
			queuePattern = queuePattern.Replace("{0}", @"\d+");
			var adtQueueUri = new Uri(_busSettings.OutgoingBusSettings.BaseUriString);
			string uri = string.Format("{0}://{1}:{2}@{3}/", adtQueueUri.Scheme, _busSettings.Username,
				_busSettings.Password, adtQueueUri.Host);
			var qInfos = RabbitMqTransportManager.GetQueuesFor(uri, "%2F");
			var qs = qInfos.Where(x => Regex.IsMatch(x.Name, queuePattern)).ToList();
			return qs;
		}

		public List<RabbitMQ.QueueInfo> GetVirtualHostQueueList(string vhost)
		{
			var queuePattern = "bus-";
			queuePattern = queuePattern.Replace("{0}", @"\d+");
			var adtQueueUri = new Uri(_busSettings.OutgoingBusSettings.BaseUriString);
			string uri = string.Format("{0}://{1}:{2}@{3}:5672/{4}", adtQueueUri.Scheme, _busSettings.Username,
				_busSettings.Password, adtQueueUri.Host, vhost);
			var qInfos = RabbitMqTransportManager.GetQueuesFor(uri, vhost);
			var qs = qInfos.Where(x => !Regex.IsMatch(x.Name, queuePattern)).ToList();
			return qs;
		}

		public List<RabbitMQ.QueueInfo> PurgeQueueList(string queuePattern,string vhost)
		{
			queuePattern = queuePattern.Replace("{0}", @"\d+");
			var adtQueueUri = new Uri(_busSettings.OutgoingBusSettings.BaseUriString);
			string uri = string.Format("{0}://{1}:{2}@{3}/{4}/", adtQueueUri.Scheme, _busSettings.Username,
				_busSettings.Password, adtQueueUri.Host,"PAS");
			//var qInfos = RabbitMqTransportManager.GetQueuesFor(uri, "%2F");
			var qInfos = RabbitMqTransportManager.GetQueuesFor(uri, vhost);
			var qs = qInfos.Where(x => Regex.IsMatch(x.Name, queuePattern)).ToList();
			foreach (var queue in qs)
			{
				 _manager.PurgeQueues(queue.Name);
			}
			return qs;
		}

		public List<RabbitMQ.QueueInfo> DeleteQueueList(string queuePattern, string vhost)
		{
			queuePattern = queuePattern.Replace("{0}", @"\d+");
			queuePattern = "VDILEWVPN";
			var adtQueueUri = new Uri(_busSettings.OutgoingBusSettings.BaseUriString);
			string uri = string.Format("{0}://{1}:{2}@{3}/", adtQueueUri.Scheme, _busSettings.Username,
				_busSettings.Password, adtQueueUri.Host);
			var qInfos = RabbitMqTransportManager.GetQueuesFor(uri, vhost);
			var qs = qInfos.Where(x => Regex.IsMatch(x.Name, queuePattern)).ToList();
			foreach (var queue in qs)
			{
				_manager.DeleteQueues(queue.Name);
			}
			return qs;
		}

		public List<RabbitMQ.QueueInfo> DeleteVdiTestingQueues(string vhost)
		{
			var queuePattern = "VDILEWVPN";
			var adtQueueUri = new Uri(_busSettings.OutgoingBusSettings.BaseUriString);
			string uri = string.Format("{0}://{1}:{2}@{3}/", adtQueueUri.Scheme, _busSettings.Username,
				_busSettings.Password, adtQueueUri.Host);
			var qInfos = RabbitMqTransportManager.GetQueuesFor(uri, vhost);
			var qs = qInfos.Where(x => Regex.IsMatch(x.Name, queuePattern)).ToList();
			foreach (var queue in qs)
			{
				_manager.DeleteQueues(queue.Name);
			}
			return qs;
		}

		public List<RabbitMQ.QueueInfo> GetQueueStats(string queuePattern, string vhost)
		{
			var msgcount = 0;
			var consumerCount = 0;
			queuePattern = queuePattern.Replace("{0}", @"\d+");
			var adtQueueUri = new Uri(_busSettings.OutgoingBusSettings.BaseUriString);
			string uri = string.Format("{0}://{1}:{2}@{3}/", adtQueueUri.Scheme, _busSettings.Username,
				_busSettings.Password, adtQueueUri.Host);
			var qInfos = RabbitMqTransportManager.GetQueuesFor(uri, vhost);
			var qs = qInfos.Where(x => Regex.IsMatch(x.Name, queuePattern)).ToList();
			foreach (var queue in qs)
			{
				if (queue.Messages >= 1)
				{
					msgcount = msgcount + 1;
				}
				if (queue.Consumers >= 1)
				{
					consumerCount = consumerCount + 1;
				}
			}
			Console.WriteLine("Number of Queues With Messages: " + msgcount);
			Console.WriteLine("Number of Queues With Consumers: " + consumerCount);
			return qs;
		}

		private int GetStartingNumber(List<RabbitMQ.QueueInfo> list)
		{
			return list.Max(info => GetQueueNameSeq(info.Name));
		}

		private int GetQueueNameSeq(string name)
		{
			int i = 0;
			var parts = name.Split('_');
			int.TryParse(parts[3], out i);
			return i;
		}

		public void Dispose()
		{
			_manager.Dispose();
		}
	}
}