using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingExamples
{
	public class ThreadingManager
	{
		public void ParallelComparison()
		{
			string[] colors = {
				"1. Red",
				"2. Green",
				"3. Blue",
				"4. Yellow",
				"5. White",
				"6. Black",
				"7. Violet",
				"8. Brown",
				"9. Orange",
				"10. Pink"
			};
			Console.WriteLine("Traditional foreach loop\n");
			//start the stopwatch for "for" loop
			var sw = Stopwatch.StartNew();
			foreach (string color in colors)
			{
				Console.WriteLine("{0}, Thread Id= {1}", color, Thread.CurrentThread.ManagedThreadId);
				Thread.Sleep(10);
			}
			Console.WriteLine("foreach loop execution time = {0} seconds\n", sw.Elapsed.TotalSeconds);
			Console.WriteLine("Using Parallel.ForEach");
			//start the stopwatch for "Parallel.ForEach"
			sw = Stopwatch.StartNew();
			Parallel.ForEach(colors, color =>
				{
					Console.WriteLine("{0}, Thread Id= {1}", color, Thread.CurrentThread.ManagedThreadId);
					Thread.Sleep(10);
				}
			);
			Console.WriteLine("Parallel.ForEach() execution time = {0} seconds", sw.Elapsed.TotalSeconds);
		}

		public async Task ParallelThreadTest()
		{
			var myCollection = GetCollectionData();
			var bag = new ConcurrentBag<object>();
			var tasks = myCollection.Select(async item =>
			{
				// some pre stuff
				var response = await DoVariousThingsFromTheUiThreadAsync(item);
				bag.Add(response);
				// some post stuff
			});
			await Task.WhenAll(tasks);
			var count = bag.Count;
			Console.WriteLine("Task Count: " + count);
		}

		public List<int> GetCollectionData()
		{
			return new List<int>{1,2};
		}

		private async Task<int> DoVariousThingsFromTheUiThreadAsync(int data)
		{
			// I have a bunch of async work to do, and I am executed on the UI thread.
			var result = await Task.Run(() => DoWork(data));
			return result;
		}

		private int DoWork(int data)
		{
			Console.WriteLine("Running for Task:" + data);
			Thread.Sleep(5000);
			return data + 10;
		}
	}
}
