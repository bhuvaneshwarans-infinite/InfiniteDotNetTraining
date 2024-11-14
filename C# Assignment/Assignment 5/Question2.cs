using System;
using System.IO;

//Write a program in C# Sharp to create a file and write an array of strings to the file.

namespace Assignment5
{
    class Question2
    {
        static void Main()
        {
            try
            {
                    Console.Write("How many Lines your poetry contain:");
                    int linesCount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nEnter your Lines Below:\n");
                    string[] lines = new string[linesCount];
                    for (int i = 0; i < linesCount; i++)
                    {
                        Console.Write($"Line {i + 1}:");
                    //lines[i] = Console.ReadLine() + "\n";
                    lines[i] = Console.ReadLine();
                    }
                string filePath = "poetry.txt";

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }
                Console.WriteLine("File Created and data is written to the file.");
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine("You don't have an access to create a file in particular folder");
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Please enter proper data");
            }
            catch (Exception e)
            {
                Console.WriteLine("Some exception didn't handled");
            }
            Console.WriteLine("\n\n***Press Enter to exit***");
            Console.ReadKey();

        }
    }
}
