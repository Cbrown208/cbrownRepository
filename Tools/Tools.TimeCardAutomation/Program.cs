using System;
using Polly;
using Polly.Retry;

namespace Tools.TimeCardAutomation
{
	class Program
	{
		private static readonly TimeCardManager Manager = new TimeCardManager();
		static void Main()
		{
			try
			{
				var maxRetryAttempts = 3;
				var pauseBetweenFailures = TimeSpan.FromSeconds(2);
				var retryPolicy = PauseBetweenFailures(maxRetryAttempts, pauseBetweenFailures);
				retryPolicy.Execute(() =>
				{
					Manager.FillOutTimeCard();
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.ReadLine();
			}
		}

		public static RetryPolicy PauseBetweenFailures(int maxRetryAttempts, TimeSpan pauseBetweenFailures)
		{
			var retryPolicy = Policy
				.Handle<Exception>().WaitAndRetry(maxRetryAttempts, i => pauseBetweenFailures);
			return retryPolicy;
		}
	}
}
