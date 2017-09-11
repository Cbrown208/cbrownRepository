using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IListExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[3];
            array[0] = 1;
            array[1] = 2;
            array[2] = 3;
            //DisplayInt(array);

            List<string> list = new List<string>();
            list.Add("Date");
            list.Add("Bob");
            list.Add("Last");
            DisplayString(list);
            Console.ReadLine();
        }

        static void DisplayInt(IList<int> list)
        {
            if (list.Contains(2)) { Console.WriteLine("2 Exsists");}
            
            Console.WriteLine("Count: {0}", list.Count);
            foreach (int value in list)
            {
                Console.WriteLine(value);
            }
        }

        static void DisplayString(IList<string> list)
        {
            //if (list.Contains("bob")) { Console.WriteLine("2 Exsists"); }
            //string fullList = null;
            string joined = string.Join("|", list);
            Console.WriteLine(joined);
            Console.WriteLine("Count: {0}", list.Count);
            foreach (string value in list)
            {
                //string joined = string.Join("|", list);
                Console.WriteLine(value);
            }
        }
    }
}

