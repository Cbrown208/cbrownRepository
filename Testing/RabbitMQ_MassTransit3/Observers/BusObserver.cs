using System;
using System.Threading.Tasks;
using MassTransit;

namespace RabbitMQ_MassTransit3.Observers
{
	public class BusObserver :
			IBusObserver
	{
		public async Task PostCreate(IBus bus)
		{
			await Console.Out.WriteLineAsync("Bus PostCreate");
			// called after the bus has been created, but before it has been started.
		}

		public async Task CreateFaulted(Exception exception)
		{
			await Console.Out.WriteLineAsync("Bus CreateFaulted");
			// called if the bus creation fails for some reason
		}

		public async Task PreStart(IBus bus)
		{
			await Console.Out.WriteLineAsync("Bus PreStart");
			// called just before the bus is started
		}

		public async Task PostStart(IBus bus, Task busReady)
		{
			await Console.Out.WriteLineAsync("Bus PostStart");
			// called once the bus has been started successfully. The task can be used to wait for
			// all of the receive endpoints to be ready.
		}

		public async Task StartFaulted(IBus bus, Exception exception)
		{
			await Console.Out.WriteLineAsync("Bus StartFaulted");
			// called if the bus fails to start for some reason (dead battery, no fuel, etc.)
		}

		public async Task PreStop(IBus bus)
		{
			await Console.Out.WriteLineAsync("Bus PreStop");
			// called just before the bus is stopped
		}

		public async Task PostStop(IBus bus)
		{
			await Console.Out.WriteLineAsync("Bus PostStop");
			// called after the bus has been stopped
		}

		public async Task StopFaulted(IBus bus, Exception exception)
		{
			await Console.Out.WriteLineAsync("Bus StopFaulted");
			// called if the bus fails to stop (no brakes)
		}
	}
}
