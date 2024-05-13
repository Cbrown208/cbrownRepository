namespace DbQueryMultipleServers.Queries
{
	public class PasQueries
	{
		public string GetActiveClientsQuery()
		{
			var query = @"/****** Run Query on All DB's in Server  ******/
DECLARE @DbList TABLE (ID INT IDENTITY(1,1),DbName VARCHAR(50) NOT NULL,QueryResult varchar(50) NULL)

DECLARE @ResultsList TABLE (
		DbServer VARCHAR(75) NULL,
		DbName VARCHAR(50) NOT NULL,
		ClientId varchar(150) NULL,
		NthriveId varchar(150) NULL,
		FacilityName varchar(150) NULL,
		FacilityId varchar(150) NULL,
		QueryResult varchar(150) NULL)

DECLARE @ResultsList2 TABLE (
		DbServer VARCHAR(75) NULL,
		DbName VARCHAR(50) NOT NULL,
		ClientId varchar(150) NULL,
		NthriveId varchar(150) NULL,
		FacilityName varchar(150) NULL,
		FacilityId varchar(150) NULL,
		QueryResult2 varchar(150) NULL)

DECLARE @ResultsList3 TABLE (
		DbServer VARCHAR(75) NULL,
		DbName VARCHAR(50) NOT NULL,
		ClientId varchar(150) NULL,
		NthriveId varchar(150) NULL,
		FacilityName varchar(150) NULL,
		FacilityId varchar(150) NULL,
		QueryResult3 varchar(150) NULL)

DECLARE @Cnt INT=1;
DECLARE @DbName VARCHAR(50)='';
DECLARE @TotalDbCount INT;

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
	DECLARE @SQL2 NVARCHAR(MAX)
	DECLARE @SQL3 NVARCHAR(MAX)

	SET @SQL = 'SELECT @@SERVERNAME,'''+@DbName+''', '+
	+ 'a.ClientId,f.MedassetsId ,f.Name as FacilityName,a.FacilityId,'
				+'CAST(FORMAT(MAX(a.CreatedOn), ''MM-dd-yy'') as varchar(150)) '+
				+'FROM ' + @DbName + '.dbo.Account a with (nolock) 
				LEFT OUTER JOIN ' + @DbName + '.dbo.aaafacilities (nolock) f ON a.FacilityId = f.FacilityId
				WHERE a.ClientId != 99001 
				GROUP BY a.ClientId,f.MedassetsId,f.Name,a.FacilityId'

	SET @SQL2 = 'SELECT @@SERVERNAME,'''+@DbName+''', '+
	+'f.ClientId,f.MedassetsId as nThriveId, f.Name as FacilityName, t.FacilityId, CAST(FORMAT(MAX(t.CreatedOn), ''MM-dd-yy'') as varchar(150)) 
  FROM ' + @DbName + '.[dbo].[HL7Transaction] t (nolock)
  LEFT OUTER JOIN ' + @DbName + '.dbo.aaaFacilities f (nolock) ON f.FacilityId = t.FacilityId
GROUP BY f.ClientId,f.MedassetsId,f.Name, t.FacilityId
ORDER BY f.Name'

	SET @SQL3= 'SELECT @@SERVERNAME,'''+@DbName+''', '+
	+'f.ClientId,f.MedassetsId as nThriveId, f.Name as FacilityName, e.FacilityId, CAST(FORMAT(MAX(e.CreatedOn), ''MM-dd-yy'') as varchar(150)) 
  FROM ' + @DbName + '.[dbo].[Estimate] e (nolock)
  LEFT OUTER JOIN ' + @DbName + '.dbo.aaaFacilities f (nolock) ON f.FacilityId = e.FacilityId
GROUP BY f.ClientId,f.MedassetsId,f.Name, e.FacilityId
ORDER BY f.Name'

	print @DbName
	PRINT @SQL2
	BEGIN TRY 

	insert into @ResultsList
	EXEC sys.sp_executesql @SQL

	insert into @ResultsList2
	EXEC sys.sp_executesql @SQL2

		insert into @ResultsList3
	EXEC sys.sp_executesql @SQL3

	UPDATE @DbList SET QueryResult = 'Successful' where DbName = @DbName
	END TRY

	BEGIN CATCH
	UPDATE @DbList SET QueryResult = 'Failed' where DbName = @DbName
	END CATCH

	SET @Cnt=@Cnt+1; 
END

--select * from @DbList
--select * from @ResultsList

select r1.*,r2.QueryResult2, r3.QueryResult3
FROM @ResultsList r1
LEFT OUTER JOIN @ResultsList2 r2 ON r1.FacilityId = r2.FacilityId AND r1.ClientId = r2.ClientId
LEFT OUTER JOIN @ResultsList3 r3 ON r1.FacilityId = r3.FacilityId AND r1.ClientId = r3.ClientId";

			return query;
		}
	}
}
