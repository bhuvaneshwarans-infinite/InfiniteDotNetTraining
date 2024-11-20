using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//2.Write a class Box that has Length and breadth as its members. 
//Write a function that adds 2 box objects and stores in the 3rd.
//Display the 3rd object details. Create a Test class to execute the above.

namespace Assessment3
{
    class Box
    {
        public int _length { get; set; } = 0;
        public int _breadth { get; set; } = 0;

        public static Box operator +(Box box1, Box box2)
        {
            Box tempBox = new Box();
            tempBox._length = box1._length + box2._length;
            tempBox._breadth = box1._breadth + box2._breadth;
            return tempBox;
        }

        public Box this[int i]
        {
            get { return this[i]; }
            set { this[i] = value; }
        }

        public static void Main()
        {
            Box[] bObj= new Box[2];
            for (int i = 0; i < 2; i++) {
                try
                {                    
                    Console.WriteLine($"Enter the {i + 1} box values:");
                    Console.Write("Length:");
                    bObj[i]._length = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Breadth:");
                    bObj[i]._breadth = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Enter the correct format data");
                }               
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }
            Box resObj = bObj[0] + bObj[1];
            Console.WriteLine("\n================Results==================\n");
            Console.WriteLine("Total length:"+resObj._length+" and breadth:"+resObj._breadth);
            Console.WriteLine("\n\nPlease press any key to exit");
            Console.ReadKey();

        }
    }
    
}
