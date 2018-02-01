using MassTransit;
using System;
using TestSubscriber2;

namespace TestSubscriber
{
	class Program
	{
		static void Main(string[] args)
		{
			var bus = Bus.Factory.CreateUsingRabbitMq(x =>
			{
				var host = x.Host(new Uri("rabbitmq://localhost/"), h => { h.Username("PAS"); h.Password("PAS"); });
				x.ReceiveEndpoint(host, "MtPubSubExample_TestSubscriber2", e => e.Consumer<SomethingHappenedSecondConsumer>());
			});
			var busHandle = bus.Start();

			Console.ReadKey();

			bus.Stop();
		}
	}
}
