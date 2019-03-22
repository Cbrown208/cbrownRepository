using System;

namespace Tools.TimeCardAutomation
{
	class Program
	{
		private static readonly TimeCardManager Manager = new TimeCardManager();
		static void Main()
		{
			try
			{
				Manager.FillOutTimeCard();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.ReadLine();
			}
		}
	}
}
