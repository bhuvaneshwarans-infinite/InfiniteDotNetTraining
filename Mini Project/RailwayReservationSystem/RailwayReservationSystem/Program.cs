using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RailwayReservationSystem
{
    class Program
    {
        public static SqlConnection con;
        public static SqlCommand cmd;

        static SqlConnection getConnection()
        {
            con = new SqlConnection("Data Source=ICS-LT-64MPV44\\SQLEXPRESS02; initial catalog=RailwayResDB;integrated security = true; ");
            con.Open();
            return con;
        }
     

        public static Users CurLogusr = new Users();
        public static void CurrentUser(Users uObj)
        {
            if (uObj != null)
            {
                CurLogusr.UserName = uObj.UserName;
                CurLogusr.UserID = uObj.UserID;
                CurLogusr.AdminStatus = uObj.AdminStatus;
            }

        }
        static bool CheckUser((string userName, string userPassword) credentials)
        {

            var query = "SELECT UserID ,UserName ,AdminStatus FROM Users where UserName=@UserName and UserPassword=@UserPassword ";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@UserName",credentials.userName);
            cmd1.Parameters.AddWithValue("@UserPassword",credentials.userPassword);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                Users usr = new Users();
                usr.UserID = Convert.ToInt32(dr1[0].ToString());
                usr.UserName = dr1[1].ToString();
                usr.AdminStatus= Convert.ToBoolean(dr1[2].ToString());
                //for (int i = 0; i < dr1.FieldCount; i++)
                //{
                //    Console.WriteLine(dr1[i]);
                //}
                CurrentUser(usr);
                return true;
            }
            else
                return false;

        }
        static (string, string) loginInput()
        {
            Console.WriteLine("\nEnter Name:");
            string UserName = Console.ReadLine().Trim();
            Console.WriteLine("Enter Password:");
            string UserPassword = Console.ReadLine().Trim();
            return (UserName, UserPassword);

        }
        static bool CreateUser()
        {
            con = getConnection();
            Users user = new Users();
            (user.UserName, user.UserPassword) = loginInput();
            bool DuplicateCheck = CheckUser((user.UserName, user.UserPassword));
            if (!(string.IsNullOrWhiteSpace(user.UserName) && string.IsNullOrWhiteSpace(user.UserPassword) && DuplicateCheck == false))
            {
                user.AdminStatus = false;
                cmd = new SqlCommand("INSERT INTO dbo.Users(UserName ,UserPassword ,AdminStatus)VALUES (@UserName,@UserPassword,@AdminStatus)", getConnection());

                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                cmd.Parameters.AddWithValue("@AdminStatus", user.AdminStatus);

                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Successfully created your credentials");
                    CurrentUser(user);
                    return true;
                }
                else
                {
                    Console.WriteLine("No records were inserted.");
                }
            }
            else if (string.IsNullOrWhiteSpace(user.UserName) && string.IsNullOrWhiteSpace(user.UserPassword))
            {
                Console.WriteLine("Data can't be empty");
            }
            else
            {
                Console.WriteLine("*************User Already Exists.****************\nCreate an account with different username/password to proceed with account creation");
            }

            return false;

        }

        //Stations
        static void ShowStations()
        {

            var query = "SELECT * FROM Stations";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());

            SqlDataReader dr1 = cmd1.ExecuteReader();
            Console.WriteLine("\n==============Available Stations===================");
            while (dr1.Read())
            {
                Console.WriteLine($"StationID:{dr1["StationID"]},StationName:{dr1["StationName"]}");
            }         


        }
        static bool CheckStation(string station)
        {
            var query = "SELECT * FROM Stations where StationName=@station";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@station", station);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            { 
                return true;
            }
            return false;
        }
        static bool CheckStationByID(int stationID)
        {
            var query = "SELECT * FROM Stations where StationID=@stationID";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@stationID", stationID);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static void AddStation()
        {
            Stations st = new Stations();
            Console.Write("Enter the station name :");
            string stationName = Console.ReadLine();
            if (!CheckStation(stationName))
            {
                cmd = new SqlCommand("INSERT INTO dbo.Stations(StationName)VALUES (@StationName)", getConnection());

                cmd.Parameters.AddWithValue("@StationName", stationName);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Station Successfully Added");
                }
                else
                {
                    Console.WriteLine("Station not added");
                }
            }
        }
        static void ModifyStation()
        {
            ShowStations();
            Console.Write("Enter the station ID you like to update :");
            int stationID = Convert.ToInt32(Console.ReadLine());

            if (CheckStationByID(stationID))
            {
                Console.Write("Enter the station name to update :");
                string stationName = Console.ReadLine();

                cmd = new SqlCommand("Update dbo.Stations set StationName=@StationName where StationID=@StationID", getConnection());
                cmd.Parameters.AddWithValue("@StationName", stationName);
                cmd.Parameters.AddWithValue("@StationID", stationID);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Updated Station detail successfully");
                }
                else
                {
                    Console.WriteLine("No Updation required");
                }
                ShowStations();
            }
            else
            {
                Console.WriteLine("Invalid station ID");
            }
        }
        static void DeleteStation()
        {
            ShowStations();
            Console.Write("Enter the station ID you like to delete :");
            int stationID = Convert.ToInt32(Console.ReadLine());

            if (CheckStationByID(stationID))
            {
                cmd = new SqlCommand("Delete from Stations where StationID=@StationID", getConnection());
                cmd.Parameters.AddWithValue("@StationID", stationID);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Deleted Station successfully");
                }
                ShowStations();
            }
            else
            {
                Console.WriteLine("Invalid station ID");
            }
        }

        // Seat Classes

        static void ShowSeatClass()
        {        
            var query = "SELECT * FROM seatClasses";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());

            SqlDataReader dr1 = cmd1.ExecuteReader();
            Console.WriteLine("\n==============Available Seat Classes===================");
            while (dr1.Read())
            {
                Console.WriteLine($"SeatClassID:{dr1["SeatClassID"]},SeatClassName:{dr1["SeatClassName"]}");
            }
        }
        static bool CheckSeatClass(string seatName)
        {
           
            var query = "SELECT * FROM seatClasses where SeatClassName=@SeatClassName";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@SeatClassName", seatName.ToLower().Trim());
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static bool CheckSeatClassByID(int seatClassID)
        {

            var query = "SELECT * FROM seatClasses where SeatClassID=@SeatClassID";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@SeatClassID", seatClassID);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static void AddSeatClass()
        {

            Console.Write("Enter the seatclass name :");
            string ClassName = Console.ReadLine();
            if (!CheckSeatClass(ClassName))
            {
                cmd = new SqlCommand("INSERT INTO dbo.SeatClasses(SeatClassName)VALUES (@SeatClassName)", getConnection());

                cmd.Parameters.AddWithValue("@SeatClassName", ClassName);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Seat Classes Successfully Added");
                }
                else
                {
                    Console.WriteLine("Seat Class not added");
                }
            }
        }
        static void ModifySeatClass()
        {
            ShowSeatClass();
            Console.Write("Enter the seat class ID you like to update :");
            int seatClassID = Convert.ToInt32(Console.ReadLine());

            if (CheckSeatClassByID(seatClassID))
            {
                Console.Write("Enter the Seat Class name to update :");
                string seatClassName = Console.ReadLine();

                cmd = new SqlCommand("Update dbo.SeatClasses set SeatClassName=@seatClassName where SeatClassID=@seatClassID", getConnection());
                cmd.Parameters.AddWithValue("@seatClassName", seatClassName);
                cmd.Parameters.AddWithValue("@seatClassID", seatClassID);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Updated Seat Class detail successfully");
                }
                else
                {
                    Console.WriteLine("No Updation required");
                }
                ShowStations();
            }
            else
            {
                Console.WriteLine("Invalid Seat Class ID");
            }

            ShowSeatClass();
        }
        static void DeleteSeatClass()
        {
            ShowSeatClass();
            Console.Write("Enter the seatClass ID you like to delete :");
            int seatClassID = Convert.ToInt32(Console.ReadLine());

            if (CheckSeatClassByID(seatClassID))
            {

                cmd = new SqlCommand("Delete from dbo.SeatClasses where SeatClassID=@seatClassID", getConnection());
                cmd.Parameters.AddWithValue("@seatClassID", seatClassID);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Deleted Successfully");
                }
            }
            else
            {
                Console.WriteLine("Enter valid ID to delete");
            }
        }

        //Days
        static void ShowDay()
        {
            var query = "SELECT * FROM Days";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());

            SqlDataReader dr1 = cmd1.ExecuteReader();
            Console.WriteLine("\n==============Available Days===================");
            while (dr1.Read())
            {
                Console.WriteLine($"dayID:{dr1["dayID"]},dayName:{dr1["dayName"]}");
            }
        }
        static bool CheckDay(string days)
        {
            var query = "SELECT * FROM Days where dayName=@days";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@days", days.ToLower().Trim());
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static bool CheckDayID(int daysID)
        {
            var query = "SELECT * FROM Days where dayID=@daysID";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@daysID", daysID);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static void AddDay()
        {
            Days dat = new Days();
            Console.Write("Enter the Day name :");
            string DayName = Console.ReadLine();
            if (!CheckDay(DayName))
            {
                cmd = new SqlCommand("INSERT INTO dbo.Days(dayName)VALUES (@DayName)", getConnection());

                cmd.Parameters.AddWithValue("@DayName", DayName);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {                  
                    Console.WriteLine("Days Successfully Added");
                }
                else
                {
                    Console.WriteLine("Days not added");
                }
            }
        }
        static void ModifyDay()
        {
            ShowDay();
            Console.Write("Enter the day ID you like to update :");
            int dayID = Convert.ToInt32(Console.ReadLine());
            if (CheckDayID(dayID))
            {
                Console.Write("Enter the day name to update :");
                string DayName = Console.ReadLine();

                cmd = new SqlCommand("Update dbo.Days set dayName=@dayName where dayID=@dayID", getConnection());
                cmd.Parameters.AddWithValue("@dayName", DayName);
                cmd.Parameters.AddWithValue("@dayID", dayID);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Days Successfully Updated");
                }
                else
                {
                    Console.WriteLine("No Updation required");
                }
            }
            else
            {
                Console.WriteLine("Days ID is invalid");
            }

            ShowDay();
        }
        static void DeleteDay()
        {
            ShowDay();
            Console.Write("Enter the day ID you like to delete :");
            int DayID = Convert.ToInt32(Console.ReadLine());

            if (CheckDayID(DayID))
            {
                cmd = new SqlCommand("Delete from dbo.Days where dayID=@DayID", getConnection());
                cmd.Parameters.AddWithValue("@dayID", DayID);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Days Successfully Deleted");
                }
            }
            else
            {
                Console.WriteLine("Enter valid ID to delete");
            }
        }
        //Trains
        static bool CheckTrainNameIfExists(string trainName)
        {
            var query = "SELECT * FROM TrainTable where TrainName=@TrainName";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@TrainName", trainName);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static void StoreAvailDay(int dayID,int trainNo)
        {
            cmd = new SqlCommand("INSERT INTO TrainAvailableDaysTable(TrainNo,DayID)VALUES (@trainNo,@dayID)", getConnection());

            cmd.Parameters.AddWithValue("@dayID", dayID);
            cmd.Parameters.AddWithValue("@trainNo", trainNo);
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                Console.WriteLine($"Day:{dayID} Successfully Added");
            }
            else
            {
                Console.WriteLine($"Day:{dayID}  not added");
            }
        }
        static void StorePerDayTrainDetails(TrainAvailablePerDayTableClass addJourniesData)
        {

            cmd = new SqlCommand("INSERT INTO TrainAvailablePerDayTable" +
                "(TrainNo,DayID,FromStationID,ToStationID,TrainStartTime,TrainEndTime,TrainJourneyBasePrice)VALUES " +
                "(@TrainNo,@DayID,@FromStationID,@ToStationID,@TrainStartTime,@TrainEndTime,@TrainJourneyBasePrice)", getConnection());

            cmd.Parameters.AddWithValue("@DayID", addJourniesData.DayID);
            cmd.Parameters.AddWithValue("@TrainNo", addJourniesData.TrainNo);
            cmd.Parameters.AddWithValue("@FromStationID", addJourniesData.FromStationID);
            cmd.Parameters.AddWithValue("@ToStationID", addJourniesData.ToStationID);
            cmd.Parameters.AddWithValue("@TrainStartTime", addJourniesData.TrainStartTime);
            cmd.Parameters.AddWithValue("@TrainEndTime", addJourniesData.TrainEndTime);
            cmd.Parameters.AddWithValue("@TrainJourneyBasePrice", addJourniesData.TrainJourneyBasePrice);

            string query = "INSERT INTO TrainAvailablePerDayTable" +
                "(TrainNo,DayID,FromStationID,ToStationID,TrainStartTime,TrainEndTime)VALUES " +
                "(@DayID,@TrainNo,@FromStationID,@ToStationID,@TrainStartTime,@TrainEndTime)";


            string debugQuery = query
                .Replace("@DayID", addJourniesData.DayID.ToString())
                .Replace("@TrainNo", $"{addJourniesData.TrainNo}") 
                .Replace("@FromStationID", addJourniesData.FromStationID.ToString())
                .Replace("@ToStationID", addJourniesData.ToStationID.ToString())
                .Replace("@TrainStartTime", $"'{addJourniesData.TrainStartTime}'")
                .Replace("@TrainEndTime", $"'{addJourniesData.TrainEndTime}'");

            Console.WriteLine(debugQuery);

            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                Console.WriteLine($"Journey Successfully Added");
            }
            else
            {
                Console.WriteLine($"Journey not added");
            }
        }
        static bool CheckRouteAvailable(int TrainNo,int fromStation,int toStation)
        {
            var query = "SELECT * FROM TrainRouteTable where TrainNo=@TrainNo " +
                "and (FromStationID=@FromStationID and ToStationID=@ToStationID) OR " +
                "(FromStationID=@ToStationID and ToStationID=@FromStationID)";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@TrainNo", TrainNo);
            cmd1.Parameters.AddWithValue("@FromStationID", fromStation);
            cmd1.Parameters.AddWithValue("@ToStationID", toStation);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
       static void AddTrainRoute(TrainAvailablePerDayTableClass trainAvailablePerDayTableClass)
        {
           
            //Station Sequence
            Console.Write("Enter the total stations in this trip:");
            int TotalStations = Convert.ToInt32(Console.ReadLine());
            List<TrainRouteTableClass> trainSceduleList = new List<TrainRouteTableClass>();
            for(int i=0;i< TotalStations; i++)
            {
                TrainRouteTableClass t = new TrainRouteTableClass();
                ShowStations();
                Console.WriteLine($"Enter the {i+1} staionID:");


                    if (i == 0)
                    {
                        Console.WriteLine("Intial station is added");
                        t.CurrentStation = trainAvailablePerDayTableClass.FromStationID;
                        t.ArrivesAt = trainAvailablePerDayTableClass.TrainStartTime;
                        t.DepartureAt = trainAvailablePerDayTableClass.TrainEndTime;
                    }
                    else if (i == TotalStations -1)
                    {
                        Console.WriteLine("Final station is added");
                        t.CurrentStation = trainAvailablePerDayTableClass.ToStationID;
                        t.ArrivesAt = trainAvailablePerDayTableClass.TrainStartTime;
                        t.DepartureAt = trainAvailablePerDayTableClass.TrainEndTime;
                    }
                    else
                    {
                        t.CurrentStation = Convert.ToInt32(Console.ReadLine());
                        if (CheckStationByID(t.CurrentStation))
                        {
                            while (true)
                            {
                                try
                                {
                                    Console.Write("Enter the arrival time to this station(hr:min:sec):");
                                    DateTime ArrivalTime = Convert.ToDateTime(Console.ReadLine());
                                    Console.Write("Enter the departure time to this station(hr:min:sec):");
                                    DateTime DepartureTime = Convert.ToDateTime(Console.ReadLine());
                                    t.ArrivesAt = ArrivalTime.TimeOfDay;
                                    t.DepartureAt = DepartureTime.TimeOfDay;
                                    break;                                
                                }
                                catch (FormatException e)
                                {
                                    Console.WriteLine("Error:Enter the input in specified format.");
                                }

                            }
                        
                        }
                        else
                        {
                            Console.WriteLine("Error:Invalid Station ID.Enter your stationID Again");
                        }
                    }

                
                t.TrainNo = trainAvailablePerDayTableClass.TrainNo;
                t.FromStationID = trainAvailablePerDayTableClass.FromStationID;
                t.ToStationID = trainAvailablePerDayTableClass.ToStationID;
                t.StationSequences = i + 1;
               
                trainSceduleList.Add(t);
            }
            int res = -1;
            foreach (var data in trainSceduleList)
            {
                cmd = new SqlCommand("INSERT INTO TrainRouteTable (TrainNo, FromStationID, " +
    "ToStationID, StationSeq, CurrentStation, ArrivesAt, DepartureAt) " +
    "VALUES(@TrainNo, @FromStationID, " +
    "@ToStationID, @StationSeq, @CurrentStation, @ArrivesAt, @DepartureAt)", getConnection());

                cmd.Parameters.AddWithValue("@TrainNo", data.TrainNo);
                cmd.Parameters.AddWithValue("@FromStationID", data.FromStationID);
                cmd.Parameters.AddWithValue("@ToStationID", data.ToStationID);
                cmd.Parameters.AddWithValue("@StationSeq", data.StationSequences);
                cmd.Parameters.AddWithValue("@CurrentStation", data.CurrentStation);
                cmd.Parameters.AddWithValue("@ArrivesAt", data.ArrivesAt);
                cmd.Parameters.AddWithValue("@DepartureAt", data.DepartureAt);
                res = cmd.ExecuteNonQuery();
            }

            if (res >0)
            {
                Console.WriteLine($"Journey Successfully Added");
            }
            else
            {
                Console.WriteLine($"Journey not added");
            }


        }
        static bool CheckClassAvailableOnTrain(ClassesAvailableInTrainClass classesAvailableInTrainClass)
        {
            var query = " Select * from ClassesAvailableInTrain where TrainNo=@TrainNo and ClassID=@ClassID ";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@TrainNo", classesAvailableInTrainClass.TrainNo);
            cmd1.Parameters.AddWithValue("@ClassID", classesAvailableInTrainClass.ClassID);

            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static bool AddTrainClass(ClassesAvailableInTrainClass classesAvailableInTrainClass)
        {
            if (!CheckClassAvailableOnTrain(classesAvailableInTrainClass))
            {          

            cmd = new SqlCommand("INSERT INTO ClassesAvailableInTrain(TrainNo ,ClassID ,SeatPrice) VALUES(@TrainNo, @ClassID, @SeatPrice)", getConnection());

            cmd.Parameters.AddWithValue("@TrainNo", classesAvailableInTrainClass.TrainNo);
            cmd.Parameters.AddWithValue("@ClassID", classesAvailableInTrainClass.ClassID);
            cmd.Parameters.AddWithValue("@SeatPrice", classesAvailableInTrainClass.SeatPrice);


            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                Console.WriteLine($"Train Seat Class Successfully Added");
                return true;
            }
            else
            {
                Console.WriteLine($"Error:Train Seat Class not added");
                return false;
            }
            }
            else
            {
                Console.WriteLine("Already Class is exists for this train.");
                return false;
            }
        }
        static void AddSeatAvailabilityInTrain(SeatsAvailabilityPerClass seatsAvailabilityPerClass)
        {
            cmd = new SqlCommand("INSERT INTO dbo.SeatsAvailabilityPerClass(TrainNo, ClassID, " +
                "LowerBerthAvailableSeats, MiddleBerthAvailableSeats, UpperBerthAvailableSeats)VALUES" +
                "(@TrainNo,@ClassID,@LowerBerthAvailableSeats,@MiddleBerthAvailableSeats," +
                "@UpperBerthAvailableSeats)", getConnection());

            cmd.Parameters.AddWithValue("@TrainNo", seatsAvailabilityPerClass.TrainNo);
            cmd.Parameters.AddWithValue("@ClassID", seatsAvailabilityPerClass.ClassID);
            cmd.Parameters.AddWithValue("@LowerBerthAvailableSeats", seatsAvailabilityPerClass.LowerBerthAvailableSeats);
            cmd.Parameters.AddWithValue("@MiddleBerthAvailableSeats", seatsAvailabilityPerClass.MiddleBerthAvailableSeats);
            cmd.Parameters.AddWithValue("@UpperBerthAvailableSeats", seatsAvailabilityPerClass.UpperBerthAvailableSeats);


            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                Console.WriteLine($"Added Seat Availablity Class Successfully Added");

            }
            else
            {
                Console.WriteLine($"Error:Added Seat Availablity not added");

            }
        }
        static bool CheckSeatPerClassInTrain(int trainNo,int classID)
        {
            cmd = new SqlCommand("Select * from SeatsAvailabilityPerClass where TrainNo=@TrainNo and ClassID=@ClassID",getConnection());
            cmd.Parameters.AddWithValue("@TrainNo", trainNo);
            cmd.Parameters.AddWithValue("@ClassID", classID);

            SqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static bool CheckClassAvailableOnTrain(int trainNo)
        {
            cmd = new SqlCommand("Select * from SeatsAvailabilityPerClass where TrainNo=@TrainNo", getConnection());
            cmd.Parameters.AddWithValue("@TrainNo", trainNo);
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static int TotalCoachesAvailable()
        {
            cmd = new SqlCommand("Select Count(*) from SeatClasses", getConnection());
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                return Convert.ToInt32(dr1[0].ToString());
            }
            return -1;
        }
        static void AddTrainClassesAndSeats(int TrainNo)
        {
            if (!CheckClassAvailableOnTrain(TrainNo)) {
                int actualCoachesAvailable = TotalCoachesAvailable();
                Console.Write("There are {0} Coaches available\nHow many seat classes/Coach available in train:", actualCoachesAvailable);
                int TotalCoaches = -1;
                while (true)
                {
                    TotalCoaches = Convert.ToInt32(Console.ReadLine());
                    if(TotalCoaches >0 && TotalCoaches <= actualCoachesAvailable)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Please Select in the range of 1 and {actualCoachesAvailable}");
                    }
                }
                             

            ClassesAvailableInTrainClass classesAvailableInTrain = new ClassesAvailableInTrainClass();
            for (int i = 0; i < TotalCoaches; i++)
            {
                ShowSeatClass();
                while (true)
                {
                    Console.Write("Enter your seat class ID:");
                    classesAvailableInTrain.ClassID = Convert.ToInt32(Console.ReadLine());
                    classesAvailableInTrain.TrainNo = TrainNo;
                    if (CheckSeatClassByID(classesAvailableInTrain.ClassID))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error:Invalid Class.Please enter correct classID");
                    }
                }
                Console.Write("Enter the seat price:");
                try
                {
                    classesAvailableInTrain.SeatPrice = Convert.ToDecimal(Console.ReadLine());
                }
                catch(OverflowException oe)
                {
                    Console.WriteLine("Invalid Price value.");
                }

                if (AddTrainClass(classesAvailableInTrain))
                {
                    if (!CheckSeatPerClassInTrain(classesAvailableInTrain.ClassID, classesAvailableInTrain.TrainNo))
                    {                    
                    SeatsAvailabilityPerClass seatsAvailabilityPerClass = new SeatsAvailabilityPerClass();
                    seatsAvailabilityPerClass.ClassID = classesAvailableInTrain.ClassID;
                    seatsAvailabilityPerClass.TrainNo = classesAvailableInTrain.TrainNo;
                    Console.Write("Available Seats in Lower Berth:");
                    seatsAvailabilityPerClass .LowerBerthAvailableSeats= Convert.ToInt32(Console.ReadLine());
                    Console.Write("Available Seats in Middle Berth:");
                    seatsAvailabilityPerClass.MiddleBerthAvailableSeats = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Available Seats in Upper Berth:");
                    seatsAvailabilityPerClass.UpperBerthAvailableSeats = Convert.ToInt32(Console.ReadLine());
                    AddSeatAvailabilityInTrain(seatsAvailabilityPerClass);
                    }
                    else
                    {
                        Console.WriteLine("Availablitiy per class already have exists.To alter go to modify option");
                    }
                }                
            }
            }
        }
        static void AddTrainAvailDays(TrainTableClass ttc)
        {
            int DayID = -1;
            int TrainNo = ttc.TrainNO; 
            Console.Write("\nHow many days a week train is available:");
            int NoOfDays = Convert.ToInt32(Console.ReadLine());
           // Console.WriteLine("\nWhich are the days?");
          
            for (int i = 0; i < NoOfDays; i++)
            {

                ShowDay();
                Console.WriteLine($"\nWhich is the {i+1} day?");
                while (true)
                {
                    Console.Write("Enter dayID:");
                    DayID = Convert.ToInt32(Console.ReadLine());
                    if (CheckDayID(DayID))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error:Invalid Day ID.Enter your DayID Again");
                    }
                }
                StoreAvailDay(DayID, TrainNo);
                Console.Write("Enter no. of Trips available on selected day:");
                int NoOfTrips = Convert.ToInt32(Console.ReadLine());
                TrainAvailablePerDayTableClass[] trainAvailPerDay = new TrainAvailablePerDayTableClass[NoOfTrips];
                int toStation = -1;
                int fromStation = -1;

                for (int j = 0; j < NoOfTrips; j++)
                {

                    ShowStations();
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine($"Enter {j + 1} Trip Details:");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++");
                    while (true)
                    {
                        Console.Write("Enter From StationID:");
                        fromStation = Convert.ToInt32(Console.ReadLine());
                        if (CheckStationByID(fromStation))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error:Invalid Station ID.Enter your stationID Again");
                        }
                    }

                    while (true)
                    {
                        Console.Write("Enter To StationID:");
                        toStation = Convert.ToInt32(Console.ReadLine());
                        if (CheckStationByID(toStation) && fromStation != toStation)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error:Invalid Station ID.Enter your stationID Again");
                        }
                    }

                    TrainAvailablePerDayTableClass tapd = new TrainAvailablePerDayTableClass();
                    while (true)
                    {
                        try
                        {
                            Console.Write("Enter the Journey Start time(hr:min:sec):");
                            DateTime JourneyStartAt = Convert.ToDateTime(Console.ReadLine());
                            Console.Write("Enter the Journey End time(hr:min:sec):");
                            DateTime JourneyEndsAt = Convert.ToDateTime(Console.ReadLine());                           
                            tapd.TrainStartTime = JourneyStartAt.TimeOfDay;
                            tapd.TrainEndTime = JourneyEndsAt.TimeOfDay;
                            break;
                        }
                        catch(FormatException e)
                        {
                            Console.WriteLine("Error:Enter the input in specified format.");
                        }

                    }

                    Console.Write("Base price for this journey:");
                    tapd.TrainJourneyBasePrice = Convert.ToSingle(Console.ReadLine());


                    tapd.DayID =DayID;
                    tapd.TrainNo = TrainNo;
                    tapd.FromStationID = fromStation;
                    tapd.ToStationID = toStation;

                    StorePerDayTrainDetails(tapd);

                    if(!CheckRouteAvailable(TrainNo, fromStation, toStation))
                    {
                        AddTrainRoute(tapd);
                    }

                    AddTrainClassesAndSeats(TrainNo);

                }



            }

        }
        static void AddTrain()
        {
            TrainTableClass ttc = new TrainTableClass();
            Console.Write("Enter the Train name :");
            ttc.TrainName = Console.ReadLine();
            if (!CheckTrainNameIfExists(ttc.TrainName))
            {
                cmd = new SqlCommand("INSERT INTO TrainTable(TrainName)VALUES (@TrainName);SELECT SCOPE_IDENTITY()", getConnection());

                cmd.Parameters.AddWithValue("@TrainName", ttc.TrainName);
                object NewTrainNo = cmd.ExecuteScalar();
                if (NewTrainNo != null)
                {
                    Console.WriteLine("Train Basic details Successfully Added");
                    ttc.TrainNO = Convert.ToInt32(NewTrainNo);
                    Console.WriteLine("Train ID:" + ttc.TrainNO);
                    AddTrainAvailDays(ttc);
                }
                else
                {
                    Console.WriteLine("Train data not added");
                }
            }
            else
            {
                Console.WriteLine("Train Name already exists.Please go to modify train option.");
            }
        }
        static void ShowTrains()
        {
            var query = "SELECT * FROM TrainTable";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());

            SqlDataReader dr1 = cmd1.ExecuteReader();
            Console.WriteLine("\n==============Available Trains===================");
            while (dr1.Read())
            {
                Console.WriteLine($"Train No:{dr1["TrainNo"]} ,TrainName:{dr1["TrainName"]}");
            }
        }
        static bool CheckTrainID(int TrainNo)
        {
            var query = "SELECT * FROM TrainTable where TrainNo=@TrainNo";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@TrainNo", TrainNo);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }
        static int GetAndReturnTrainNo()
        {
            int TrainNo;
            while (true)
            {
                ShowTrains();
                Console.Write("\nEnter the TrainNo:");
                TrainNo = Convert.ToInt32(Console.ReadLine());
                if (CheckTrainID(TrainNo))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error:Invalid Train No");
                }

            }

            return TrainNo;
        }
        static int GetAndReturnDayID()
        {
            int DayID;
            while (true)
            {
                ShowDay();
                Console.Write("\nEnter the DayID:");
                DayID = Convert.ToInt32(Console.ReadLine());
                if (CheckDayID(DayID))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error:Invalid Day ID");
                }

            }

            return DayID;
        }
        static void UpdateTrainName()
        {       
                Console.Write("Enter the Train Name:");
                string TrainName = Console.ReadLine();
                TrainTableClass trainTableClass = new TrainTableClass();
                trainTableClass.TrainNO = GetAndReturnTrainNo();
                trainTableClass.TrainName = TrainName;
                cmd = new SqlCommand("Update TrainTable set TrainName=@TrainName where TrainNo=@TrainNo", getConnection());

                cmd.Parameters.AddWithValue("@TrainNo", trainTableClass.TrainNO);
                cmd.Parameters.AddWithValue("@TrainName", trainTableClass.TrainName);              


                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine($"Update Train Name Successfully");

                }
                else
                {
                    Console.WriteLine($"Error:Train Name not updated");
                }
            
        }
        static void DeleteTrainAvailDays(int TrainNo,int DayID)
        {
            cmd = new SqlCommand("Delete from TrainAvailableDaysTable where TrainNo=@TrainNo and DayID=@DayID", getConnection());

            cmd.Parameters.AddWithValue("@TrainNo", TrainNo);
            cmd.Parameters.AddWithValue("@DayID", DayID);
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                Console.WriteLine($"Deleted from TrainAvailableDaysTable Successfully");
                cmd = new SqlCommand("Delete from TrainAvailablePerDayTable where TrainNo=@TrainNo and DayID=@DayID", getConnection());

                cmd.Parameters.AddWithValue("@TrainNo", TrainNo);
                cmd.Parameters.AddWithValue("@DayID", DayID);
                res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine($"Deleted from TrainAvailablePerDayTable Successfully");
                }
                else
                {
                    Console.WriteLine($"Error:Unable to Delete from TrainAvailablePerDayTable");
                }
            }
            else
            {
                Console.WriteLine($"Error:Unable to Delete from TrainAvailableDaysTable");
            }
        }
        static void GetAndSetTrips(int dayID,int trainNo)
        {
            Console.Write("Enter no. of Trips available on selected day:");
            int NoOfTrips = Convert.ToInt32(Console.ReadLine());
            TrainAvailablePerDayTableClass[] trainAvailPerDay = new TrainAvailablePerDayTableClass[NoOfTrips];
            int toStation = -1;
            int fromStation = -1;

            for (int j = 0; j < NoOfTrips; j++)
            {

                ShowStations();
                Console.WriteLine("++++++++++++++++++++++++++++++++++++");
                Console.WriteLine($"Enter {j + 1} Trip Details:");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++");
                while (true)
                {
                    Console.Write("Enter From StationID:");
                    fromStation = Convert.ToInt32(Console.ReadLine());
                    if (CheckStationByID(fromStation))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error:Invalid Station ID.Enter your stationID Again");
                    }
                }

                while (true)
                {
                    Console.Write("Enter To StationID:");
                    toStation = Convert.ToInt32(Console.ReadLine());
                    if (CheckStationByID(toStation) && fromStation != toStation)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error:Invalid Station ID.Enter your stationID Again");
                    }
                }

                TrainAvailablePerDayTableClass tapd = new TrainAvailablePerDayTableClass();
                while (true)
                {
                    try
                    {
                        Console.Write("Enter the Journey Start time(hr:min:sec):");
                        DateTime JourneyStartAt = Convert.ToDateTime(Console.ReadLine());
                        Console.Write("Enter the Journey End time(hr:min:sec):");
                        DateTime JourneyEndsAt = Convert.ToDateTime(Console.ReadLine());
                        tapd.TrainStartTime = JourneyStartAt.TimeOfDay;
                        tapd.TrainEndTime = JourneyEndsAt.TimeOfDay;
                        break;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Error:Enter the input in specified format.");
                    }

                }

                Console.Write("Base price for this journey:");
                tapd.TrainJourneyBasePrice = Convert.ToSingle(Console.ReadLine());


                tapd.DayID = dayID;
                tapd.TrainNo = trainNo;
                tapd.FromStationID = fromStation;
                tapd.ToStationID = toStation;

                StorePerDayTrainDetails(tapd);

            }
        }
        static void InsertAvailDays(int TrainNo,int DayID)
        {
            StoreAvailDay(DayID, TrainNo);
            GetAndSetTrips(DayID, TrainNo);
        }
        static void ShowTrainAvailDays(int TrainNo)
        {
            var query = "SELECT tt.TrainNo,tt.TrainName,tad.dayID,d.dayName FROM TrainTable tt " +
                "join TrainAvailableDaysTable tad on tt.TrainNo = tad.TrainNo " +
                "join Days d on tad.DayID = d.dayID where tt.TrainNo = @TrainNo";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@TrainNo",TrainNo);
            //cmd.Parameters.AddWithValue("@DayID", DayID);

            SqlDataReader dr1 = cmd1.ExecuteReader();
            Console.WriteLine("\n==============Available Trains===================");
            while (dr1.Read())
            {
                Console.WriteLine($"Train No:{dr1["TrainNo"]} ,TrainName:{dr1["TrainName"]} ,dayID:{dr1["dayID"]} ,dayName:{dr1["dayName"]}");
            }
        }
        static void UpdateTrainAvailDays()
        {
            Console.WriteLine("Select your options:");
            TrainAvailableDaysTableClass trainAvailableDaysTableClassobj = new TrainAvailableDaysTableClass();
            //ShowTrains(); 
            trainAvailableDaysTableClassobj.TrainNo = GetAndReturnTrainNo();
            Console.WriteLine("Train available in");
            ShowTrainAvailDays(trainAvailableDaysTableClassobj.TrainNo);
            int DayID;
            while (true)
            {
                Console.Write("\nEnter the DayID:");
                trainAvailableDaysTableClassobj.DayID = Convert.ToInt32(Console.ReadLine());
                if (CheckDayID(trainAvailableDaysTableClassobj.DayID))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error:Invalid Day ID");
                }

            }
           // ShowTrainAvailDays(trainAvailableDaysTableClassobj.TrainNo, trainAvailableDaysTableClassobj.DayID);
            DeleteTrainAvailDays(trainAvailableDaysTableClassobj.TrainNo, trainAvailableDaysTableClassobj.DayID);
            InsertAvailDays(trainAvailableDaysTableClassobj.TrainNo, trainAvailableDaysTableClassobj.DayID);
        }
        static void DeleteTrainRoute(int TrainNo, int fromStation, int toStation)
        {
            cmd = new SqlCommand("Delete from TrainRouteTable where TrainNo=@TrainNo " +
                "and FromStationID=@FromStationID and toStationID=@toStationID", getConnection());

            cmd.Parameters.AddWithValue("@TrainNo", TrainNo);
            cmd.Parameters.AddWithValue("@FromStationID", fromStation);
            cmd.Parameters.AddWithValue("@toStationID", toStation);
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                Console.WriteLine("Deleted route for this train");
            }
        }
        static void InsertTrainRoute(int TrainNo, int fromStation, int toStation)
        {
            //Station Sequence
            Console.Write("Enter the total stations in this trip:");
            int TotalStations = Convert.ToInt32(Console.ReadLine());
            List<TrainRouteTableClass> trainSceduleList = new List<TrainRouteTableClass>();
            for (int i = 0; i < TotalStations; i++)
            {
                TrainRouteTableClass t = new TrainRouteTableClass();
                ShowStations();
                Console.WriteLine($"Enter the {i + 1} staionID:");


                if (i == 0)
                {
                    Console.WriteLine("Intial station is added");
                    t.CurrentStation = fromStation;                    
                }
                else if (i == TotalStations - 1)
                {
                    Console.WriteLine("Final station is added");
                    t.CurrentStation = toStation;
                   
                }
                else
                {
                    while (true)
                    {
                        t.CurrentStation = Convert.ToInt32(Console.ReadLine());
                        if (CheckStationByID(t.CurrentStation))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error:Invalid Station ID.Enter your stationID Again");
                        }
                    }

                }

                while (true)
                {
                    try
                    {
                        Console.Write("Enter the arrival time to this station(hr:min:sec):");
                        DateTime ArrivalTime = Convert.ToDateTime(Console.ReadLine());
                        Console.Write("Enter the departure time to this station(hr:min:sec):");
                        DateTime DepartureTime = Convert.ToDateTime(Console.ReadLine());
                        t.ArrivesAt = ArrivalTime.TimeOfDay;
                        t.DepartureAt = DepartureTime.TimeOfDay;
                        break;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Error:Enter the input in specified format.");
                    }

                }

                t.TrainNo = TrainNo;
                t.FromStationID = fromStation;
                t.ToStationID = toStation;
                t.StationSequences = i + 1;

                trainSceduleList.Add(t);
            }
            int res = -1;
            foreach (var data in trainSceduleList)
            {
                cmd = new SqlCommand("INSERT INTO TrainRouteTable (TrainNo, FromStationID, " +
    "ToStationID, StationSeq, CurrentStation, ArrivesAt, DepartureAt) " +
    "VALUES(@TrainNo, @FromStationID, " +
    "@ToStationID, @StationSeq, @CurrentStation, @ArrivesAt, @DepartureAt)", getConnection());

                cmd.Parameters.AddWithValue("@TrainNo", data.TrainNo);
                cmd.Parameters.AddWithValue("@FromStationID", data.FromStationID);
                cmd.Parameters.AddWithValue("@ToStationID", data.ToStationID);
                cmd.Parameters.AddWithValue("@StationSeq", data.StationSequences);
                cmd.Parameters.AddWithValue("@CurrentStation", data.CurrentStation);
                cmd.Parameters.AddWithValue("@ArrivesAt", data.ArrivesAt);
                cmd.Parameters.AddWithValue("@DepartureAt", data.DepartureAt);
                res = cmd.ExecuteNonQuery();
            }

            if (res>0)
            {
                Console.WriteLine($"Journey Successfully Added");
            }
            else
            {
                Console.WriteLine($"Journey not added");
            }

        }
        static void UpdateTrainRoute()
        {
            Console.WriteLine("Select your options:");
      
            int TrainNo = GetAndReturnTrainNo();
            int fromStation = -1;
            int toStation = -1;
            while (true)
            {
                ShowStations();
                Console.Write("Enter From StationID:");
                fromStation = Convert.ToInt32(Console.ReadLine());
                if (CheckStationByID(fromStation))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error:Invalid Station ID.Enter your stationID Again");
                }
            }

            while (true)
            {
                ShowStations();
                Console.Write("Enter To StationID:");
                toStation = Convert.ToInt32(Console.ReadLine());
                if (CheckStationByID(toStation) && fromStation != toStation)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error:Invalid Station ID.Enter your stationID Again");
                }
            }

            DeleteTrainRoute(TrainNo, fromStation, toStation);
            InsertTrainRoute(TrainNo, fromStation, toStation);
        }
        static void ShowTrainAvailClasses(int TrainNo)
        {
            var query = "SELECT TrainNo,sc.SeatClassID,sc.SeatClassName,ct.SeatPrice FROM " +
                "ClassesAvailableInTrain ct join SeatClasses sc on ct.ClassID=sc.SeatClassID " +
                "where TrainNo=@TrainNo;";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@TrainNo", TrainNo);          

            SqlDataReader dr1 = cmd1.ExecuteReader();
            Console.WriteLine("\n==============Available Coach classes in Train===================");
            while (dr1.Read())
            {
                Console.WriteLine($"Train No:{dr1["TrainNo"]} ,SeatClassID:{dr1["SeatClassID"]} ,SeatClassName:{dr1["SeatClassName"]} ,SeatPrice:{dr1["SeatPrice"]}");
            }
        }
        static void DeleteTrainSeatClasses(int TrainNo,int ClassID)
        {
            cmd = new SqlCommand("Delete from ClassesAvailableInTrain where TrainNo=@TrainNo and ClassID =@ClassID", getConnection());

            cmd.Parameters.AddWithValue("@TrainNo", TrainNo);
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                Console.WriteLine($"Deleted from ClassesAvailableInTrain Successfully");
                cmd = new SqlCommand("Delete from SeatsAvailabilityPerClass where TrainNo=@TrainNo and ClassID =@ClassID", getConnection());

                cmd.Parameters.AddWithValue("@TrainNo", TrainNo);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine($"Deleted from SeatsAvailabilityPerClass Successfully");
                }
                else
                {
                    Console.WriteLine($"Error:Unable to Delete from SeatsAvailabilityPerClass");
                }
            }
            else
            {
                Console.WriteLine($"Error:Unable to Delete from ClassesAvailableInTrain");
            }
        }
        static void InsertTrainSeatClasses(int TrainNo, int ClassID)
        {
            ClassesAvailableInTrainClass classesAvailableInTrainClassObj = new ClassesAvailableInTrainClass();
            classesAvailableInTrainClassObj.TrainNo = TrainNo;
            classesAvailableInTrainClassObj.ClassID = ClassID;
            if (!CheckClassAvailableOnTrain(classesAvailableInTrainClassObj))
            {
                    ClassesAvailableInTrainClass classesAvailableInTrain = new ClassesAvailableInTrainClass();
                                    
                    classesAvailableInTrain.ClassID = ClassID;
                    classesAvailableInTrain.TrainNo = TrainNo;

                    while (true)
                    {
                        try
                        {
                        Console.Write("Enter the seat price:");
                        classesAvailableInTrain.SeatPrice = Convert.ToDecimal(Console.ReadLine());
                        break;
                        }
                        catch (OverflowException oe)
                        {
                            Console.WriteLine("Invalid Price value.");
                        }
                    }
                

                    if (AddTrainClass(classesAvailableInTrain))
                    {
                        if (!CheckSeatPerClassInTrain(classesAvailableInTrain.ClassID, classesAvailableInTrain.TrainNo))
                        {
                            SeatsAvailabilityPerClass seatsAvailabilityPerClass = new SeatsAvailabilityPerClass();
                            seatsAvailabilityPerClass.ClassID = classesAvailableInTrain.ClassID;
                            seatsAvailabilityPerClass.TrainNo = classesAvailableInTrain.TrainNo;
                            Console.Write("Available Seats in Lower Berth:");
                            seatsAvailabilityPerClass.LowerBerthAvailableSeats = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Available Seats in Middle Berth:");
                            seatsAvailabilityPerClass.MiddleBerthAvailableSeats = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Available Seats in Upper Berth:");
                            seatsAvailabilityPerClass.UpperBerthAvailableSeats = Convert.ToInt32(Console.ReadLine());
                            AddSeatAvailabilityInTrain(seatsAvailabilityPerClass);
                        }
                        else
                        {
                            Console.WriteLine("Availablitiy per class already have exists.To alter go to modify option");
                        }
                    }
                
            }
        }
        static void ChangeTrainStatus()
        {
            int TrainNo = -1;
            int TrainStatus = -1;
            while (true)
            {
                ShowTrains();
                Console.Write("Enter The Train No :");
                TrainNo = Convert.ToInt32(Console.ReadLine());
                if (CheckTrainID(TrainNo))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error:InValid TrainNo.Please Enter Correct No.");
                }
            }

            while (true)
            {
                Console.WriteLine("Select your option:\n1)Deactive\n2)Active\nEnter your choice:");
                TrainStatus = Convert.ToInt32(Console.ReadLine().Trim());
                if (TrainStatus == 1)
                {
                    TrainStatus = 1;
                    break;
                }
                else if (TrainStatus == 2)
                {
                    TrainStatus = 0;
                    break;
                }
                else
                {
                    Console.WriteLine("Error:Invalid Selection.\n");
                }
            }
           
            cmd = new SqlCommand("sp_Update_TrainStatus", getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@trainNo";
            param1.Value = TrainNo;
            param1.DbType = DbType.Int32;
            param1.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@deActiveStatus";
            param2.Value = TrainStatus;
            param2.DbType = (DbType)SqlDbType.Bit;
            param2.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(param2);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Train status updated successfully.");
        }
        static void UpdateTrainCoachAndBerth()
        {
            Console.WriteLine("Select your options:");
        
            int TrainNo = GetAndReturnTrainNo();
            Console.WriteLine("Train available in");
            ShowTrainAvailClasses(TrainNo);
            int ClassID;
            while (true)
            {
                Console.Write("\nEnter the ClassID:");
                ClassID = Convert.ToInt32(Console.ReadLine());
                ClassesAvailableInTrainClass classesAvailableInTrainClassObj = new ClassesAvailableInTrainClass();
                classesAvailableInTrainClassObj.TrainNo = TrainNo;
                classesAvailableInTrainClassObj.ClassID = ClassID;
                if (CheckClassAvailableOnTrain(classesAvailableInTrainClassObj))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error:Invalid Class ID");
                }

            }          
            DeleteTrainSeatClasses(TrainNo, ClassID);
            InsertTrainSeatClasses(TrainNo, ClassID);
        }
        static void ModifyTrain()
        {
            while (true)
            {
                Console.WriteLine("\nModification Options:\n");
                Console.WriteLine("1.Update Train Name");
                Console.WriteLine("2.Update Train Available Days");
                Console.WriteLine("3.Update Train Route");
                Console.WriteLine("4.Update Train Coach and berth");
                Console.WriteLine("5.Exit");
                Console.Write("\nEnter your choice:");
                int ModifyOp = Convert.ToInt32(Console.ReadLine());
                if (ModifyOp == 1)
                {
                    UpdateTrainName();
                }
                else if (ModifyOp == 2)
                {
                    UpdateTrainAvailDays();
                }
                else if (ModifyOp == 3)
                {
                    UpdateTrainRoute();
                }
                else if (ModifyOp == 4)
                {
                    UpdateTrainCoachAndBerth();
                }
                else if (ModifyOp == 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Choice");
                }
            }
            

        }

        static (int,int,int) GetTrainsForBooking(int fromStationID,int toStationID)
        {
            int TrainNo = -1;
            int ActualTrainFrom = -1;
            int ActualTrainTo = -1;
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.fn_ShowTrainsByFromToStation(@FromStation, @ToStation)", getConnection());
            cmd.CommandType = CommandType.Text;


            cmd.Parameters.Add(new SqlParameter("@FromStation", SqlDbType.Int)).Value = fromStationID;
            cmd.Parameters.Add(new SqlParameter("@ToStation", SqlDbType.Int)).Value = toStationID;
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (!dr1.HasRows)
            {
                Console.WriteLine("No trains available for the selected stations.");
            }
            else
            {
                List<TrainRouteTableClass> trains = new List<TrainRouteTableClass>();
                Console.WriteLine("\n==============Trains Availables for this location===================");
                               
                    int index = 1;
                    while (dr1.Read())
                    {

                        Console.WriteLine($"{index}.Train Name: {dr1["TrainName"]}, " +
                                          $" From Station Name: {dr1["FromStationName"]}, " +
                                          $" To Station Name: {dr1["ToStationName"]}");

                        trains.Add(new TrainRouteTableClass
                        {
                            TrainNo = (int)dr1["TrainNo"],
                            FromStationID = (int)dr1["FromStationID"],
                            ToStationID = (int)dr1["ToStationID"]
                        });

                        index++;
                    }


                    Console.Write("\nEnter the number of the train you want to select:");
                    int trainIndex = int.Parse(Console.ReadLine()) - 1;

                    if (trainIndex >= 0 && trainIndex < trains.Count)
                    {
                        TrainRouteTableClass selectedTrain = trains[trainIndex];
                        Console.WriteLine("\nYou have selected:");
                        Console.WriteLine($"Train No: {selectedTrain.TrainNo},");
                        Console.WriteLine($"From Station:{selectedTrain.FromStationID},");
                        Console.WriteLine($"To Station:{selectedTrain.ToStationID}");

                        return (selectedTrain.TrainNo, selectedTrain.FromStationID, selectedTrain.ToStationID);
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please try again.");
                    }
                
            }
            return (TrainNo, ActualTrainFrom, ActualTrainTo);
        }

        static (int, float) GetTrainBookingDays(int TrainNo,int fromStationID, int toStationID)
        {   int DayID = -1;
            int BaseJourneyPrice = -1;
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.fn_ShowAvailDays(@TrainNo,@FromStation, @ToStation)", getConnection());
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("@TrainNo", SqlDbType.Int)).Value = TrainNo;
            cmd.Parameters.Add(new SqlParameter("@FromStation", SqlDbType.Int)).Value = fromStationID;
            cmd.Parameters.Add(new SqlParameter("@ToStation", SqlDbType.Int)).Value = toStationID;
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (!dr1.HasRows)
            {
                Console.WriteLine("No trains available Days for this train.");
            }
            else
            {
                List<TrainAvailablePerDayTableClass> trains = new List<TrainAvailablePerDayTableClass>();
                Console.WriteLine("\n============== Days Availables for this Train===================");
                
                  
                    int index = 1;
                    while (dr1.Read())
                    {

                        Console.WriteLine($"{index}.Train Name: {dr1["TrainName"]}, " +
                                          $" DayID: {dr1["DayID"]}, " +
                                          $" Day Name: {dr1["DayName"]}, " +
                                          $" Journey Base Price: {dr1["JourneyBasePrice"]}");

                        trains.Add(new TrainAvailablePerDayTableClass
                        {
                            TrainNo = Convert.ToInt32(dr1["TrainNo"]),
                            DayID = Convert.ToInt32(dr1["DayID"]),
                            TrainJourneyBasePrice = Convert.ToInt32(dr1["JourneyBasePrice"])
                        });

                        index++;
                    }


                    Console.Write("\nEnter the number of the train you want to select:");
                    int trainIndex = int.Parse(Console.ReadLine()) - 1;

                    if (trainIndex >= 0 && trainIndex < trains.Count)
                    {
                        TrainAvailablePerDayTableClass selectedTrain = trains[trainIndex];
                        Console.WriteLine("\nYou have selected:");
                        Console.WriteLine($"Train No: {selectedTrain.TrainNo},");
                        Console.WriteLine($"Day ID:{selectedTrain.DayID},");
                        Console.WriteLine($"Jounrey Price: {selectedTrain.TrainJourneyBasePrice}");

                        return (selectedTrain.DayID, selectedTrain.TrainJourneyBasePrice);
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please try again.");
                    }
                }
            

            return (DayID, BaseJourneyPrice);
        }

        static int GetTotalStations(ClassesAvailableInTrainClass classesAvailableInTrainClass, int DayID, int ActFromStation, int ActToStation)
        {
            SqlCommand cmd = new SqlCommand("select count(*) from TrainRouteTable where TrainNo=@trainNo" +
                " and FromStationID=@fromStation and ToStationID = @toStation; ", getConnection());

            cmd.Parameters.Add(new SqlParameter("@trainNo", SqlDbType.Int)).Value = classesAvailableInTrainClass.TrainNo;
            cmd.Parameters.Add(new SqlParameter("@fromStation", SqlDbType.Int)).Value = ActFromStation;
            cmd.Parameters.Add(new SqlParameter("@toStation", SqlDbType.Int)).Value = ActToStation;
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.HasRows && dr1.Read())
            {
                //Console.WriteLine("Total Stations:" + dr1[0]);
                return Convert.ToInt32(dr1[0]);
            }
            else
            {
                Console.WriteLine("No Station Available for this route");
            }
            return -1;
        }

        static (int,int) GetSequences(ClassesAvailableInTrainClass classesAvailableInTrainClass, int DayID, int ActFromStation, int ActToStation, int fromStationID, int toStationID)
        {
            var query = " select stationSeq  from TrainRouteTable where TrainNo=@TrainNo and FromStationID=@ActFromStation and ToStationID=@ActToStation and CurrentStation=@fromStationID " +
                "Union select stationSeq from TrainRouteTable where TrainNo=@TrainNo and FromStationID=@ActFromStation and ToStationID=@ActToStation and CurrentStation=@toStationID";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@TrainNo", classesAvailableInTrainClass.TrainNo);
            cmd1.Parameters.AddWithValue("@ActFromStation", ActFromStation);
            cmd1.Parameters.AddWithValue("@ActToStation", ActToStation);
            cmd1.Parameters.AddWithValue("@fromStationID", fromStationID);
            cmd1.Parameters.AddWithValue("@toStationID", toStationID);

            SqlDataReader dr1 = cmd1.ExecuteReader();
            int FromSeq = -1;
            int ToSeq = -1;
            while (dr1.Read())
            {
                if(FromSeq == -1)
                {
                    FromSeq = Convert.ToInt32(dr1[0]);
                }
                else
                {
                    ToSeq = Convert.ToInt32(dr1[0]);
                }
            }
            return (FromSeq, ToSeq);
        }

        static float CalCulatePrice(int TotalStation,int FromSeq,int ToSeq,float seatPrice)
        {
            //            formula: Perc = Res(Result of two difference in station) / TotalNoOfStation)*100// here we get percentage
            ////According to the price of each class seats, separate the calculation as below
            //EachIndividualPrice = trainJourneyBasePrice + (SeatPrice / 100) * Perc
            float percReduce=(Math.Abs(FromSeq-ToSeq)/TotalStation)*100;
            float actualPrice = (seatPrice / 100) * percReduce;
            return actualPrice;
        }

        static float GetSeatPrice(ClassesAvailableInTrainClass classesAvailableInTrainClass,int DayID,int ActFromStation,int ActToStation,int fromStationID,int toStationID)
        {
            //Checking in  route table
            int TotalStations = GetTotalStations(classesAvailableInTrainClass,DayID,ActFromStation, ActToStation);
            var (FromStationSeq, ToStationSeq) = GetSequences(classesAvailableInTrainClass, DayID, ActFromStation, ActToStation, fromStationID,toStationID);
            var query = " Select SeatPrice from ClassesAvailableInTrain where TrainNo=@TrainNo and ClassID=@ClassID ";
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@TrainNo", classesAvailableInTrainClass.TrainNo);
            cmd1.Parameters.AddWithValue("@ClassID", classesAvailableInTrainClass.ClassID);

            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {

                CalCulatePrice(TotalStations, FromStationSeq, ToStationSeq,Convert.ToSingle(dr1[0]));
                return Convert.ToSingle(dr1[0]);
            }
            return -1;
        }

        static bool checkBerthAvailability(int TrainNo, int ClassID,string berth)
        {
            var query = " Select SeatPrice from SeatsAvailabilityPerClass saps join ClassesAvailableInTrain cat on  " +
                "saps.TrainNo= cat.TrainNo and saps.ClassID=cat.ClassID where cat.TrainNo=@TrainNo and cat.ClassID=@ClassID and ";
            if (berth.Equals("lower"))
            {
                query = query + " LowerBerthAvailableSeats>1";
            }
            else if (berth.Equals("middle"))
            {
                query = query + " MiddleBerthAvailableSeats>1";
            }
            else
            {
                query = query + " UpperBerthAvailableSeats>1";
            }
          
            SqlCommand cmd1 = new SqlCommand(query, getConnection());
            cmd1.Parameters.AddWithValue("@TrainNo", TrainNo);
            cmd1.Parameters.AddWithValue("@ClassID", ClassID);

            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                return true;
            }
            return false;
        }

        static int StoreBookingTable1(int UserID,int TrainNo,int fromStationID,int toStationID,float totalPay)
        {
            cmd = new SqlCommand("INSERT INTO TrainBookingTable1(BookedCustNo,TrainNo," +
                "FromStationID,ToStationID,TotalPrice)VALUES(@BookedCustNo,@TrainNo,@FromStationID," +
                "@ToStationID,@TotalPrice); " +
                "SELECT SCOPE_IDENTITY()", getConnection());

            cmd.Parameters.AddWithValue("@BookedCustNo", UserID);
            cmd.Parameters.AddWithValue("@TrainNo", TrainNo);
            cmd.Parameters.AddWithValue("@FromStationID", fromStationID);
            cmd.Parameters.AddWithValue("@ToStationID", toStationID);
            cmd.Parameters.AddWithValue("@TotalPrice", totalPay);
            object NewBookID = cmd.ExecuteScalar();
            if (NewBookID != null)
            {
                return Convert.ToInt32(NewBookID);
            }
            return -1;
        }

        static void StoreBookingTable2(int BookingID,string[] customerName,long[] custToken,int[] custClassID,string[] custBerth,float[] custSeatPrice)
        {
            for(int i = 0; i < customerName.Length; i++)
            {
                cmd = new SqlCommand("INSERT INTO dbo.TrainBookingTable2(BookingID,ClassID,UserID,berth,SeatPrice,PassengerName)" +
                    " VALUES(@BookingID, @ClassID, @UserID, @berth, @SeatPrice, @PassengerName)", getConnection());

                cmd.Parameters.AddWithValue("@BookingID", BookingID);
                cmd.Parameters.AddWithValue("@PassengerName", customerName[i]);
                cmd.Parameters.AddWithValue("@ClassID", custClassID[i]);
                cmd.Parameters.AddWithValue("@UserID", custToken[i]);
                cmd.Parameters.AddWithValue("@berth", custBerth[i]);
                cmd.Parameters.AddWithValue("@SeatPrice", custSeatPrice[i]);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"\n {customerName[i]} booking Successful");
            }
        }

        static int BookingPayment(int userID,float totalPay)
        {
            cmd = new SqlCommand("Select * from custBank where UserID=@UserID and AvailBalance>@totalPay", getConnection());
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@totalPay", totalPay);
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                cmd = new SqlCommand("UPDATE CustBank SET AvailBalance =AvailBalance-@totalPay where UserID=@UserID", getConnection());

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@totalPay", totalPay);
                cmd.ExecuteNonQuery();
                return 1;
            }
            return -1;
        }
        static void BookTickets()
        {
            int fromStationID = -1;
            int toStationID = -1;
            while (true)
            {
                ShowStations();
                Console.Write("Enter From Station ID:");
                fromStationID = int.Parse(Console.ReadLine());
                if (CheckStationByID(fromStationID))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error:Invalid Station");
                }
            }

            while (true)
            {
                ShowStations();
                Console.Write("Enter To Station ID:");
                toStationID = int.Parse(Console.ReadLine());
                if (CheckStationByID(toStationID))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error:Invalid Station");
                }
            }
           var (TrainNo,ActFromStation,ActToStation)= GetTrainsForBooking(fromStationID,toStationID);

           var (DayID,BaseJourneyPrice)=GetTrainBookingDays(TrainNo, ActFromStation, ActToStation);
            if(ActToStation>0 && ActToStation>0 && TrainNo > 0)
            {
                Console.WriteLine("\nConfirm Booking? (yes/no):");
                string confirm = Console.ReadLine();

                if (confirm.ToLower() == "yes")
                {
                    Console.WriteLine("Enter Number of Passengers:");
                    int passengerCount = int.Parse(Console.ReadLine());
                    string[] customerName = new string[passengerCount];
                    long[] custToken = new long[passengerCount];
                    int[] custClassID = new int[passengerCount];
                    string[] custBerth = new string[passengerCount];
                    float[] custSeatPrice = new float[passengerCount];
                    float totalPay = 0;
                    for (int i = 0; i < passengerCount; i++)
                    {
                        long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                        custToken[i] = milliseconds % 10000000000;
                        Console.WriteLine($"\nEnter the {i + 1} Name:");
                        customerName[i] = Console.ReadLine();

                        ShowTrainAvailClasses(TrainNo);
                        ClassesAvailableInTrainClass classesAvailableInTrainClass = new ClassesAvailableInTrainClass();
                        classesAvailableInTrainClass.TrainNo = TrainNo;
                        Console.Write("Enter the ClassID:");
                        while (true)
                        {
                            classesAvailableInTrainClass.ClassID = Convert.ToInt32(Console.ReadLine());
                            if (CheckClassAvailableOnTrain(classesAvailableInTrainClass))
                            {
                                custClassID[i] = classesAvailableInTrainClass.ClassID;
                                Console.WriteLine("\nWhich Berth you like book for this person?\n" +
                                    "Enter Your choice(Lower/Middle/Upper):");
                                custBerth[i] = Console.ReadLine().Trim().ToLower();
                                if (checkBerthAvailability(TrainNo, custClassID[i], custBerth[i]))
                                {
                                    break;
                                }
                                else
                                {
                                    custBerth[i] = "";
                                    Console.WriteLine("Sorry Seats are filled for this berth try other");
                                }


                            }
                        }

                        custSeatPrice[i] = BaseJourneyPrice + GetSeatPrice(classesAvailableInTrainClass, DayID, ActFromStation, ActToStation, fromStationID, toStationID);
                        totalPay = totalPay + custSeatPrice[i];

                    }
                    int PaymentStatus = BookingPayment(CurLogusr.UserID, totalPay);
                    if (PaymentStatus > 0)
                    {
                        int BookingID = StoreBookingTable1(CurLogusr.UserID, TrainNo, fromStationID, toStationID, totalPay);
                        StoreBookingTable2(BookingID, customerName, custToken, custClassID, custBerth, custSeatPrice);
                        Console.WriteLine("\nSuccessfully Tickets are booked...!!!");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Balance.Try again later");
                    }

                }
            }
            else
            {
                Console.WriteLine("No Trains Available for the matching stations");
            }
            
           

        }
        static void ShowCurrentUserBookings()
        {

            string query = @" SELECT tb1.BookingID, tb1.TrainNo, tb1.BookingDate, tb1.TotalPrice, tb1.CancellationStatus,
                    tb2.ClassID, tb2.PassengerName, tb2.berth, tb2.SeatPrice, tb2.CancellationStatus AS PartialCancellationStatus
                FROM TrainBookingTable1 tb1 LEFT JOIN TrainBookingTable2 tb2 ON tb1.BookingID = tb2.BookingID
                WHERE tb1.BookedCustNo = @UserID";

            SqlCommand fetchCommand = new SqlCommand(query, getConnection());
            fetchCommand.Parameters.AddWithValue("@UserID", CurLogusr.UserID);

            using (SqlDataReader data = fetchCommand.ExecuteReader())
            {
                if (!data.HasRows)
                {
                    Console.WriteLine("No bookings found.");
                    return;
                }

                Console.WriteLine("Your Bookings:");
                while (data.Read())
                {
                    Console.WriteLine($"BookingID: {data["BookingID"]}, TrainNo: {data["TrainNo"]}, " +
                        $"BookingDate: {data["BookingDate"]}, TotalPrice: {data["TotalPrice"]}, " +
                       // $"CancellationStatus: {data["CancellationStatus"]}, " +
                        $"PassengerName: {data["PassengerName"]}, SeatPrice: {data["SeatPrice"]}");
                }
            }

        }
        static void ProcessFullCancellation(int userId)
        {
            Console.WriteLine("Enter Booking ID for full cancellation:");
            int bookingId = int.Parse(Console.ReadLine());

            string query = @"
                UPDATE TrainBookingTable1
                SET CancellationStatus = 1
                WHERE BookingID = @BookingID AND BookedCustNo = @UserID;
 
                UPDATE TrainBookingTable2
                SET CancellationStatus = 1
                WHERE BookingID = @BookingID;
 
                INSERT INTO TransactionTable (BookingID, BookedCustNo, TransactionDate, RefundAmount, IRCTCFee, Remarks)
                SELECT tb1.BookingID, tb1.BookedCustNo, GETDATE(), tb1.TotalPrice, 10, 'Full Cancellation'
                FROM TrainBookingTable1 tb1
                WHERE tb1.BookingID = @BookingID AND tb1.BookedCustNo = @UserID;
 
                UPDATE CustBank
                SET AvailBalance = AvailBalance + (
                    SELECT tb1.TotalPrice
                    FROM TrainBookingTable1 tb1
                    WHERE tb1.BookingID = @BookingID AND tb1.BookedCustNo = @UserID
                )
                WHERE UserID = @UserID;";

            SqlCommand cmd = new SqlCommand(query, getConnection());
            cmd.Parameters.AddWithValue("@BookingID", bookingId);
            cmd.Parameters.AddWithValue("@UserID", userId);

            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                Console.WriteLine("Full cancellation processed successfully.");
            }
            else
            {
                Console.WriteLine("No bookings found to cancel.");
            }

        }
        static void ProcessPartialCancellation(int userId)
        {
            Console.WriteLine("Enter Booking ID for partial cancellation:");
            int bookingId = int.Parse(Console.ReadLine());

            string query1 = @"SELECT UserID,ClassID, SeatPrice, PassengerName, CancellationStatus
                FROM TrainBookingTable2 WHERE BookingID = @BookingID AND CancellationStatus = 0";

            SqlCommand cmd = new SqlCommand(query1, getConnection());
            cmd.Parameters.AddWithValue("@BookingID", bookingId);

            SqlDataReader data = cmd.ExecuteReader();
            
            if (!data.HasRows)
            {
                Console.WriteLine("No passengers found for partial cancellation.");
                return;
            }

            Console.WriteLine("Available passengers for cancellation:");
            while (data.Read())
            {
                Console.WriteLine($"UserID: {data["UserID"]},ClassID: {data["ClassID"]}, SeatPrice: {data["SeatPrice"]}, " +
                    $" PassengerName: {data["PassengerName"]}");
            }
            

            Console.WriteLine("Enter UserID to cancel:");
            int UserID = int.Parse(Console.ReadLine());

            string query = @"
                UPDATE TrainBookingTable2
                SET CancellationStatus = 1
                WHERE BookingID = @BookingID AND UserID = @UserID;
 
                INSERT INTO TransactionTable (BookingID, BookedCustNo, TransactionDate, RefundAmount, IRCTCFee, Remarks)
                SELECT @BookingID, @LoginUserID, GETDATE(), SeatPrice, 5, 'Partial Cancellation'
                FROM TrainBookingTable2
                WHERE BookingID = @BookingID AND UserID = @UserID;
 
                UPDATE CustBank
                SET AvailBalance = AvailBalance + (
                    SELECT SeatPrice
                    FROM TrainBookingTable2
                    WHERE BookingID = @BookingID AND UserID = @UserID
                )
                WHERE UserID = @LoginUserID;";

            SqlCommand FinalQuery = new SqlCommand(query, getConnection());
            FinalQuery.Parameters.AddWithValue("@BookingID", bookingId);
            FinalQuery.Parameters.AddWithValue("@UserID", UserID);
            FinalQuery.Parameters.AddWithValue("@LoginUserID", CurLogusr.UserID);

            int rowsAffected = FinalQuery.ExecuteNonQuery();
            if(rowsAffected > 0)
            {
                Console.WriteLine("Partial cancellation processed successfully.");
            }
            else
            {
                Console.WriteLine("No passengers found to cancel.");
            }
         
        }
        static void CancelTickets()
        {
           ShowCurrentUserBookings();
            Console.WriteLine("Do you want to delete complete booking or partial booking (all/partial)?");
            string action = Console.ReadLine()?.ToLower();

            if (action == "all")
            {
                ProcessFullCancellation(CurLogusr.UserID);
            }
            else if (action == "partial")
            {
                ProcessPartialCancellation(CurLogusr.UserID);
            }
            else
            {
                Console.WriteLine("Invalid action.");
            }
        }
        static void Main(string[] args)
        {

            Console.WriteLine("===================================================");
            Console.WriteLine("Welcome to Railway ticket reservation");
            Console.WriteLine("===================================================");

            Console.Write("\nDo you have an account to use the appliation (Yes/No):");
            bool validUser = false;
            try
            {
                string AccStatus = Console.ReadLine();
                switch (AccStatus.ToLower().Trim())
                {
                    case "yes":
                        validUser = CheckUser(loginInput());
                        break;
                    case "no":
                        validUser = CreateUser();
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        goto ExitApplication;
                }

            }
            catch (FormatException fe)
            {
                Console.WriteLine("Invalid input.Please enter valid input");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            if (validUser == true && CurLogusr != null)
            {
                if (CurLogusr.AdminStatus == true)
                {

                    Console.WriteLine("===================================================");
                    Console.WriteLine("Welcome Admin to the dashboard");
                    Console.WriteLine("===================================================");
                    int category = -1;
                    int OpChoice = -1;
                        while (true)
                        {
                            Console.WriteLine("What you like to Add/Modify/Delete today?\n");
                            Console.WriteLine("1)Add/Modify/Delete Stations");
                            Console.WriteLine("2)Add/Modify/Delete Seat Classes");
                            Console.WriteLine("3)Add/Modify/Delete Days");
                            Console.WriteLine("4)Add/Modify/ChangeTrainStatus Train");
                            Console.WriteLine("5)Exit");
                            Console.Write("Enter your choice:");
                            category = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("\nThanks for choosing the option!!!\n");
                            if (category == 1)
                            {
                                Console.WriteLine("In stations data what you like to do\n");
                               
                            }
                            else if (category == 2)
                            {
                                Console.WriteLine("In seat classes data what you like to do\n");
                           
                            }
                            else if (category == 3)
                            {
                                Console.WriteLine("In days data what you like to do\n");
                              
                            }
                            else if (category == 4)
                            {
                                Console.WriteLine("In train data what you like to do\n");
                                
                            }
                            else if(category == 5)
                            {
                            break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid selection\n");
                            }
                    
                            Console.WriteLine("1)Add");
                            Console.WriteLine("2)Modify");
                        if (category != 4)
                        {
                            Console.WriteLine("3)Delete");
                        }
                        else
                        {
                            Console.WriteLine("3)Change Train Status");
                        }

                            Console.Write("Enter your operation choice:");
                            OpChoice = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine();
                            if (category == 1 && OpChoice == 1)
                            {
                                AddStation();
                            }
                            else if (category == 1 && OpChoice == 2)
                            {
                                ModifyStation();
                            }
                            else if (category == 1 && OpChoice == 3)
                            {
                                DeleteStation();
                            }
                            else if (category == 2 && OpChoice == 1)
                            {
                                AddSeatClass();
                            }
                            else if (category == 2 && OpChoice == 2)
                            {
                                ModifySeatClass();
                            }
                            else if (category == 2 && OpChoice == 3)
                            {
                                DeleteSeatClass();
                            }
                            else if (category == 3 && OpChoice == 1)
                            {
                                AddDay();
                            }
                            else if (category == 3 && OpChoice == 2)
                            {
                                ModifyDay();
                            }
                            else if (category == 3 && OpChoice == 3)
                            {
                                DeleteDay();
                            }
                            else if (category == 4 && OpChoice == 1)
                            {
                                AddTrain();
                               
                            }
                            else if (category == 4 && OpChoice == 2)
                            {
                                ModifyTrain();
                               
                            }
                            else if (category == 4 && OpChoice == 3)
                            {
                                ChangeTrainStatus();
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid Operation input");
                            }

                            Console.WriteLine("\nDo you like to do anything(Yes/No)");
                            string OpStatus = Console.ReadLine();
                            if (OpStatus.ToLower().Trim() == "no")
                            {
                                break;
                            }


                        }

                }
                else
                {
                    Console.WriteLine("===================================================");
                    Console.WriteLine("Welcome user to the dashboard");
                    Console.WriteLine("===================================================");
                    while (true) {
                        Console.WriteLine("\nSelect an Option:\n1. Book Tickets\n2. Cancel Tickets\n3. Exit");
                        int choice = int.Parse(Console.ReadLine());

                        if (choice == 1)
                            BookTickets();
                        else if (choice == 2)
                            CancelTickets();
                        else if (choice == 3)
                            break;
                        else
                            Console.WriteLine("Invalid Option.");
                    }

                }
            }
            else
            {
                Console.WriteLine("InCorrect Credentials.Try Again Later!!!");
            }
            goto ExitApplication;
        ExitApplication:
            Console.WriteLine("***********************************************");
            Console.WriteLine("\n\nPress any key to exit from application...");
            Console.ReadKey();
        }
    }
}
