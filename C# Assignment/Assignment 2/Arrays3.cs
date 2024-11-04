using System;
namespace Assignment2Solving
{
    public class Arrays3
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

            Console.Write("Source array :");
            foreach (int i in arr)
            {
                Console.Write(i + " ");
            }

            int[] arr1 = new int[arrSize];
            for (int i = 0; i < arrSize; i++)
            {
                arr1[i] = arr[i];
            }

            Console.Write("\nAfter copying all the elements from exsiting array :");
            foreach (int i in arr1)
            {
                Console.Write(i + " ");
            }

            Console.Read();
        }
    }
}
