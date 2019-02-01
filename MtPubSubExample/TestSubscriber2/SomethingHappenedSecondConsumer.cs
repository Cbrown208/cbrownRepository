using System;
using System.Threading.Tasks;
using Contracts;
using MassTransit;

namespace TestSubscriber2
{
	class SomethingHappenedSecondConsumer : IConsumer<SomethingHappened>
	{
		public Task Consume(ConsumeContext<SomethingHappened> message)
		{
			Console.Write("Second Consumer Message #: " + message.Message.What);
			Console.Write("  SENT: " + message.Message.When);
			Console.Write("  PROCESSED: " + DateTime.Now);
			Console.WriteLine(" (" + System.Threading.Thread.CurrentThread.ManagedThreadId + ")");
			return Task.FromResult(0);
		}
	}
}
