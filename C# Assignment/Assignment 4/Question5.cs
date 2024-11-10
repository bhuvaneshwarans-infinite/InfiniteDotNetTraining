using System;

//5.Create an Interface IStudent with StudentId and Name as Properties, void ShowDetails() as its method. 
//Create 2 classes Dayscholar and Resident that implements the interface Properties and Methods.

namespace Assignment4Solving
{
    interface IStudent
    {
        string _studId { get; set; }
        string _studName { get; set; }
        void ShowDetials();

    }


    public class DayScholar : IStudent
    {

        public string _studId { get; set; }
        public string _studName { get; set; }

        public DayScholar(string studId, string name)
        {
            _studId = studId;
            _studName = name;
        }
        
        public void ShowDetials()
        {
            Console.WriteLine($"DayScholar Student Detail:\nID: {_studId}\tName: {_studName}\n");
        }
    }


    public class Resident : IStudent
    {
        public string _studId { get; set; }
        public string _studName { get; set; }

        public Resident(string studentId, string name)
        {
            _studId = studentId;
            _studName = name;
        }

        public void ShowDetials()
        {
            Console.WriteLine($"Resident Student Detail:\nID: {_studId}\tName: {_studName}");
        }

    }

    class Question5
    {
        static void Main()
        {
            Console.Write("Enter the Student ID:");
            string studID = Console.ReadLine();
            Console.Write("Enter the Student Name:");
            string studName = Console.ReadLine();
            IStudent dayScholar = new DayScholar(studID, studName);
            Console.Write("\nEnter the Student ID:");
            studID = Console.ReadLine();
            Console.Write("Enter the Student Name:");
            studName = Console.ReadLine();
            IStudent resident = new Resident(studID, studName);

            Console.WriteLine("\n\n===============Result==============");
            dayScholar.ShowDetials();
            resident.ShowDetials();
            Console.WriteLine("\n\n***Press Enter to exit***");
            Console.ReadKey();
        }
    }

}
