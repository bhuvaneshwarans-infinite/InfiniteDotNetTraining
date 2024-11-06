using System;

//Write a program in C# to accept two words from user and find out if they are same. 

namespace Assigment3Solving
{
    class StringEqual
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the first word :");
            String str1 = Console.ReadLine();
            Console.Write("Enter the Second word :");
            String str2 = Console.ReadLine();

            if (str1 == str2 && object.ReferenceEquals(str1, str2))
                Console.WriteLine("String is equal and both string located in same space");
            else if (str1 == str2 && object.ReferenceEquals(str1, str2) != true)
                Console.WriteLine("String is equal and both string located in different space");
            else
                Console.WriteLine("Strings are not equal");

            Console.Read();
        }
    }
}
