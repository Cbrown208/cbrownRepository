using Dapper;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace RunDbUpdateScripts
{
	public class RunSqlScriptsManager
	{
		public void RunDbScripts()
		{
			var ciqDbConnection = "data source=nprod-cleariq-dev-scus-db1-sqlmi.61a4b50c50df.database.windows.net;initial catalog=Dev_CIQ_Main_Chris;User ID=Cleariq; Password=Sqlmiserveradmin123;MultiSubnetFailover=True;";
			var scriptPath = @"C:\Dev\ClearIQ\app-cleariq\SQL\DBChanges";

			var scriptsAlreadyRan = GetScriptsAlreadyRan(ciqDbConnection);

			var missingFiles = new List<string>();

			DirectoryInfo d = new DirectoryInfo(@"C:\Dev\ClearIQ\app-cleariq\SQL\DBChanges"); //Assuming Test is your Folder

			var fileList = d.GetFiles("Script_00*").ToList().OrderBy(x => x.Name);

			foreach (FileInfo file in fileList)
			{
				if (scriptsAlreadyRan.FirstOrDefault(x => x.Version.Contains(file.Name)) == null)
				{
					missingFiles.Add(file.Name);
					string contents = File.ReadAllText(scriptPath + @"\" + file.Name);

					IEnumerable<string> commandStrings = Regex.Split(contents, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

					Console.WriteLine("RunningFile: " + file.Name);

					foreach (string commandString in commandStrings)
					{
						CiqRunCommand(ciqDbConnection, commandString);
					}
					var updateQuery = string.Format("INSERT INTO [Dev_CIQ_Main_Chris].[dbo].[DBUpdates]([Version],[DateRan],[Comment],[RollbackDate]) VALUES ('SQL.DBChanges.{0}',GETDATE(),'Ran {0}',null) ", file.Name);
					CiqRunQuery<dynamic>(ciqDbConnection, updateQuery);
				}
			}

			Console.WriteLine("Missing Files are: ");
			foreach (var missingFile in missingFiles)
			{
				Console.WriteLine(missingFile);
			}

		}

		private List<QueryResult> GetScriptsAlreadyRan(string connectionString)
		{
			var query = "USE [Dev_CIQ_Main_Chris]; Select Version from DbUpdates order by DateRan desc";

			var results = CiqRunQuery<QueryResult>(connectionString, query);

			return results;

		}

		public List<T> CiqRunQuery<T>(string dbConnectionString, string query)
		{
			var resultsList = new List<T>();

			using (var db = new SqlConnection(dbConnectionString))
			{
				db.Open();
				var queryResults = db.Query<T>(query).ToList();
				resultsList.AddRange(queryResults);
				db.Close();
			}
			return resultsList;
		}

		public bool CiqRunCommand(string dbConnectionString, string query)
		{
			using (var db = new SqlConnection(dbConnectionString))
			{
				db.Open();
				if (!string.IsNullOrWhiteSpace(query.Trim()))
				{
					using (var command = new SqlCommand(query, db))
					{
						command.ExecuteNonQuery();
					}
				}
				db.Close();
			}
			return true;
		}
	}

	public class QueryResult
	{
		public string Version { get; set; }
	}
}

