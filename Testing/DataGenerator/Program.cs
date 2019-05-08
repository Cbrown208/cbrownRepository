namespace DataGenerator
{
	class Program
	{
		private static readonly DataGeneratorManager Manager = new DataGeneratorManager();
		static void Main(string[] args)
		{
			Manager.RunBogusTests();
		}
	}
}
