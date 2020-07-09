using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    const int numberBusesToDisplay = 5;
    static void Main(string[] args)
    {
      BusBoardInput inputSystem = new ConsoleBusBoardInput();
      string userInput = inputSystem.GetStringInput();
      //todo: validate the stop code
      //Example stop code: 490008660N

      List<BusInfo> busList = TfLClient.GetBusesAtStopCode(userInput);
      busList.Sort((x, y) => x.TimeToStation.CompareTo(y.TimeToStation));

      foreach (BusInfo busInfo in busList.Take(numberBusesToDisplay))
      {
        Console.WriteLine($"\nBus Station: {busInfo.StationName} \nBus Destination: {busInfo.DestinationName} \nTime to Arrive: {busInfo.TimeToStation} \nLine Name: {busInfo.LineName}");
      }
    }
  }
}
