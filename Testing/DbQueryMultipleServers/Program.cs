using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbQueryMultipleServers
{
    class Program
    {
	    private static QueryManager _manager = new QueryManager();
        static void Main(string[] args)
        {
	        _manager.RunQueryOnCmDatabases();
	        Console.ReadLine();
        }
    }
}
