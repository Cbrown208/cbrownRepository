using System;
using System.IO;
using Tools.Common;

namespace Tools.FileNameFixer
{
	public class FileFixerManager
	{
		private readonly string _filePath = @"C:\MyScripts\Temp\FileRenameTesting";
		private readonly RegExHelper _regExHelper = new RegExHelper();

		public string FixFileNamesAtDirectoryLevel()
		{
			DirectoryInfo d = new DirectoryInfo(_filePath);
			FileInfo[] infos = d.GetFiles();
			foreach (FileInfo f in infos)
			{
				File.Move(f.FullName, f.FullName.Replace("abc_", ""));
			}
			return "Done";
		}

		public string TitleCaseFilesWithSubDirectories()
		{
			try
			{
				var fileNames = Directory.EnumerateFiles(_filePath, "*", SearchOption.AllDirectories);
				foreach (var path in fileNames)
				{
					var dir = Path.GetDirectoryName(path);
					var fileName = Path.GetFileName(path);
					var newFileName = _regExHelper.CapitalizeFirstLetterOfString(fileName);
					if (dir != null)
					{
						var newPath = Path.Combine(dir, newFileName);
						File.Move(path, newPath);
					}
				}

				Console.WriteLine("Done");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return "Done";
		}
	}
}
