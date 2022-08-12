using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XMLExamples
{
	public class Program
	{
		private const string StartAccountNumberTag = "<AccountNumber>";
		private const string EndAccountNumberTag = "</AccountNumber>";
		private static readonly XmlManager _manager = new XmlManager();
		static readonly XmlTestMessage xmlTestMessages = new XmlTestMessage();

		public static void Main(string[] args)
		{
			//Console.WriteLine("Example to get Value Between a tag");
			//Console.WriteLine("Expected Result: V00046246005");
			//var testMessage = xmlTestMessages.GetXmlMessage();
			//var accountNumber = manager.GetXmlValue(testMessage, StartAccountNumberTag, EndAccountNumberTag);
			//Console.WriteLine("Function Returned: " + accountNumber);

			////XML to Object Example
			//var deserializeTestMessage = xmlTestMessages.GetXmlSerializeTestMessage();
			//using (StringReader sr = new StringReader(deserializeTestMessage))
			//{
			//	var xmlSerializer = new XmlSerializer(typeof(Employee));
			//	var temp = xmlSerializer.Deserialize(sr) as Employee;
			//}

			_manager.ReadXmlDynamically();


			EmployeeTypes typesTest = new EmployeeTypes();
			var testEmp = new Employee(){FirstName = "Jim",LastName = "Bob"};
			typesTest.ArrayTest = new Employee[3];
			typesTest.ListStringTest = new List<string>();
			typesTest.ListStringTest.Add("Testing");

			typesTest.ListObjectTest = new List<Employee>();
			typesTest.ListObjectTest.Add(testEmp);
			var results = _manager.SerializeToString(typesTest);

			typesTest.ArrayTest = new Employee[0];
			typesTest.ListStringTest = null;
			typesTest.ListObjectTest = new List<Employee>();

			var results2 = _manager.SerializeToString(typesTest);
			Console.ReadLine();
		}
	}
}