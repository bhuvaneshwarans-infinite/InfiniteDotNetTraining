using System;


namespace Assignment2Solving
{
    public class Question2
    {
        static void Main()
        {
            Console.Write("Enter a digit: ");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n========================Result========================\n");
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    if (i % 2 != 0)
                    {
                        Console.Write(num + " ");
                    }
                    else
                    {
                        Console.Write(num);
                    }
                }
                Console.WriteLine();
            }
            Console.Read();
        }
    }
}

