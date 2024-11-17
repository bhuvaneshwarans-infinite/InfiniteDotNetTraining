using System;
using System.Linq;


namespace Assignment6
{
    class Data
    {
        public int num { get; internal set; }
        public double sqr { get; internal set; }
    }
    class Question1 : Data
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the size of inputs:");
            Data[] data = new Data[Convert.ToInt32(Console.ReadLine())];
            //Console.WriteLine(data.Length);

            for (int i = 0; i < data.Length; i++)
            {
                try
                {
                    data[i] = new Data();
                    Console.Write($"Enter the {i+1} value:");                    
                    data[i].num = Convert.ToInt32(Console.ReadLine());
                    data[i].sqr = data[i].num* data[i].num;
                    
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e) {
                    Console.WriteLine("Enter the correct format input");
                }
                catch (Exception e) {

                    Console.WriteLine(e.Message);
                }

            }

            Data[] resData=(from obj in data where obj.sqr >20 select obj).ToArray();

            Console.WriteLine("\n========Result==========");
            foreach(var obj in resData)
            {
                Console.WriteLine(obj.num+" - "+obj.sqr);
            }

            Console.WriteLine("\n\n***Press Enter to exit***");
            Console.ReadKey();

        }
    }
}
