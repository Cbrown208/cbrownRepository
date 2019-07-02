namespace QueueTools
{
	public class LocalBusSettings
	{
		public static BusSettings GetLocalBusSettings()
		{
			BusSettings localBusSettings = new BusSettings()
			{
				ConcurrentConsumerLimit = "1",
				HeartBeatInSeconds = 10,
				IncomingUriString = "rabbitmq://localhost/",
				IncomingQueue = "PAS_ADT_HL7_INGRESS_IV",
				Username = "PAS",
				Password = "PAS",
				OutgoingBusSettings = new OutgoingBusSettings()
				{
					BaseUriString = "rabbitmq://localhost/",
					AdtCommandReadyQueue = "PAS_ADT_CMD_READY_IV",
					AdtCommandCompleteQueue = "PAS_ADT_CMD_COMPLETE_IV",
					OutgoingQueue = "PAS_ADT_WORKER_{0}_IV",
					InitialQueueCount = 5,
					IncrementQueueCount = 1,
					Username = "PAS",
					Password = "PAS",
					ConcurrentConsumerLimit = "1",
					RetryLimit = "1",
					HeartBeatInSeconds = 10
					
				}
			};
			return localBusSettings;
		}

		public static BusSettings GetIvBusSettings()
		{
			BusSettings localBusSettings = new BusSettings()
			{
				ConcurrentConsumerLimit = "1",
				HeartBeatInSeconds = 10,
				IncomingUriString = "rabbitmq://iv-rmq-pas.nthrive.com/PAS",
				IncomingQueue = "PAS_ADT_HL7_INGRESS_IV",
				Username = "PAS",
				Password = "PAS",
				OutgoingBusSettings = new OutgoingBusSettings()
				{
					BaseUriString = "rabbitmq://iv-rmq-pas.nthrive.com/PAS",
					AdtCommandReadyQueue = "PAS_ADT_CMD_READY_IV",
					AdtCommandCompleteQueue = "PAS_ADT_CMD_COMPLETE_IV",
					OutgoingQueue = "PAS_ADT_WORKER_{0}_IV",
					InitialQueueCount = 5,
					IncrementQueueCount = 1,
					Username = "PAS",
					Password = "PAS",
					ConcurrentConsumerLimit = "1",
					RetryLimit = "1",
					HeartBeatInSeconds = 10

				}
			};
			return localBusSettings;
		}

		public static BusSettings GetPerfLabBusSettings()
		{
			BusSettings localBusSettings = new BusSettings()
			{
				ConcurrentConsumerLimit = "1",
				HeartBeatInSeconds = 10,
				IncomingUriString = "rabbitmq://perflab-rmqcluster.medassets.com/DI",
				IncomingQueue = "PAS_ADT_HL7_INGRESS_IV",
				Username = "perfuser",
				Password = "perfuser1",
				OutgoingBusSettings = new OutgoingBusSettings()
				{
					BaseUriString = "rabbitmq://perflab-rmqcluster.medassets.com/PAS",
					AdtCommandReadyQueue = "PAS_ADT_CMD_READY_IV",
					AdtCommandCompleteQueue = "PAS_ADT_CMD_COMPLETE_IV",
					OutgoingQueue = "PAS_ADT_WORKER_{0}",
					InitialQueueCount = 5,
					IncrementQueueCount = 1,
					Username = "perfuser",
					Password = "perfuser1",
					ConcurrentConsumerLimit = "1",
					RetryLimit = "1",
					HeartBeatInSeconds = 10
				}
			};
			return localBusSettings;
		}
	}
}