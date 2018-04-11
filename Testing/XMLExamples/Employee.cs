using System;
using System.Collections.Generic;

namespace XMLExamples
{
	public class Employee
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	public class EmployeeTypes
	{
		public string TestString { get; set; }
		public Employee[] ArrayTest { get; set; }
		public List<string> ListStringTest { get; set; }
		public List<Employee> ListObjectTest { get; set; }
	}
}