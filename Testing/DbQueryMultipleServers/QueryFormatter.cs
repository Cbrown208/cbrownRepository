using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbQueryMultipleServers
{
	public class QueryFormatter
	{
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

		public string GetColumnCsvStringFromClass(object obj)
		{
			var templist = obj.GetType().GetProperties();
			var columnList = new List<string>();
			foreach (var prop in templist)
			{
				columnList.Add(prop.Name);
			}
			var csvList = string.Join(",", columnList);
			return csvList;
		}
	}
}
