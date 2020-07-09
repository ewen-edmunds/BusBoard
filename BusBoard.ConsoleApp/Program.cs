﻿using System;
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
      //Example stop code: 490008660N

      try
      {
        List<BusInfo> busList = TfLClient.GetBusesAtStopCode(userInput);

        displaySystem.DisplayShortestTimeBuses(busList, numberBusesToDisplay);
      }
      catch (Exception e)
      {
        displaySystem.DisplayError(e.Message);
      }
    }
  }
}
