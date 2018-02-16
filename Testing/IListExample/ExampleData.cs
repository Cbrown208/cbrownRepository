using System.Collections.Generic;

namespace IListExample
{
	public class ExampleData
	{
		public List<Customer> GetCustomerData()
		{
			var customer1 = new Customer() { AccountNumber = "1", FirstName = "John", LastName = "Snow" };
			var customer2 = new Customer() { AccountNumber = "2", FirstName = "Ned", LastName = "Stark" };
			var customer3 = new Customer() { AccountNumber = "3", FirstName = "Aria", LastName = "Stark" };

			var list = new List<Customer> {customer1, customer2, customer3};
			return list;
		}

		public List<Customer> GetCustomerData2()
		{
			var customer1 = new Customer() { AccountNumber = "1", FirstName = "John", LastName = "Snow" };
			var customer3 = new Customer() { AccountNumber = "9", FirstName = "Aria", LastName = "Stark" };
			var customer4 = new Customer() { AccountNumber = "4", FirstName = "Aria", LastName = "Stark" };

			var list = new List<Customer> { customer1, customer3, customer4 };
			return list;
		}
	}
}
