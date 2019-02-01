using Contracts;
using System;
using System.Threading;
using MassTransit;

namespace TestPublisher
{
	class Program
	{
		static void Main()
		{
			var bus = CreateBus();
			var busHandle = bus.Start();
			var endpoint = new Uri("rabbitmq://localhost/Test_PrioritySubscriber");

			Console.WriteLine("1: Send 500 Messages");
			Console.WriteLine("2: Send 1 Priority Message");
			Console.WriteLine("3: Send 4 Priority Message");
			Console.WriteLine("Enter q to quit");
			var input = Console.ReadLine();
			while (input != null && input.ToLower() != "q")
			{
				if (input == "1")
				{
					for (var i = 0; i < 500; i++)
					{
						var message = new SomethingHappened() { What = i.ToString(), When = DateTime.Now };
						bus.Publish(message);
					}
					Console.WriteLine("500 Messages Sent");
				}
				else if (input == "2")
				{
					var message = new SomethingHappened() { What = "999999", When = DateTime.Now };
					bus.GetSendEndpoint(endpoint).Result.Send(message, x => x.SetPriority(1));
					//bus.Publish(message, x => x.SetPriority(1));
					Console.WriteLine("1 Priority Message Sent");
				}
				else if (input == "3")
				{
					var message = new SomethingHappened() { What = "999999", When = DateTime.Now };
					for (var i = 0; i < 4; i++)
					{
						bus.Publish(message, x => x.SetPriority(1));
					}
					Console.WriteLine("4 Priority Messags Sent");
				}
				Thread.Sleep(1000);
				input = Console.ReadLine();
			}
			busHandle.Stop();
		}

		private static IBusControl CreateBus()
		{
			return Bus.Factory.CreateUsingRabbitMq(x =>
				x.Host(new Uri("rabbitmq://localhost/"), h =>
				{
					h.Username("PAS");
					h.Password("PAS");
					//x.EnablePriority(1);
				}));
		}
	}
}
