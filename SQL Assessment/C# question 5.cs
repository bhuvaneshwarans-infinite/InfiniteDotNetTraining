using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//C Sharp Question
//⦁	Create a Generic List Collection empList and populate it with the following records.
//EmployeeID 	FirstName	 LastName 	Title 	         DOB 	DOJ 	City
//1001	        Malcolm        	 Daruwalla 	Manager 	16-11-1984	08-06-2011	Mumbai
//1002	Asdin	        Dhalla 	AsstManager 	20-08-1994	07-07-2012	Mumbai
//1003	Madhavi         	Oza 	Consultant 	14-11-1987	12-04-2015	        Pune
//1004	Saba 	Shaikh	SE 	03-06-1990	02-02-2016	Pune
//1005	Nazia 	Shaikh 	SE 	08-03-1991	02-02-2016	Mumbai
//1006	Amit 	Pathak 	Consultant 	07-11-1989	08-08-2014	Chennai
//1007	Vijay 	Natrajan	Consultant 	02-12-1989	01-06-2015	Mumbai
//1008	Rahul 	Dubey 	Associate	11-11-1993	06-11-2014	Chennai
//1009	Suresh 	Mistry	Associate 	12-08-1992	03-12-2014	Chennai
//1010	Sumit 	Shah 	Manager 	12-04-1991	02-01-2016	Pune
						


//Now once the collection is created write down and execute the LINQ queries for collection 
//as follows :

//a.Display detail of all the employee
//b. Display details of all the employee whose location is not Mumbai
//c. Display details of all the employee whose title is AsstManager
//d. Display details of all the employee whose Last Name start with S

namespace Assessment_4
{
    class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string City { get; set; }
        


        public override string ToString()
        {
               
            return "Employee ID:" +this.EmployeeID + ",Title:"+this.Title+",FirstName: " + this.FirstName+",LastName: "+ this.LastName+",DOB:"+this.DOB +",DOJ:"+this.DOJ+",City:"+this.City;
        }


    }
    class Program
    {

        static void Main(string[] args)
        {
            List<Employee> lobj = new List<Employee>();
            lobj.Add(new Employee { EmployeeID = 1001, FirstName = "Malcolm", LastName = "Daruwalla ", Title = "Manager ", DOB = "16-11-1984", DOJ = "08-06-2011", City = "Mumbai" });
            lobj.Add(new Employee { EmployeeID = 1002, FirstName = "Asdin", LastName = "Dhalla ", Title = "AsstManager ", DOB = "20-08-1994", DOJ = "07-07-2012", City = "Mumbai" });
            lobj.Add(new Employee { EmployeeID = 1003, FirstName = "Madhavi", LastName = "Oza ", Title = "Consultant ", DOB = "14-11-1987", DOJ = "12-04-2015", City = "Pune" });
            lobj.Add(new Employee { EmployeeID = 1004, FirstName = "Saba", LastName = "Shaikh", Title = "SE ", DOB = "03-06-1990", DOJ = "02-02-2016", City = "Pune" });
            lobj.Add(new Employee { EmployeeID = 1005, FirstName = "Nazia", LastName = "Shaikh", Title = "SE ", DOB = "08-03-1991", DOJ = "02-02-2016", City = "Mumbai" });
            lobj.Add(new Employee { EmployeeID = 1006, FirstName = "Amit", LastName = "Pathak ", Title = "Consultant ", DOB = "07-11-1989", DOJ = "08-08-2014", City = "Chennai" });
            lobj.Add(new Employee { EmployeeID = 1007, FirstName = "Vijay", LastName = "Natrajan", Title = "Consultant ", DOB = "02-12-1989", DOJ = "01-06-2015", City = "Mumbai" });
            lobj.Add(new Employee { EmployeeID = 1008, FirstName = "Rahul", LastName = "Dubey ", Title = "Associate", DOB = "11-11-1993", DOJ = "06-11-2014", City = "Chennai" });
            lobj.Add(new Employee { EmployeeID = 1009, FirstName = "Suresh", LastName = "Mistry", Title = "Associate ", DOB = "12-08-1992", DOJ = "03-12-2014", City = "Chennai" });
            lobj.Add(new Employee { EmployeeID = 1010, FirstName = "Sumit", LastName = "Shah ", Title = "Manager ", DOB = "12-04-1991", DOJ = "02-01-2016", City = "Pune" });

            //a.Display detail of all the employee
            Console.WriteLine("\n\nDisplay detail of all the employee\n");
            foreach (Employee obj in lobj)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine("\n\nDisplay details of all the employee whose location is not Mumbai\n");
            // b.Display details of all the employee whose location is not Mumbai
            List<Employee> lobj1 = (from obj in lobj where obj.City != "Mumbai" select obj).ToList();
            foreach (Employee obj in lobj1)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine("\n\nDisplay details of all the employee whose title is AsstManager\n");
            //c. Display details of all the employee whose title is AsstManager
            List<Employee> lobj2 = (from obj in lobj where obj.Title.Equals("AsstManager") select obj).ToList();
            foreach (Employee obj in lobj2)
            {
                Console.WriteLine(obj);
            }

            //d. Display details of all the employee whose Last Name start with S
            Console.WriteLine("\n\nDisplay details of all the employee whose Last Name start with S\n");
            List<Employee> lobj3 = (from obj in lobj where obj.LastName.StartsWith("S") select obj).ToList();
            foreach (Employee obj in lobj3)
            {
                Console.WriteLine(obj);
            }

            Console.ReadLine();
        
        }
    }
}
