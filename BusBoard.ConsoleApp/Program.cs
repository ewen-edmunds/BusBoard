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

      var busList = client.Get<List<BusInfo>>(request);
      
      Console.WriteLine(response.IsSuccessful);
      Console.WriteLine("That's all");

      foreach (BusInfo busInfo in busList.Data)
      {
        Console.WriteLine($"\nBus Station: {busInfo.StationName} \nBus Destination: {busInfo.DestinationName} \nTime to Arrive: {busInfo.TimeToStation} \nLine Name: {busInfo.LineName}");
      }
    }
  }

  public class BusInfo
  {
    public string StationName { get; set; }
    public string DestinationName { get; set; }
    public string LineName { get; set; }
    public int TimeToStation { get; set; }
  }

  public abstract class BusBoardInput
  {
    public abstract string GetStringInput();
  }

  public class ConsoleBusBoardInput : BusBoardInput
  {
    public override string GetStringInput()
    {
      Console.Write("\nPlease enter a string:\n> ");
      return Console.ReadLine();
    }
  }
}
