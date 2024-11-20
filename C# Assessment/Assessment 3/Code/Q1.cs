using System;
using System.Collections.Generic;
using System.Linq;


//1.Write a program to find the Sum and the Average points scored by the teams in the IPL.
//Create a Class called CricketTeam that has a function called Pointscalculation(int no_of_matches) 
//that takes no.of matches as input and accepts that many scores from the user. 
//The function should then return the Count of Matches, Average and Sum of the scores.

namespace Assessment3
{
    class CricketTeam
    {
        public static (int count,double avg,int sum) Pointscalculation(int no_of_matches)
        {
            List<int> Scores = new List<int>();
            for (int i = 0; i < no_of_matches; i++) {
                Console.Write($"Enter the match {i + 1} score:");
                Scores.Add(Convert.ToInt32(Console.ReadLine()));
            }
            double avg= Scores.Sum() / no_of_matches;
            //Console.WriteLine(Scores.Count() +" "+ avg + " " + Scores.Sum());
            return (Scores.Count(), avg, Scores.Sum());
        }
    }
    class Q1
    {
        static void Main()
        {
            Console.Write("Enter the no of matches :");
            try
            {
                int matches = Convert.ToInt32(Console.ReadLine());
                if (matches < 0)
                {
                    throw new ArgumentException("matches can't be in a negative value");
                }
                var matchData = CricketTeam.Pointscalculation(matches);
                Console.WriteLine("\n================Results==================\n");
                Console.WriteLine("Sum of all matches score :" + matchData.sum);
                Console.WriteLine("Avg of all matches score :" + matchData.avg);
                Console.WriteLine("Count of all matches score :" + matchData.count);
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Enter the correct format data");
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("\n\nPlease press any key to exit");
            Console.ReadKey();
           
        }
    }
}
