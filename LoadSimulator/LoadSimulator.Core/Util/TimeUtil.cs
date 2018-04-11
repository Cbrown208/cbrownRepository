using System;

namespace LoadSimulator.Core.Util
{
	public static class TimeUtil
	{
		public static double SecondsTill(DateTime time)
		{
			var runTime = (DateTime.Now - time).TotalSeconds;
			return runTime;
		}
	}
}