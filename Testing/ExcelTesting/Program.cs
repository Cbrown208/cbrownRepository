namespace ExcelTesting
{
	class Program
	{
		private static readonly ExcelTestingManager _manager = new ExcelTestingManager();
		static void Main(string[] args)
		{
			_manager.RunExcelTests();
		}
	}
}
