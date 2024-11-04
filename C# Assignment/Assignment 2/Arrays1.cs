using System;
using System.Linq;

namespace Assignment2Solving
{
    public class Arrays1
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the array size:");
            int arrSize = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[arrSize];

            for (int i = 0; i < arrSize; i++)
            {
                Console.Write("\n\nEnter the array {0} number:", i + 1);
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("\n========================Result========================\n");

            Console.WriteLine("\nAverage value of Array elements is  {0}", arr.Average());
            Console.WriteLine("Minimum and Maximum value in an array : {0} and {1}", arr.Min(), arr.Max());
            Console.Read();
        }

    }
}
