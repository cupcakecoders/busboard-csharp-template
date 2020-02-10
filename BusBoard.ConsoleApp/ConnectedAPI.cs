using System.Collections.Generic;
using RestSharp;

namespace BusBoard
{
    public class ConnectedApi
    {
        private RestClient RestClient { get; set; } 
        
        public ConnectedApi(string baseUrl)
        {
            RestClient = new RestClient(baseUrl);
        }
        public T GetResponse<T>(string resource)
        {
            var restRequest = new RestRequest(resource, DataFormat.Json);
            return RestClient.Get<T>(restRequest).Data;
        }
        //create method to pass in env variables
    }
}