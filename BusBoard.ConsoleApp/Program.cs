using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    const int numberBusesToDisplay = 5;
    const int maximumSearchRadius = 250;
    static void Main(string[] args)
    {
      BusBoardInput inputSystem = new ConsoleBusBoardInput();
      BusBoardDisplay displaySystem = new ConsoleBusBoardDisplay();
      
      displaySystem.DisplayWelcome();
      displaySystem.DisplayMessage("Enter a post code.");
      string userInput = inputSystem.GetStringInput();
      //Example stop code: 490008660N
      
      try
      {
        PostcodeInfo.LongLat longLat = PostcodeClient.GetLongitudeLatitudePair(userInput);
        
        string stopCode = TfLClient.GetClosestStopCode(longLat, maximumSearchRadius);
      
        List<BusInfo> busList = TfLClient.GetBusesAtStopCode(stopCode);

        displaySystem.DisplayShortestTimeBuses(busList, numberBusesToDisplay);
      }
      catch (Exception e)
      {
        displaySystem.DisplayError(e.Message);
      }
      
      
    }
  }
}
