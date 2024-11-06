using System;

using System.Linq;

//Write a program in C# to accept a word from the user and display the reverse of it. 
namespace Assigment3Solving
{
    class WordReverse
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the word :");
            String str = Console.ReadLine();
            string revStr = new string(str.Reverse().ToArray());
            Console.WriteLine($"Reversed word is: {revStr}");
            Console.Read();
        }
    }
}
