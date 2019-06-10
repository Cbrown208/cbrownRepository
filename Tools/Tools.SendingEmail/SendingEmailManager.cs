using System;
using System.Linq;
using Tools.Core.Models;
using Tools.DataAccess;

namespace Tools.SendingEmail
{
	public class SendingEmailManager
	{
		private readonly  LogReader _logStats = new LogReader();
		private readonly SendEmail _sendMail = new SendEmail();
		private readonly ManualBackupRepository _backupRepository = new ManualBackupRepository();

		public void SendBackupLogEmail()
		{
			var isTesting = 1;
			Console.WriteLine("Starting V2");
			var msgBody = GetEmailBody();
			var message = new System.Net.Mail.MailMessage {Body = msgBody, IsBodyHtml = true};

			if (isTesting == 0)
			{
				_sendMail.SendMailWithBody(message.Body);
			}

			//Console.ReadLine();
		}

		private string GetEmailBody()
		{
			var logAuditStats = new LogStats();
			var today = $"{DateTime.Now.ToLocalTime():MM/dd/yy HH:mm:ss}";
			var logPaths = _logStats.GetLogPaths();

			var msgBody = "<!DOCTYPE html> <html> <style> table, th, td { border: 1px solid black;}</style> <body> Backup has been completed at: " + today + Environment.NewLine + "Backup Statistics: " + Environment.NewLine;

			foreach (var path in logPaths)
			{
				var stats = _logStats.GetLogStatsDetails(path);
				logAuditStats = AddStats(path, stats, logAuditStats);
				msgBody = msgBody + GetSectionHeader(path);

				msgBody = msgBody + @"<table border='2'>";
				msgBody = msgBody + "<tr><th></th> <th> Total </th><th>Copied </th><th> Skipped </th><th> Mismatch </th><th> FAILED </th><th>Extras</th></tr>";

				msgBody = msgBody + "<tr><td>DirectorySummary </td><td>" + stats.Dirs.Total + "</td><td>"
						  + stats.Dirs.Copied + "</td><td>"
						  + stats.Dirs.Skipped + "</td><td>"
						  + stats.Dirs.Mismatched + "</td><td>"
						  + stats.Dirs.Failed + "</td><td>"
						  + stats.Dirs.Extras + "</td></tr>";

				msgBody = msgBody + "<tr><td>Files </td><td>" + stats.Files.Total
						  + "</td><td>" + stats.Files.Copied
						  + "</td><td>" + stats.Files.Skipped
						  + "</td><td>" + stats.Files.Mismatched
						  + "</td><td>" + stats.Files.Failed
						  + "</td><td>" + stats.Files.Extras + "</td></tr>";

				msgBody = msgBody + "<tr><td>Bytes </td><td>" + stats.Bytes.Total
						  + "</td><td>" + stats.Bytes.Copied
						  + "</td><td>" + stats.Bytes.Skipped
						  + "</td><td>" + stats.Bytes.Mismatched
						  + "</td><td>" + stats.Bytes.Failed
						  + "</td><td>" + stats.Bytes.Extras + "</td></tr>";

				msgBody = msgBody + "<tr><td>Times </td><td>" + stats.Times.Total
						  + "</td><td>" + stats.Times.Copied
						  + "</td><td> </td><td> </td><td>" + stats.Times.Failed
						  + "</td><td>" + stats.Times.Extras + "</td></tr>";

				msgBody = msgBody + "</tr></table>" + Environment.NewLine;
				msgBody = msgBody + "Ended: " + stats.EndedTime[2] + ", " + stats.EndedTime[3] + " " + stats.EndedTime[4] + ", " + stats.EndedTime[5] + " " + stats.EndedTime[6] + " " + stats.EndedTime[7];

				if (stats.FilesAdded != null && stats.FilesAdded.Any())
				{
					msgBody = msgBody + "<br>" + "Files Added: " + Environment.NewLine + "<br>";
					foreach (var file in stats.FilesAdded)
					{
						msgBody = msgBody + file + Environment.NewLine + "<br>";
					}
				}
				if (stats.FilesUpdated != null && stats.FilesUpdated.Any())
				{
					msgBody = msgBody + "<br>" + "Files Updated: " + Environment.NewLine + "<br>";
					foreach (var file in stats.FilesUpdated)
					{
						msgBody = msgBody + file + Environment.NewLine + "<br>";
					}
				}
			}
			msgBody = msgBody + "</body></html>";

			try
			{
				_backupRepository.AddRecord(logAuditStats);
				//var tempResults = JsonConvert.SerializeObject(logAuditStats);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}

			return msgBody;
		}

		private LogStats AddStats(string path, LogStatsDetails stats, LogStats logAuditStats)
		{
			if (path.Contains(@"Documents_backup"))
			{
				logAuditStats.Documents = stats;
			}
			else if (path.Contains(@"Music_backup"))
			{
				logAuditStats.Music = stats;
			}
			else if (path.Contains(@"MyScripts_backup"))
			{
				logAuditStats.MyScripts = stats;
			}
			else
			{
				logAuditStats.Pictures = stats;
			}

			return logAuditStats;
		}

		private static string GetSectionHeader(string path)
		{
			string results;
			if (path.Contains(@"Documents_backup"))
			{
				results = "<br>Documents: ";
			}
			else if (path.Contains(@"Music_backup"))
			{
				results = "<br>Music: ";
			}
			else if (path.Contains(@"MyScripts_backup"))
			{
				results = "<br>MyScripts: ";
			}
			else
			{
				results = "<br>Pictures: ";
			}
			results = results + Environment.NewLine;

			return results;
		}
	}
}
