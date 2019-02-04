using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevStartPage.Core.Models;

namespace DevStartPage.Core.Managers
{
	public class DownloadsManager
	{
		public List<FileDetails> GetFileList()
		{
			var path = AppContext.BaseDirectory + @"src\app\Downloads\Files\";
			var d = new DirectoryInfo(path);//Assuming Test is your Folder
			var files = d.GetFiles("*");
			return files.Select(file => new FileDetails
				{
					Name = file.Name,
					FileSize = GetFileSize(file.Length),
					LastModified = file.LastWriteTime
				})
				.ToList();
		}

		public string GetFileSize(long byteSize)
		{
			string[] sizes = { "B", "KB", "MB", "GB", "TB" };
			double len = byteSize;
			var order = 0;
			while (len >= 1024 && order < sizes.Length - 1)
			{
				order++;
				len = len / 1024;
			}

			// Adjust the format string to your preferences. For example "{0:0.#}{1}" would
			// show a single decimal place, and no space.
			return String.Format("{0:0.##} {1}", len, sizes[order]);

		}
	}
}
