using System;

namespace UnitTestsExample
{
	public class TestingManager 
	{
		public int AddingNumbers(int a, int b)
		{
			return a + b;
		}

		public bool ThrowExceptionTest(bool shouldThrow)
		{
			if (shouldThrow)
			{
				throw new ApplicationException("ErrorMessage");
			}
			return true;
		}
	}
}
