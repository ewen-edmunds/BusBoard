using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.ConsoleApp;

namespace BusBoard.Web.ViewModels
{
  public class BusInfo
  {
    public string PostCode { get; set; }
    public List<ConsoleApp.BusInfo> nextBuses;
    
    public BusInfo(string postCode)
    {
      this.PostCode = postCode;
      this.nextBuses = new List<ConsoleApp.BusInfo>();
      FetchNextBuses();
    }

    //todo: make maxSearch and noToConsider part of config file
    public void FetchNextBuses()
    {
      nextBuses = BusBoardAPI.GetSoonestBusesTo(PostCode);
    }
  }
}