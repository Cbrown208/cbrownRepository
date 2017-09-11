using System;

namespace XMLExamples
{
	public class Program
	{
		private const string StartAccountNumberTag = "<AccountNumber>";
		private const string EndAccountNumberTag = "</AccountNumber>";
		static readonly XmlManager manager = new XmlManager();
		static readonly XmlTestMessage xmlTestMessages = new XmlTestMessage();

		public static void Main(string[] args)
		{
			Console.WriteLine("Example to get Value Between a tag");
			Console.WriteLine("Expected Result: V00046246005");
			var testMessage = xmlTestMessages.GetXmlMessage();
			var accountNumber = manager.GetXmlValue(testMessage, StartAccountNumberTag, EndAccountNumberTag);
			Console.WriteLine("Function Returned: "+accountNumber);
			Console.ReadLine();

		}
	}
}