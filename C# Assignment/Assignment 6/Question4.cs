    using System;
    using ClassLibraryAssignment;

    namespace Assignment6
    {
        public class Question4
        {
            public static int _TotalFare = 1000;
            public string name { get; set; }
            public int age {  get; set; }

            static void Main()
            {
                Console.Write("\nEnter the name:");
                string empName = Console.ReadLine();
                Console.Write("\nEnter the age:");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n========Result==========");
                Console.WriteLine(ClassLibrary.CalculateConcession(age,_TotalFare));
                Console.WriteLine("\n\n***Press Enter to exit***");
                Console.ReadKey();
            }

        }
    }
