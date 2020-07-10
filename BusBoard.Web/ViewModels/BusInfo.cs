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
      PostcodeInfo.LongLat longLat = PostcodeClient.GetLongitudeLatitudePair(PostCode);

      List<BusStopInfo> busStopInfos = TfLClient.GetClosestStops(longLat, 250);
        
      if (busStopInfos.Count == 0)
      {
        throw new Exception("No valid bus stops were detected nearby!");
      }

      int numberBusStopsToConsider = Math.Min(busStopInfos.Count, 2);
        
      List<string> nearestStopCodes = busStopInfos.ConvertAll(x => x.NaptanID).GetRange(0,numberBusStopsToConsider);
      nextBuses = TfLClient.GetBusesAtStopCodes(nearestStopCodes);
    }
  }
}