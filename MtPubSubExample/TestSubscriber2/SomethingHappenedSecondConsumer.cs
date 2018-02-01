using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			Console.Write("  SENT: " + message.Message.When.ToString());
			Console.Write("  PROCESSED: " + DateTime.Now.ToString());
			Console.WriteLine(" (" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + ")");
			return Task.FromResult(0);
		}
	}
}
