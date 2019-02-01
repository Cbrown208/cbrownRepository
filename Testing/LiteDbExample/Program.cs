using System;

namespace LiteDbExample
{
	class Program
	{
		private static readonly DocumentDbManager Manager = new DocumentDbManager();
		static void Main(string[] args)
		{
			Manager.QueryDocumentDb();
			Console.ReadLine();
		}
	}
}
