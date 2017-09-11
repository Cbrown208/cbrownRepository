using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RabbitMQ_MassTransit3.RabbitMQ;

namespace RabbitMQ_MassTransit3
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
					_manager = new RabbitMqTransportManager(uri.Host, DefaultRabbitPort, _busSettings.Username, _busSettings.Password);
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

		public List<RabbitMQ.QueueInfo> PurgeQueueList(string queuePattern)
		{
			queuePattern = queuePattern.Replace("{0}", @"\d+");
			var adtQueueUri = new Uri(_busSettings.OutgoingBusSettings.BaseUriString);
			string uri = string.Format("{0}://{1}:{2}@{3}/", adtQueueUri.Scheme, _busSettings.Username,
				_busSettings.Password, adtQueueUri.Host);
			var qInfos = RabbitMqTransportManager.GetQueuesFor(uri, "%2F");
			var qs = qInfos.Where(x => Regex.IsMatch(x.Name, queuePattern)).ToList();
			foreach (var queue in qs)
			{
				 _manager.PurgeQueues(queue.Name);
			}
			return qs;
		}

		public List<RabbitMQ.QueueInfo> DeleteQueueList(string queuePattern)
		{
			queuePattern = queuePattern.Replace("{0}", @"\d+");
			var adtQueueUri = new Uri(_busSettings.OutgoingBusSettings.BaseUriString);
			string uri = string.Format("{0}://{1}:{2}@{3}/", adtQueueUri.Scheme, _busSettings.Username,
				_busSettings.Password, adtQueueUri.Host);
			var qInfos = RabbitMqTransportManager.GetQueuesFor(uri, "%2F");
			var qs = qInfos.Where(x => Regex.IsMatch(x.Name, queuePattern)).ToList();
			foreach (var queue in qs)
			{
				_manager.PurgeQueues(queue.Name);
			}
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