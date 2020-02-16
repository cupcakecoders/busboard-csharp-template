using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RestSharp;

namespace BusBoard
{
    public class ConnectedApi
    {
        private RestClient RestClient { get; set; }
        
        public ConnectedApi(string baseUrl, string config)
        {
            RestClient = (RestClient) new RestClient(baseUrl);
            if (config == "tfl")
            {
                RestClient
                    .AddDefaultQueryParameter("app_id", Environment.GetEnvironmentVariable("TFL_APP_ID"))
                    .AddDefaultQueryParameter("app_key", Environment.GetEnvironmentVariable("TFL_APP_KEY"));
            }
        }
        
        public T GetResponse<T>(string resource)
        {
            var restRequest = new RestRequest(resource, DataFormat.Json);
            return RestClient.Get<T>(restRequest).Data;
        }
        //create method to pass in env variables
    }
}