using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using Dapper;

namespace DbQueryMultipleServers
{
	public class QueryManager
	{
		private const string OutputFileName = "MultipleQueryDbOutput.csv";
		public void RunQuery()
		{
			var resultList = new List<QueryResults>();
			for (int i = 1; i < 8; i++)
			{
				var serverNumber = i.ToString("D2");
				var clientDb = string.Format("Server=LEWVPPASDB{0}.nthrive.nthcrp.com;Database=Platform_Sub; Trusted_Connection=true;", serverNumber);
				using (var db = new SqlConnection(clientDb))
				{
					db.Open();
					var query = GetQuery();
					var queryResults = db.Query<QueryResults>(query).ToList();
					resultList.AddRange(queryResults);
					db.Close();
				}
			}
		}

		public void RunQueryOnCmDatabases()
		{
			CleanOutputFile(OutputFileName);
			var resultList = new List<QueryResults>();
			for (int i = 1; i < 3; i++)
			{
				var serverNumber = i.ToString("D2");
				//Console.WriteLine(serverNumber);
				var clientDb = string.Format("Server=LEWVPCMGDB{0}.nthrive.nthcrp.com;Database=CBO_Global; Trusted_Connection=true;", serverNumber);
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
			foreach (var result in resultList)
			{
				var outputResult = result.DbServer + "," + result.DbName + "," + result.QueryResult;
				WriteValueToFile(outputResult);
			}
			Console.WriteLine();
			Console.WriteLine();
			var outputPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)) + "\\" + OutputFileName;
			Console.WriteLine(outputPath);
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

	--DECLARE @ClientId int
	--SET @ClientId = (SELECT TOP(1) clientId from @ResultsList where DbName = @DbName AND ClientId != 99001)
	--DECLARE @ClientName VARCHAR(150) = (select TOP(1) Name from aaafacilities where ClientID in (@ClientId))
	
	--UPDATE @ResultsList
	--SET ClientName = @ClientName
	--where DbName = @DbName

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
	}

	public class QueryResults
	{
		public string DbServer { get; set; }
		public string DbName { get; set; }
		public string QueryResult { get; set; }
	}
}
