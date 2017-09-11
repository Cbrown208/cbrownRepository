using System;
using MassTransit;

namespace QueueTools
{
	public class BusDetails
	{
		//private  BusSettings BusSettings { get; set; }


		public IBusControl CreateBus(BusSettings busSettings)
		{
			var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
			{
				var host = sbc.Host(new Uri(busSettings.IncomingUriString), h =>
				{
					h.Username(busSettings.Username);
					h.Password(busSettings.Password);
					h.Heartbeat(busSettings.HeartBeatInSeconds);
				});
				sbc.Durable = true;
				sbc.AutoDelete = false;
			});
			return bus;
		}
	}
}