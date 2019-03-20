using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CopyDataUtil.Core.Models;
using CopyDataUtil.Core.Models.DbModels;
using Dapper;
using NLog;

namespace CopyDataUtil.DataAccess
{

	public class DbContext
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private readonly QueryBuilder _queryBuilder;
		private string ConnectionString;

		public DbContext(string connectionString)
		{
			ConnectionString = connectionString;
			_queryBuilder = new QueryBuilder();
		}

		public List<TableInfoSchema> GetListofTableNames()
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				dbConnection.Open();
				var results = dbConnection.Query<TableInfoSchema>(@"SELECT * FROM Information_Schema.Tables where Table_Name not like '%Staging%' AND TABLE_NAME Not Like '%_Ref%' AND TABLE_NAME IN('BatchSequencor',
'BillDiag',
'BillMast',
'BillPayrClassSmry',
'BillPhys',
'BillProc',
'BillRevc',
'ChangeTrackingVersion',
'ChargeData',
'ChargeDataCustomFieldMaster',
'ChargeDataCustomFields',
'ClmChgx',
'CpBatchExcept',
'CpBatchExptDtls',
'CpBatchPkgPatnum',
'CpBatchReturn',
'CpConfidenceLevels',
'CpDrg',
'CpDtDiag',
'CpDtProc',
'CPLOGPKGPARAM',
'CpMedicalCptMap',
'CpProCptExcl',
'CpRevcodeDetails',
'CpWorkingParams',
'DrgGrouper',
'DtCdmDet',
'DtDiag',
'DtDrg',
'DtRev',
'ErroredParams',
'GlDrgDet',
'GlDrgWt',
'Glt1det',
'GlTable1',
'ScProcessConfig',
'TbProc',
'TbProcMessage',
'TempClaimData',
'TempParams',
'UniDef') ORDER BY Table_Name").ToList();
				return results;
			}
		}

		public List<ColumnInfoSchema> GetColumnsNamesForTable(string tableName)
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				dbConnection.Open();
				//var query = @"SELECT * FROM Information_Schema.Columns where Table_Name = '" + tableName + "' ORDER BY Column_Name";
				var query = @"SELECT * FROM Information_Schema.Columns where Table_Name = '" + tableName + "'";
				var results = dbConnection.Query<ColumnInfoSchema>(query).ToList();
				return results;
			}
		}

		public List<ColumnInfoSchema> CustomChemaCheckTable(string tableName)
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				dbConnection.Open();
				var query = new StringBuilder();
				query.Append(@"SELECT * FROM Information_Schema.Columns where ");
				query.Append(
					@"(Column_Name like '%ModifiedOn%' OR Column_Name like '%ModifiedDate%' OR Column_Name like 'Mdate%') AND TABLE_NAME NOT LIKE '%_Staging%'");
				var results = dbConnection.Query<ColumnInfoSchema>(query.ToString()).ToList();
				return results;
			}
		}

		public List<ColumnInfoSchema> GetColumnsNamesForTable(string tableName, List<string> columnsToSkip)
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				var queryString = @"SELECT * FROM Information_Schema.Columns where Table_Name = '" + tableName + "' ";
				if (columnsToSkip != null && columnsToSkip.Any())
				{
					queryString = queryString + "AND (";
					var i = 0;
					foreach (var col in columnsToSkip)
					{
						if (i < columnsToSkip.Count && i != 0)
						{
							queryString = queryString + "OR";
						}

						queryString = queryString + " Column_Name NOT LIKE '%" + col + "%' ";
						if (i == columnsToSkip.Count)
						{
							queryString = queryString + ")";
						}
					}
				}
				//queryString = queryString + @" ORDER BY Column_Name";
				dbConnection.Open();
				var results = dbConnection.Query<ColumnInfoSchema>(queryString).ToList();
				return results;
			}
		}

		public List<dynamic> GetTableData(string tableName)
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				dbConnection.Open();
				var query = @"SELECT TOP 999 * FROM " + tableName;
				var results = dbConnection.Query(query).ToList();
				return results;
			}
		}

		public bool PurgeTableData(string tableName)
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				dbConnection.Open();
				var truncateQuery = "TRUNCATE TABLE " + tableName;
				try
				{
					dbConnection.Query(truncateQuery);
					return true;
				}
				catch (Exception ex)
				{
					Logger.Error(ex);
					return false;
				}
			}
		}

		public string GetInsertSetupString(string tableName)
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				dbConnection.Open();
				var results = dbConnection.Query<string>(@"select 'data.' + Column_Name from INFORMATION_SCHEMA.COLUMNS where table_name like '" + tableName + "' order by ORDINAL_POSITION").ToList();

				var stringResults = string.Join(",", results);
				return stringResults;
			}
		}

		public List<dynamic> GetAllValuesFromTable(string tableName)
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				dbConnection.Open();
				var results = dbConnection.Query<dynamic>(@"select * from " + tableName).ToList();
				return results;
			}
		}

		public bool UpdateValueInTable(List<UpdateValueDetails> updateValueList)
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				dbConnection.Open();
				foreach (var valueDetail in updateValueList)
				{
					var sqlString = @"UPDATE " + valueDetail.TableName + " SET " + valueDetail.ColumnName + " = '" + valueDetail.UpdateValue + "' WHERE " +
									valueDetail.UniqueColumnName + " = '" + valueDetail.UniqueColumnValue + "'";
					try
					{
						var results = dbConnection.Query(sqlString);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						throw;
					}
				}
				return true;
			}
		}

		public string InsertTableData(string tableName, List<ColumnInfoSchema> columnNames, List<dynamic> tableDataList)
		{
			using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
			{
				dbConnection.Open();
				var insertQuery = _queryBuilder.BuildInsertQuery(tableName, columnNames);
				var queryValueList = "";
				var dataCount = 0;
				foreach (var data in tableDataList)
				{
					var query = _queryBuilder.BuildInsertValueString(columnNames);
					queryValueList = queryValueList + string.Format(query, data.SYSKEY,
						data.PATTYPE,
						data.DATEFROM,
						data.DATETO,
						data.LOSMAX,
						data.LOSMIN,
						data.CDATE,
						data.CTIME,
						data.CUSER,
						data.MDATE,
						data.MTIME,
						data.MUSER,
						data.DOMDEF,
						data.EFF_DATE,
						data.TRM_DATE,
						data.NPI);
					dataCount++;
					if (dataCount < tableDataList.Count)
					{
						queryValueList = queryValueList + ", ";
					}
				}
				insertQuery = insertQuery + queryValueList;
				var temp = queryValueList;
				//var results = "";

				var results = dbConnection.Execute(insertQuery);
				return "";
			}
		}

		public string BuildIdempotentInsertScript(string tableName, List<ColumnInfoSchema> columnNames, List<dynamic> tableDataList)
		{
			var resultQuery = "";

			var schemaList = CustomChemaCheckTable(tableName);

			var insertStatement = _queryBuilder.BuildInsertQuery(tableName, columnNames);
			var endStatement = _queryBuilder.BuildEndStatement();
			foreach (var data in tableDataList)
			{
				var queryValueList = "";
				var temp = data;
				var checkStatement = _queryBuilder.BuildCheckStatement(tableName, "FacId", data.FacId);
				var query = _queryBuilder.BuildInsertValueString(columnNames);

				//foreach (var val in data)
				//{
				//	Console.WriteLine("Key: "+val.Key);
				//	Console.WriteLine("Value: "+val.Value);

				//}

				//var doesTableExsist = schemaList.FirstOrDefault(x => x.Table_Name == data.TableName);
				//var customColumnName = "";
				//var customColumnType = "";
				//var customModifiedDate = "";
				//var customGetEntierTable = 1;
				//if (doesTableExsist != null)
				//{
				//	customColumnName = doesTableExsist.Column_Name;
				//	customColumnType = "DateTime";
				//	customModifiedDate = DateTime.MinValue.ToString("yyyy/MM/dd");
				//	customGetEntierTable = 0;
				//}

				//queryValueList = queryValueList + string.Format(query, data.TableName, customColumnName, customColumnType, customModifiedDate, customGetEntierTable);
				resultQuery = resultQuery + checkStatement + insertStatement + queryValueList + endStatement;
			}
			return resultQuery;
		}
	}
}
