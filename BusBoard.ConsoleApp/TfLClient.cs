using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public static class TfLClient
    {
        static RestClient client = new RestClient("https://api.tfl.gov.uk/"); 
        //client.Authenticator = new HttpBasicAuthenticator("username", "password");
        public static List<BusInfo> GetBusesAtStopCodes(List<string> stopCodes)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            
            List<BusInfo> busInfos = new List<BusInfo>();

            foreach (string stopCode in stopCodes)
            {
                var request = new RestRequest($"StopPoint/{stopCode}/Arrivals", Method.GET);
                request.AddUrlSegment("app_id", "1ca74190");
                request.AddUrlSegment("app_key", "ad2ece2581024fa28abe323c0d4109c7");
            
                if (client.Get<List<BusInfo>>(request).IsSuccessful == false)
                {
                    throw new Exception("An error occurred while trying to obtain the bus info data. Check the stop code is correct and try again.");
                }
                
                busInfos.AddRange(client.Get<List<BusInfo>>(request).Data);
            }

            return busInfos;
        }

        public static List<BusStopInfo> GetClosestStops(PostcodeInfo.LongLat longLat,int maxSearchRadius)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      
            var request = new RestRequest($"StopPoint?radius={maxSearchRadius}&lat={longLat.Latitude}&lon={longLat.Longitude}", Method.GET);
            request.AddUrlSegment("app_id", "1ca74190");
            request.AddUrlSegment("app_key", "ad2ece2581024fa28abe323c0d4109c7");
            request.AddQueryParameter("stopTypes", "NaptanPublicBusCoachTram");
      
            var response = client.Execute(request);
            
            if (client.Get<WrappedBusStopInfo>(request).IsSuccessful == false)
            {
                throw new Exception("An error occurred while trying to obtain the closest stop code. Check the postcode is correct (and close enough to a stop code!) and try again.");
            }

            WrappedBusStopInfo stopInfos = client.Get<WrappedBusStopInfo>(request).Data;
            stopInfos.StopPoints.Sort((x, y) => x.Distance.CompareTo(y.Distance));
            
            return stopInfos.StopPoints;
        }
    }
}