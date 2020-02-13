namespace BusBoard.WebApp.ViewModel
{
    public class BusInfo
    {
        public string Postcode { get; }
        
        public BusInfo(string postcode)
        {
            Postcode = postcode;
        }
    }
}