using System;
using RabbitMQ_MassTransit3.Messages;

namespace RabbitMQ_MassTransit3
{
    class Program
    {
        static void Main(string[] args)
        {
			BusDetails busDetails = new BusDetails();
			BusSettings busSettings = LocalBusSettings.GetLocalBusSettings();
			QueueManager queueManager = new QueueManager(busSettings);
			MessageGenerator msgGenerator = new MessageGenerator();

			var bus = busDetails.CreateBus(busSettings);
			var endpoint = new Uri(busSettings.IncomingUriString + busSettings.IncomingQueue);
			var text = "";
	        bus.Start();
			Console.WriteLine("Please select from the following options: ");
			Console.WriteLine("1 : Send X Messages to PAS_ADT_HL7_INGRESS queue");
			Console.WriteLine("2 : Purge All PAS_ADT_WORKER queues");
			Console.WriteLine("3 : Delete All PAS_ADT_WORKER queues");
			Console.WriteLine("to exit the program enter: quit");
			while (text != "quit")
			{
				if (text == "1")
				{
					Console.WriteLine("How many messages would you like to send? ");
						var msgCount = Convert.ToInt32(Console.ReadLine());
					try
					{
						while (msgCount > 0)
						{
							var msg = msgGenerator.GetAdtQueueMessage();
							bus.GetSendEndpoint(endpoint).Result.Send(msg);
							msgCount = msgCount - 1;
						}
						Console.WriteLine("Messages have been sent");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}
				else if (text == "2")
				{
					try
					{
						queueManager.PurgeQueueList(busSettings.OutgoingBusSettings.OutgoingQueue);
						Console.WriteLine("Purge Completed");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
					queueManager.Dispose();
				}
				else if (text == "3")
				{
					try
					{
						queueManager.DeleteQueueList(busSettings.OutgoingBusSettings.OutgoingQueue);
						Console.WriteLine("Delete Completed");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
					queueManager.Dispose();
				}
				text = Console.ReadLine();
	        }
			Console.WriteLine("Exiting Program .... Bye");
			//queueManager.Dispose();
			bus.Stop();

			//TestPublisher publisher = new TestPublisher();
			//SomethingHappenedConsumer consumer = new SomethingHappenedConsumer();

			//Console.WriteLine("Publishing");
			//publisher.Publish();
			//Console.WriteLine("Published Complete");

			//Console.WriteLine("Consuming");
			////consumer.MessageConsumer();
			//Console.WriteLine("Consuming Complete");


		}
    }
}
