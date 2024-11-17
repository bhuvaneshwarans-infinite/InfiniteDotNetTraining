using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment6
{
     class Employee: IComparable<Employee>
    {

        public int _empID { get; set; }
        public string _empName { get; set; }
        public string _empCity { get; set; }
        public double _empSalary { get; set; }
        
        public Employee(int empID, string empName, string empCity, double empSalary)
        {
            this._empID = empID;
            this._empName = empName;
            this._empCity = empCity;
            this._empSalary = empSalary;
        }

        public override string ToString()
        {
            return "Employee ID:" + _empID + ",Employee Name:" + _empName + ",Employee City:" + _empCity + ",Employee Salary:" + _empSalary;
        }

        public int CompareTo(Employee other)
        {
            if (other != null)
                return this._empName.CompareTo(other._empName);
            return -2;
        }
    }

    class EmpOps
    {
        Employee[] _empData= new Employee[5];

        public Employee[] EmpData
        {
            get { return this._empData; }
        }
        public Employee this[int index]
        {
            get
            {
                return _empData[index];
            }
            set
            {
                _empData[index] = value;
            }
        }
        public void DisplayEmployeeData(List<Employee> listData) {
            foreach (Employee emp in listData) {
                if(emp != null)
                    Console.WriteLine(emp);
            }
        }

    }

class Question3
    {
    static void Main(string[] args)
    {

            EmpOps mainEmpData = new EmpOps();
            List<Employee> listEmpData = new List<Employee>();
            Console.WriteLine("Enter your 5 employees data:");
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Console.Write("\nEnter the Employee ID:");
                    int empID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nEnter the Employee Name:");
                    string empName = Console.ReadLine();
                    Console.Write("\nEnter the Employee City:");
                    string empCity = Console.ReadLine();
                    Console.Write("\nEnter the Employee Salary:");
                    double empSalary = Convert.ToDouble(Console.ReadLine());
                    mainEmpData[i] = new Employee(empID, empName, empCity, empSalary);
                    listEmpData.Add(mainEmpData[i]);
                }
                catch (FormatException ex) { 
                    Console.WriteLine("Enter the proper data");
                }

            }
            Console.WriteLine("\n========Result==========");
            Console.WriteLine("\n==============Employee Data==============");
            mainEmpData.DisplayEmployeeData(listEmpData);


            Console.WriteLine("\n==Employees with Salary Greater Than 45000===");
            mainEmpData.DisplayEmployeeData(listEmpData.Where(arrData => arrData._empSalary > 45000).ToList());


            Console.WriteLine("\n=========Employees from Bangalore=============");
            mainEmpData.DisplayEmployeeData(listEmpData.Where(arrData => arrData._empCity == "Bangalore").ToList());

            Console.WriteLine("\n=========Employees sorted by their Names=============");
            Array.Sort(mainEmpData.EmpData);
            mainEmpData.DisplayEmployeeData(mainEmpData.EmpData.ToList());

            Console.WriteLine("\n*** Press Enter to exit ***");
            Console.ReadLine();
    }

   
}
}
