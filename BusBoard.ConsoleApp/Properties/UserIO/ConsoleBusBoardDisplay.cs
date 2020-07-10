using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.Api;

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
            Console.WriteLine("\nWARNING: An error occurred.");
            Console.WriteLine(message);
        }
    
        public override void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public override void DisplayBuses(List<BusInfo> busList, int numberBusesToDisplay)
        {
            foreach (BusInfo busInfo in busList.Take(numberBusesToDisplay))
            {
                Console.WriteLine($"\nBus Station: {busInfo.StationName} \nBus Destination: {busInfo.DestinationName} \nExpected Arrival: {busInfo.ExpectedArrival.ToLocalTime()} \nTime to Station: {busInfo.TimeToStation} \nLine Name: {busInfo.LineName}");
            }
        }
    }
}