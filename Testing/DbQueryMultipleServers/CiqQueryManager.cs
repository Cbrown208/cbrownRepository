using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DbQueryMultipleServers
{
	public class CiqQueryManager
	{
		private const string OutputFileName = "CiqMultipleQueryDbOutput.csv";
		private readonly string _outputPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)) + "\\" + OutputFileName;

		public void CiqRunMulitpleDbQuery()
		{
			CleanOutputFile(OutputFileName);
			var user = @"chbrown@finthrive.com";
			//User ID=Cleariq; Password=Sqlmiserveradmin123;MultiSubnetFailover=True;"
			var ciqDbConnection = "Server=tcp:prod-cleariq-scus-db0{0}-sqlmi.8276cba25254.database.windows.net,1433;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Integrated;";
			var queryText = GetCiqWorkflowQueueCountQuery();

			queryText = GetCiqUsersListQuery();
			queryText = GetFacilityAppSettingQuery();

			var resultList = CiqRunQuery<QueryResults>(ciqDbConnection, 10, queryText);

			WriteValueToFile("DbServer,DbName,QueryResult,SiteName");
			foreach (var result in resultList)
			{
				var outputResult = result.DbServer + "," + result.DbName + "," + result.QueryResult + "," + result.SiteName;
				WriteValueToFile(outputResult);
			}

			Console.WriteLine(Environment.NewLine + Environment.NewLine + _outputPath);

			// Open Results in Excel / Whatever program is default for .csv
			Process.Start(_outputPath);

			Console.WriteLine("File Should be open test...");
			Console.ReadLine();
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
				Console.WriteLine(value);
			}
		}

		private void CleanOutputFile(string outputFileName)
		{
			File.WriteAllText(outputFileName, string.Empty);
		}

		public List<T> CiqRunQuery<T>(string dbConnectionString, int serverCount, string query)
		{
			var resultsList = new List<T>();
			for (int i = 1; i <= serverCount; i++)
			{
				var serverNumber = i.ToString("D2");
				var clientDb = string.Format(dbConnectionString, serverNumber);

				using (var db = new SqlConnection(clientDb))
				{
					db.Open();
					var queryResults = db.Query<T>(query).ToList();
					resultsList.AddRange(queryResults);
					db.Close();
				}
			}

			return resultsList;
		}

		public string GetCiqWorkflowQueueCountQuery()
		{
			return @"/****** Run Query on All DB's in Server  ******/
DECLARE @DbList TABLE ( ID INT IDENTITY(1,1), ClientId INT NULL, ClientName VARCHAR(100) NULL, NAME VARCHAR(50) NOT NULL, Counts INT NULL, QueryResult varchar(50) NULL);
DECLARE @ResultsList TABLE (DbServer VARCHAR(75) NULL, DbName VARCHAR(50) NOT NULL, QueryResult varchar(150) NULL,SiteName varchar(max) NULL);
DECLARE @Cnt INT=1;
DECLARE @DbName VARCHAR(50)='';
DECLARE @TotalDbCount INT; 
INSERT INTO @DbList     
SELECT null,null,NAME,NULL,NULL FROM SYS.DATABASES    
WHERE (NAME LIKE 'CIQ_%' AND NAME NOT LIKE 'CIQ_%Training%')
SELECT @TotalDbCount = COUNT(1) FROM @DbList
WHILE (@Cnt<=@TotalDbCount)    
BEGIN     
SELECT @DbName=NAME     
FROM @DbList     
WHERE ID=@Cnt;
DECLARE @SQL NVARCHAR(MAX)
SET @SQL = 'SELECT @@SERVERNAME,'''+@DbName+''', '+'CAST(COUNT(1) as varchar(150)), (select AppTierUrl+''/IntegrationQueue/v1.1.0/ProcessIntegrationQueue.xamlx'' from '+@DbName+'.dbo.GlobalSettings (nolock)) as SiteName
FROM ' + @DbName + '.dbo.[IntegrationQueue_WorkflowQueue] (nolock)'
print @SQL
BEGIN TRY
insert into @ResultsList
EXEC sys.sp_executesql @SQL
UPDATE @DbList
SET QueryResult = 'Successful'
where NAME = @DbName
END TRY
BEGIN CATCH
UPDATE @DbList
SET QueryResult = 'Failed'
where NAME = @DbName
END CATCH
SET @Cnt=@Cnt+1; 
END
select * from @ResultsList";
		}

		public string GetCiqUsersListQuery()
		{
			var results = @"DECLARE @DbList TABLE (
		ID INT IDENTITY(1,1),
		DbName VARCHAR(50) NOT NULL,
		QueryResult varchar(50) NULL)

DECLARE @ResultsList TABLE (
		DbServer VARCHAR(75) NULL,
		DbName VARCHAR(50) NOT NULL,
		QueryResult varchar(150) NULL)
DECLARE @Cnt INT=1;
DECLARE @DbName VARCHAR(50)='';
DECLARE @TotalDbCount INT;

INSERT INTO @DbList     
SELECT NAME,NULL FROM SYS.DATABASES    
WHERE name != 'model' and name != 'DBA_DB'

SELECT @TotalDbCount = COUNT(1) FROM @DbList

WHILE (@Cnt<=@TotalDbCount)    
BEGIN     
	SELECT @DbName=DbName FROM @DbList WHERE ID=@Cnt;
	
	DECLARE @SQL NVARCHAR(MAX)

	SET @SQL = 'SELECT @@SERVERNAME,'''+@DbName+''', name as UserName '
	+'FROM ' + @DbName + '.sys.database_principals --WHERE name like ''%P-TSG%'''
	print @DbName

	BEGIN TRY 

	insert into @ResultsList
	EXEC sys.sp_executesql @SQL

	UPDATE @DbList
	SET QueryResult = 'Successful'
	where DbName = @DbName
	END TRY

	BEGIN CATCH
	UPDATE @DbList
	SET QueryResult = 'Failed'
	where DbName = @DbName
	END CATCH

	SET @Cnt=@Cnt+1; 
END

--select * from @DbList where QueryResult = 'Failed'
select * from @ResultsList
order by DbName ";

			return results;
		}

		private string GetFacilityAppSettingQuery()
		{
			var query = @"/****** Run Query on All DB's in Server  ******/
DECLARE @DbList TABLE ( ID INT IDENTITY(1,1), ClientId INT NULL, ClientName VARCHAR(100) NULL, NAME VARCHAR(50) NOT NULL, Counts INT NULL, QueryResult varchar(50) NULL);
DECLARE @ResultsList TABLE (DbServer VARCHAR(75) NULL, DbName VARCHAR(50) NOT NULL, QueryResult varchar(150) NULL,SiteName varchar(max) NULL);
DECLARE @Cnt INT=1;
DECLARE @DbName VARCHAR(50)='';
DECLARE @TotalDbCount INT; 
INSERT INTO @DbList     
SELECT null,null,NAME,NULL,NULL FROM SYS.DATABASES    
WHERE (NAME LIKE 'CIQ_%' AND NAME NOT LIKE 'CIQ_%Training%')
SELECT @TotalDbCount = COUNT(1) FROM @DbList

WHILE (@Cnt<=@TotalDbCount)    
BEGIN     
SELECT @DbName=NAME     
FROM @DbList     
WHERE ID=@Cnt;
DECLARE @SQL NVARCHAR(MAX)
SET @SQL = 'SELECT Top 1 @@SERVERNAME,'''+@DbName+''', '+'[Value],(select AppTierUrl from '+@DbName+'.dbo.GlobalSettings (nolock)) as SiteName
FROM ' + @DbName + '.dbo.[FacilityAppSettings] (nolock) WHERE [key] = ''EnableAdditionalProviders'' AND [value] = ''True'''
print @SQL
BEGIN TRY
insert into @ResultsList
EXEC sys.sp_executesql @SQL
UPDATE @DbList
SET QueryResult = 'Successful'
where NAME = @DbName
END TRY
BEGIN CATCH
UPDATE @DbList
SET QueryResult = 'Failed'
where NAME = @DbName
END CATCH
SET @Cnt=@Cnt+1; 
END
select * from @ResultsList";
			return query;
		}

		private string GetDbList(string dbLike, string dbNotLike)
		{
			var notLikeQuery = string.Format(" AND NAME NOT LIKE '{1}'", dbNotLike);


			var queryText = string.Format(@"/****** Run Query on All DB's in Server  ******/
DECLARE @DbList TABLE (ID INT IDENTITY(1,1), DbName VARCHAR(50) NOT NULL,QueryStatus varchar(100));
DECLARE @ResultsList TABLE (DbServer VARCHAR(75) NULL, DbName VARCHAR(50) NOT NULL, QueryResult varchar(150) NULL,SiteName varchar(max) NULL);
DECLARE @Cnt INT=1;
DECLARE @DbName VARCHAR(50)='';
DECLARE @TotalDbCount INT;

INSERT INTO @DbList     
SELECT NAME FROM SYS.DATABASES    
WHERE (NAME LIKE '{0}' AND NAME NOT LIKE '{1}')
SELECT @TotalDbCount = COUNT(1) FROM @DbList

WHILE (@Cnt<=@TotalDbCount)    
BEGIN     
SELECT @DbName=DbName FROM @DbList WHERE ID=@Cnt;
DECLARE @SQL NVARCHAR(MAX) ",dbLike,dbNotLike); 
			return queryText;
		}
	}
}
