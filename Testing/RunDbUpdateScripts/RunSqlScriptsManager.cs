using Dapper;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace RunDbUpdateScripts
{
	public class RunSqlScriptsManager
	{
		public const string DbName = "Dev_CIQ_Main_Chris";
		public void RunDbScripts()
		{
			var scriptPath = @"C:\Dev\ClearIQ\app-cleariq\SQL\DBChanges";

			var ciqDbConnection = "data source=azrvdciqdb201.NTHRIVENP.NTHCRPNP.com; initial catalog=CIQ_Main_UTSW_Automation3; Trusted_Connection=True; MultiSubnetFailover=True;TrustServerCertificate=True;";
			                      //"User ID=Cleariq; Password=Sqlmiserveradmin123;";
			ciqDbConnection =
				"data source=azrvdciqdb201.nthrivenp.nthcrpnp.com; initial catalog=" + DbName+"; User ID=Cleariq; Password=Sqlmiserveradmin123; MultiSubnetFailover=True;TrustServerCertificate=True;";

			var scriptsAlreadyRan = GetScriptsAlreadyRan(ciqDbConnection);

			var missingFiles = new List<string>();

			DirectoryInfo d = new DirectoryInfo(@"C:\Dev\ClearIQ\app-cleariq\SQL\DBChanges");

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
					var updateQuery = string.Format("INSERT INTO [{0}].[dbo].[DBUpdates]([Version],[DateRan],[Comment],[RollbackDate]) VALUES ('SQL.DBChanges.{1}',GETDATE(),'Ran {1}',null) ", DbName, file.Name);
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
			var query = "USE ["+DbName+"]; Select Version from DbUpdates order by DateRan desc";

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

