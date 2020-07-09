using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    const int numberBusesToDisplay = 5;
    static void Main(string[] args)
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      //490008660N
      BusBoardInput inputSystem = new ConsoleBusBoardInput();
      string userInput = inputSystem.GetStringInput();
      
      var client = new RestClient("https://api.tfl.gov.uk/");
      client.Authenticator = new HttpBasicAuthenticator("username", "password");
      
      var request = new RestRequest($"StopPoint/{userInput}/Arrivals", Method.GET);
      request.AddUrlSegment("app_id", "1ca74190");
      request.AddUrlSegment("app_key", "ad2ece2581024fa28abe323c0d4109c7");
      
      var response = client.Execute(request);

      var busList = client.Get<List<BusInfo>>(request).Data;

      busList.Sort((x, y) => x.TimeToStation.CompareTo(y.TimeToStation));
      

      foreach (BusInfo busInfo in busList.Take(numberBusesToDisplay))
      {
        Console.WriteLine($"\nBus Station: {busInfo.StationName} \nBus Destination: {busInfo.DestinationName} \nTime to Arrive: {busInfo.TimeToStation} \nLine Name: {busInfo.LineName}");
      }
    }
  }
}
