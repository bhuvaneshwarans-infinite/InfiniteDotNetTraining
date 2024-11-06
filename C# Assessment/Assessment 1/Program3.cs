using System;
using System.Linq;

namespace Assessment1
{
    class Program3
    {
        static void Main(string[] args)
        {
            int[] arr = new int[3];

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write("Enter your {0} number :", i + 1);
                arr[i] = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
            }
            Console.WriteLine("==========Result=========\n");
            Console.WriteLine("Largest of three number is {0}", arr.Max());
            Console.ReadKey();

        }
    }
}
