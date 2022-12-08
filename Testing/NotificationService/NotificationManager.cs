using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;

namespace NotificationService
{
	public class NotificationManager
	{
		private readonly EmailManager _emailManager = new EmailManager();
		private const string ConnectionString = @"Data Source=LEWVPPASDBGL01.nthrive.nthcrp.com;Initial catalog=MessageDb;Integrated Security=true;";
		private const string emailList = "chbrown@nthrive.com,cr.brown208@gmail.com";
		//private const string _emailList = "chbrown@nthrive.com,cr.brown208@gmail.com,2144784282@txt.att.net";
		private const string OutputPath = @"NotificationLog.txt";

		public void RunNotificationService()
		{
			CheckDb();
		}

		private void CheckDb()
		{
			WriteValueToLogFile(DateTime.Now+ "-Checking RegQaRequest Table");
			using (var db = new SqlConnection(ConnectionString))
			{
				db.Open();
				var query = @"SELECT COUNT(1) as Cnt FROM [MessageDb].[dbo].[RegistrationQARequest] (nolock) WHERE Status = 1";

				var queryResults = db.Query<dynamic>(query).ToList();
				if (queryResults.Any())
				{
					var cnt = 0;
					foreach (dynamic result in queryResults)
					{
						//var status = result.Status;
						cnt = result.Cnt;
						Console.WriteLine("1-"+cnt);
					}

					if (cnt >= 500)
					{
						// Send Notification email - 
						WriteValueToLogFile("Sending Notification Email - Current Message Count: " + cnt);
						_emailManager.SendEmail(emailList, cnt);
					}
				}
			}
		}

		public void WriteValueToLogFile(string value)
		{
			try
			{
				using (var file = new StreamWriter(OutputPath, true))
				{
					file.WriteLineAsync(value);
				}

				Console.WriteLine(value);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
