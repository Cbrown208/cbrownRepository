using System;
using System.Linq;

namespace Common.Formaters
{
	public class Program
	{
		private static void Main()
		{
		var manager = new FormatManager();
		Console.WriteLine("Formatting Values");
			var phoneNumber = "214-777-9090";
			var phoneNumberRaw = "2147779090";
			var phoneNumberParins = "(214)777-9090";

			Console.WriteLine("Normal Phone Number: " + phoneNumber);
			Console.WriteLine("Formatted Number: " + manager.FormatPhoneNumber(phoneNumber)+ Environment.NewLine);
			
			Console.WriteLine("Raw Phone Number: " + phoneNumberRaw);
			Console.WriteLine("Formatted Number: " + manager.FormatPhoneNumber(phoneNumberRaw) + Environment.NewLine);

			Console.WriteLine("Partially Formatted Phone Number: " + phoneNumberParins);
			Console.WriteLine("Formatted Number: "+manager.FormatPhoneNumber(phoneNumberParins) + Environment.NewLine);

			var dollarDecimal = 100M;
			var thousandDecimal = 1000M;
			var millionDecimal = 1000000M;

			Console.WriteLine("Hundred Number/Currency: " + dollarDecimal);
			Console.WriteLine("Formatted Number: " + manager.FormatNumber(dollarDecimal) + Environment.NewLine);

			Console.WriteLine("Thousand Number/Currency: " + thousandDecimal);
			Console.WriteLine("Formatted Number: " + manager.FormatNumber(thousandDecimal) + Environment.NewLine);

			Console.WriteLine("Million Number/Currency: " + millionDecimal);
			Console.WriteLine("Formatted Number: " + manager.FormatNumber(millionDecimal) + Environment.NewLine);

			var commaList = "0,2,4,8,9,10,100";

			Console.WriteLine("Comma Seperated List: " + commaList);
			var parsedCommaList = manager.ParseCommaSeperatedString(commaList);
			Console.WriteLine("First Value: " + parsedCommaList.FirstOrDefault());
			Console.WriteLine("Last Value: " + parsedCommaList.LastOrDefault());

			Console.ReadLine();
		}
	}
}
