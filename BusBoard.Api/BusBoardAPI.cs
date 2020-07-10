using System;
using System.Collections.Generic;
using System.Configuration;

namespace BusBoard.Api
{
    public static class BusBoardAPI
    {
        private static int maximumSearchRadiusMetres = int.Parse(ConfigurationManager.AppSettings.Get("max_search_radius")); 
        private static int maxNumberBusStopsToConsider = int.Parse(ConfigurationManager.AppSettings.Get("max_bus_stop")); 
        public static List<BusInfo> GetSoonestBusesTo(string postcode)
        {
            PostcodeInfo.LongLat longLat = PostcodeClient.GetLongitudeLatitudePair(postcode);
            List<BusStopInfo> busStopInfos = TfLClient.GetClosestStops(longLat, maximumSearchRadiusMetres);
            if (busStopInfos.Count == 0)
            {
                throw new Exception("No valid bus stops were detected nearby!");
            }
            
            int numberBusStopsToConsider = Math.Min(busStopInfos.Count, maxNumberBusStopsToConsider);
        
            List<string> nearestStopCodes = busStopInfos.ConvertAll(x => x.NaptanID).GetRange(0,numberBusStopsToConsider);
            List<BusInfo> busList = TfLClient.GetBusesAtStopCodes(nearestStopCodes);

            busList.Sort((x, y) => x.ExpectedArrival.CompareTo(y.ExpectedArrival));
            busList.RemoveAll(x => x.ExpectedArrival.ToUniversalTime() <= DateTime.Now.ToUniversalTime());

            return busList;
        }
    }
}