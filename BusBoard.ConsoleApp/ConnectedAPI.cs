using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RestSharp;

namespace BusBoard
{
    public class ConnectedApi
    {
        internal RestClient RestClient { get; set; }

        public T GetResponse<T>(string resource)
        {
            var restRequest = new RestRequest(resource, DataFormat.Json);
            return RestClient.Get<T>(restRequest).Data;
        }
    }

    public class TflConnectedApi : ConnectedApi
    {

        public TflConnectedApi(string baseUrl)
        {
            RestClient = (RestClient) new RestClient(baseUrl)
                .AddDefaultQueryParameter("app_id", Environment.GetEnvironmentVariable("TFL_APP_ID"))
                .AddDefaultQueryParameter("app_key", Environment.GetEnvironmentVariable("TFL_APP_KEY"));
        }
    }
    
    public class PostCodesConnectedApi : ConnectedApi
    {
        public PostCodesConnectedApi(string baseUrl)
        {
            RestClient = (RestClient) new RestClient(baseUrl);
        }
    }
}