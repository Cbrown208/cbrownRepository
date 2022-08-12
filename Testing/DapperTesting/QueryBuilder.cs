using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperTesting
{
	public class QueryBuilder
	{
		public string BuildInsertQuery(string tableName, List<string> columns)
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
				insertQuery = insertQuery + columns[i] + endingSegment;
			}
			insertQuery = insertQuery + Environment.NewLine;
			return insertQuery;
		}

		public string BuildCheckStatement(string tableName, string column, string value)
		{
			var checkStatement = string.Format("IF NOT exists(select 1 from [{0}] where {1} = '{2}')", tableName, column, value);
			checkStatement = checkStatement + Environment.NewLine+ "BEGIN" + Environment.NewLine; 
			return checkStatement;
		}

		public string BuildEndStatement()
		{
			var checkStatement = "END" + Environment.NewLine+ Environment.NewLine;
			return checkStatement;
		}

		public string BuildPccInsertStatement(string sName, string sDescription)
		{
			var checkStatement = string.Format("IF NOT EXISTS(SELECT 1 FROM [Servers] WHERE ServerName = '{0}' AND Environment = 5)", sName);
			var result = checkStatement + Environment.NewLine + "BEGIN" + Environment.NewLine;
			result = result + "INSERT INTO[dbo].[Servers]([ServerName],[ServerDescription],[Environment],[Status],[IsActive],[LastUpdated]) VALUES" + Environment.NewLine;
			result = result + string.Format("('{0}', '{1}', 5, 0, 1, GETDATE())", sName, sDescription) + Environment.NewLine + "END" + Environment.NewLine + Environment.NewLine;
			return result;
		}
	}
}
