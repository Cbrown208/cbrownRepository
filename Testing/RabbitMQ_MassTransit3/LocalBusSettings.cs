namespace RabbitMQ_MassTransit3
{
	public class LocalBusSettings
	{
		public static BusSettings GetLocalBusSettings()
		{
			BusSettings localBusSettings = new BusSettings()
			{
				ConcurrentConsumerLimit = "1",
				HeartBeatInSeconds = 10,
				IncomingUriString = "rabbitmq://rcm41vqhl7app10/",
				IncomingQueue = "PAS_ADT_HL7_INGRESS_IV",
				Username = "PAS",
				Password = "PAS",
				OutgoingBusSettings = new OutgoingBusSettings()
				{
					BaseUriString = "rabbitmq://rcm41vqpasapp03/",
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
	}
}