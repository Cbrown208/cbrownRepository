using RabbitMQ.Client;

namespace LoadSimulator.RabbitMQ
{
	public class QueueManager
	{
		private readonly IModel _channel;

		public QueueManager(BusSettings busSettings)
		{
			var factory = new ConnectionFactory();
			factory.HostName = busSettings.IncomingUri;
			factory.UserName = busSettings.Username;
			factory.Password = busSettings.Password;
			var connection = factory.CreateConnection();
			_channel = connection.CreateModel();
		}

		public void CreateQueue(string name)
		{
			_channel.QueueDeclare(name, true, false, false);
		}
	}
}
