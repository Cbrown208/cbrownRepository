using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
	class Program
	{
		static void Main(string[] args)
		{
			//var input = Console.ReadLine();

			//var input2 = Console.ReadLine();
			//var input3 = Console.ReadLine();
			BubbleSort();

			if (string.Compare("cash", "Cash", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				Console.WriteLine("True");
			}
			else
			{
				Console.WriteLine("False");
			}

			//string[] elements = { input, input2, input3 };
			string[] arraytest = new string[5];



			int numPlayers = 3;
			for (int i = 0; i < numPlayers; i++)
			{
				Console.WriteLine("Enter the name of the player...Enter q to exit...");
				string playerName = Console.ReadLine();
				arraytest[i] = playerName;
			}


			foreach (string element in arraytest)
			{
				Console.WriteLine(element);
			}

			Console.ReadLine();
		}

		private static void BubbleSort()
		{
			int[] arr = { 800, 11, 50, 771, 649, 770, 240, 9 };

			for (int i = 0; i < arr.Length; i++)
				Console.Write(arr[i] + " ");
			Console.Write(Environment.NewLine);
			int temp = 0;

			for (int write = 0; write < arr.Length; write++)
			{
				for (int sort = 0; sort < arr.Length - 1; sort++)
				{
					if (arr[sort] > arr[sort + 1])
					{
						temp = arr[sort + 1];
						arr[sort + 1] = arr[sort];
						arr[sort] = temp;
					}
				}
			}

			for (int i = 0; i < arr.Length; i++)
				Console.Write(arr[i] + " ");

			Console.Write(Environment.NewLine);
			Console.ReadKey();
		}
	}
}
