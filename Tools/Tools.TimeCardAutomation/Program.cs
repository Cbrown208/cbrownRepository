namespace Tools.TimeCardAutomation
{
	class Program
	{
		private static readonly TimeCardManager Manager = new TimeCardManager();
		static void Main(string[] args)
		{
			Manager.FillOutTimeCard();
		}
	}
}
