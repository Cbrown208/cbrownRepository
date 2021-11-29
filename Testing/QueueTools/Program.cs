using System;
using System.Linq;
using QueueTools.Messages;

namespace QueueTools
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Please enter the number of the bus settings you would like:");
			Console.WriteLine("1: Local");
			Console.WriteLine("2: IV");
			Console.WriteLine("3: Perf");
			Console.WriteLine("4: RC");
			//========== BUS OPTION ==============//
			var busSettingsOption = Console.ReadLine();
			//========== BUS OPTION ==============//


			BusDetails busDetails = new BusDetails();
			BusSettings busSettings;
			if (busSettingsOption == "1")
			{
				busSettings = LocalBusSettings.GetLocalBusSettings();
			}
			else if (busSettingsOption == "2")
			{
				busSettings = LocalBusSettings.GetIvBusSettings();
			}
			else if (busSettingsOption == "3")
			{
				busSettings = LocalBusSettings.GetPerfLabBusSettings();
			}
			else if (busSettingsOption == "4")
			{
				busSettings = LocalBusSettings.GetRCBusSettings();
			}
			else
			{
				busSettings = LocalBusSettings.GetLocalBusSettings();
			}

			QueueManager queueManager = new QueueManager(busSettings);
			MessageGenerator msgGenerator = new MessageGenerator();
			bool disposeNeeded = false;
			var bus = busDetails.CreateBus(busSettings);
			var endpoint = new Uri(busSettings.IncomingUriString +busSettings.IncomingQueue);
			var incomingUri = new Uri(busSettings.IncomingUriString);
			var OutgoingUri = new Uri(busSettings.OutgoingBusSettings.BaseUriString);
			var text = "";
			bus.Start();
			Console.WriteLine("Please select from the following options: ");
			Console.WriteLine("1  : Send X Messages to PAS_ADT_HL7_INGRESS queue");
			Console.WriteLine("2  : Purge All PAS_ADT_WORKER queues");
			Console.WriteLine("3  : Delete All PAS_ADT_WORKER queues");
			Console.WriteLine("4  : Thread Testing");
			Console.WriteLine("5  : SendCmd Message");
			Console.WriteLine("6  : SendCmd Message");
			Console.WriteLine("7  : Stats");
			Console.WriteLine("8  : Get QueueList");
			Console.WriteLine("9  : Delete VDI Queues");
			Console.WriteLine("10 : Delete Extra Exchanges");
			Console.WriteLine("to exit the program enter: q");
			while (text != "q")
			{
				if (text == "1")
				{
					Console.WriteLine("How many messages would you like to send? ");
					var msgCount = Convert.ToInt32(Console.ReadLine());
					try
					{
						while (msgCount > 0)
						{
							Random rnd = new Random();
							int month = rnd.Next(1, 13);
							var msg = msgGenerator.GetAdtQueueMessageWithAccountNumber("test"+ month);
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
						if (OutgoingUri.Segments.Last() != "/")
						{
							queueManager.PurgeQueueList(busSettings.OutgoingBusSettings.OutgoingQueue, "PAS");
						}
						queueManager.PurgeQueueList(busSettings.OutgoingBusSettings.OutgoingQueue, "");
						
						Console.WriteLine("Purge Completed");
						disposeNeeded = true;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}
				else if (text == "3")
				{
					try
					{
						if (OutgoingUri.Segments.Last() != "/")
						{
							queueManager.DeleteQueueList(busSettings.OutgoingBusSettings.OutgoingQueue, OutgoingUri.Segments.Last());
						}
						else
						{
							queueManager.DeleteQueueList(busSettings.OutgoingBusSettings.OutgoingQueue, "");
							//queueManager.DeleteQueueList(busSettings.OutgoingBusSettings.OutgoingQueue, "");
						}

						Console.WriteLine("Delete Completed");
						disposeNeeded = true;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}
				else if (text == "4")
				{
					try
					{
						for (int i = 0; i < 1000; i++)
						{
							var msg = msgGenerator.GetAdtQueueMessageWithAccountNumber("testingMessg" + i);
							bus.GetSendEndpoint(endpoint).Result.Send(msg);
						}
						Console.WriteLine("Finished sending Messages");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}
				else if (text == "5")
				{
					try
					{
						endpoint = new Uri(busSettings.IncomingUriString + busSettings.IncomingQueue);
						Console.WriteLine("Enter the Queue Number to send Command For:");
						var queueNumber = Console.ReadLine();
						var msg = msgGenerator.GetAdtCommandCompletedMessageWithQueueNumber(queueNumber);
						bus.GetSendEndpoint(endpoint).Result.Send(msg);
						Console.WriteLine("Finished sending Messages");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}
				else if (text == "6")
				{
					try
					{
						endpoint = new Uri(busSettings.IncomingUriString + busSettings.IncomingQueue);
						var queueList = queueManager.GetQueueList(busSettings.OutgoingBusSettings.OutgoingQueue);
						foreach (var qlist in queueList)
						{
							if (qlist.Messages > 0)
							{
								var msg = msgGenerator.GetAdtCommandCompletedMessageWithQueueName(qlist.Name);
								bus.GetSendEndpoint(endpoint).Result.Send(msg);
							}
						}
						Console.WriteLine("Finished sending Messages");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}
				else if (text == "7")
				{
					try
					{
						if (OutgoingUri.Segments.Last() != "/")
						{
							queueManager.GetQueueStats(busSettings.OutgoingBusSettings.OutgoingQueue, OutgoingUri.Segments.Last());
						}
						else
						{
							queueManager.GetQueueStats(busSettings.OutgoingBusSettings.OutgoingQueue, "");
						}
						disposeNeeded = true;
						Console.WriteLine("Stats End");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}

				else if (text == "8")
				{
					try
					{
						var queueList = queueManager.GetVirtualHostQueueList(OutgoingUri.Segments.Last());

						foreach (var info in queueList)
						{
							if (info.Messages > 0)
							{
								Console.WriteLine("Name:      " + info.Name);
								Console.WriteLine("Messages:  " + info.Messages);
								Console.WriteLine("Consumers: " + info.Consumers);
							}
						}
						disposeNeeded = true;
						Console.WriteLine("Stats End");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}

				else if (text == "9")
				{
					try
					{
						if (OutgoingUri.Segments.Last() != "/")
						{
							queueManager.DeleteVdiTestingQueues(OutgoingUri.Segments.Last());
						}
						else
						{
							queueManager.DeleteVdiTestingQueues("");
						}

						Console.WriteLine("Delete Completed");
						disposeNeeded = true;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}

				else if (text == "10")
				{
					try
					{
						if (OutgoingUri.Segments.Last() != "/")
						{
							queueManager.DeleteUnusedExchanges(OutgoingUri.Segments.Last());
						}
						else
						{
							queueManager.DeleteUnusedExchanges("");
						}

						Console.WriteLine("Delete Completed");
						disposeNeeded = true;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.InnerException);
					}
				}

				text = Console.ReadLine();
			}
			Console.WriteLine("Exiting Program .... Bye");
			bus.Stop();
			if (disposeNeeded != false)
			{
				queueManager.Dispose();
			}
		}
	}
}
