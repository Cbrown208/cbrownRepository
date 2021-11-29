namespace FirewallConnectionChecker
{
	class Program
	{
		private static readonly ConnectionCheckerManager ConnectionCheckerManager = new ConnectionCheckerManager();
		static void Main(string[] args)
		{
			ConnectionCheckerManager.CheckConnections();
		}
	}
}
