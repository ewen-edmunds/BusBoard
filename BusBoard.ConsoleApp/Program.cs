using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusBoard.Api;
using BusBoard.ConsoleApp;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    const int numberBusesToDisplay = 5;
    const int maximumSearchRadiusMetres = 250;
    private const int maxNumberBusStopsToConsider = 2;
    static void Main(string[] args)
    {
      BusBoardInput inputSystem = new ConsoleBusBoardInput();
      BusBoardDisplay displaySystem = new ConsoleBusBoardDisplay();
      
      displaySystem.DisplayWelcome();
      displaySystem.DisplayMessage("Enter a post code.");
      string userInput = inputSystem.GetStringInput();

      try
      {
        List<BusInfo> soonestBuses = BusBoardAPI.GetSoonestBusesTo(userInput);
        displaySystem.DisplayBuses(soonestBuses, numberBusesToDisplay);
      }
      catch (Exception e)
      {
        displaySystem.DisplayError(e.Message);
      }
    }
  }
}
