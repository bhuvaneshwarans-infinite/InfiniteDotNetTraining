using System;
//3.Write a C# program to implement a method that takes an integer as input and throws an exception 
//if the number is negative. Handle the exception in the calling code.

namespace Assessment2
{

    class CheckNegativeInput : ApplicationException
    {
        public CheckNegativeInput(String msg) : base(msg) { }
    }
    class Question3
    {
        static void Main()
        {
            try
            {
                Console.Write("Enter the Number:");
                int num = int.Parse(Console.ReadLine());
                    if (num < 0)
                    {
                        throw new CheckNegativeInput("Invalid Input.Please enter positive number.");
                    }
                    else
                    {
                    Console.WriteLine("\n=========Result============\n");
                    Console.WriteLine("\nEntered number is in correct format");
                    }

            }
            catch (CheckNegativeInput ex) {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nPress enter to exit");
            Console.Read();


        }

    }
}
