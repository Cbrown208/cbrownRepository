namespace DataAccessTesting
{
	class Program
	{
		private static readonly DataAccessManager Manager = new DataAccessManager();
		
		static void Main()
		{
			Manager.RunDataAccessTesting();
		}
	}
}
