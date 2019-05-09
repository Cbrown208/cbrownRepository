using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tools.SendingEmail.Models;

namespace Tools.SendingEmail
{
	public class LogReader
	{
		private readonly char[] _delimiterChars = { ' ', 'g', 'k', 'm', ',', ':', '\t' };
		private readonly char[] _timeDelimiterChars = { ' ', ',', '\t' };
		private readonly char[] _bytesDelimiterChars = { ' ', ':' };
		public List<string> GetLogPaths()
		{
			var basePath = @"C:\MyScripts\Temp\Logs";
			var logPaths = new List<string>
			{
				basePath+@"\Documents_backup.log",
				basePath+@"\Music_backup.log",
				basePath+@"\MyScripts_backup.log",
				basePath+@"\Pictures_backup.log"
			};
			return logPaths;
		}

		public LogStatsDetails GetLogStatsDetails(string path)
		{
			var dirsLine = GetLogFileLineDetails(path, "Dirs :");
			var filesLine = GetLogFileLineDetails(path, "Files : ");
			var bytesLine = GetLogFileLineDetails(path, "Bytes :");
			var timesLine = GetLogFileLineDetails(path, "Times :");
			var endedLine = GetLogFileLineDetails(path, "Ended :");

			var logStatsDetails = new LogStatsDetails
			{
				Dirs = GetLineCopyDetails(dirsLine),
				Files = GetLineCopyDetails(filesLine),
				Bytes = GetBytesLineCopyDetails(bytesLine),
				Times = GetTimesLineCopyDetails(timesLine),
				EndedTime = endedLine.Split(_timeDelimiterChars, StringSplitOptions.RemoveEmptyEntries),
				FilesAdded = GetCopiedFilesList(path, "New File  "),
				FilesUpdated = GetCopiedFilesList(path, "Newer  ")
			};

			return logStatsDetails;
		}

		private string GetLogFileLineDetails(string path, string startLineString)
		{
			var valuesArray = "";
			foreach (var match in File.ReadLines(path).Select((text, index) => new { text, lineNumber = index + 1 }).Where(x => x.text.Contains(startLineString)))
			{
				valuesArray = match.text;
			}
			return valuesArray;
		}

		private CopyDetails GetLineCopyDetails(string value)
		{
			var data = value.Split(_delimiterChars, StringSplitOptions.RemoveEmptyEntries);
			var result = new CopyDetails
			{
				Total = data[1],
				Copied = data[2],
				Skipped = data[3],
				Mismatched = data[4],
				Failed = data[5],
				Extras = data[6]
			};

			return result;
		}

		private List<string> GetCopiedFilesList(string path, string fileTypeString)
		{
			var filesList = new List<string>();
			foreach (var match in File.ReadLines(path).Select((text, index) => new { text, lineNumber = index + 1 }).Where(x => x.text.Contains(fileTypeString)))
			{
				var fileName = match.text;
				var filesListTemp = fileName.Split(_timeDelimiterChars, StringSplitOptions.RemoveEmptyEntries);
				filesList.Add(filesListTemp.LastOrDefault());
			}

			return filesList;

		}

		private CopyDetails GetBytesLineCopyDetails(string bytesLine)
		{
			var bytes2 = bytesLine.Split(_bytesDelimiterChars, StringSplitOptions.RemoveEmptyEntries);
			var formattedBytesValues = FormatBytesData(bytes2);

			var result = new CopyDetails
			{
				Total = formattedBytesValues[1],
				Copied = formattedBytesValues[2],
				Skipped = formattedBytesValues[3],
				Mismatched = formattedBytesValues[4],
				Failed = formattedBytesValues[5],
				Extras = formattedBytesValues[6]
			};
			return result;
		}

		private CopyDetails GetTimesLineCopyDetails(string timesLine)
		{
			var times = timesLine.Split(_timeDelimiterChars, StringSplitOptions.RemoveEmptyEntries);
			var result = new CopyDetails
			{
				Total = times[2],
				Copied = times[3],
				Skipped = "",
				Mismatched = "",
				Failed = times[4],
				Extras = times[5]
			};
			return result;

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

