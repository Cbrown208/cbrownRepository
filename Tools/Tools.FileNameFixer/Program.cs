using System;

namespace Tools.FileNameFixer
{
	class Program
	{
		private static readonly FileFixerManager Manager = new FileFixerManager();
		static void Main(string[] args)
		{
			Manager.TitleCaseFilesWithSubDirectories();
			Console.ReadKey();
		}
	}
}
