using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.FileGenerator
{
	class Program
	{
		private static readonly FileGeneratorManager FileGeneratorManager = new FileGeneratorManager();
		static void Main(string[] args)
		{
			FileGeneratorManager.RunFileGenerator();

		}
	}
}
