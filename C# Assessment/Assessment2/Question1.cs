using System;
using System.Diagnostics;

//1.Create an Abstract class Student with Name, StudentId, Grade as members
//and also an abstract method Boolean Ispassed(grade) which takes grade as an input
//and checks whether student passed the course or not.  

//Create 2 Sub classes Undergraduate and Graduate that inherits all members of the student
//and overrides Ispassed(grade) method

//For the UnderGrad class, if the grade is above 70.0, then isPassed returns true,
//otherwise it returns false. For the Grad class, if the grade is above 80.0, then isPassed returns true,
//otherwise returns false.

//Test the above by creating appropriate objects

namespace Assessment2
{
    abstract class  Student{
        public string _Name {  get; set; }
        public string _StudentID { get; set; }
        public float _Grade { get; set; }
        public abstract bool IsPassed(float grade);
    }

    class Undergraduate : Student
    {
        private string name;
        private string studID;
        private float grade;

        public Undergraduate(string name, string studID, float grade)
        {
            this.name = name;
            this.studID = studID;
            this.grade = grade;
        }

        public override bool IsPassed(float grade)
        {
            if (grade > 70) { 
                return true;
            }
            return false;
        }

    }

    class Graduate : Student
    {

        private string name;
        private string studID;
        private float grade;

        public Graduate(string name, string studID, float grade)
        {
            this.name = name;
            this.studID = studID;
            this.grade = grade;
        }
        public override bool IsPassed(float grade)
        {
            if (grade > 80)
            {
                return true;
            }
            return false;
        }

    }



    class Question1
    {
        static void Main(string[] args)
        {
            bool res = false;
            Console.WriteLine("Select your choice: \n1.Student is undergraduate\n2.Student graduate\n");
            try
            {
                int choice = int.Parse(Console.ReadLine());
                Console.Write("Enter the student name:");
                string name = Console.ReadLine();
                Console.Write("\nEnter the student ID:");
                string studID = Console.ReadLine();
                Console.Write("\nEnter the student grade:");
                float grade = Convert.ToSingle(Console.ReadLine());
                
                switch (choice) {
                    case 1:
                        Student uGradeStudent = new Undergraduate(name,studID,grade);
                        res = uGradeStudent.IsPassed(grade);
                        break;
                    case 2:
                        Student gradStudent = new Graduate(name, studID, grade);                        
                        res = gradStudent.IsPassed(grade);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }       
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\n=========Result============");
            if (res)
            {
                Console.WriteLine("Student is passed");
            }
            else
            {
                Console.WriteLine("Student is failed");
            }

            Console.WriteLine("\nPress enter to exit");
            Console.Read();


        }
    }
}
