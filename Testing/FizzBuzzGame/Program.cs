using System;

namespace FizzBuzzGame
{
	class Program
	{
		static void Main(string[] args)
		{
			BetterAnswer();
			Console.ReadLine();
		}

		private static void OrigionalAnswer()
		{
			for (int i = 0; i < 101; i++)
			{
				if (i % 15 == 0)
				{
					Console.WriteLine("FizzBuzz");
				}
				else if (i % 5 == 0)
				{
					Console.WriteLine("Buzz");
				}
				else if (i % 3 == 0)
				{
					Console.WriteLine("Fizz");
				}
				else
				{
					Console.WriteLine(i);
				}
			}
			Console.ReadLine();
		}

		private static void BetterAnswer()
		{
			for (int i = 1; i < 101; i++)
			{
				var output = "";
				if (i % 3 == 0)
				{
					output += "Fizz";
				}

				if (i % 5 == 0)
				{
					output += "Buzz";
				}

				if (output != "")
				{
					Console.WriteLine(output);
				}
				else
				{
					Console.WriteLine(i);
				}
			}
		}
	}
}
