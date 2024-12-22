using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationSystem
{
    class TrainSchedulesTableClass
    {
        public int TrainNo { get; set; }
        public int StationID { get; set; }
        public DateTime ArrivesAt { get; set; }
        public DateTime DepartureAt  { get; set; }
    }
}
