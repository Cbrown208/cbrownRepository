using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppendStringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // This is your input string.
            //
            string value = "Dot Net ";
            //
            // Append the word "Perls" to the string.
            //
            value += "Perls";
            //
            // Write the string to the screen.
            //
            Console.WriteLine(value);
            //
            // Append this word to the string.
            //
            value += " Basket";
            //
            // Write the new string.
            //
            if (value == null)
            {
                throw new ArgumentException("Patient First Name cannot be null");
            }
            Console.WriteLine(value);
            Console.ReadLine();
        }
    }
}
