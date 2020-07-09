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
            
            return client.Get<PostcodeInfo>(request).Data.Result;
        }
    }
}