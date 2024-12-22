using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationSystem
{
    class TrainAvailablePerDayTableClass
    {
        public int TrainNo { get; set; }
        public int DayID { get; set; }
        public int FromStationID { get; set; }
        public int ToStationID { get; set; }
        public TimeSpan TrainStartTime { get; set; }
        public TimeSpan TrainEndTime { get; set; }
        public float TrainJourneyBasePrice { get; set; }
        
    }
}
