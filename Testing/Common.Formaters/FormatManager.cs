using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Formaters
{
	public class FormatManager
	{
		private readonly DatabaseStringFormatter _dbStringFormatter = new DatabaseStringFormatter();
		public string FormatPhoneNumber(string phoneNumber)
		{
			return Regex.Replace(phoneNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
		}

		public string CheckGuidFormat(string guidCheck)
		{
			return Regex.Replace(guidCheck, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
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

		public string ToTitleCase(string str)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
		}

		public string ConvertToCamelCase(string phrase)
		{
			string[] splittedPhrase = phrase.Split(' ', '-', '.');
			var sb = new StringBuilder();
			//sb.Append(splittedPhrase[0].ToLower());
			//splittedPhrase[0] = string.Empty;

			foreach (String s in splittedPhrase)
			{
				char[] splittedPhraseChars = s.ToCharArray();
				if (splittedPhraseChars.Length > 0)
				{
					splittedPhraseChars[0] = ((new String(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
				}
				sb.Append(new String(splittedPhraseChars));
			}
			return sb.ToString();
		}

		public string FormatToUsqlString(string str)
		{
			var results = _dbStringFormatter.FormatUsqlString(str);
			return results;
		}
		public string FormatSqlString(string str)
		{
			var results = _dbStringFormatter.FormatSqlString(str);
			return results;
		}

		public string FormatToCustom(string str)
		{
			var results = _dbStringFormatter.FormatCustomSqlString(str);
			return results;
		}
	}
}
