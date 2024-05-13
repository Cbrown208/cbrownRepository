using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Dapper;

namespace DbQueryMultipleServers
{
	public class CiqQueryManager
	{
		private const string OutputFileName = "CiqMultipleQueryDbOutput.csv";
		private readonly string _outputPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)) + "\\" + OutputFileName;
		private List<string> _columnList = new List<string> { "DbServer", "DbName", "QueryResult", "SiteName" };

		public void CiqRunMulitpleDbQuery()
		{
			CleanOutputFile(OutputFileName);
			var ciqDbConnection = "Server=azrvpciqdb{0}01.nthrive.nthcrp.com,1433;Trusted_Connection=True;TrustServerCertificate=True;";
			var queryText = GetCiqWorkflowQueueCountQuery();

			queryText = GetCiqUsersListQuery();
			queryText = GetFacilityAppSettingQuery();
			queryText = GetMostRecentDbUpScriptRan();
			queryText = GetOneOffQuery();

			var _columnList = GetColumnListFromClass(new QueryResults());

			var queryResultTable = BuildResultsTableString(_columnList);

			var resultList = CiqRunQuery<QueryResults>(ciqDbConnection, 6, queryText);

			string joinedColumns = string.Join(",", _columnList);
			WriteValueToFile(joinedColumns);



			foreach (var result in resultList)
			{
				var outputResult = result.DbServer + "," + result.DbName + "," + result.QueryResult + "," + result.QueryResult2 + ","+ result.QueryResult3;
				WriteValueToFile(outputResult);
			}

			Console.WriteLine(Environment.NewLine + Environment.NewLine + _outputPath);

			// Open Results in Excel / Whatever program is default for .csv
			Process.Start(_outputPath);

			Console.WriteLine("File Should be open test...");
			Console.ReadLine();
		}

		public List<string> GetColumnListFromClass(object obj)
		{
			var templist = obj.GetType().GetProperties();
			var columnList = new List<string>();
			foreach (var prop in templist)
			{
				columnList.Add(prop.Name);
			}

			return columnList;
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
			for (int i = 2; i <= serverCount; i++)
			{
				var serverNumber = i.ToString();
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


		public string BuildResultsTableString(List<string> columnList)
		{
			var baseResultList = "DECLARE @ResultsList TABLE (DbServer VARCHAR(75) NULL, DbName VARCHAR(50) NOT NULL, QueryResult varchar(150) NULL,QueryResult1 varchar(150) NULL,QueryResult2 varchar(150) NULL);";

			var query = "DECLARE @ResultsList TABLE (";

			foreach (var col in columnList)
			{
				query = query + col + " varchar(100) NULL, ";
			}
			int index = query.LastIndexOf(',');
			query = query.Remove(index, 1);

			query = query + ");";

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
DECLARE @SQL NVARCHAR(MAX) ", dbLike, dbNotLike);
			return queryText;
		}

		private string GetMostRecentDbUpScriptRan()
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
SET @SQL = 'SELECT Top 1 @@SERVERNAME,'''+@DbName+''', '+'[Version] ,(select AppTierUrl from '+@DbName+'.dbo.GlobalSettings (nolock)) as SiteName
FROM ' + @DbName + '.dbo.DBUpdates with (nolock) order by DbUpdateId desc'
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

		private string GetOneOffQuery()
		{
			var query = @"/****** Run Query on All DB's in Server  ******/
DECLARE @DbList TABLE (
		ID INT IDENTITY(1,1),
		DbName VARCHAR(50) NOT NULL,
		QueryResult varchar(50) NULL)

DECLARE @ResultsList TABLE (
		DbServer VARCHAR(75) NULL,
		DbName VARCHAR(50) NOT NULL,
		QueryResult varchar(150) NULL,
		QueryResult2 varchar(150) NULL)

DECLARE @Cnt INT=1;
DECLARE @DbName VARCHAR(50)='';
DECLARE @TotalDbCount INT;

INSERT INTO @DbList     
SELECT NAME,NULL FROM SYS.DATABASES    
WHERE (NAME LIKE 'CIQ_%' AND Name like '%IPAS%' AND NAME NOT LIKE 'CIQ_%Training%')

SELECT @TotalDbCount = COUNT(1) FROM @DbList

WHILE (@Cnt<=@TotalDbCount)    
BEGIN     
	SELECT @DbName=DbName FROM @DbList WHERE ID=@Cnt;
	
	DECLARE @SQL NVARCHAR(MAX)
	DECLARE @SQL2 NVARCHAR(MAX)

	SET @SQL = 'SELECT distinct @@SERVERNAME,'''+@DbName+''',
	(select right(AppTierUrl, charindex(''/'', reverse(AppTierUrl)) - 1) from '+@DbName+'.dbo.GlobalSettings (nolock)) as SiteName,
	Alias
	from ' + @DbName + '.dbo.InsPlanAliases (nolock)'

	SET @SQL2 = 'SELECT distinct @@SERVERNAME,'''+@DbName+''', 
	(select right(AppTierUrl, charindex(''/'', reverse(AppTierUrl)) - 1) from '+@DbName+'.dbo.GlobalSettings (nolock)) as SiteName,
	InValue 
	from ' + @DbName + '.dbo.Mappings (nolock) WHERE Component = ''ContractAliases'''

	print @DbName
	BEGIN TRY 

	insert into @ResultsList
	EXEC sys.sp_executesql @SQL

	insert into @ResultsList
	EXEC sys.sp_executesql @SQL2


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

--select * from @DbList
select distinct * from @ResultsList order by DbServer,DbName,QueryResult

";
			return query;
		}
	}
}
