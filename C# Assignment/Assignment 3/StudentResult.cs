using Assigment3Solving;
using System;

//2.Create a class called student which has data members like rollno, name, class, Semester, branch, int [] marks = new int marks[5](marks of 5 subjects)

//- Pass the details of student like rollno, name, class, SEM, branch in constructor

//-For marks write a method called GetMarks() and give marks for all 5 subjects

//-Write a method called displayresult, which should calculate the average marks

//-If marks of any one subject is less than 35 print result as failed
//-If marks of all subject is >35, but average is < 50 then also print result as failed
//-If avg > 50 then print result as passed.

//-Write a DisplayData() method to display all object members values.

namespace Assigment3Solving
{  
        public class Student
        {
            internal string rollNo;
            internal string name;
            internal string studentClass;
            internal string sem;
            internal string branch;
            internal int[] marks = new int[5];

            public Student()
            {
            }

            public Student(string rollNo, string name, string studentClass, string sem, string branch)
            {
                this.rollNo = rollNo;
                this.name = name;
                this.studentClass = studentClass;
                this.sem = sem;
                this.branch = branch;
            }

            public void GetMarks()
            {
                Console.WriteLine("Enter marks for 5 subjects:");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write("Subject {0} :",(i + 1));
                    marks[i] = Convert.ToInt32(Console.ReadLine());
                }
            }

                      
        }

    class StudentOps: Student
    {
        public StudentOps(string rollNo, string studName, string studClass, string sem, string branch) : base(rollNo, studName, studClass, sem, branch) { }
        public void DisplayData()
        {
            Console.WriteLine("\n\n============Student Details=============");
            Console.WriteLine("Roll No: " + rollNo);
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Class: " + studentClass);
            Console.WriteLine("Branch: " + branch);
            Console.WriteLine("Sem: " + sem);
        }

        public void DisplayResult()
        {
            int totalMarks = 0;
            bool resultStatus = false;

            foreach (int mark in marks)
            {
                if (mark < 35)
                {
                    resultStatus = true;
                    break;
                }
                totalMarks += mark;
            }

            if (resultStatus || totalMarks / 5 < 50)
            {
                Console.WriteLine("\nFinal Result:Roll No " + this.rollNo + " failed to clear the criteria to get pass.");
            }
            else
            {
                Console.WriteLine("\nFinal Result:Congratulations!! Roll No " + this.rollNo + " have passed the exam ");
            }
        }
    }

    class StudentResult
    {
        static void Main()
            {
            Console.Write("Enter the Roll No:");
            string rollNo = Console.ReadLine();
            Console.Write("Enter the Student Name:");
            string studName = Console.ReadLine();
            Console.Write("Enter the Class:");
            string studClass = Console.ReadLine();
            Console.Write("Enter the Semester:");
            string sem = Console.ReadLine();
            Console.Write("Enter the Branch:");
            string branch = Console.ReadLine();

            StudentOps sos = new StudentOps(rollNo, studName, studClass, sem, branch);
            sos.GetMarks();
            sos.DisplayData();
            sos.DisplayResult();
            Console.Read();
            }
        }

    }

