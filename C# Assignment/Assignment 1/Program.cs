using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1Solving
{
    class Program
    {

        public static void Task1(){
            Console.Write("Input 1st number: ");
            int FirstInput = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input 2nd number: ");
            int SecondInput = Convert.ToInt32(Console.ReadLine());

            if (FirstInput == SecondInput)
            {
                Console.WriteLine($"{FirstInput} and {SecondInput} are equal");
            }
            else
            {
                Console.WriteLine($"{FirstInput} and {SecondInput} are not equal");
            }

        }

        public static void Task2()
        {
            Console.Write("Input a number: ");
            int CheckNum = Convert.ToInt32(Console.ReadLine());

            if (CheckNum > 0)
            {
                Console.WriteLine($"{CheckNum} is a positive number");
            }
            else if (CheckNum < 0)
            {
                Console.WriteLine($"{CheckNum} is a negative number");
            }
            else
            {
                Console.WriteLine("The number is zero");
            }

        }

        public static void Task3()
        {
            Console.Write("Input first number: ");
            int FirstNum = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input Operator (+, -, *, /): ");
            char Operator = Convert.ToChar(Console.ReadLine());

            Console.Write("Input second number: ");
            int SecondNum = Convert.ToInt32(Console.ReadLine());

            switch (Operator)
            {
                case '+':
                    Console.WriteLine($"{FirstNum} + {SecondNum} = {FirstNum + SecondNum}");
                    break;
                case '-':
                    Console.WriteLine($"{FirstNum} - {SecondNum} = {FirstNum - SecondNum}");
                    break;
                case '*':
                    Console.WriteLine($"{FirstNum} * {SecondNum} = {FirstNum * SecondNum}");
                    break;
                case '/':
                    if (SecondNum != 0)
                    {
                        Console.WriteLine($"{FirstNum} / {SecondNum} = {FirstNum / SecondNum}");
                    }
                    else
                    {
                        Console.WriteLine("Cannot divide by zero");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Operator");
                    break;
            }

        }

        public static void Task4()
        {
            Console.Write("Enter the number: ");
            int num = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine($"{num} * {i} = {num * i}");
            }

        }

        public static void Task5()
        {
            Console.Write("Input first number: ");
            int FirstNum = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input second number: ");
            int SecondNum = Convert.ToInt32(Console.ReadLine());

            int Total = FirstNum + SecondNum;

            if (FirstNum == SecondNum)
            {
                Total *= 3;
            }

            Console.WriteLine($"The result is: {Total}");

        }
        static void Main(string[] args)
        {
            Console.WriteLine("===============Questions================");
            Console.WriteLine("1. Write a C# Sharp program to accept two integers and check whether they are equal or not. \n\n");
            Console.WriteLine("2. Write a C# Sharp program to check whether a given number is positive or negative. \n\n");
            Console.WriteLine("3. Write a C# Sharp program that takes two numbers as input and performs all operations (+,-,*,/) on them and displays the result of that operation. \n\n");
            Console.WriteLine("4. Write a C# Sharp program that prints the multiplication table of a number as input.\n\n");
            Console.WriteLine("5.  Write a C# program to compute the sum of two given integers. If two values are the same, return the triple of their sum.\n\n");
            Console.Write("Select the question:");
            int choice=Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\n=============Operation==================\n");
            switch (choice)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 3:
                    Task3();
                    break;
                case 4:
                    Task4();
                    break;
                case 5:
                    Task5();
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
            Console.Read();

        }
    }
}
