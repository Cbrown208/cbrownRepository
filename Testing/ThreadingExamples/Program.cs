using System;

namespace ThreadingExamples
{
	class Program
	{
		private static readonly ThreadingManager ThreadManager = new ThreadingManager();
		private static readonly SemaphoreManager SemaphoreManager = new SemaphoreManager();
		static void Main(string[] args)
		{
			ThreadManager.RunThreadingTests();
			SemaphoreManager.RunSemaphoreDemo();

			Console.Read();
		}
	}
}
