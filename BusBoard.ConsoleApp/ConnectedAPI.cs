using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RestSharp;

namespace BusBoard
{
    public class ConnectedApi
    {
        internal IRestClient Rc { get; set; }

        public T GetResponse<T>(string resource)
        {
            var restRequest = new RestRequest(resource, DataFormat.Json);
            return Rc.Get<T>(restRequest).Data;
        }
    }

    public class TflConnectedApi : ConnectedApi
    {

        public TflConnectedApi(string baseUrl)
        {
            Rc = new RestClient(baseUrl)
                .AddDefaultQueryParameter("app_id", Environment.GetEnvironmentVariable("TFL_APP_ID"))
                .AddDefaultQueryParameter("app_key", Environment.GetEnvironmentVariable("TFL_APP_KEY"));
        }
      
        public List<Bus> GetTflBusesFromStopCode(string naptanId)
        {
            var tflBusResponse = GetResponse<List<Bus>>($"/StopPoint/{naptanId}/Arrivals");
            return tflBusResponse;
        }

        public StopPointsRadius TflBusStopsNearLatLon(string longitude, string latitude)
        {
            return GetResponse<StopPointsRadius>($"https://api.tfl.gov.uk/StopPoint?stopTypes=NaptanPublicBusCoachTram&lat={latitude}&lon={longitude}");
        }
    }
    
    public class PostCodesConnectedApi : ConnectedApi
    {
        public PostCodesConnectedApi(string baseUrl)
        {
            Rc = new RestClient(baseUrl);
        }
        
        public Postcode GetResponseFromPostcodesIo(string postcode)
        {
            Postcode postcodeResponse = GetResponse<Postcode>($"/postcodes/{postcode}");
            return postcodeResponse;
        }
    }
}