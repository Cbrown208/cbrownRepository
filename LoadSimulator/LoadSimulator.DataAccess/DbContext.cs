using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace LoadSimulator.DataAccess
{
	public class DbContext
	{
		private readonly IDbConnection _db;
		private readonly QueryBuilder _queryBuilder;

		public DbContext()
		{
			_db = new SqlConnection(@"Server=.;Database=CommandCenter.Local;Trusted_Connection=true;");
			_queryBuilder = new QueryBuilder();
		}

		public dynamic GetById(int id)
		{
			using (IDbConnection dbConnection = _db)
			{
				string sQuery = "SELECT * FROM Products"
								+ " WHERE ProductId = @Id";
				dbConnection.Open();
				var result = dbConnection.Query(sQuery, new { Id = id }).FirstOrDefault();
				return result;
			}
		}

		public void Delete(int id)
		{
			using (IDbConnection dbConnection = _db)
			{
				string sQuery = "DELETE FROM Products"
								+ " WHERE ProductId = @Id";
				dbConnection.Open();
				dbConnection.Execute(sQuery, new { Id = id });
			}
		}

		public void Update(object prod)
		{
			using (IDbConnection dbConnection = _db)
			{
				string sQuery = "UPDATE Products SET Name = @Name,"
								+ " Quantity = @Quantity, Price= @Price"
								+ " WHERE ProductId = @ProductId";
				dbConnection.Open();
				dbConnection.Query(sQuery, prod);
			}
		}
	}
}
