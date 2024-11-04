
using System;

namespace Assignment2Solving
{
    enum Days
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
    public class Question3
    {
        static void Main(string[] args)
        {
            Console.Write("Input :");
            int DayOfWeek = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n========================Result========================\n");
            if (DayOfWeek > 0 && DayOfWeek < 8)
                Console.WriteLine((Days)DayOfWeek);
            else
                Console.WriteLine("Invalid data");
            Console.Read();
        }
        
    }
}

