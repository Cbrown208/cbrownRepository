using MassTransit3_Example;

namespace RabbitMQ_MassTransit3
{
	public class LocalBusSettings
	{
		public BusSettings GetLocalBusSettings()
		{
			BusSettings localBusSettings = new BusSettings()
			{
				ConcurrentConsumerLimit = "1",
				HeartBeatInSeconds = 10,
				IncomingUriString = "rabbitmq://iv-rmq-pas.nthrive.com/DI",
				IncomingQueue = "PAS_ADT_HL7_INGRESS_TEST",
				OutgoingQueue = "PAS_ADT_HL7_INGRESS_TEST",
				Username = "PAS",
				Password = "PAS"
			};
			return localBusSettings;
		}
	}
}