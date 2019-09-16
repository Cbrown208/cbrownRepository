using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			var insertValueString = "(";
			var colCounter = 0;
			foreach (var col in columns)
			{

				if (col.Data_Type.Contains("char") || col.Data_Type.Contains("date"))
				{
					insertValueString = insertValueString + "'{"+ colCounter+ "}'";
				}
				if (col.Data_Type.Contains("int") || col.Data_Type.Contains("money")|| col.Data_Type.Contains("double") || col.Data_Type.Contains("bit"))
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

		/// <summary>
		/// Builds Check Statement using Table Name, Unique Column Name value and value to check against 
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="column"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public string BuildCheckStatement(string tableName, string column, string value)
		{
			var checkStatement = "BEGIN" + Environment.NewLine;
			checkStatement = checkStatement + string.Format("IF NOT EXISTS(SELECT 1 FROM [{0}] WHERE {1} = '{2}')", tableName, column, value);
			checkStatement = checkStatement + Environment.NewLine;
			return checkStatement;
		}

		public string BuildEndStatement()
		{
			var checkStatement = "END" + Environment.NewLine+ Environment.NewLine;
			return checkStatement;
		}

		public string RemoveLastCharacterInStringBuilder()
		{
			var sql = new StringBuilder();
			sql.Append("hello,");
			Console.WriteLine("Before = "+ sql);
			sql.Length -= 1;//remove last comma
			Console.WriteLine("After = " + sql);

			return sql.ToString();
		}
	}
}
