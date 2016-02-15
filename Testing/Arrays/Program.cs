using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            //var input = Console.ReadLine();
            
            //var input2 = Console.ReadLine();
            //var input3 = Console.ReadLine();
            
            //string[] elements = { input, input2, input3 };
            string[] arraytest = new string[5];
 


            int numPlayers = 3;
            for (int i = 0; i < numPlayers; i++)
            {
                Console.WriteLine("Enter the name of the player...Enter \"Q to exit...");
                string playerName = Console.ReadLine();
                arraytest[i] = playerName;
            }


            foreach (string element in arraytest)
            {
                Console.WriteLine(element);
            }

            Console.ReadLine();
        }
    }
}
