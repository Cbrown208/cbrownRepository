using Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace TestSubscriber
{
	class SomethingHappenedConsumer : IConsumer<SomethingHappened>
	{
		public Task Consume(ConsumeContext<SomethingHappened> message)
		{
			Console.Write("TXT: " + message.Message.What);
			Console.Write("  SENT: " + message.Message.When.ToString());
			Console.Write("  PROCESSED: " + DateTime.Now.ToString());
			Console.WriteLine(" (" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + ")");
			return Task.FromResult(0);
		}
	}
}
