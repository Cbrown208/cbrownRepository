using System;
using System.Configuration;
using System.Text;
using LoadSimulator.Core.Util;

namespace LoadSimulator.RabbitMQ
{
	//For LoadSimulator
	public class BusSettings
	{
		public string IncomingUri { get; set; }
		public string IncomingQueue { get; set; }
		public string OutgoingUri { get; set; }
		public string OutgoingQueue { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public ushort HeartBeatInSeconds { get; set; }
		public ushort ConcurrentConsumerLimit { get; set; }
		public ushort PrefetchCount { get; set; }

		public static BusSettings FromAppConfig()
		{
			var settings = new BusSettings
			{
				IncomingUri = Get("BusSettings.IncomingUri"),
				IncomingQueue = Get("BusSettings.IncomingQueue"),
				OutgoingUri = Get("BusSettings.OutgoingUri"),
				OutgoingQueue = Get("BusSettings.OutgoingQueue"),
				Username = Get("BusSettings.Username"),
				Password = Get("BusSettings.Password"),
				HeartBeatInSeconds = Convert.ToUInt16(Get("BusSettings.HeartBeatInSeconds")),
				ConcurrentConsumerLimit = Convert.ToUInt16(Get("BusSettings.ConcurrentConsumerLimit")),
				PrefetchCount = Convert.ToUInt16(Get("BusSettings.PrefetchCount"))
			};
			return settings;
		}

		public static BusSettings From(BusSettings worklistBusSettings)
		{
			var settings = new BusSettings();
			settings.IncomingUri = worklistBusSettings.IncomingUri;
			settings.IncomingQueue = worklistBusSettings.IncomingQueue;
			settings.Username = worklistBusSettings.Username;
			settings.Password = worklistBusSettings.Password;
			settings.HeartBeatInSeconds = Convert.ToUInt16(worklistBusSettings.HeartBeatInSeconds);
			settings.ConcurrentConsumerLimit = Convert.ToUInt16(worklistBusSettings.ConcurrentConsumerLimit);
			settings.PrefetchCount = Convert.ToUInt16(worklistBusSettings.PrefetchCount);
			return settings;
		}

		public bool Validate()
		{
			if (IsEmpty(IncomingUri) || IsEmpty(IncomingQueue))
				throw new ApplicationException($"Invalid Bus Settings - [IncommingUri:{IncomingUri}, IncommingQueue:{IncomingQueue}]");
			return true;
		}

		private bool IsEmpty(string s)
		{
			return string.IsNullOrWhiteSpace(s);
		}

		private static string Get(string key)
		{
			var value = ConfigurationManager.AppSettings[key];
			return Convert.ToString(value);
		}

		public override string ToString()
		{
			var newLine = StringUtil.NewLine;
			Func<string> Seperator = () => StringUtil.Seperator('-', 60);
			Func<string, object, string> Field = (n, v) => StringUtil.Field(n, v);

			var builder = new StringBuilder();
			builder.Append(Seperator());
			builder.Append($"BUS Settings{newLine}");
			builder.Append(Seperator());
			builder.Append(Field("IncomingUri", IncomingUri));
			builder.Append(Field("IncommingQueue", IncomingQueue));
			builder.Append(Field("OutgoingUri", OutgoingUri));
			builder.Append(Field("OutgoingQueue", OutgoingQueue));
			builder.Append(Field("Username", Username));
			builder.Append(Field("Password", Password));
			builder.Append(Field("HeartBeatInSeconds", HeartBeatInSeconds));
			builder.Append(Field("ConcurrentConsumerLimit", ConcurrentConsumerLimit));
			builder.Append(Field("PrefetchCount", PrefetchCount));
			builder.Append(Seperator());
			return builder.ToString();
		}
	}
}