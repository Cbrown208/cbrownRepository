using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DataAccessTesting
{
	public class DbSaveFileManager
	{
		private const string ConnectionString = "Data Source=localhost;Initial catalog=TestingDb;Integrated Security=true;";
		public int DatabaseFilePut(MemoryStream fileToPut)
		{
			int varId = 0;
			byte[] file = fileToPut.ToArray();
			const string preparedCommand = @"
                    INSERT INTO [dbo].[FileStorage]
                               ([FileContents])
                         VALUES
                               (@File)
                        SELECT [FileId] FROM [dbo].[FileStorage]
            WHERE [FileId] = SCOPE_IDENTITY()
                    ";
			using (var varConnection = new SqlConnection(ConnectionString))
			{
				varConnection.Open();
				using (var sqlWrite = new SqlCommand(preparedCommand, varConnection))
				{
					sqlWrite.Parameters.Add("@File", SqlDbType.VarBinary, file.Length).Value = file;

					using (var sqlWriteQuery = sqlWrite.ExecuteReader())
						while (sqlWriteQuery != null && sqlWriteQuery.Read())
						{
							varId = sqlWriteQuery["FileId"] is int ? (int)sqlWriteQuery["FileId"] : 0;
						}
				}
				varConnection.Close();
			}
			return varId;
		}

		public MemoryStream DatabaseFileRead(string varId)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (var varConnection = new SqlConnection(ConnectionString))
			{
				varConnection.Open();
				using (var sqlQuery =
					new SqlCommand(@"SELECT [FileContents] FROM [dbo].[FileStorage] WHERE [FileId] = @varID",
						varConnection))
				{
					sqlQuery.Parameters.AddWithValue("@varID", varId);
					using (var sqlQueryResult = sqlQuery.ExecuteReader())
						if (sqlQueryResult != null)
						{
							sqlQueryResult.Read();
							var blob = new Byte[(sqlQueryResult.GetBytes(0, 0, null, 0, int.MaxValue))];
							sqlQueryResult.GetBytes(0, 0, blob, 0, blob.Length);
							//using (var fs = new MemoryStream(memoryStream, FileMode.Create, FileAccess.Write)) {
							memoryStream.Write(blob, 0, blob.Length);
							//}
						}
				}
				varConnection.Close();
			}
			return memoryStream;
		}

		public bool SaveStreamToNewFile(MemoryStream memStream)
		{
			FileStream file = new FileStream(@"C:\MyScripts\Websites\Output.txt", FileMode.Create, FileAccess.Write);
			memStream.WriteTo(file);
			file.Close();
			memStream.Close();
			return true;
		}
	}
}
