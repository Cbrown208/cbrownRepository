using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

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
	}
}
