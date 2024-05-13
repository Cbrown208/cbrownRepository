using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Dapper;
using DbQueryMultipleServers.Queries;

namespace DbQueryMultipleServers
{
	public class QueryManager
	{
		private readonly QueryFormatter _formatter = new QueryFormatter();
		private readonly PasQueries _pasQueries = new PasQueries();
		private const string OutputFileName = "MultipleQueryDbOutput.csv";

		private readonly string _outputPath =
			Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)) +
			"\\" + OutputFileName;

		private CiqQueryManager ciqManager = new CiqQueryManager();

		public void RunMultipleDbQuery()
		{
			var pasDbServerConnection =
				"Server=LEWVPPASDB{0}.nthrive.nthcrp.com;Database=Platform_Sub; Trusted_Connection=true;";
			var pasDbClientDbCount = 7;
			var cmDbConnection =
				"Server=LEWVPCMGDB{0}.nthrive.nthcrp.com;Database=CBO_Global; Trusted_Connection=true;";

			var dbList = GetDbConnectionStringList(pasDbServerConnection, pasDbClientDbCount);

			//RunQuery(pasDbServerConnection, pasDbClientDbCount);
			RunFacilityOptionQuery(pasDbServerConnection, pasDbClientDbCount);


			Console.WriteLine(Environment.NewLine + Environment.NewLine + _outputPath);

			// Open Results in Excel / Whatever program is default for .csv
			Process.Start(_outputPath);

			Console.WriteLine("File Should be open test...");
		}

		private List<string> GetDbConnectionStringList(string dbConnectionString, int serverCount)
		{
			var dbConnectionStringList = new List<string>();
			for (int i = 1; i <= serverCount; i++)
			{
				var serverNumber = i.ToString("D2");
				var clientDb = string.Format(dbConnectionString, serverNumber);
				dbConnectionStringList.Add(clientDb);
			}

			return dbConnectionStringList;
		}

		public void RunQuery(string dbConnectionString, int serverCount)
		{
			CleanOutputFile(OutputFileName);
			var resultList = new List<PasQueryResultsResults>();
			for (int i = 1; i <= serverCount; i++)
			{
				var serverNumber = i.ToString("D2");
				var clientDb = string.Format(dbConnectionString, serverNumber);

				using (var db = new SqlConnection(clientDb))
				{
					
					Dapper.SqlMapper.Settings.CommandTimeout = 0;
					db.Open();
					var query = GetClientDbQuery();
					query = GetOneOffQuery();
					query = _pasQueries.GetActiveClientsQuery();
					query = GetTempQuery();
					var queryResults = db.Query<PasQueryResultsResults>(query).ToList();
					resultList.AddRange(queryResults);
					db.Close();
				}
			}

			var csvList = _formatter.GetColumnCsvStringFromClass(new PasQueryResultsResults());
			WriteValueToFile(csvList);


			//WriteValueToFile("DbServer,DbName,QueryResults");
			foreach (var result in resultList)
			{
				var facName = "";
				if (result.FacilityName != null)
				{
					facName = result.FacilityName.Replace(",", "");
				}



				//var outputResult = result.DbServer + "," + result.DbName + "," + result.QueryResult;
				var outputResult = result.DbServer + "," + result.DbName + "," + result.ClientId + "," +
				                   result.NthriveId + "," + facName + "," + result.FacilityId + "," +
				                   result.QueryResult + "," + result.QueryResult2 + "," + result.QueryResult3;
				WriteValueToFile(outputResult);
			}

		}

		public void RunFacilityOptionQuery(string dbConnectionString, int serverCount)
		{
			var facOptionIds = "810";
			CleanOutputFile(OutputFileName);
			var resultList = new List<FacilityOptionQueryResults>();
			for (int i = 1; i <= serverCount; i++)
			{
				var serverNumber = i.ToString("D2");
				var clientDb = string.Format(dbConnectionString, serverNumber);

				using (var db = new SqlConnection(clientDb))
				{
					db.Open();
					var query = GetFacilityOptionQuery(facOptionIds);
					var queryResults = db.Query<FacilityOptionQueryResults>(query).ToList();
					resultList.AddRange(queryResults);
					db.Close();
				}
			}

			var csvList = _formatter.GetColumnCsvStringFromClass(new FacilityOptionQueryResults());
			WriteValueToFile(csvList);

			//WriteValueToFile("DbServer,DbName,QueryResults");
			foreach (var result in resultList)
			{
				var facName = "";
				if (result.FacilityName != null)
				{
					facName = result.FacilityName.Replace(",", "");
				}

				var outputResult = result.DbServer + "," + result.DbName + "," + result.ClientId + "," +
				                   facName + "," + result.FacilityId+ "," + result.FacilityOptionName + "," +
				                   result.FacilityOptionTypeId + "," + result.OptionValue;
				WriteValueToFile(outputResult);
			}
		}

		public void RunQueryOnCmDatabases()
		{
			CleanOutputFile(OutputFileName);
			var resultList = new List<QueryResults>();
			for (int i = 1; i < 3; i++)
			{
				var serverNumber = i.ToString("D2");
				var clientDb =
					string.Format(
						"Server=LEWVPCMGDB{0}.nthrive.nthcrp.com;Database=CBO_Global; Trusted_Connection=true;",
						serverNumber);

				using (var db = new SqlConnection(clientDb))
				{
					db.Open();
					var query = GetQuery();
					var queryResults = db.Query<QueryResults>(query).ToList();
					resultList.AddRange(queryResults);
					db.Close();
				}
			}

			WriteValueToFile("DbServer,DbName,QueryResults");
			//foreach (var result in resultList)
			//{
			//	var outputResult = result.DbServer + "," + result.DbName + "," + result.QueryResult;
			//	WriteValueToFile(outputResult);
			//}
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


		public string GetDbList()
		{
			return @"SELECT NAME as DbName FROM SYS.DATABASES
					 WHERE (NAME LIKE 'AMS_%' AND NAME NOT LIKE 'AMS_%DEMO%' AND NAME NOT LIKE 'AMS_Standard' 
					 AND NAME NOT LIKE 'AMS_Internal' AND NAME NOT LIKE 'AMS_HL7')";
		}

		public string GetQuery()
		{
			return @"/****** Run Query on All DB's in Server  ******/
DECLARE @DbList TABLE (
		ID INT IDENTITY(1,1),
		ClientId INT NULL,
		ClientName VARCHAR(100) NULL,
		NAME VARCHAR(50) NOT NULL,
		Counts INT NULL,
		QueryResult varchar(50) NULL)

DECLARE @ResultsList TABLE (
		DbServer VARCHAR(75) NULL,
		DbName VARCHAR(50) NOT NULL,
		QueryResult varchar(150) NULL)

DECLARE @Cnt INT=1;
DECLARE @DbName VARCHAR(50)='';
DECLARE @TotalDbCount INT;    
DECLARE @Date30 DATETIME;    
SET @Date30=DateAdd(d, -30, getdate()) --for cleaning table data before 30 days    

INSERT INTO @DbList     
SELECT null,null,NAME,NULL,NULL FROM SYS.DATABASES
WHERE name != 'model' AND name != 'DBA_DB' AND name != 'LiteSpeedLocal'
--WHERE (NAME LIKE 'AMS_%' AND NAME NOT LIKE 'AMS_%DEMO%' AND NAME NOT LIKE 'AMS_Standard' AND NAME NOT LIKE 'AMS_Internal' AND NAME NOT LIKE 'AMS_HL7')

SELECT @TotalDbCount = COUNT(1) FROM @DbList  

WHILE (@Cnt<=@TotalDbCount)    
BEGIN     
	SELECT @DbName=NAME     
	FROM @DbList     
	WHERE ID=@Cnt;
	
	DECLARE @SQL NVARCHAR(MAX)

	SET @SQL = 'SELECT @@SERVERNAME,'''+@DbName+''', '+
				--+'CAST(format(COUNT(1), ''N0'') as varchar(150)) '+ -- Integer Results with commas 
				'[Name] from ' + @DbName + '.sys.database_principals WHERE name = ''nthrive\svc_rafweb_prod'''
	print @DbName
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

--select * from @DbList where QueryResult != 'Successful'
select * from @ResultsList";
		}

		// Used to get Misc Queries on Client DB's
		public string GetClientDbQuery()
		{
			var queryLoopStart = @"/****** Run Query on All DB's in Server  ******/
				DECLARE @DbList TABLE (
				ID INT IDENTITY(1,1),
				ClientId INT NULL,
				ClientName VARCHAR(100) NULL,
				NAME VARCHAR(50) NOT NULL,
				Counts INT NULL,
				QueryResult varchar(50) NULL)

				DECLARE @ResultsList TABLE (
				DbServer VARCHAR(75) NULL,
				DbName VARCHAR(50) NOT NULL,
				QueryResult varchar(150) NULL)

				DECLARE @Cnt INT=1;
				DECLARE @DbName VARCHAR(50)='';
				DECLARE @TotalDbCount INT;    
				
				INSERT INTO @DbList     
				SELECT null,null,NAME,NULL,NULL FROM SYS.DATABASES
				WHERE (NAME LIKE 'AMS_%' AND NAME NOT LIKE 'AMS_%DEMO%' AND NAME NOT LIKE 'AMS_Standard' AND NAME NOT LIKE 'AMS_Internal' AND NAME NOT LIKE 'AMS_HL7')

				SELECT @TotalDbCount = COUNT(1) FROM @DbList 
				
				WHILE (@Cnt<=@TotalDbCount)   
				BEGIN   
				SELECT @DbName=NAME   
				FROM @DbList   
				WHERE ID=@Cnt;
				
				DECLARE @SQL NVARCHAR(MAX)";

			var searchQuery = @"
				SET @SQL = 'SELECT @@SERVERNAME,'''+@DbName+''', '+
				--+'CAST(format(COUNT(1), ''N0'') as varchar(150)) '+ -- Integer Results with commas 
				+'FORMAT (MAX(CreatedOn), ''yyyy-MM-dd'')'+ 
				+'FROM ' + @DbName + '.dbo.PosCollection (nolock)'";

			var queryLoopEnd = @"
				print @DbName
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

			return queryLoopStart + searchQuery + queryLoopEnd;
		}

		public string GetFacilityOptionQuery(string facilityOptionTypeIds)
		{
			var queryLoopStart = @"/****** Run Query on All DB's in Server  ******/
				DECLARE @DbList TABLE (
				ID INT IDENTITY(1,1),
				ClientId INT NULL,
				ClientName VARCHAR(100) NULL,
				NAME VARCHAR(50) NOT NULL,
				Counts INT NULL,
				QueryResult varchar(50) NULL)

DECLARE @ResultsList TABLE (
	DbServer VARCHAR(75) NULL,
	DbName VARCHAR(50) NOT NULL,
	ClientId int NULL,    
	FacilityName VARCHAR(150) NULL,
	[FacilityId] [uniqueidentifier] NULL,
	[FacilityOptionName] varchar(150) NULL,
	[FacilityOptionTypeId] [int] NULL,
	[OptionValue] [varchar](max) NULL)

				DECLARE @Cnt INT=1;
				DECLARE @DbName VARCHAR(50)='';
				DECLARE @TotalDbCount INT;    
				
				INSERT INTO @DbList     
				SELECT null,null,NAME,NULL,NULL FROM SYS.DATABASES
				WHERE (NAME LIKE 'AMS_%' AND NAME NOT LIKE 'AMS_%DEMO%' AND NAME NOT LIKE 'AMS_Standard' AND NAME NOT LIKE 'AMS_Internal' AND NAME NOT LIKE 'AMS_HL7')

				SELECT @TotalDbCount = COUNT(1) FROM @DbList 
				
				WHILE (@Cnt<=@TotalDbCount)   
				BEGIN   
				SELECT @DbName=NAME   
				FROM @DbList   
				WHERE ID=@Cnt;
				
				DECLARE @SQL NVARCHAR(MAX)";

			var searchQuery = @"
					SET @SQL = '	
	SELECT @@SERVERNAME,'''+@DbName+'''as DbName,fac.ClientId as ClientId, fac.Name as FacilityName, f.FacilityId, ft.Name as FacilityOptionName, f.FacilityOptionTypeId,OptionValue 
	FROM ' + @DbName + '.[dbo].[FacilityOptionValue] (nolock) f
	LEFT OUTER JOIN ' + @DbName + '.[dbo].[FacilityOptionType] (nolock) ft ON f.FacilityOptionTypeId = ft.FacilityOptionTypeId
	LEFT OUTER JOIN aaaFacilities (nolock) fac ON f.FacilityId = fac.FacilityId
	WHERE f.FacilityOptionTypeId in (" + facilityOptionTypeIds + ") --AND f.OptionValue = ''TRUE'''";

			var queryLoopEnd = @"
				print @DbName
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

			return queryLoopStart + searchQuery + queryLoopEnd;
		}

		public string GetOneOffQuery()
		{
			var queryLoopStart = @"/****** Run Query on All DB's in Server  ******/
				DECLARE @DbList TABLE (
				ID INT IDENTITY(1,1),
				ClientId INT NULL,
				ClientName VARCHAR(100) NULL,
				NAME VARCHAR(50) NOT NULL,
				Counts INT NULL,
				QueryResult varchar(50) NULL)

				DECLARE @ResultsList TABLE (
				DbServer VARCHAR(75) NULL,
				DbName VARCHAR(50) NOT NULL,
				QueryResult varchar(150) NULL)

				DECLARE @Cnt INT=1;
				DECLARE @DbName VARCHAR(50)='';
				DECLARE @TotalDbCount INT;    
				
				INSERT INTO @DbList     
				SELECT null,null,NAME,NULL,NULL FROM SYS.DATABASES
				WHERE name != 'model' and name != 'DBA_DB'

				SELECT @TotalDbCount = COUNT(1) FROM @DbList 
				
				WHILE (@Cnt<=@TotalDbCount)   
				BEGIN   
				SELECT @DbName=NAME   
				FROM @DbList   
				WHERE ID=@Cnt;
				
				DECLARE @SQL NVARCHAR(MAX)";

			var searchQuery = @"
	SET @SQL = 'SELECT @@SERVERNAME,'''+@DbName+''', name as UserName '
	+'FROM ' + @DbName + '.sys.database_principals WHERE name like ''%P-TSG%''' ";

			var queryLoopEnd = @"
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

			return queryLoopStart + searchQuery + queryLoopEnd;
		}


		public string GetTempQuery()
		{
			return @"/****** Run Query on All DB's in Server  ******/
DECLARE @DbList TABLE (ID INT IDENTITY(1,1), DbName VARCHAR(50) NOT NULL, QueryResult varchar(50) NULL)

DECLARE @ResultsList TABLE (
		DbServer VARCHAR(75) NULL,
		DbName VARCHAR(50) NOT NULL,
		QueryResult varchar(150) NULL)

DECLARE @Cnt INT=1;
DECLARE @DbName VARCHAR(50)='';
DECLARE @TotalDbCount INT;
DECLARE @YearsToKeep int = 2

DECLARE @admitDateStopAt varchar(50) = (CAST(DATEADD(year, (-1)*@YearsToKeep, GETDATE()) AS varchar(40)))

INSERT INTO @DbList     
SELECT NAME,NULL FROM SYS.DATABASES    
WHERE (NAME LIKE 'AMS_%' AND NAME NOT LIKE 'AMS_%DEMO%' AND NAME NOT LIKE 'AMS_Standard' AND NAME NOT LIKE 'AMS_Internal' AND NAME NOT LIKE 'AMS_HL7')

SELECT @TotalDbCount = COUNT(1) FROM @DbList

WHILE (@Cnt<=@TotalDbCount)    
BEGIN     
	SELECT @DbName=DbName     
	FROM @DbList     
	WHERE ID=@Cnt;
	
	DECLARE @SQL NVARCHAR(MAX)

	SET @SQL = 'SELECT @@SERVERNAME,'''+@DbName+''', '+
				+'COUNT(1) '+ -- Integer Results with commas 
				+'FROM ' + @DbName + '.dbo.Account a with (nolock) WHERE AdmitDateTime < '''+@admitDateStopAt+''' GROUP BY a.ClientId'
	print @DbName
	BEGIN TRY 
	print @SQL

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

--select * from @DbList
select * from @ResultsList";
		}
}

public class QueryResults
	{
		public string DbServer { get; set; }
		public string DbName { get; set; }
		public string QueryResult { get; set; }
		public string QueryResult2 { get; set; }
		public string QueryResult3 { get; set; }
	}

	public class PasQueryResultsResults
	{
		public string DbServer { get; set; }
		public string DbName { get; set; }
		public string ClientId { get; set; }
		public string NthriveId { get; set; }
		public string FacilityName { get; set; }
		public string FacilityId { get; set; }
		public string QueryResult { get; set; }
		public string QueryResult2 { get; set; }
		public string QueryResult3 { get; set; }
	}

	public class FacilityOptionQueryResults
	{
		public string DbServer { get; set; }
		public string DbName { get; set; }
		public int? ClientId { get; set; }
		public string FacilityName { get; set; }
		public Guid FacilityId { get; set; }
		public string FacilityOptionName { get; set; }
		public string FacilityOptionTypeId { get; set; }
		public string OptionValue { get; set; }
	}
}
