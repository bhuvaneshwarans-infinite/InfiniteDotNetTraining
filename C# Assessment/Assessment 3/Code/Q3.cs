using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Write a program in C# Sharp to append some text to an existing file.
//If file is not available, then create one in the same workspace.

namespace Assessment3
{   
    class Q3
    {
        public static void WriteData()
        {
            string filePath = "file123.txt";

            try
            {
                    using (FileStream fstream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    {
                        StreamWriter wStream = new StreamWriter(fstream);
                        Console.WriteLine("Enter a write into the file :");
                        string data = Console.ReadLine();
                        wStream.Write(data);
                        wStream.Flush();
                    }
            }
            catch (FileNotFoundException ffe)
            {
                Console.WriteLine(ffe.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }

        static void Main()
        {
            WriteData();
        }
    }
}


