using System;
using System.IO;
using System.Reflection;

namespace Tools.Common
{
	public static class FileHelper
	{
		public static string GetRootPath()
		{
			return Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath));
		}

		public static string GetFilePath(string fileName)
		{
			var relativePath = GetRootPath() + "\\" + fileName;
			return relativePath;
		}
	}
}
