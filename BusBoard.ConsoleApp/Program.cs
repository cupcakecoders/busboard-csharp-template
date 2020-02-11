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
            var tflBusResponse = GetTflBusesFromStopCode(busStop);
            DisplayAllBuses(tflBusResponse);
            
            //provide postcode, display 
            var postcode = new Prompts().GetUserInput("Enter your postcode");
            var postcodeList = GetLatLonFromPostcodesIo(postcode);
            TflBusStopsNearLatLon(postcodeList);

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

         public static List<Postcode> GetLatLonFromPostcodesIo(string postcode)
         {
             ConnectedApi postcodeClient = new ConnectedApi("https://api.postcodes.io");
             var postcodeResponse = postcodeClient.GetResponse<List<Postcode>>($"/postcodes/{postcode}");
             return postcodeResponse;
         }

         //https://api.tfl.gov.uk/StopPoint?stopTypes=NaptanPublicBusCoachTram&lat=51.576756&lon=-0.43125
         
         private static List<Postcode> TflBusStopsNearLatLon(List<Postcode> postcodes, string lat, string lon)
         {
             ConnectedApi tflClient = new ConnectedApi("https://api.tfl.gov.uk");
             var tflStopPointsWithinResponse = tflClient.GetResponse<List<Postcode>>($"https://api.tfl.gov.uk/StopPoint?stopTypes=NaptanPublicBusCoachTram&lat={lat}&lon=-{lon}");
             tflStopPointsWithinResponse.ForEach(); //get each bus stop
         }
         
    } 
}