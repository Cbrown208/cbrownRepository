using System;
using System.Threading.Tasks;

namespace RetryExample
{
	class Program
	{
		private static readonly PollyRetryManager PollyRetryManager = new PollyRetryManager();
		private static readonly AsyncPollyRetryManager AsyncPollyRetryManager = new AsyncPollyRetryManager();
		static void Main(string[] args)
		{
			var asyncResult = AsyncRunTests().Result;
			var result = RunTests();
			Console.WriteLine("Result = "+ result);
			Console.WriteLine("Async Result = " + asyncResult);
			Console.ReadLine();
		}

		private static async Task<int> AsyncRunTests()
		{
			var asyncResults = await AsyncPollyRetryManager.AsyncPollyRetryExample();
			return asyncResults;
		}

		private static int RunTests()
		{
			var result = PollyRetryManager.PollyRetryExample();
			return result;
		}
	}
}
