using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{

    class Sample 
    {
        public static void Main() 
        {
            Guid g;
        // Create and display the value of two GUIDs.
            g = Guid.NewGuid();
            Console.WriteLine(g);
            //Console.WriteLine(Guid.NewGuid());
            Console.ReadLine();
        }
    }
}
