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

            var tflBusResponse = GetTflBusesFromStopCode(busStop);
            DisplayAllBuses(tflBusResponse);
            
            
            var postcode = new Prompts().GetUserInput("Enter your postcode");
            ConnectedApi postcodeClient = new ConnectedApi("https://api.postcodes.io");
            var postcodeResponse = postcodeClient.GetResponse<List<Postcode>>($"/postcodes/{postcode}");
            postcodeResponse.ForEach(pc => Console.WriteLine(pc.Result.Latitude));
            postcodeResponse.ForEach(pc => Console.WriteLine(pc.Result.Longitude));
            
            //https://api.tfl.gov.uk/StopPoint?stopTypes=bus&location.lat=51.576756&location.lon=-0.43125

            Postcode postcodeInstance = new Postcode();
            var tflStopPointsWithinResponse = tflClient.GetResponse<List<Postcode>>($"https://api.tfl.gov.uk/StopPoint?stopTypes=bus&location.lat={postcodeInstance.Result.Latitude}&location.lon={postcodeInstance.Result.Longitude}");
            
        }

         private static List<Bus> GetTflBusesFromStopCode(string busStop)
        {
            ConnectedApi tflClient = new ConnectedApi("https://api.tfl.gov.uk");
            var tflBusResponse = tflClient.GetResponse<List<Bus>>($"/StopPoint/{busStop}/Arrivals");
            return tflBusResponse;
        }

         public static void DisplayAllBuses(List<Bus> buses)
         {
             buses.ForEach(bus => Console.WriteLine(bus.LineName));
         }
    } 
}