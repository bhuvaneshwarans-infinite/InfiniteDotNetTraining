using System;
using System.Linq;

namespace Assignment2Solving
{
    public class Arrays2
    {
        static void Main()
        {
            int[] arr = new int[10];

            for (int i = 0; i < 10; i++)
            {
                Console.Write("\n\nEnter the array {0} number:", i + 1);
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("\n========================Result========================\n");
            Console.WriteLine("\nTotal mark is  {0}", arr.Sum());
            Console.WriteLine("Average Mark is  {0}", arr.Average());
            Console.WriteLine("Minimum and Maximum Mark is : {0} and {1}", arr.Min(), arr.Max());
            Array.Sort(arr);
            Console.WriteLine("Ascending order of marks are");
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Array.Reverse(arr);
            Console.WriteLine("\nDescending order of marks are");
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }

            Console.Read();

        }
       
	
	}
}
