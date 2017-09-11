using System;
using System.Threading.Tasks;
using MassTransit;

namespace RabbitMQ_MassTransit3.Observers
{
	public class SendObserver : ISendObserver
	{
		public async Task PreSend<T>(SendContext<T> context)
			where T : class
		{
			await Console.Out.WriteLineAsync("Send Observer PreSend");
			// called just before a message is sent, all the headers should be setup and everything
		}

		public async Task PostSend<T>(SendContext<T> context)
			where T : class
		{
			await Console.Out.WriteLineAsync("Send Observer PostSend");
			// called just after a message it sent to the transport and acknowledged (RabbitMQ)
		}

		public async Task SendFault<T>(SendContext<T> context, Exception exception)
			where T : class
		{
			await Console.Out.WriteLineAsync("Send Observer SendFault");
			// called if an exception occurred sending the message
		}
	}
}
