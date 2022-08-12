using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace FirewallConnectionChecker
{
	public class DbConnectionManager
	{
		public const string OutputFileName = "Output.txt";
		public void CheckDbConnectionList()
		{
			var path = @"DbList.txt";

			WriteValueToFile("------------------------------------------Db Connection Test------------------------------------------------------------------------");
			WriteValueToFile("Start Time: " + DateTime.Now.ToLocalTime());
			WriteValueToFile("Input File: " + path);
			WriteValueToFile("Output File: " + OutputFileName + Environment.NewLine);

			var dbList = File.ReadLines(path);
			var connectionList = new List<string>();
			foreach (var line in dbList)
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					connectionList.Add(line.Trim());
				}
			}

			var checkingSitesHeader = "----------------- Checking " + connectionList.Count + " Db's from DbList.txt ----------------------------------------------------";
			WriteValueToFile(checkingSitesHeader);
			foreach (var dbConnectionString in connectionList)
			{
				CheckDbConnection(dbConnectionString);
			}

		}
		public void CheckDbConnection(string connectionString)
		{
			try
			{
				//var connectionString = @"server = LEWVQCMGDB02\VAL_GLOBAL01; database = EventHandlerDB; Trusted_Connection = true";
				using (var db = new SqlConnection(connectionString))
				{
					db.Open();
					//var query = @"select top 1 * from [Internet_Accounts].[dbo].[FAC_SFDC] (nolock)";
					//var results = db.Query<dynamic>(query).FirstOrDefault();
					//Console.WriteLine(results.FacilityLongName);
					var result = "Server: " + db.DataSource + " | Connection Successful!";
					WriteValueToFile(result);
					db.Close();
				}
			}
			catch (Exception ex)
			{
				WriteValueToFile("Server: "+ GetServerNameFromConnectionString(connectionString) + " | Error: "+ ex.Message);
			}
		}

		private string GetServerNameFromConnectionString(string connectionString)
		{
			var pos1 = connectionString.IndexOf("=", StringComparison.Ordinal)+ 1;
			var pos2 = connectionString.IndexOf(";", StringComparison.Ordinal);
			var serverName = connectionString.Substring(pos1, pos2 - pos1);
			return serverName;
		}

		public void WriteValueToFile(string value)
		{
			try
			{
				using (var file = new StreamWriter(OutputFileName, true))
				{
					file.WriteLineAsync(value);
				}

				Console.WriteLine(value);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message+ value);
			}
		}
	}
}
