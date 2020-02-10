using System;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var client = new RestClient("https://api.tfl.gov.uk/");
            client.Authenticator = new HttpBasicAuthenticator("f8163132", "fc375759d5162e056d8437f676e78aef");

            var request = new RestRequest("StopPoint/490008660N", DataFormat.Json);

            var response = client.Get(request);
            Console.WriteLine(response);
        }
    }
}