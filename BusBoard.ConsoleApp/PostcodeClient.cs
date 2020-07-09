using System;
using System.Net;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public static class PostcodeClient
    {
        static RestClient client = new RestClient("https://api.postcodes.io/postcodes/");
        
        public static PostcodeInfo.LongLat GetLongitudeLatitudePair(string postcode)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var request = new RestRequest($"{postcode}", Method.GET);
            var response = client.Get<PostcodeInfo>(request);
            
            if (response.IsSuccessful == false)
            {
                throw new Exception("An error occurred while trying to obtain the postcode data. Check the postcode is correct and try again.");
            }

            return response.Data.Result;
        }
    }
}