using System;
using System.Linq;


namespace Assignment6
{
    class Question2
    {
      static void Main()
        {
            Console.Write("Enter the size of inputs:");
            int[] inputSize = new int[Convert.ToInt32(Console.ReadLine())];

            string[] words = new string[inputSize.Length];

            for (int i = 0; i < inputSize.Length; i++)
            {
                Console.Write($"Enter the {i + 1} word:");
                words[i] = Console.ReadLine();
            }

            string[] resArr= (from word in words where word.StartsWith("a") && word.EndsWith("m") select word).ToArray();
            Console.WriteLine("\n========Result==========");
            foreach (var obj in resArr)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine("\n\n***Press Enter to exit***");
            Console.ReadKey();
        }
    }
}
