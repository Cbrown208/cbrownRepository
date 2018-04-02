using FileController.Core;

namespace FileController.FileWatcher
{
	class Program
	{
		static void Main()
		{
			var dir = @"C:\myscripts\Temp\RenameTesting";
			Watcher.Run(dir);
		}
	}
}
