using System;
using System.Collections.Generic;
using System.Linq;
using CopyDataUtil.Core.Models.DbModels;

namespace CopyDataUtil.DataAccess
{
	public class QueryBuilder
	{
		public string BuildInsertQuery(string tableName, List<ColumnInfoSchema> columns)
		{
			var insertQuery = string.Format("INSERT INTO [{0}] (", tableName);

			if (columns.Count == 1)
			{
				insertQuery = insertQuery + columns.FirstOrDefault() + ") VALUES";
			}
			for(var i=0; i <= columns.Count - 1; i++)
			{
				var endingSegment = ",";
				if (i == columns.Count - 1)
				{
					endingSegment = ") VALUES";
				}
				insertQuery = insertQuery + columns[i].Column_Name + endingSegment;
			}
			insertQuery = insertQuery + Environment.NewLine;
			return insertQuery;
		}

		public string BuildInsertValueString(List<ColumnInfoSchema> columns)
		{
			var insertValueString = "( ";
			var colCounter = 0;
			foreach (var col in columns)
			{

				if (col.Data_Type.Contains("char") || col.Data_Type.Contains("date"))
				{
					insertValueString = insertValueString + "'{"+ colCounter+ "}'";
				}
				if (col.Data_Type.Contains("int") || col.Data_Type.Contains("money")|| col.Data_Type.Contains("double"))
				{
					insertValueString = insertValueString + "{" + colCounter + "}";
				}
				colCounter++;
				if (colCounter != columns.Count)
				{
					insertValueString = insertValueString + ",";
				}
			}
			insertValueString = insertValueString + ")";
			return insertValueString;
		}

		public string BuildCheckStatement(string tableName, string column, string value)
		{
			var checkStatement = "BEGIN" + Environment.NewLine;
			checkStatement = checkStatement + string.Format("IF NOT exists(select 1 from [{0}] where {1} = '{2}')", tableName, column, value);
			checkStatement = checkStatement + Environment.NewLine;
			return checkStatement;
		}

		public string BuildEndStatement()
		{
			var checkStatement = "END" + Environment.NewLine+ Environment.NewLine;
			return checkStatement;
		}
	}
}
