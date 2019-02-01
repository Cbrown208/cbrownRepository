using MassTransit;
using System;

namespace TestSubscriber
{
	class Program
	{
		static void Main()
		{
			var bus = CreateBusWithPriority();
			bus.Start();
			Console.ReadKey();
			bus.Stop();
		}

		private static IBusControl CreateBusWithPriority()
		{
			return Bus.Factory.CreateUsingRabbitMq(x =>
			{
				var host = x.Host(new Uri("rabbitmq://localhost/"), h => { h.Username("PAS"); h.Password("PAS"); });
				x.ReceiveEndpoint(host, "Test_PrioritySubscriber", e =>
				{
					e.Consumer<PriorityConsumer>();
					e.EnablePriority(1);
				});
			});
		}
	}
}
