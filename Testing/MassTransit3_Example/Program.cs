using System;
using System.Collections.Generic;
using MassTransit;
using MassTransit3_Example.Messages;
using MedAssets.AMS.Common;

namespace MassTransit3_Example
{
	public class Program
	{
		private const int DefaultRabbitPort = 5672;
		static void Main(string[] args)
		{
			BusDetails busDetails = new BusDetails();
			LocalBusSettings localBusSettings = new LocalBusSettings();
			BusSettings busSettings;
			busSettings = localBusSettings.GetLocalBusSettings();
			
			var bus = busDetails.CreateBus(busSettings);
			var endpoint = new Uri(busSettings.IncomingUriString + "/" + busSettings.OutgoingQueue);
			ExMessages exMessage = new ExMessages();

			var personList = new List<Person>();

			for (int i = 0; i < 200; i++)
			{
				var subject = new Person {FirstName = "John", LastName = "Snow" + i};
				personList.Add(subject);
			}

			Console.WriteLine("Press any button to send a message");
			Console.ReadLine();
			bus.Start();
			var msg = exMessage.GetAdtQueueMessageWithAccountNumber("Test123");

			foreach (var person in personList)
			{
				GetValue(bus, endpoint, person);
			}

			//GetValue(bus, endpoint, msg);

			Console.WriteLine("Send Finished");
			
			bus.StopAsync();
			bus.Stop();

		}

		private static async void GetValue<T>(IBusControl bus, Uri endpoint, T msg)
		{
			//await bus.Publish(msg);

			await bus.GetSendEndpoint(endpoint).Result.Send(msg);
		}
	}
}
