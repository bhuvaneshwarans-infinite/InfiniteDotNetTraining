using System;
using System.IO;


namespace Assignment5
{
    class Question3
    {
        static void Main()
        {

            try
            {
		string filePath = "poetry.txt";
            	int lineCount = 0;

                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (reader.ReadLine() != null)
                        {
                            lineCount++;
                        }
                    }
                    Console.WriteLine("===========Result===========\n");
                    Console.WriteLine($"No of lines we can find in your file is {lineCount}");
                }
                else
                {
                    throw new FileNotFoundException("Please check your file name and file path.Their is no file exists.");

                }
            }
            catch (FileNotFoundException  ffe)
            {
                Console.WriteLine(ffe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            Console.WriteLine("\n\n***Press Enter to exit***");
            Console.ReadKey();            
        }
    }
}
