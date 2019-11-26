using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Formatters;


namespace ProveIt
{
	class Program
	{
		static void Main(string[] args)
		{
			//RunStringAlterTest();
			//QuestionMarkComparisonOperatorTest();
			//RunTest();
			FormatRunAsUser();
			Console.ReadLine();
		}

		private static void RunStringAlterTest()
		{
			var input = "\"{\"Status\":0,\"ErrorCode\":13,\"ErrorMessage\":\"Error course does not exist\",\"Completions\":null,\"NotFound\":[7336,7626,7813]}\"";

			input = input.Replace(@"\", "");
			input = input.Replace(@"""{", "{");
			input = input.Replace(@"}""", "}");
			input = input.Replace(@"""{", "{");
			var result = input;
			Console.WriteLine(result);
		}

		private static void QuestionMarkComparisonOperatorTest()
		{
			Console.WriteLine("Comparison Tests Start ------------------");
			var input = true;
			Console.WriteLine("input = "+ input);
			var result = (input) ? "True Response": "False Response";
			Console.WriteLine(result);

			input = false;
			Console.WriteLine("input = " + input);
			result = (input) ? "True Response" : "False Response";
			Console.WriteLine(result);

			Console.WriteLine("Comparison Tests End ------------------");
		}

		private static void RunTest()
		{
			var personList = new List<Person>();

			var details = new List<Person>().AsEnumerable();

			var temp = personList.Select(x => x.Id);
			Console.WriteLine(temp.FirstOrDefault());


			personList.Add(new Person { FirstName = "joe", LastName = "bob" });
			personList.Add(new Person { FirstName = "joe", LastName = "bob" });
			var hospitalProcedure = personList.FindAll(obj => obj.FirstName == "Hosp").ToList();
			for (var counter = 0; counter < hospitalProcedure.Count; counter++)
			{
				var procedure = hospitalProcedure[counter];
				procedure = details.ToList().FindAll(obj => !string.IsNullOrEmpty(obj.LastName.Trim('0')))[counter];
			}
			Console.WriteLine("MAde It");

			var specProcedure = personList.FindAll(obj => obj.FirstName == "Hosp").ToList();
			for (var counter = 0; counter < specProcedure.Count; counter++)
			{
				var procedure = specProcedure[counter];
				procedure = details.ToList().FindAll(obj => string.IsNullOrEmpty(obj.LastName.Trim('0')))[counter];
			}
			Console.WriteLine("MAde It 2");

			Console.ReadLine();
		}

		private static void FormatRunAsUser()
		{
			var svcStatus = @"NT SERVICE\DIAHostService";
			if (svcStatus.Contains(@"\"))
			{
				var user = svcStatus.Split('\\');
				user[0] = user[0].ToLower();
				svcStatus = String.Join("\\", user.ToArray());
			}
			Console.WriteLine(svcStatus);
		}
	}
}
