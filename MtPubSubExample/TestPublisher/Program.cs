using Configuration;
using Contracts;
using System;
using System.Threading;
using MassTransit;

namespace TestPublisher
{
	class Program
	{
		static void Main(string[] args)
		{
			var bus = Bus.Factory.CreateUsingRabbitMq(x =>
				x.Host(new Uri("rabbitmq://localhost/"), h => { h.Username("PAS"); h.Password("PAS"); }));
			var busHandle = bus.Start();
			string text = "";
			int count = 0;
			while (text != "quit")
			{
				
				var message = new SomethingHappenedMessage() { What = count.ToString(), When = DateTime.Now };
				Console.WriteLine("Publishing Message Number "+message.What +" At "+ message.When);
				 bus.Publish<SomethingHappened>(message);
				Thread.Sleep(1000);
				count = count + 1;
				if (count == 1000)
				{
					Thread.Sleep(1000);
					text = "quit";
				}
			}

			busHandle.Stop();
		}
	}
}
