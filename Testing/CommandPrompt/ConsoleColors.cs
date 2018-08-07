using System;

namespace CommandPrompt
{
	public class ConsoleColors
	{
		public void RunColorChangeEx()
		{
			// Get an array with the values of ConsoleColor enumeration members.
			ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
			// Save the current background and foreground colors.
			ConsoleColor currentBackground = Console.BackgroundColor;
			ConsoleColor currentForeground = Console.ForegroundColor;

			// Display all foreground colors except the one that matches the background.
			Console.WriteLine("All the foreground colors except {0}, the background color:", currentBackground);
			foreach (var color in colors)
			{
				if (color == currentBackground) continue;

				Console.ForegroundColor = color;
				Console.WriteLine("   The foreground color is {0}.", color);
			}
			Console.WriteLine();
			// Restore the foreground color.
			Console.ForegroundColor = currentForeground;

			// Display each background color except the one that matches the current foreground color.
			Console.WriteLine("All the background colors except {0}, the foreground color:", currentForeground);
			foreach (var color in colors)
			{
				if (color == currentForeground) continue;

				Console.BackgroundColor = color;
				Console.WriteLine("   The background color is {0}.", color);
			}

			// Restore the original console colors.
			Console.ResetColor();
			Console.WriteLine("\nOriginal colors restored...");
			Console.ReadLine();
		}
	}
}