using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Formaters
{
	public class FormatManager
	{
		public string FormatPhoneNumber(string phoneNumber)
		{
			return Regex.Replace(phoneNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
		}
		public string FormatNumber(decimal value)
		{
			return String.Format("{0:n}", value);
		}

		public string FormatNumberCSharp6(decimal value)
		{
			return $"{value:n}";
		}

		public List<string> ParseCommaSeperatedString(string value)
		{
			string[] values = value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			return values.ToList();
		}
	}
}
