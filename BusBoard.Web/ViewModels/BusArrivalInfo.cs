using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
  public class BusArrivalInfo
  {
    public string PostCode { get; set; }
    public bool isRetrievedDataSuccessful { get; set; }
    public string ErrorMessage { get; set; }
    public List<BusInfo> NextBuses { get; set; }
    
    public BusArrivalInfo(string postCode)
    {
      PostCode = postCode;
      NextBuses = new List<BusInfo>();
    }

    public void FetchNextBuses()
    {
      try
      {
        NextBuses = BusBoardAPI.GetSoonestBusesTo(PostCode);
        isRetrievedDataSuccessful = true;
      }
      catch (Exception e)
      {
        ErrorMessage = e.Message;
        isRetrievedDataSuccessful = false;
      }
    }
  }
}