using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard
{
    public class StopPointsRadius
    {
        public List<StopPoints> StopPoints { get; set; }

        public string GetNaptanId(int index)
        {
            try
            {
                return StopPoints[index].NaptanId;
            }
            catch (System.ArgumentOutOfRangeException err)
            {
                Console.WriteLine($"We got an error {err}");
                return "";
            }
        }
    }

    public class StopPoints
    {
        public string NaptanId { get; set; } //StopPoints
    }
    
    
}