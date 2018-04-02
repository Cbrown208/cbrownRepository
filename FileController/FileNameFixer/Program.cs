using System;
using FileController.Core;

namespace FileNameFixer
{
	public class Program
	{

		static void Main()
		{
			FileNameManager manager = new FileNameManager();
			var dir = @"C:\myscripts\Temp\RenameTesting";

			manager.CapitalizeFileName(dir);
			Console.ReadLine();
		}
	}
}
