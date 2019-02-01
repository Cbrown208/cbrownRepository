using System;
using System.Runtime.CompilerServices;

namespace FileExamples
{
	class Program
	{
		static void Main(string[] args)
		{
			var manager = new FileExampleManager();
			//manager.RunFileExamples();

			var result = manager.TestingReturn();
			Console.WriteLine(result);
			Console.ReadLine();
		}
	}
}
