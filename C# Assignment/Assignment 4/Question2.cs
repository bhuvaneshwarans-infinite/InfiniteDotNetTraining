using System;

//2.Create a class called Scholarship which has a function Public void Merit() that takes marks and fees as an input. 
//If the given mark is >= 70 and <=80, then calculate scholarship amount as 20% of the fees
//If the given mark is > 80 and <=90, then calculate scholarship amount as 30% of the fees
//If the given mark is >90, then calculate scholarship amount as 50% of the fees.
//In all the cases return the Scholarship amount

namespace Assignment4Solving
{
    class Scholarship
    {
        public static float Merit(float mark, float fees)
        {
            float scolarshipAmt = 0;
            if (mark >= 70 && mark<=80) {
                scolarshipAmt = (fees / 100) * 20;
            }
            else if (mark > 80 && mark <= 90) {
                scolarshipAmt = (fees / 100) * 30;
            }
            else if (mark > 90) {
                scolarshipAmt = (fees / 100) * 50;
            }
            else {
                scolarshipAmt = 0;
                Console.WriteLine("Candiate is not eligible for scholarship");
            }
            return scolarshipAmt;
        }
    }

    class CandidateEntry
    {
        static void Main()
        {
            try
            {
                Console.Write("Enter the Mark:");
                float mark = Convert.ToSingle(Console.ReadLine());
                if(!(mark >= 0  && mark <= 100))
                {
                    throw new OverflowException("Mark should be in range of 0 and 100");
                }
                Console.Write("Enter the Fees:");
                float fees = Convert.ToSingle(Console.ReadLine());
                if (fees<0)
                {
                    throw new OverflowException("Fees can't be in Negative");
                }
                Console.WriteLine("Scholarship amount of candiate is " + Scholarship.Merit(mark, fees));
            }catch(OverflowException oe)
            {
                Console.WriteLine(oe.Message);
            }
            Console.WriteLine("\n\n***Press Enter to exit***");
            Console.ReadKey();
        }
    }
}
