using System;
using System.IO;

namespace DataAccessTesting
{
	public class DataAccessManager
	{
		private static readonly BulkCopyManager BulkCopyManager = new BulkCopyManager();
		private static readonly DbSaveFileManager FileManager = new DbSaveFileManager();
		public void RunDataAccessTesting()
		{
			//RunBulkCopyTests();
			RunFileManagerTests();

			Console.ReadLine();
		}

		private void RunFileManagerTests()
		{
			var filePath = @"C:\MyScripts\Websites\htmlTesting.html";
			var saveDatabaseResult = 0;

			using (FileStream fileStream = File.OpenRead(filePath))
			{
				MemoryStream memStream = new MemoryStream();
				memStream.SetLength(fileStream.Length);
				fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);

				//Save to Db
				saveDatabaseResult = FileManager.DatabaseFilePut(memStream);
				Console.WriteLine("FileId = " + saveDatabaseResult);
			}

			// Get from Db
			var getFileContents = FileManager.DatabaseFileRead(saveDatabaseResult.ToString());
			// Save to output File
			FileManager.SaveStreamToNewFile(getFileContents);
		}

		private void RunBulkCopyTests()
		{
			var result = BulkCopyManager.StartCopy();
			if (result)
			{
				//Console.ReadLine();
				System.Threading.Thread.Sleep(10000);
			}
		}
	}
}
