using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public static class TfLClient
    {
        static RestClient client = new RestClient("https://api.tfl.gov.uk/"); 
        //client.Authenticator = new HttpBasicAuthenticator("username", "password");
        public static List<BusInfo> GetBusesAtStopCode(string stopCode)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      
            var request = new RestRequest($"StopPoint/{stopCode}/Arrivals", Method.GET);
            request.AddUrlSegment("app_id", "1ca74190");
            request.AddUrlSegment("app_key", "ad2ece2581024fa28abe323c0d4109c7");
      
            var response = client.Execute(request);

            if (client.Get<List<BusInfo>>(request).IsSuccessful == false)
            {
                throw new Exception("An error occurred while trying to obtain the data. Check the stop code is correct and try again.");
            }
            
            return client.Get<List<BusInfo>>(request).Data;;
        }
    }
}