using System;

namespace LoadSimulator.Core.Util
{
	public static class StringUtil
	{
		public static string NewLine { get; } = Environment.NewLine;

		public static string Seperator(char c, int count) => $"{new String(c, count)}{NewLine}";

		public static string Field(string n, object v) => $"{n,-25}: {v,-25}{NewLine}";
	}
}