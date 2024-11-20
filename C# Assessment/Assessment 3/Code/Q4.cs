using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Write a console program that uses delegate object as an argument to call Calculator Functionalities like 
//1. Addition, 2. Subtraction and 3. Multiplication by taking 2 integers and returning the output to the user.
//You should display all the return values accordingly.

namespace Assessment3
{
    public delegate int CalDel(int x, int y);
    internal class Q4
    {
        public int addNum(int x, int y)
        {
            return x + y;
        }

        public int subNum(int x, int y)
        {
            return x - y;
        }

        public int mulNum(int x, int y)
        {
            return x * y;
        }

        static int Calculator(int x,int y,CalDel fun)
        {
           return fun(x, y);
        }

        static void Main()
        {
            Q4 q=new Q4();
            Console.Write($"Enter two inputs:");
            Console.Write($"\nEnter first input:");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write($"\nEnter second input:");
            int y = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n================Results==================\n");
            Console.WriteLine($"Additon of num is {Q4.Calculator(x,y,q.addNum)}");
            Console.WriteLine($"Subtraction of num is {Q4.Calculator(x, y, q.subNum)}");
            Console.WriteLine($"Multiplication of num is {Q4.Calculator(x, y, q.mulNum)}");
            Console.WriteLine("\n\nPlease press any key to exit");
            Console.ReadKey();
        }
    }
}
