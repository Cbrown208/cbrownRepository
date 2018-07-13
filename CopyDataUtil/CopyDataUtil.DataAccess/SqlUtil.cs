using System.Data.SqlClient;

namespace CopyDataUtil.DataAccess
{
	public class SqlUtil
	{
		public int ExecuteSql(string connectionString, string query)
		{
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
				{
					return cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
