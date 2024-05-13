using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Common.Formatters;
using Newtonsoft.Json;


namespace ProveIt
{
	class Program
	{
		private static readonly DotNetVersionManager _dotNetVersionManager = new DotNetVersionManager();

		static void Main(string[] args)
		{
			//RunDbConnectionParserTest();
			//RunURLParserTest();
			//_dotNetVersionManager.CheckDotNetVersion();
			//RunStringAlterTest();
			//EditList();
			//QuestionMarkComparisonOperatorTest();
			//RunTest();
			//FormatRunAsUser();
			TempTest();
			Console.ReadLine();
		}

		public static void TempTest()
		{

			var result = DateTime.Today.AddDays(-1); 
			Console.WriteLine("Results: " + result);
		}

		public static void RunURLParserTest()
		{
			Uri uri = new Uri("https://iv-rmq-pas.nthrive.com:15672/#/");
			var result = uri.Scheme + "://" + uri.Authority;
			Console.WriteLine("Result 1: " + result);

			var result2 = uri.GetLeftPart(System.UriPartial.Authority);

			Console.WriteLine("Result 2: " + result2);
		}
		public static void RunDbConnectionParserTest()
		{
			var connectionString = "server=nprod-cleariq-dev-scus-db1-sqlmi.61a4b50c50df.database.windows.net;Database=CIQ_main_UTSW_Automation;User Id =Cleariq;Password=Sqlmiserveradmin123";
			IDbConnection connection = new SqlConnection(connectionString);
			var dbName = connection.Database;
			var dbServer = new SqlConnection(connectionString).DataSource;
			
			Console.WriteLine("Result 1: " + dbName);
			Console.WriteLine("Result 2: " + dbServer);
		}

		private static void RunStringAlterTest()
		{
			var input =
				"\"{\"Status\":0,\"ErrorCode\":13,\"ErrorMessage\":\"Error course does not exist\",\"Completions\":null,\"NotFound\":[7336,7626,7813]}\"";

			input = input.Replace(@"\", "");
			input = input.Replace(@"""{", "{");
			input = input.Replace(@"}""", "}");
			input = input.Replace(@"""{", "{");
			var result = input;
			Console.WriteLine(result);
		}

		private static void QuestionMarkComparisonOperatorTest()
		{
			Console.WriteLine("------------------ Comparison Tests Start ------------------");
			var input = true;
			Console.WriteLine("input = " + input);
			var result = (input) ? "True Response" : "False Response";
			Console.WriteLine(result);

			input = false;
			Console.WriteLine("input = " + input);
			result = (input) ? "True Response" : "False Response";
			Console.WriteLine(result);

			var pTest = new Person();
			pTest.FirstName = null;
			Console.WriteLine("Person firstName = " + pTest.FirstName);
			result = pTest.FirstName != null ? "True Response" : "False Response";
			Console.WriteLine(result);

			pTest.FirstName = "Joe";
			Console.WriteLine("Person firstName = " + pTest.FirstName);
			result = pTest.FirstName != null ? "True Response" : "False Response";
			Console.WriteLine(result);

			Console.WriteLine("------------------ Comparison Tests End ------------------");


			var valueToCheck = 'F';
			var patientSex = valueToCheck == 'B' ? 'O' : valueToCheck;

			Console.WriteLine("Input Value: " + valueToCheck + " | Results: " + patientSex);

		}

		private static void RunTest()
		{
			var personList = new List<Person>();

			personList = null;

			var temp2 = personList.FirstOrDefault(x => x.FirstName == "nope");

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

		private static void EditList()
		{
			var personList = new List<Person> { new Person { Id = 1234, FirstName = "John", LastName = "Snow" } };
			Console.WriteLine("Original Value: " + personList.First().Id);
			// Option 1
			personList[0].Id = 5678;
			Console.WriteLine("Option 1: " + personList.First().Id);

			// Option 2
			var temp = personList.FirstOrDefault();
			temp.Id = 8999;
			var newPersonList = new List<Person> { temp };

			Console.WriteLine("Option 2: " + newPersonList.First().Id);
		}
	}
}
