using System;
using System.ComponentModel;
using MassTransit;

namespace RabbitMQ_MassTransit3
{
	public class BusDetails
	{
		private  BusSettings BusSettings { get; set; }


		public IBusControl CreateBus(BusSettings busSettings)
		{
			BusSettings = LocalBusSettings.GetLocalBusSettings();

			var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
			{
				var host = sbc.Host(new Uri(BusSettings.IncomingUriString), h =>
				{
					h.Username(BusSettings.Username);
					h.Password(BusSettings.Password);
					h.Heartbeat(BusSettings.HeartBeatInSeconds);
				});
			});
			return bus;
		}
	}
}