﻿using System;
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
            var client = new RestClient("https://api.tfl.gov.uk");
            client.Authenticator = new HttpBasicAuthenticator("f8163132", "fc375759d5162e056d8437f676e78aef");
            
            var postcodeClient = new RestClient("https://api.postcodes.io");

            Prompts busCodePrompt = new Prompts();
            var busStop = busCodePrompt.GetUserInput("Type a bus stop code to see the next 5 buses at that stop");

            var nextFiveBusesRequest = new RestRequest($"/StopPoint/{busStop}/Arrivals", DataFormat.Json);
            var nextFiveBusesResponse = client.Get<List<Bus>>(nextFiveBusesRequest);

            var data = nextFiveBusesResponse.Data;
            
            foreach (var bus in data)
            {
                Console.WriteLine(bus.LineName);
            }
            
            /*foreach (var item in nextFiveBusesResponse.Data)
            {
                var lineName = item.LineName;
                Console.WriteLine(lineName);
            }*/
            
            Prompts postCodePrompt = new Prompts();
            var postcode = postCodePrompt.GetUserInput("Enter your postcode");

            var postcodeLatLonRequest = new RestRequest($"/postcodes/{postcode}");
            var postcodeLatLonResponse = client.Get<List<Postcode>>(postcodeLatLonRequest);
            Console.WriteLine(postcodeLatLonResponse);
        }
    } 
}