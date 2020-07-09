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
    const int maximumSearchRadius = 250;
    private const int numberCloseStopsToConsider = 2;
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
        
        List<BusStopInfo> busStopInfos = TfLClient.GetClosestStops(longLat, maximumSearchRadius);
        List<string> nearestStopCodes = busStopInfos.ConvertAll(x => x.NaptanID).GetRange(0,2);
        
        List<BusInfo> busList = TfLClient.GetBusesAtStopCodes(nearestStopCodes);

        displaySystem.DisplayShortestTimeBuses(busList, numberBusesToDisplay);
      }
      catch (Exception e)
      {
        displaySystem.DisplayError(e.Message);
      }
      
      
    }
  }
}
