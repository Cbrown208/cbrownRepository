using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Pipeline;
using MassTransit3_Example.Messages;
using MedAssets.AMS.Common;
using QueueTools.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ_MassTransit3;

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

			Console.WriteLine("Press any button to send a message");
			Console.ReadLine();
			bus.Start();
			var msg = exMessage.GetAdtQueueMessageWithAccountNumber("Test123");

			GetValue(bus, endpoint, msg);

			Console.WriteLine("Send Finished");
			
			bus.StopAsync();
			bus.Stop();

		}

		private static async void GetValue(IBusControl bus, Uri endpoint, IAdtQueueMessage msg)
		{
			//await bus.Publish(msg);

			await bus.GetSendEndpoint(endpoint).Result.Send(msg);
		}
	}
}
