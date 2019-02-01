namespace DataAccessTesting
{
	class Program
	{
		private static readonly BulkCopyManager Manager = new BulkCopyManager();
		static void Main()
		{
			var result = Manager.StartCopy();
			if (result)
			{
				//Console.ReadLine();
				System.Threading.Thread.Sleep(10000);
			}
		}
	}
}
