using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FileController.Core
{
	public class FileNameManager
	{
		public void CapitalizeFileName(string dir)
		{
			Console.WriteLine("Starting File Renaming");
			DirectoryInfo d = new DirectoryInfo(dir);
			
			var fname = Directory.GetFiles(dir,"*.*", SearchOption.AllDirectories).ToList();
			//FileInfo[] infos = d.GetFiles();
			foreach (var f in fname)
			{
				var newFileName = ToTitleCase(f);

				File.Move(f, newFileName);
				var results = $"\"{f}\" to titlecase: {newFileName}";
				//Console.WriteLine(results);
			}
			Console.WriteLine("File Renameing Successful");
		}

		public string ToTitleCase(string fileName)
		{
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			return textInfo.ToTitleCase(fileName);
		}

		public string ToUppereCase(string fileName)
		{
			return fileName.ToUpperInvariant();
		}

		public string ToLowerCase(string fileName)
		{
			return fileName.ToLowerInvariant();
		}
	}
}
