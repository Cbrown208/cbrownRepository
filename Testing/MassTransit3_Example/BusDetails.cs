using System;
using MassTransit;
using MassTransit.Pipeline;

namespace MassTransit3_Example
{
	public class BusDetails
	{
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
				sbc.EnablePriority(1);
			});
			
			
			return bus;

		}
	}
}
