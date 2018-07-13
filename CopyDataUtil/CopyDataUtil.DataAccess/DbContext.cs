using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CopyDataUtil.Core.Models;
using CopyDataUtil.Core.Models.DbModels;
using Dapper;

namespace CopyDataUtil.DataAccess
{
	public class DbContext
	{
		private readonly IDbConnection _db;
		private readonly QueryBuilder _queryBuilder;
		private string ConnectionString;
		private IDbConnection _dbConnection;

		public DbContext(string connectionString)
		{
			ConnectionString = connectionString;
			_db = new SqlConnection(ConnectionString);
			_queryBuilder = new QueryBuilder();
		}

		public void RenewConnection()
		{
			_dbConnection = new SqlConnection(ConnectionString);
		}

		public List<TableInfoSchema> GetListofTableNames()
		{
			RenewConnection();
			using (IDbConnection dbConnection = _dbConnection)
			{
				dbConnection.Open();
				var results = dbConnection.Query<TableInfoSchema>(@"SELECT * FROM Information_Schema.Tables where Table_Name not like '%Staging%' AND TABLE_NAME Not Like '%_Ref%' ORDER BY Table_Name").ToList();
				return results;
			}
		}

		public List<ColumnInfoSchema> GetColumnsNamesForTable(string tableName)
		{
			RenewConnection();
			using (IDbConnection dbConnection = _dbConnection)
			{
				dbConnection.Open();
				var results = dbConnection.Query<ColumnInfoSchema>(@"SELECT * FROM Information_Schema.Columns where Table_Name = '"+ tableName +"' ORDER BY Ordinal_Position").ToList();

				return results;
			}
		}

		public List<dynamic> GetTableData(string tableName)
		{
			RenewConnection();
			using (IDbConnection dbConnection = _dbConnection)
			{
				dbConnection.Open();
				var results = dbConnection.Query(@"SELECT TOP 999 * FROM "+ tableName).ToList();
				return results;
			}
		}

		public string GetInsertSetupString(string tableName)
		{
			RenewConnection();
			using (IDbConnection dbConnection = _dbConnection)
			{
				dbConnection.Open();
				var results = dbConnection.Query<string>(@"select 'data.' + Column_Name from INFORMATION_SCHEMA.COLUMNS where table_name like '"+tableName+"' order by ORDINAL_POSITION").ToList();

				var stringResults = string.Join(",", results);
				return stringResults;
			}
		}

		public List<dynamic> GetAllValuesFromTable(string tableName)
		{
			RenewConnection();
			using (IDbConnection dbConnection = _dbConnection)
			{
				dbConnection.Open();
				var results = dbConnection.Query<dynamic>(@"select * from " + tableName).ToList();
				return results;
			}
		}

		public bool UpdateValueInTable(List<UpdateValueDetails> updateValueList)
		{
			RenewConnection();
			using (IDbConnection dbConnection = _dbConnection)
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
			RenewConnection();
			using (IDbConnection dbConnection = _dbConnection)
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
	}
}
