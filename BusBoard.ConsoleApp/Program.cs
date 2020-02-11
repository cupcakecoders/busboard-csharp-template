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
            var postcodeResponse = GetResponseFromPostcodesIo(postcode);
            var latitute = GetLatFromPostcodeIoResponse(postcodeResponse);
            var longitude = GetLonFromPostcodeIoResponse(postcodeResponse);
            var stopPoints = TflBusStopsNearLatLon(longitude, latitute);
            var stopPointData = DisplayStopPointName(stopPoints);
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

         public static Postcode GetResponseFromPostcodesIo(string postcode)
         {
             ConnectedApi postcodeClient = new ConnectedApi("https://api.postcodes.io");
             Postcode postcodeResponse = postcodeClient.GetResponse<Postcode>($"/postcodes/{postcode}");

             return postcodeResponse;
         }

         public static double GetLatFromPostcodeIoResponse(Postcode postcodes)
         {
             var latitude = postcodes.Result.Latitude;
             return latitude; 
         }
         
         public static double GetLonFromPostcodeIoResponse(Postcode postcodes)
         {
             var longitude = postcodes.Result.Longitude;
             return longitude; 
         }
         
         private static List<StopPointsRadius> TflBusStopsNearLatLon(double latitude, double longitude)
         {
             ConnectedApi tflClient = new ConnectedApi("https://api.tfl.gov.uk");
             var tflStopPointsInRadius = tflClient.GetResponse<List<StopPointsRadius>>($"https://api.tfl.gov.uk/StopPoint?stopTypes=NaptanPublicBusCoachTram&lat={latitude}&lon=-{longitude}");
             return tflStopPointsInRadius;
         }

         private static string DisplayStopPointName(List<StopPointsRadius> stopPoints)
         {
             
             return null;
         }
    } 
}