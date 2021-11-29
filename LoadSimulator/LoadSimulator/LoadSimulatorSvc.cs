using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoadSimulator.DataAccess;
using LoadSimulator.RabbitMQ;
using MassTransit;
using PAS.Worklist.Contract.Models;
using PAS.WorkList.Contract.Implementation;

namespace LoadSimulator
{
	public class LoadSimulatorSvc
	{
		//private readonly CommandCenterRepository _pccRepository;
		private readonly AmsRepository _amsRepository;
		private readonly IBusControl _busControl;
		private readonly BusSettings _busSettings;
		private readonly TestPublisher _publisher;

		public LoadSimulatorSvc()
		{
			//_pccRepository = IoC.Get<CommandCenterRepository>();
			_amsRepository = IoC.Get<AmsRepository>();
			_busSettings = BusSettings.FromAppConfig();
			_busControl = IoC.Get<IBusControl>();
			_publisher = new TestPublisher(_busControl);
		}

		public bool Run()
		{
			try
			{
				//var temp = _pccRepository.GetById(2);
				//Console.WriteLine(temp);
				var messageCount = 1000;

				var syncOneAccountList = _amsRepository.GetAccountFacilityList(messageCount);
				_busControl.StartAsync();
				var testMsg = new WorklistSyncOne(3503, new Account { AccountNumber = "1234", FacilityId = new Guid() });


				var sendResult = SendListtoEndpoint(syncOneAccountList).Result;
				//var publishResult = PublishToEndpoint(testMsg).Result;
				//var publishListResult = PublishToEndpoint(syncOneAccountList).Result;

				_busControl.StopAsync();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				_busControl.StopAsync();
				return false;
			}
		}

		private async Task<bool> SendListtoEndpoint(List<WorklistSyncOne> messageList)
		{
			var endpoint = new Uri(_busSettings.IncomingUri + "/" + _busSettings.IncomingQueue);
			foreach (var msg in messageList)
			{
				await _busControl.GetSendEndpoint(endpoint).Result.Send(msg);
				Console.WriteLine($"PUBLISHED {msg.ClientId} - {msg.Account}");
			}
			return true;
		}

		private async Task<bool> PublishToEndpoint(WorklistSyncOne msg)
		{
			await _publisher.Publish(msg).ContinueWith((t) =>
			{
				Console.WriteLine($"PUBLISHED {msg.ClientId} - {msg.Account}");
			});
			return true;
		}

		private async Task<bool> PublishToEndpoint(List<WorklistSyncOne> messageList)
		{
			foreach (var command in messageList)
			{
				await _publisher.Publish(command).ContinueWith((t) =>
				{
				});
			}
			return true;
		}

	}
}
