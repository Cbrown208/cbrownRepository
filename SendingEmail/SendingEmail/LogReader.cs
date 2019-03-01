using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SendingEmail
{
	public class LogReader
	{
		public List<string> GetLogPaths()
		{
			var logPaths = new List<string>
			{
				@"C:\Myscripts\ManualBackup\Logs\Documents_backup.log",
				@"C:\Myscripts\ManualBackup\Logs\Music_backup.log",
				@"C:\Myscripts\ManualBackup\Logs\MyScripts_backup.log",
				@"C:\Myscripts\ManualBackup\Logs\Pictures_backup.log"
			};
			return logPaths;
		}

		public LoggingTableStats GetStats(string path)
		{
			LoggingTableStats statsTable = new LoggingTableStats { FilesAdded = new List<string>(), FilesUpdated = new List<string>() };
			char[] delimiterChars = { ' ', 'g', ',', ':', '\t' };

			foreach (var match in File.ReadLines(path).Select((text, index) => new { text, lineNumber = index + 1 }).Where(x => x.text.Contains("Dirs :")))
			{
				statsTable.DirectorySummary = match.text;
			}

			foreach (var match in File.ReadLines(path).Select((text, index) => new { text, lineNumber = index + 1 }).Where(x => x.text.Contains("Files : ")))
			{
				statsTable.FilesSummary = match.text;
			}

			foreach (var match in File.ReadLines(path).Select((text, index) => new { text, lineNumber = index + 1 }).Where(x => x.text.Contains("Times :")))
			{
				statsTable.times = match.text;
			}

			foreach (var match in File.ReadLines(path).Select((text, index) => new { text, lineNumber = index + 1 }).Where(x => x.text.Contains("Ended :")))
			{
				statsTable.EndedOn = match.text;
			}

			foreach (var match in File.ReadLines(path).Select((text, index) => new { text, lineNumber = index + 1 }).Where(x => x.text.Contains("Bytes :")))
			{
				statsTable.bytes = match.text.Trim();
				string[] words = statsTable.bytes.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in words)
				{
					System.Console.WriteLine(s);
				}
			}

			foreach (var match in File.ReadLines(path).Select((text, index) => new { text, lineNumber = index + 1 }).Where(x => x.text.Contains("New File  ")))
			{
				statsTable.FilesAdded.Add(match.text);
			}

			foreach (var match in File.ReadLines(path).Select((text, index) => new { text, lineNumber = index + 1 }).Where(x => x.text.Contains("Newer  ")))
			{
				statsTable.FilesUpdated.Add(match.text);
			}

			return statsTable;
		}


		public List<string> FormatBytesData(string[] bytes2)
		{
			var bytesList = bytes2.ToList();
			for (var i = 0; i < bytesList.Count - 1; ++i)
			{
				var listCount = bytesList.Count - 1;
				if ((bytesList[i] != "k" || bytesList[i] != "m" || bytesList[i] != "g") && (bytesList[i + 1] == "k" || bytesList[i + 1] == "m" || bytesList[i + 1] == "g"))
				{
					var combinedBytes = bytesList[i] + bytesList[i + 1] + "b ";

					if (i != listCount)
					{
						var valuetoRemove = bytesList[i + 1];
						bytesList[i] = combinedBytes;
						bytesList.Remove(valuetoRemove);
					}
				}
			}
			return bytesList;
		}
	}
}

