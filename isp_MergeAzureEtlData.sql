-- SP to merge data after DF ETL process
--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[isp_MergeAzureEtlData]') AND type in (N'P', N'PC'))
--DROP PROCEDURE [dbo].[isp_MergeAzureEtlData]
--GO
CREATE PROCEDURE [dbo].[isp_MergeAzureEtlData](
@tableName VARCHAR(200)
)
AS 
BEGIN

DECLARE @sql VARCHAR(MAX) = '',
@deleteSql VARCHAR(MAX) = '',
@primaryKeyCount INT = 0,
@primaryKey1 NVARCHAR(100),
@primaryKey2 VARCHAR(100)

IF OBJECT_ID(N'tempdb..#tmpPrimayKeyTable') IS NOT NULL
	  BEGIN DROP TABLE #tmpPrimayKeyTable END

IF OBJECT_ID(N'tempdb..#tmpMergeResults') IS NOT NULL
	  BEGIN DROP TABLE #tmpMergeResults  END

SELECT ROW_NUMBER() OVER (ORDER BY Column_Name ASC) AS RowNumber,COLUMN_NAME
INTO #tmpPrimayKeyTable
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1
AND TABLE_NAME = @tableName

SET @primaryKeyCount = (SELECT COUNT(1) FROM #tmpPrimayKeyTable)

IF(@primaryKeyCount = 1)
BEGIN 
	PRINT 'Single Primary Key'
	SELECT TOP 1 @primaryKey1 =COLUMN_NAME FROM #tmpPrimayKeyTable where RowNumber = 1

	SET @sql = ' Select s.*, p.'+@primaryKey1+' AS Pk'+@primaryKey1+' 
	INTO dbo.#tmpMergeResults 
	FROM dbo.'+ @tableName+'_Staging s 
	INNER JOIN dbo.'+ @tableName+' p ON p.'+@primaryKey1+' = s.'+@primaryKey1

	SET @deleteSql = 'DELETE bm FROM '+@tableName+' bm JOIN #tmpMergeResults t ON bm.'+@primaryKey1+' = t.'+@primaryKey1

END 
ELSE IF(@primaryKeyCount > 1)
BEGIN 
	PRINT 'Multiple Primary Key'
	SELECT TOP 1 @primaryKey1 =COLUMN_NAME FROM #tmpPrimayKeyTable where RowNumber = 1
	SELECT TOP 1 @primaryKey2 =COLUMN_NAME FROM #tmpPrimayKeyTable where RowNumber = 2

	SET @sql = ' Select s.*, p.'+@primaryKey1+' AS Pk'+@primaryKey1+' 
	INTO dbo.#tmpMergeResults 
	FROM dbo.'+ @tableName+'_Staging s 
	INNER JOIN dbo.'+ @tableName+' p ON p.'+@primaryKey1+' = s.'+@primaryKey1+' AND '+'p.'+@primaryKey2+' = s.'+@primaryKey2
	
	SET @deleteSql = 'DELETE bm FROM '+@tableName+' bm JOIN #tmpMergeResults t ON bm.'+@primaryKey1+' = t.'+@primaryKey1+' AND '+'bm.'+@primaryKey2+' = t.'+@primaryKey2

END
ELSE IF(@primaryKeyCount = 0 AND @tableName LIKE '%DtCdmDet%')
BEGIN
	PRINT 'No Primary Key'
	SET @primaryKey1 = 'SYSKEY'
	SET @sql = ' Select s.*, p.'+@primaryKey1+' AS Pk'+@primaryKey1+' 
	INTO dbo.#tmpMergeResults 
	FROM dbo.'+ @tableName+'_Staging s 
	INNER JOIN dbo.'+ @tableName+' p ON p.'+@primaryKey1+' = s.'+@primaryKey1

	SET @deleteSql = 'DELETE bm FROM '+@tableName+' bm JOIN #tmpMergeResults t ON bm.'+@primaryKey1+' = t.'+@primaryKey1

END

IF(@sql IS NOT NULL AND @sql != '')
BEGIN 
	SET @sql = @sql +' '+ @deleteSql
	EXEC (@sql)

	SET @sql = 'INSERT INTO '+@tableName+' SELECT * FROM '+ @tableName+'_Staging'
	EXEC(@sql)

	SET @sql = 'TRUNCATE TABLE '+ @tableName+'_Staging'
	EXEC(@sql)
END
END