using System;


namespace ClassLibraryAssignment
{
     public class ClassLibrary
    {
        public static string CalculateConcession(int age,int _TotalFare)
        {
            if (age <= 5)
                return "Little Champs - Free Ticket";
            else if (age > 60)
                return $"Senior Citizen Price is {((_TotalFare) / 100) * 30}";
            else
                return $"Ticket Booked Price is {_TotalFare}";

        }
    }
}
