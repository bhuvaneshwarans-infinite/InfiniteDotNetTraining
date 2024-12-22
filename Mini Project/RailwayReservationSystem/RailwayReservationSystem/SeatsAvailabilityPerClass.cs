using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationSystem
{
    class SeatsAvailabilityPerClass
    {
        //        TrainNo INT,
        //ClassID INT,
        //    LowerBerthAvailableSeats INT,
        //    MiddleBerthAvailableSeats INT,
        //    UpperBerthAvailableSeats INT,
        public int TrainNo { get; set; }
        public int ClassID { get; set; }
        public int LowerBerthAvailableSeats { get; set; }
        public int MiddleBerthAvailableSeats { get; set; }
        public int UpperBerthAvailableSeats { get; set; }
    }
}
