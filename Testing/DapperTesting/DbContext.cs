using System;
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
		private readonly QueryBuilder _queryBuilder;

		public DbContext()
		{
			_db = new SqlConnection(@"Server=.;Database=CommandCenter.Local;Trusted_Connection=true;");
			_queryBuilder = new QueryBuilder();
		}

		public void AddXmlMapping(List<Hl7XmlMappings> mappingList, string tableName)
		{
			using (IDbConnection dbConnection = _db)
			{
				dbConnection.Open();
				var query = "";
				foreach (var mapping in mappingList)
				{
					List<string> columns = new List<string>();
					columns.Add("ClientId");
					columns.Add("FacilityId");
					columns.Add("MappingFileName");
					var insertQueryCheck = _queryBuilder.BuildCheckStatement(tableName, "FacilityId", mapping.FacilityId.ToString());
					var insertQuery = _queryBuilder.BuildInsertQuery(tableName, columns);

					var valuesQuery = string.Format("({0},'{1}','{2}')", mapping.ClientId, mapping.FacilityId,
						mapping.MappingFileName)+ Environment.NewLine;

					query = query+ insertQueryCheck + insertQuery + valuesQuery + _queryBuilder.BuildEndStatement();

					//dbConnection.Execute(sQuery, mapping);
				}
				var temp = query;
				//dbConnection.Execute(sQuery, mapping);
			}
		}

		public IEnumerable<HealthChecks> GetAllHealthChecks()
		{
			using (IDbConnection dbConnection = _db)
			{
				dbConnection.Open();
				var results = dbConnection.Query<HealthChecks>("SELECT * FROM dbo.HealthChecks");
				return results;
			}
		}

		public IEnumerable<Hl7XmlMappings> GetAllXmlMappings()
		{
			using (IDbConnection dbConnection = _db)
			{
				dbConnection.Open();
				var results = dbConnection.Query<Hl7XmlMappings>("SELECT * FROM dbo.HealthChecks");
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
