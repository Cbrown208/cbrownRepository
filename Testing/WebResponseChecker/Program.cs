namespace WebResponseChecker
{
	internal class Program
	{
		private static readonly WebResponseManager Manager = new WebResponseManager();
		static void Main(string[] args)
		{
			Manager.CheckConnections();
		}
	}
}
