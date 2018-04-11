using System;
using MassTransit;

namespace LoadSimulator.RabbitMQ
{
	public class BusDetails
	{
		public IBusControl CreateBus(BusSettings busSettings)
		{
			Console.WriteLine(busSettings);
			var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
			{
				var host = sbc.Host(new Uri(busSettings.IncomingUri), h =>
				{
					h.Username(busSettings.Username);
					h.Password(busSettings.Password);
					h.Heartbeat(busSettings.HeartBeatInSeconds);
				});
				if (!string.IsNullOrWhiteSpace(busSettings.OutgoingUri))
				{
					var outgoingHost = sbc.Host(new Uri(busSettings.OutgoingUri), h =>
					{
						h.Username(busSettings.Username);
						h.Password(busSettings.Password);
						h.Heartbeat(busSettings.HeartBeatInSeconds);
					});
				}
			});
			return bus;
		}
	}
}
