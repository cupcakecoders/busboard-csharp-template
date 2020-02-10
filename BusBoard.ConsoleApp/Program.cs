using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            var busStop = new Prompts().GetUserInput("Type a bus stop code to see the next 5 buses at that stop");
            
            ConnectedApi tflClient = new ConnectedApi("https://api.tfl.gov.uk");
            var tflBusResponse = tflClient.GetResponse<List<Bus>>($"/StopPoint/{busStop}/Arrivals");
            tflBusResponse.ForEach(bus => Console.WriteLine(bus.LineName));

            
            //var client = new RestClient("https://api.tfl.gov.uk");
            //var postcodeClient = new RestClient("https://api.postcodes.io");
            var postcode = new Prompts().GetUserInput("Enter your postcode");
            ConnectedApi postcodeClient = new ConnectedApi("https://api.postcodes.io");
            var postcodeResponse = postcodeClient.GetResponse<List<Postcode>>($"/postcodes/{postcode}");
            postcodeResponse.ForEach(pc => Console.WriteLine(pc.Lat));
            
            // Console.WriteLine(postcodeLatLonResponse);
        }
    } 
}