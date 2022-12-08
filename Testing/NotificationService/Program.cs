namespace NotificationService
{
	internal class Program
	{
		private static readonly NotificationManager Manager = new NotificationManager();
		static void Main(string[] args)
		{
			Manager.RunNotificationService();
			//Console.ReadLine();
		}
	}
}
