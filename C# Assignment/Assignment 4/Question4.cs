using System;
using System.Xml.Linq;

//4.Create an Employee class with Empid int, Empname string, Salary float. Pass values to the members through Constructor.
//Create a derived class called ParttimeEmployee with Wages as a data member. Instantiate the base class through the derived class constructor

namespace Assignment4Solving
{
    class Employee1
    {
        public int _empId {  get; set; }
        public string _empName { get; set; }
        public float _salary { get; set; }

        public Employee1(int empID,string empName,float empSalary) {
            _empId = empID;
            _empName = empName;
            _salary = empSalary;
        }   
    }

    class ParttimeEmployee:Employee1
    {
        public float _wages { get; set; }
        public ParttimeEmployee(float wages, int empID, string empName, float empSalary) : base(empID,empName,empSalary)
        { 
            _wages = wages; 
        }
        public void DisplayData()
        {
            Console.WriteLine("\n\n===============Result==============");
            Console.WriteLine("Employee ID: {0}", _empId);
            Console.WriteLine("Employee Name: {0}", _empName);
            Console.WriteLine("Employee Salary: {0}", _salary);
            Console.WriteLine("Employee PartTime Wages: {0}", _wages);
        }
    }

    class Question4
    {
        static void Main()
        {
            try
            {
                Console.Write("Enter the Employee ID:");
                int empID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the Employee Name:");
                String empName = Console.ReadLine();
                Console.Write("Enter the Salary:");
                float salary = Convert.ToSingle(Console.ReadLine());
                Console.Write("Enter the partTime wages:");
                float wages = Convert.ToSingle(Console.ReadLine());
                ParttimeEmployee objPt = new ParttimeEmployee(wages, empID, empName, salary);
                objPt.DisplayData();
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Invalid data entry.");
            }
            catch (Exception ex) { 
                Console.WriteLine("Error Occured :"+ex.StackTrace);
            }
            Console.WriteLine("\n\n***Press Enter to exit***");
            Console.ReadKey();
        }
    }
}
