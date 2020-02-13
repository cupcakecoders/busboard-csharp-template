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
            var stopPointCodes = TflBusStopsNearLatLon(longitude, latitute);
            var busStopTwo = GetStopPointCode(stopPointCodes);
            var buses = GetTflBusesFromStopCode1(busStopTwo);
            DisplayAllBuses(buses);
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

         
         public static string GetLatFromPostcodeIoResponse(Postcode postcodes)
         {
             try
             {
                 var latitude = postcodes.Result.Latitude;
                 return latitude; 
                 
             }
             catch (UnhandledExceptionEventArgs e)
             {
                 Console.WriteLine(e);
                 throw;
             }
             
         }
         
         public static string GetLonFromPostcodeIoResponse(Postcode postcodes)
         {
             var longitude = postcodes.Result.Longitude;
             return longitude; 
         }
         
         private static StopPointsRadius TflBusStopsNearLatLon(string latitude, string longitude)
         {
             ConnectedApi tflClient = new ConnectedApi("https://api.tfl.gov.uk");
             var tflStopPointsInRadius = tflClient.GetResponse<StopPointsRadius>($"https://api.tfl.gov.uk/StopPoint?stopTypes=NaptanPublicBusCoachTram&lat={longitude}&lon={latitude}");
             return tflStopPointsInRadius;
         }

         private static string GetStopPointCode(StopPointsRadius stopPoints)
         {
             var busstop = stopPoints.StopPoints[0].NaptanId;
             return busstop;
         }
         
         private static List<Bus> GetTflBusesFromStopCode1(string busStopTwo)
         {
             ConnectedApi tflClient = new ConnectedApi("https://api.tfl.gov.uk");
             var tflBusResponse1 = tflClient.GetResponse<List<Bus>>($"/StopPoint/{busStopTwo}/Arrivals");
             return tflBusResponse1;
         }
        
    } 
}

//"longitude": -0.43125,
//"latitude": 51.576756,
//e1 6gw
//490008660N - stop codes works
//490008360N