using System;
using System.Linq;

namespace Common.Formaters
{
	public class Program
	{
		private static readonly FormatManager Manager = new FormatManager();
		private static void Main()
		{
			RunFormatDisplay();

			var stringToFormat =
				@"  select distinct patnum, PkgParamSyskey        
   into #TEMP_CLAIM_DATA        
   from TEMP_CLAIM_DATA        
   where RIGHT('0000' + RTRIM(revcode), 4) BETWEEN '0100' AND '0219'        
           
   delete a        
   from  TEMP_CLAIM_DATA a        
   where a.pattype = 'I'        
   and not exists         
   (select 1 from #TEMP_CLAIM_DATA b         
   WHERE a.patnum = b.patnum AND a.PkgParamSyskey = b.PkgParamSyskey )        ";
			var scrubbedString = Manager.FormatToUsqlString(stringToFormat);

			Console.WriteLine("*** Formatted String ***");
			Console.WriteLine(Environment.NewLine);
			Console.WriteLine(scrubbedString);
			Console.WriteLine(Environment.NewLine);

			Console.ReadLine();
		}

		private static void RunFormatDisplay()
		{
			Console.WriteLine("Formatting Values");
			var phoneNumber = "214-777-9090";
			var phoneNumberRaw = "2147779090";
			var phoneNumberParins = "(214)777-9090";

			Console.WriteLine("Normal Phone Number: " + phoneNumber);
			Console.WriteLine("Formatted Number: " + Manager.FormatPhoneNumber(phoneNumber) + Environment.NewLine);

			Console.WriteLine("Raw Phone Number: " + phoneNumberRaw);
			Console.WriteLine("Formatted Number: " + Manager.FormatPhoneNumber(phoneNumberRaw) + Environment.NewLine);

			Console.WriteLine("Partially Formatted Phone Number: " + phoneNumberParins);
			Console.WriteLine("Formatted Number: " + Manager.FormatPhoneNumber(phoneNumberParins) + Environment.NewLine);

			var dollarDecimal = 100M;
			var thousandDecimal = 1000M;
			var millionDecimal = 1000000M;

			Console.WriteLine("Hundred Number/Currency: " + dollarDecimal);
			Console.WriteLine("Formatted Number: " + Manager.FormatNumber(dollarDecimal) + Environment.NewLine);

			Console.WriteLine("Thousand Number/Currency: " + thousandDecimal);
			Console.WriteLine("Formatted Number: " + Manager.FormatNumber(thousandDecimal) + Environment.NewLine);

			Console.WriteLine("Million Number/Currency: " + millionDecimal);
			Console.WriteLine("Formatted Number: " + Manager.FormatNumber(millionDecimal) + Environment.NewLine);

			var commaList = "0,2,4,8,9,10,100";

			Console.WriteLine("Comma Seperated List: " + commaList);
			var parsedCommaList = Manager.ParseCommaSeperatedString(commaList);
			Console.WriteLine("First Value: " + parsedCommaList.FirstOrDefault());
			Console.WriteLine("Last Value: " + parsedCommaList.LastOrDefault());

			Console.WriteLine("Capatolise the first letter of every word in a string");
			var lowerCaseString = "hello this is all in lower case";
			Console.WriteLine(lowerCaseString);
			Console.WriteLine("ToTitleCase: " + Manager.ToTitleCase(lowerCaseString));

			Console.WriteLine("Camel Case string");
			var camelCaseString = "hello this case";
			Console.WriteLine(camelCaseString);
			Console.WriteLine("ToTitleCase: " + Manager.ConvertToCamelCase(camelCaseString));
		}
	}
}
