using System;


namespace Assessment1
{
    class Program1
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your string:");
            string orgStr=Console.ReadLine();

            string tempstr1 ="",tempstr2="";

            Console.Write("Enter the position to remove the character:");
            int charPos = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n==========Result=========\n");
            if (charPos >= 0) {
                for (int i = 0; i < orgStr.Length; i++)
                {
                    if(charPos!= i) {
                        if (charPos > 0 && i < charPos)
                        {
                            tempstr1 += orgStr[i];
                        }
                        else
                        {
                            tempstr2 += orgStr[i];
                        }
                    }
             
                }
                
                Console.WriteLine("After removing the character : {0}",tempstr1+tempstr2);
            }
            else
            {
                Console.Write("Invalid position");
            }

            Console.Read();
        }
    }
}
