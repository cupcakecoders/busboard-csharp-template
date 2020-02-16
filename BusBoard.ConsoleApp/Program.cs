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
            //ask for bus stop code, display upcoming buses
            var busStop = new Prompts().GetUserInput("Type a bus stop code to see the next 5 buses at that stop");
            var tflClientInstance = new TflConnectedApi("https://api.tfl.gov.uk");
            var tflBusResponse = tflClientInstance.GetTflBusesFromStopCode(busStop);
            
            DisplayAllBuses(tflBusResponse);

            //provide postcode, display 
            var postcodeInput = new Prompts().GetUserInput("Enter your postcode");
            var postcode =
                new PostCodesConnectedApi("https://api.postcodes.io").GetResponseFromPostcodesIo(postcodeInput);

            var stopPointsInRadius = tflClientInstance.TflBusStopsNearLatLon(postcode.Longitude(), postcode.Result.Latitude); //two ways to access properties in class
            var naptainIdOne = stopPointsInRadius.GetNaptanId(0);
            var naptainIdTwo = stopPointsInRadius.GetNaptanId(1);
            var busesNaptanOne = new List<Bus>();
            var busesNaptanTwo = new List<Bus>();
            if (naptainIdOne != "")
            {
                busesNaptanOne = tflClientInstance.GetTflBusesFromStopCode(naptainIdOne);
            }
            if (naptainIdTwo != "")
            {
                busesNaptanTwo = tflClientInstance.GetTflBusesFromStopCode(naptainIdTwo);
            }

            DisplayAllBuses(busesNaptanOne.Concat(busesNaptanTwo).ToList());
        }
        
         public static void DisplayAllBuses(List<Bus> buses)
         {
             buses.ForEach(bus => Console.WriteLine(bus.LineName));
         }
         
    }
}
//e1 6gw
//490008360N