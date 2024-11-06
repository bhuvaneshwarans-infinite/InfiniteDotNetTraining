using System;
using System.Linq;

namespace Assessment1
{
    class Program4
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your string:");
            string orgStr = Console.ReadLine();
            Console.Write("\nEnter the character to find count of it :");
            char findChar = Convert.ToChar(Console.ReadLine());

            int countOfChar = 0;
            for (int i = 0; i < orgStr.Length; i++)
            {
                if(findChar == orgStr[i])
                {
                    countOfChar++;
                }
            }
            Console.WriteLine("\n==========Result=========\n");
            Console.WriteLine("Count of a character \'{0}\' is :{1}", findChar, countOfChar);
            Console.Read();

            }
    }
}
