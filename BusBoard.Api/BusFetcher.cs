using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace BusBoard.Api
{
    public static class BusFetcher
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

            Console.WriteLine(busList[0].ExpectedArrival.ToUniversalTime().ToString());
            busList.Sort((x, y) => x.ExpectedArrival.ToUniversalTime().CompareTo(y.ExpectedArrival.ToUniversalTime()));
            busList.RemoveAll(x => DateTime.ParseExact(x.ExpectedArrival.ToUniversalTime().ToString(),  "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) <= DateTime.ParseExact(DateTime.Now.ToUniversalTime().ToString(),  "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture));

            return busList;
        }
    }
}