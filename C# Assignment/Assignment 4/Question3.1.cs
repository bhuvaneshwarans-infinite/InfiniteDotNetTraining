using System;


//3.Create a Class called Doctor with RegnNo, Name, Feescharged as Private members. Create properties to give values and also to display the same.


namespace Assignment4Solving
{

    class Doctor
    {
        private string _RegNo;
        private string _Name;
        private float _Feescharged;

        public string RegNo
        {
            get { return _RegNo; }
            set { _RegNo = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public float Feescharged
        {
            get { return _Feescharged; }
            set { _Feescharged = value; }
        }

        public void DisplayData()
        {
            Console.WriteLine("\n\n===============Result==============");
            Console.WriteLine("Name of the doctor: {0}", _Name);
            Console.WriteLine("RegNo of the doctor: {0}", _RegNo);
            Console.WriteLine("FeesCharged by the doctor: {0}", _Feescharged);
        }

    }
    class FeedAndDisplayData
    {
        static void Main()
        {
            Doctor dt = new Doctor();
            Console.Write("Enter RegNo:");
            dt.RegNo = Console.ReadLine();
            Console.Write("\nEnter Name:");
            dt.Name = Console.ReadLine();
            Console.Write("\nEnter Fees:");
            dt.Feescharged=Convert.ToSingle(Console.ReadLine());
            dt.DisplayData();
            Console.WriteLine("\n\n***Press Enter to exit***");
            Console.ReadKey();
        }
    }
}
