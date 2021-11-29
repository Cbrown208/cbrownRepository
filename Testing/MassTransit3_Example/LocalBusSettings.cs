namespace MassTransit3_Example
{
	public class LocalBusSettings
	{
		public BusSettings GetLocalBusSettings()
		{
			var localBusSettings = new BusSettings
			{
				ConcurrentConsumerLimit = "1",
				HeartBeatInSeconds = 10,
				IncomingUriString = "rabbitmq://localhost/PAS",
				IncomingQueue = "ConsumerTesting",
				OutgoingQueue = "Worklist_Sync",
				Username = "PAS",
				Password = "PAS"
			};
			return localBusSettings;
		}
	}
}