using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FileExamples
{
	public class FileExampleManager
	{
		public void RunFileExamples()
		{
			ReadFile();

			Console.ReadLine();
		}

		public bool TestingReturn()
		{
			var temp1 = new List<string>();
			Console.WriteLine("Origional Logic 0:" + !(temp1.Count > 1));
			temp1.Add("hello");
			Console.WriteLine("Origional Logic 1:" + !(temp1.Count > 1));
			temp1.Add("hello2");
			Console.WriteLine("Origional Logic 2:" + !(temp1.Count > 1));
			return !(temp1.Count > 1);
			
		}

		public TimeSpan ReadFile()
		{
			var searchString = "Time Elapsed";
			var path = @"C:\MyScripts\Temp\BuildLogs\TestingBuildLog.txt";
			string contents = File.ReadAllText(path);
			var indexList = GetAllIndexesRegEx(contents, searchString);

			var buildTimesList = new List<TimeSpan>();
			var totalTime = TimeSpan.Zero;
			foreach (var index in indexList)
			{
				var startIndex = index + 13;
				var buildTimeValue = contents.Substring(startIndex, 11);
				TimeSpan time = TimeSpan.Parse(buildTimeValue);
				buildTimesList.Add(time);

				totalTime = totalTime + time;
			}

			// Output Section
			Console.WriteLine("BuildTimes: ");
			foreach (var time in buildTimesList)
			{
				Console.WriteLine(time.ToString());
			}
			Console.WriteLine("Total Build Time: "+ totalTime);
			//var totalTime =  buildTimesList.Sum(x=> x);

			return totalTime;
		}

		public List<int> GetAllIndexesRegEx(string source, string matchString)
		{
			matchString = Regex.Escape(matchString);
			var matchList = new List<int>();

			foreach (Match match in Regex.Matches(source, matchString))
			{
				matchList.Add(match.Index);
			}

			return matchList;
		}

		public static int[] AllIndexesOf(string str, string substr, bool ignoreCase = false)
		{
			if (string.IsNullOrWhiteSpace(str) ||
			    string.IsNullOrWhiteSpace(substr))
			{
				throw new ArgumentException("String or substring is not specified.");
			}

			var indexes = new List<int>();
			int index = 0;

			while ((index = str.IndexOf(substr, index,
				       ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) != -1)
			{
				indexes.Add(index++);
			}

			return indexes.ToArray();
		}
	}
}
