using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Context;
using MedAssets.AMS.Common;

namespace UnitTests
{
	public class ConsumerTests
	{
		//[TestFixture]
		//public class AdtMasterRouterConsumerTests
		//{
		//	private ConsumeContext<IAdtQueueMessage> _context;
		//	private ADTMasterRouterBusSettings _busSettings;
		//	private AdtMasterRouterConsumer _consumer;
		//	private IMessageManager _messageManager;
		//	private UnitTestBusSettings _UnitTestBus = new UnitTestBusSettings();
		//	private ConsumerTestMessages _testMsg = new ConsumerTestMessages();
		//	private QueueManager _manager;
		//	private MessageQueueMappings _mapping;
		//	private NLog.Targets.MemoryTarget _target;

		//	private void Setup()
		//	{
		//		_target = new MemoryTarget();
		//		_target.Layout = "${message}";
		//		NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(_target, NLog.LogLevel.Debug);

		//		// Setup Consumer Context / Consumer
		//		_mapping = MessageQueueMappings.Instance;
		//		SetupMappings();
		//		_manager = new QueueManager(_busSettings);
		//		_context = Substitute.For<ConsumeContext<IAdtQueueMessage>>();
		//		_context = SetContext();
		//		_messageManager = Substitute.For<IMessageManager>();
		//		_busSettings = _UnitTestBus.GetUnitTestBusSettings();
		//		_consumer = new AdtMasterRouterConsumer(_messageManager, _busSettings, _manager);
		//	}

		//	private void SetupMappings()
		//	{
		//		List<QueueInfo> list = new List<QueueInfo>();
		//		QueueInfo qinfo = new QueueInfo();
		//		list.ForEach(x => MessageQueueMappings.Instance.AddMappings(x.Name));

		//		for (int i = 0; i < 4; i++)
		//		{
		//			qinfo.Name = "PAS_ADT_WORKER_" + i;
		//			list.Add(qinfo);
		//		}
		//		list.ForEach(x => _mapping.AddMappings(x.Name));
		//	}

		//	private ConsumeContext<IAdtQueueMessage> SetContext()
		//	{
		//		var AdtMessage = _testMsg.GetAdtQueueMessage();
		//		_context.Message.AccountNumber = AdtMessage.AccountNumber;
		//		_context.Message.Message = AdtMessage.Message;
		//		_context.Message.ClientId = AdtMessage.ClientId;
		//		_context.Message.FacilityId = AdtMessage.FacilityId;
		//		_context.Message.MessageControlId = AdtMessage.MessageControlId;

		//		return _context;
		//	}


		//	// Should Throw Exception Assert
		//	//Action action = () => _consumer.Consume(_context).Wait();
		//	//action.Throws<ArgumentException>();

		//	[Test]
		//	public void Given_Valid_Message_Consumer_Should_Show_1_Message_Sent()
		//	{
		//		Setup();
		//		_consumer.Consume(_context).Wait();
		//		_context.Received(1);
		//		_messageManager.Received(1);
		//	}


		//	//[Test]
		//	//public void Given_Valid_Message_Consumed_Should_Send_Message_To_Queue()
		//	//{
		//	//	Setup();
		//	//	var testingMessage = _testMsg.GetAdtQueueMessage();
		//	//	ConsumeContext <IAdtQueueMessage> msgContext = new MessageConsumeContext<IAdtQueueMessage>(_context, testingMessage);
		//	//	_consumer.Consume(msgContext).Wait();

		//	//	_consumer.Received(1);
		//	//	_messageManager.Received(1);
		//	//}

		//	[Test]
		//	public void Given_Invalid_Message_Consumer_Should_Show_0_Messages_Sent_And_Nothing_Inserted_Into_DB()
		//	{
		//		Setup();
		//		_context.Message.AccountNumber = null;
		//		_consumer.Consume(_context).Wait();
		//		_context.Received(1);
		//		_messageManager.Received(0);
		//	}

		//	[Test]
		//	public void Given_Message_Recived_should_Return_Command_Endpoint()
		//	{
		//		Setup();
		//		var result = _consumer.GetCommandEndpoint();
		//		result.Should().Be("rabbitmq://rcm41vqpasapp03/PAS_UnitTest_CMD_READY_IV");
		//	}

		//	[Test]
		//	public void Given_FacID_And_ClientID_should_Return_Allocated_Queue_Url()
		//	{
		//		Setup();
		//		FacilityAccountPair acctPair = new FacilityAccountPair()
		//		{
		//			AccountNumber = "T12345",
		//			Facility = "32430F0F-82AA-4EDB-BD37-6886D1956CAE"
		//		};
		//		var result = _consumer.DistributeMessages("PAS_ADT_WORKER_2", acctPair);

		//		result.Should().Be("rabbitmq://rcm41vqpasapp03/PAS_ADT_WORKER_2");
		//	}


		//	//// Should Throw Exception Assert
		//	//Action action = () =>
		//	//{
		//	//	_consumer.Consume(_context).Wait();
		//	//};
		//	//action.Throws<ArgumentException>();
		//	//	_target.Logs.FirstOrDefault().Should().Contain("Account");
		//	//target.Logs.First().Should().Be("error logged");

		//	//assertions
		//	//initially stores the state of the message
		//	//messageManager.Received(1).Insert

		//	//send message to temporary queue
		//	//context.Received(1).Send

		//	//send message to workers
		//	//context.Received(1).Send();

		//	//store the updated state of the message
		//	//messageManaer.Received(1).Update
		//}
	}
}
