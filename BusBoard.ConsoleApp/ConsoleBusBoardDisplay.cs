using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.ConsoleApp
{
    public class ConsoleBusBoardDisplay : BusBoardDisplay
    {
        public override void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Bus Board program!");
            Console.WriteLine("=================================");
        }

        public override void DisplayError(string message)
        {
            Console.WriteLine(message);
        }
    
        public override void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public override void DisplayShortestTimeBuses(List<BusInfo> busList, int numberBusesToDisplay)
        {
            busList.Sort((x, y) => x.TimeToStation.CompareTo(y.TimeToStation));
            foreach (BusInfo busInfo in busList.Take(numberBusesToDisplay))
            {
                Console.WriteLine($"\nBus Station: {busInfo.StationName} \nBus Destination: {busInfo.DestinationName} \nTime to Arrive: {busInfo.TimeToStation} \nLine Name: {busInfo.LineName}");
            }
        }
    }
}