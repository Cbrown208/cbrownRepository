namespace WebDriverUiTesting
{
	class Program
	{
		private static readonly UiTestCases TestCases = new UiTestCases();
		static void Main(string[] args)
		{
			TestCases.RunTestCases();
		}
	}
}
