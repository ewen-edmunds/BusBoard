using System;

namespace BusBoard.Api
{
    public class BusInfo
    {
        public string StationName { get; set; }
        public string DestinationName { get; set; }
        public string LineName { get; set; }
        public int TimeToStation { get; set; }
        public DateTime ExpectedArrival { get; set; }
    }
}