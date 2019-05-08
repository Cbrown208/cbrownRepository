using System;

namespace ThreadingExamples
{
	class Program
	{
		private static readonly ThreadingManager ThreadManager = new ThreadingManager();
		static void Main(string[] args)
		{
			ThreadManager.ParallelComparison();
			ThreadManager.ParallelThreadTest();

			Console.Read();
		}
	}
}
