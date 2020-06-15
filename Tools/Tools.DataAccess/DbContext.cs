using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Tools.DataAccess
{
	public class DbContext
	{
		private readonly IDbConnection _db;

		public DbContext(string connectionString)
		{
			_db = new SqlConnection(connectionString);
		}

		public List<T> GetAllList<T>(string query)
		{
			using (var dbConnection = _db)
			{
				dbConnection.Open();
				var results = dbConnection.Query<T>(query).ToList();
				return results;
			}
		}

		public List<T> GetSingle<T>(string query)
		{
			using (var dbConnection = _db)
			{
				dbConnection.Open();
				var results = dbConnection.Query<T>(query).ToList();
				return results;
			}
		}
	}
}
