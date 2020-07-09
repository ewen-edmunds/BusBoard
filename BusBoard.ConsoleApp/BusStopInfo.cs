using System.Collections.Generic;

namespace BusBoard.ConsoleApp
{
    public class BusStopInfo
    {
        public string NaptanID { get; set; }
        public int Distance { get; set; }
    }
    
    public class WrappedBusStopInfo
    {
        public List<BusStopInfo> StopPoints { get; set; }
    }
}