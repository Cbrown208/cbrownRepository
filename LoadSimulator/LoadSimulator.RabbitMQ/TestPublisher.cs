using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using PAS.WorkList.Contract.Implementation;

namespace LoadSimulator.RabbitMQ
{
	public class TestPublisher
	{
		private readonly IBusControl _bus;

		public TestPublisher(IBusControl bus)
		{
			_bus = bus;
		}

		public async Task Publish(WorklistSyncOne command)
		{
			await _bus.Publish(command);
		}

		public async Task Publish(WorklistSyncAll command)
		{
			await _bus.Publish(command);
		}

		public void Publish(List<WorklistSyncOne> commands)
		{
			commands.ForEach(o => o.Queue());
			Parallel.ForEach(commands, async c =>
			{
				await Publish(c);
			});
		}
	}
}