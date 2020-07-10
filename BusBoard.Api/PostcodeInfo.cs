namespace BusBoard.Api
{
    public class PostcodeInfo
    {
        public LongLat Result { get; set; }
    
        public class LongLat
        {
            public string Longitude { get; set; }
            public string Latitude { get; set; }
        }
    }
}