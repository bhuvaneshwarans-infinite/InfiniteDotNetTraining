using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    class Program2
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your string:");
            StringBuilder str = new StringBuilder(Console.ReadLine());
            char firstChar = str[0];
            char lastChar = str[str.Length-1];
            str[0] = lastChar;
            str[str.Length - 1] = firstChar;
            Console.WriteLine("\n==========Result=========\n");
            Console.WriteLine(str.ToString());
            Console.ReadKey();
        }
    }
}
