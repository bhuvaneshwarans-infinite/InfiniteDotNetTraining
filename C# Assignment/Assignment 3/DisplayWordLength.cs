using System;


//Write a program in C# to accept a word from the user and display the length of it.

namespace Assigment3Solving
{
    class DisplayWordLength
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the word to find its length:");
            String str= Console.ReadLine();
            Console.WriteLine($"Length of a word is {str.Length} ");
            Console.Read();
        }
    }
}
