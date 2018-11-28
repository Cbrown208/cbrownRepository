using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace UnitTestsExample
{
	[TestFixture]
	public class ManagerTests
	{
		private TestingManager _manager;
		[SetUp]
		public void SetUp()
		{
			_manager = Substitute.For<TestingManager>();
		}

		[Test]
		public void ValidTest()
		{
			var result = _manager.AddingNumbers(2, 2);
			result.Should().Be(4);
			Assert.AreEqual(result, 4);
		}

		[Test]
		public void ThrowExceptionTest()
		{
			var ex = Assert.Throws<ApplicationException>(() => _manager.ThrowExceptionTest(true));
			Assert.That(ex.Message.Contains("ErrorMessage"));
		}

		//[TestMethod] - Older NUnit framework
		[TestCase(2, 2, 4)]
		[TestCase(3, 3, 6)]
		[TestCase(5, 5, 10)]
		public void TestMethodExample(int inputValue1, int inputValue2, int expectedResult)
		{
			var result = _manager.AddingNumbers(inputValue1, inputValue2);
			Assert.AreEqual(result, expectedResult);
		}
	}
}
