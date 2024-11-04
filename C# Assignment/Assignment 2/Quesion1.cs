using System;


namespace Assignment2Solving
{
    internal class Question1
    {
        static void Main(string[] args)
        {
            (int a, int b) = (5, 10);
            Console.WriteLine("Before Swap a:{0}, b:{1}",a,b);
            (a, b) = (b, a);

            Console.WriteLine("\n========================Result========================\n");
            Console.WriteLine("After Swap a:{0}, b:{1}", a, b);
            Console.Read();
        }
    }
}
