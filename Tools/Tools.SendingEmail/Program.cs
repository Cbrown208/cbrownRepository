using System;
using System.Linq;

namespace Tools.SendingEmail
{
	public class Program
	{
		static void Main()
		{
			Console.WriteLine("starting");
			var smail = new SendEmail();
			var logstats = new LogReader();
			var message = new System.Net.Mail.MailMessage();
			var todaysDate = String.Format("{0:MM/dd/yy HH:mm:ss}", DateTime.Now.ToLocalTime());
			char[] delimiterChars = { ' ', 'g', 'k', 'm', ',', ':', '\t' };
			char[] testingDelimiterChars = { ' ', ':' };
			char[] timeDelimiterChars = { ' ', ',', '\t' };

			var logPaths = logstats.GetLogPaths();
			var msgBody = "<!DOCTYPE html> <html> <style> table, th, td { border: 1px solid black;}</style> <body> Backup has been completed at: " + todaysDate + Environment.NewLine
   + "Backup Statistics: " + Environment.NewLine;

			foreach (var path in logPaths)
			{
				var stats = logstats.GetStats(path);
				if (path == @"C:\Myscripts\ManualBackup\Logs\Documents_backup.log")
				{
					msgBody = msgBody + "<br>Documents: " + Environment.NewLine;
				}
				else if (path == @"C:\Myscripts\ManualBackup\Logs\Music_backup.log")
				{
					msgBody = msgBody + "<br>Music: " + Environment.NewLine;
				}

				else if (path == @"C:\Myscripts\ManualBackup\Logs\MyScripts_backup.log")
				{
					msgBody = msgBody + "<br>MyScripts: " + Environment.NewLine;
				}
				else
				{
					msgBody = msgBody + "<br>Pictures: " + Environment.NewLine;
				}
				message.IsBodyHtml = true;

				msgBody = msgBody + @"<table border='2'>";

				msgBody = msgBody + "<tr><th></th> <th> Total </th><th>Copied </th><th> Skipped </th><th> Mismatch </th><th> FAILED </th><th>Extras</th></tr>";

				var bytes2 = stats.bytes.Split(testingDelimiterChars, StringSplitOptions.RemoveEmptyEntries);
				var dirs = stats.DirectorySummary.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
				var files = stats.FilesSummary.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
				var ended = stats.EndedOn.Split(timeDelimiterChars, StringSplitOptions.RemoveEmptyEntries);
				var times = stats.times.Split(timeDelimiterChars, StringSplitOptions.RemoveEmptyEntries);

				var bytesList = logstats.FormatBytesData(bytes2);

				msgBody = msgBody + "<tr><td>DirectorySummary </td><td>" + dirs[1] + "</td><td>" + dirs[2] + "</td><td>" + dirs[3] + "</td><td>" + dirs[4] + "</td><td>" + dirs[5] + "</td><td>" + dirs[6] + "</td></tr>";
				msgBody = msgBody + "<tr><td>Files </td><td>" + files[1] + "</td><td>" + files[2] + "</td><td>" + files[3] + "</td><td>" + files[4] + "</td><td>" + files[5] + "</td><td>" + files[6] + "</td></tr>";
				msgBody = msgBody + "<tr><td>Bytes </td><td>" + bytesList[1] + "</td><td>" + bytesList[2] + "</td><td>" + bytesList[3] + "</td><td>" + bytesList[4] + "</td><td>" + bytesList[5] + "</td><td>" + bytesList[6] + "</td></tr>";
				msgBody = msgBody + "<tr><td>Times </td><td>" + times[2] + "</td><td>" + times[3] + "</td><td> </td><td> </td><td>" + times[4] + "</td><td>" + times[5] + "</td></tr>";

				msgBody = msgBody + "</tr>";

				msgBody = msgBody + "</table>" + Environment.NewLine;
				msgBody = msgBody + "Ended: " + ended[2] + ", " + ended[3] + " " + ended[4] + ", " + ended[5] + " " + ended[6] + " " + ended[7];
				if (stats.FilesAdded != null && stats.FilesAdded.Any())
				{
					msgBody = msgBody + "<br>" + "Files Added: " + Environment.NewLine + "<br>";
					foreach (var file in stats.FilesAdded)
					{
						var filesListTemp = file.Split(timeDelimiterChars, StringSplitOptions.RemoveEmptyEntries);
						msgBody = msgBody + filesListTemp.LastOrDefault() + Environment.NewLine + "<br>";
					}
				}
				if (stats.FilesUpdated != null && stats.FilesUpdated.Any())
				{
					msgBody = msgBody + "<br>" + "Files Updated: " + Environment.NewLine + "<br>";
					foreach (var file in stats.FilesUpdated)
					{
						var filesListTemp = file.Split(timeDelimiterChars, StringSplitOptions.RemoveEmptyEntries);
						msgBody = msgBody + filesListTemp.LastOrDefault() + Environment.NewLine + "<br>";
					}
				}
			}
			msgBody = msgBody + "</body></html>";
			message.Body = msgBody;
			message.IsBodyHtml = true;
			smail.SendMailWithBody(message.Body);

			//var status = smail.SendMail();
			//Console.WriteLine(status);
			//Console.WriteLine("Email has been Sent");
			//Console.WriteLine("Press any key to exit");

			//Console.ReadLine();

		}
	}
}
