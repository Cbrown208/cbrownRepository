using System;
using System.Collections.ObjectModel;

namespace Tools.Common.Formatters
{
	public class DateFormatter
	{
		public ReadOnlyCollection<TimeZoneInfo> GetAllTimeZoneOptions()
		{
			var timeZoneList = TimeZoneInfo.GetSystemTimeZones();
			foreach (var zoneInfo in timeZoneList)
			{
				Console.WriteLine(zoneInfo);
			}
			return timeZoneList;
		}

		public DateTimeOffset GetCentralTime()
		{
			DateTimeOffset newTime = TimeZoneInfo.ConvertTime(
				DateTimeOffset.UtcNow,
				TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")
			);

			var temp = TimeZoneInfo.GetSystemTimeZones();
			Console.WriteLine(newTime);
			Console.ReadLine();
			return newTime;
		}
	}
}
