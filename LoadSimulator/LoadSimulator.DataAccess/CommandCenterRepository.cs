using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace LoadSimulator.DataAccess
{
	public class CommandCenterRepository
	{
		private readonly IDbConnection _db;

		public CommandCenterRepository()
		{
			_db = new SqlConnection(@"Server=.;Database=CommandCenter.Local;Trusted_Connection=true;");
		}
		public dynamic GetById(int id)
		{
			using (IDbConnection dbConnection = _db)
			{
				var sQuery = "SELECT * FROM Products WHERE Id = @Id";
				dbConnection.Open();
				var result = dbConnection.Query(sQuery, new { Id = id }).FirstOrDefault();
				var resultName = "";
				if (result != null)
				{
					resultName = result.Name;
				}
				return resultName;
			}
		}
	}
}
