using System.Collections.Generic;
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
      BusBoardDisplay displaySystem = new ConsoleBusBoardDisplay();
      
      displaySystem.DisplayWelcome();
      string userInput = inputSystem.GetStringInput();
      //todo: validate the stop code
      //Example stop code: 490008660N

      List<BusInfo> busList = TfLClient.GetBusesAtStopCode(userInput);
      
      displaySystem.DisplayShortestTimeBuses(busList, numberBusesToDisplay);
    }
  }
}
