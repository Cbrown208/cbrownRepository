using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Tools.Core.Models;

namespace Tools.DataAccess
{
	public class EiDbContext
	{
		private readonly IDbConnection _db;

		public EiDbContext(string connectionString)
		{
			_db = new SqlConnection(connectionString);
		}
		public List<EbPortalMapping> GetEbPortalMappingList()
		{
			using (IDbConnection dbConnection = _db)
			{
				dbConnection.Open();
				var results = dbConnection.Query<EbPortalMapping>(@"SELECT * FROM EbPortalMapping").ToList();
				return results;
			}
		}
	}
}
