namespace BusBoard
{
    public class Postcode
    {
        public Result Result { get; set; } //property
        
    }

    public class Result
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}