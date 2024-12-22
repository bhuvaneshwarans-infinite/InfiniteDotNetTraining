using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationSystem
{
    class TrainRouteTableClass
    {
        public int TrainNo { get; set; }
        public int FromStationID { get; set; }
        public int ToStationID { get; set; }
        public int CurrentStation { get; set; }
        public int StationSequences { get; set; }
        public TimeSpan ArrivesAt { get; set; }
        public TimeSpan DepartureAt { get; set; }

    }
}
