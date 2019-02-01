using Contracts;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestSubscriber
{
	class PriorityConsumer : IConsumer<SomethingHappened>
	{
		private static int _messageCount;
		private static int _priorityMessageCount;

		// Priority Consumer
		public Task Consume(ConsumeContext<SomethingHappened> message)
		{
			if (message.Message.What == "999999")
			{
				_priorityMessageCount = _priorityMessageCount + 1;
				Console.WriteLine("Priority Message #: " + message.Message.What);
				Console.WriteLine("  SENT: " + message.Message.When);
				Console.WriteLine("  PROCESSED: " + DateTime.Now + " (" + Thread.CurrentThread.ManagedThreadId + ")");
				Console.WriteLine("Message Processed Before Priority Message: "+ _messageCount);
			}
			else
			{
				Console.WriteLine("Message #: " + message.Message.What);
			}
			//Thread.Sleep(500);
			_messageCount++;
			return Task.FromResult(0);
		}
	}
}
