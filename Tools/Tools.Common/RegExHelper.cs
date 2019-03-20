using System.Text.RegularExpressions;

namespace Tools.Common
{
	public class RegExHelper
	{
		public string CapitalizeFirstLetterOfString(string value)
		{
			var result = Regex.Replace(value, @"\b(\w)", m => m.Value.ToUpper());
			result = Regex.Replace(result, @"(\s(of|in|by|and)|\'[st])\b", m => m.Value.ToLower(), RegexOptions.IgnoreCase);
			return result;
		}
	}
}
