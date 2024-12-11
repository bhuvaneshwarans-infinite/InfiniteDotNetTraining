using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAssigment
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string City { get; set; }
    }

    class LinqAssignment
    {
        static void Main(string[] args)
        {
            List<Employee> empList = new List<Employee>
            {
                new Employee { EmployeeID = 1001, FirstName = "Malcolm",LastName = "Daruwalla", Title = "Manager", DOB = new DateTime(1984, 11,16), DOJ = new DateTime(2011, 6, 8), City = "Mumbai" },
                new Employee { EmployeeID = 1002, FirstName = "Asdin",LastName = "Dhalla", Title = "AsstManager", DOB = new DateTime(1984, 8,20), DOJ = new DateTime(2012, 7, 7), City = "Mumbai" },
                new Employee { EmployeeID = 1003, FirstName = "Madhavi",LastName = "Oza", Title = "Consultant", DOB = new DateTime(1987, 11, 14),DOJ = new DateTime(2015, 4, 12), City = "Pune" },
                new Employee { EmployeeID = 1004, FirstName = "Saba",LastName = "Shaikh", Title = "SE", DOB = new DateTime(1990, 6, 3), DOJ =new DateTime(2016, 2, 2), City = "Pune" },
                new Employee { EmployeeID = 1005, FirstName = "Nazia",LastName = "Shaikh", Title = "SE", DOB = new DateTime(1991, 3, 8), DOJ =new DateTime(2016, 2, 2), City = "Mumbai" },
                new Employee { EmployeeID = 1006, FirstName = "Amit",LastName = "Pathak", Title = "Consultant", DOB = new DateTime(1989, 11,7), DOJ = new DateTime(2014, 8, 8), City = "Chennai" },
                new Employee { EmployeeID = 1007, FirstName = "Vijay",LastName = "Natrajan", Title = "Consultant", DOB = new DateTime(1989, 12,2), DOJ = new DateTime(2015, 6, 1), City = "Mumbai" },
                new Employee { EmployeeID = 1008, FirstName = "Rahul",LastName = "Dubey", Title = "Associate", DOB = new DateTime(1993, 11,11), DOJ = new DateTime(2014, 11, 6), City = "Chennai" },
                new Employee { EmployeeID = 1009, FirstName = "Suresh", LastName = "Mistry", Title = "Associate", DOB = new DateTime(1992, 8, 12), DOJ = new DateTime(2014, 12, 3), City = "Chennai" },
                new Employee { EmployeeID = 1010, FirstName = "Sumit", LastName = "Shah", Title = "Manager", DOB = new DateTime(1991, 4, 12), DOJ = new DateTime(2016, 1, 2), City = "Pune" },
            };

            //1.Display a list of all the employee who have joined before 1 / 1 / 2015
            List<Employee> joinedBefore2015 = (empList.Where(el => el.DOJ < new DateTime(2015, 1, 1))).ToList();
            Console.WriteLine("Employees joined before 1/1/2015");
            foreach (Employee e in joinedBefore2015)
                Console.WriteLine($"{e.FirstName} {e.LastName}");
            Console.WriteLine("==================================================");

            //2.Display a list of all the employee whose date of birth is after 1 / 1 / 1990
            List<Employee> dobAfter1990 = (empList.Where(e => e.DOB > new DateTime(1990,1, 1))).ToList();
            Console.WriteLine("\nEmployees whose DOB after 1/1/1990 date is ");
            foreach (Employee e in dobAfter1990)
                Console.WriteLine($"{e.FirstName} {e.LastName}");
            Console.WriteLine("==================================================");

            //3.Display a list of all the employee whose designation is Consultant and Associate
            List<Employee> consultantsAndAssociates = (empList.Where(e => e.Title == "Consultant" || e.Title == "Associate")).ToList();
            Console.WriteLine("\nEmployees with designation Consultant or Associate ");
            foreach (var e in consultantsAndAssociates)
                Console.WriteLine($"{e.FirstName} {e.LastName}");
            Console.WriteLine("==================================================");

            //4.Display total number of employees
            Console.WriteLine($"\nTotal number of employees are { empList.Count} members");
            Console.WriteLine("==================================================");

            // 5.Display total number of employees belonging to “Chennai”
            Console.WriteLine($"\nTotal number of employees in Chennai are { empList.Count(e => e.City == "Chennai")} members");
            Console.WriteLine("==================================================");

            // 6.Display highest employee id from the list
            Console.WriteLine($"\nHighest Employee ID: {empList.Max(e => e.EmployeeID)}");
            Console.WriteLine("==================================================");

            //7.Display total number of employee who have joined after 1 / 1 / 2015
            Console.WriteLine($"\nEmployees who joined after 1/1/2015 are {empList.Count(e => e.DOJ > new DateTime(2015, 1, 1))} members");
            Console.WriteLine("==================================================");

            //8.Display total number of employee whose designation is not “Associate”
            Console.WriteLine($"\n Total employees whose designation is not Associate is {empList.Count(e => e.Title != "Associate")} ");
            Console.WriteLine("==================================================");

            //9.Display total number of employee based on City
            var empCountByCity = from e in empList group e by e.City into cityGrp
                                 select new {City=cityGrp.Key,Count=cityGrp.Count()};
            
            Console.WriteLine("\nEmployees grouped by city");
            foreach (var group in empCountByCity)
                Console.WriteLine($"{group.City}: {group.Count}");
            Console.WriteLine("==================================================");

            //10.Display total number of employee based on city and title
            var empCountByCityAndTitle = from e in empList group e by new { e.City, e.Title } into assoKey
                                         select new { assoKey.Key.City, assoKey.Key.Title, Count = assoKey.Count() };
            
            Console.WriteLine("\nEmployees group by city and title");
            foreach (var group in empCountByCityAndTitle)
                Console.WriteLine($"{group.City}, {group.Title}: { group.Count}");
            Console.WriteLine("==================================================");

            // 11.Display total number of employee who is youngest in the list
            var youngestEmployee = empList.OrderByDescending(e => e.DOB).FirstOrDefault();
            Console.WriteLine($"\nYoungest Employee is { youngestEmployee.FirstName} { youngestEmployee.LastName} ");

            Console.ReadLine();
        }
    }
}
