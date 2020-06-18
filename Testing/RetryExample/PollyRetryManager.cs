using System;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;

namespace RetryExample
{
	public class PollyRetryManager
	{
		private int _runCounter;

		public int PollyRetryExample()
		{
			var maxRetryAttempts = 3;
			var pauseBetweenFailures = TimeSpan.FromSeconds(2);
			var response = 0;
			var retryPolicy = PauseBetweenFailures(maxRetryAttempts, pauseBetweenFailures);

			retryPolicy.Execute(() =>
			{
				response = FailureTesting().Result;
			});
			Console.WriteLine("Finished Retry Logic.");
			return response;
		}

		public RetryPolicy PauseBetweenFailures(int maxRetryAttempts, TimeSpan pauseBetweenFailures)
		{
			var retryPolicy = Policy
				.Handle<Exception>().WaitAndRetry(maxRetryAttempts, i => pauseBetweenFailures);
			return retryPolicy;
		}

		public AsyncRetryPolicy AsyncPauseBetweenFailures(int maxRetryAttempts, TimeSpan pauseBetweenFailures)
		{
			var retryPolicy = Policy
				.Handle<Exception>()
				.WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);
			return retryPolicy;
		}

		/// <summary>
		/// First Fail - 2 Seconds 
		/// Second Fail - 4 Seconds 
		/// Third Fail - 8 Seconds 
		/// </summary>
		/// <returns></returns>
		public AsyncRetryPolicy AsyncCalculateIncrementalDelayAtRuntime(int maxRetryAttempts)
		{
			var policy = Policy
				.Handle<Exception>()
				.WaitAndRetryAsync(3, retryAttempt =>
					TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
				);
			return policy;
		}

		public async Task<int> FailureTesting()
		{
			if (_runCounter != 3)
			{
				_runCounter++;
				var errorMsg = "Polly Retry Error #" + _runCounter;
				Console.WriteLine(errorMsg);
				throw new ApplicationException(errorMsg);
			}
			var result = await Task.Run(() => CalculateValue(5, 5));
			return result;
		}

		public int CalculateValue(int a, int b)
		{
			var result = a + b;
			return result;
		}
	}
}
