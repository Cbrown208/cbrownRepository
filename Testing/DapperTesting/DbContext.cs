using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperTesting.Models;

namespace DapperTesting
{
	public class DbContext
	{
		private readonly IDbConnection _db;

		public DbContext()
		{
			_db = new SqlConnection(@"Server=.;Database=CommandCenter.Local;Trusted_Connection=true;");
		}

		public void Add(object prod)
		{
			using (IDbConnection dbConnection = _db)
			{
				string sQuery = "INSERT INTO Products (Name, Quantity, Price)"
								+ " VALUES(@Name, @Quantity, @Price)";
				dbConnection.Open();
				dbConnection.Execute(sQuery, prod);
			}
		}

		public IEnumerable<HealthChecks> GetAll()
		{
			using (IDbConnection dbConnection = _db)
			{
				dbConnection.Open();
				var results = dbConnection.Query<HealthChecks>("SELECT * FROM dbo.HealthChecks");
				return results;
			}
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
