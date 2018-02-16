using System;
using System.Collections.Generic;

namespace IListExample
{
    class Program
    {
	    private static readonly ExampleData ExData = new ExampleData();
		static void Main()
        {
			var functions = new ListFunctions();

			var starkList1 = ExData.GetCustomerData();
	        var starkList2 = ExData.GetCustomerData2();

	        var list1Vslist2 = functions.GetValuesInFirstListNotInSecondList(starkList1, starkList2);
			var list1Andlist2 = functions.GetSimilaritiesBetween(starkList1, starkList2);

			Console.WriteLine("Values In List 1 that are NOT in List 2");
	        DisplayList(list1Vslist2);

			Console.WriteLine(Environment.NewLine+ "Values In List 1 AND List 2");
			DisplayList(list1Andlist2);

			Console.ReadLine();
        }

        static void DisplayList(List<Customer> list)
        {
            foreach (var value in list)
            {
	            //var outputString = string.Format("AccountNumber: {0} FirstName: {1}, LastName: {2}", value.AccountNumber, value.FirstName, value.LastName);
				var outputString =
		            $"AccountNumber: {value.AccountNumber} FirstName: {value.FirstName}, LastName: {value.LastName}";

				Console.WriteLine(outputString);
            }
        }
    }
}

