namespace BusBoard
{
    public class Postcode
    {
        public Result Result { get; set; } //property

        /*public string Latitude()
        {
            return Result.Latitude;
        }*/
        
        public string Longitude()
        {
            return Result.Longitude;
        }
    }

    public class Result
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}