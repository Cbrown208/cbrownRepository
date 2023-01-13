using System;

namespace DbQueryMultipleServers
{
    class Program
    {
	    private static readonly QueryManager Manager = new QueryManager();
        static void Main(string[] args)
        {
	        Manager.RunMultipleDbQuery();
	        Console.ReadLine();
        }
    }
}
