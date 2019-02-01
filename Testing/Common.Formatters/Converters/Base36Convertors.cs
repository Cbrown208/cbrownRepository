using System;
using System.Collections.Generic;

namespace Common.Formatters.Converters
{
	public class Base36Convertors
	{
		private const string CharList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public List<string> GetFullList()
		{
			var syskey = "000";
			var syskeyList = new List<string> { syskey };

			while (syskey != "ZZZ")
			{
				syskey = GetNewSyskeyValue(syskey);
				syskeyList.Add(syskey);
			}
			return syskeyList;
		}

		public List<string> GetPartialList(string startSyskey, int count)
		{
			
			var syskeyList = new List<string> { startSyskey };

			for (int i = 1; i < count; i++)
			{
				startSyskey = GetNewSyskeyValue(startSyskey);
				syskeyList.Add(startSyskey);
			}
			return syskeyList;
		}

		public string GetNewSyskeyValue(string currentSyskey)
		{
			var decodedValue = Decode(currentSyskey);
			var encodedValue = Encode(decodedValue + 1);

			var formattedValue = encodedValue;
			if (formattedValue.Length == 2)
			{
				formattedValue = "0" + formattedValue;
			}
			if (formattedValue.Length <= 1)
			{
				formattedValue = "00" + formattedValue;
			}
			return formattedValue;
		}

		public static long Decode(string value)
		{
			var database = new List<char>(CharList);
			var tmp = new List<char>(value.ToUpper().ToCharArray());
			tmp.Reverse();

			long number = 0;
			int index = 0;
			foreach (var character in tmp)
			{
				number += database.IndexOf(character) * (long)Math.Pow(36, index);
				index++;
			}

			return number;
		}

		private static string Encode(long number)
		{
			var database = new List<char>(CharList);
			var value = new List<char>();
			long tmp = number;

			while (tmp != 0)
			{
				value.Add(database[Convert.ToInt32(tmp % 36)]);
				tmp /= 36;
			}

			value.Reverse();
			return new string(value.ToArray());
		}
	}
}
