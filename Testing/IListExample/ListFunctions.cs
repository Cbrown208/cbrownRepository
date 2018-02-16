using System.Collections.Generic;
using System.Linq;

namespace IListExample
{
	public class ListFunctions
	{
		public List<Customer> GetValuesInFirstListNotInSecondList(List<Customer> list1, List<Customer> list2)
		{
			//Returns List<Value>
			//Find out what is in list 1 that is NOT in list 2
			//var list1ValueDifference = list1.Select(x => x.AccountNumber).Except(list2.Select(j => j.AccountNumber)).ToList();

			//Returns List<Customer>
			//Find out what is in list 1 that is ALSO in list 2
			var list1ObjectDifference = list1.Where(item => !list2.Select(item2 => item2.AccountNumber).Contains(item.AccountNumber)).ToList();

			return list1ObjectDifference;
		}

		public List<Customer> GetSimilaritiesBetween(List<Customer> list1, List<Customer> list2)
		{
			//Returns List<Customer>
			//Find out what is in list 1 that is ALSO in list 2
			var list1ObjectDifference = list1.Where(item => list2.Select(item2 => item2.AccountNumber).Contains(item.AccountNumber)).ToList();

			return list1ObjectDifference;
		}
	}
}
