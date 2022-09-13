using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;


//information sur le vol

namespace BE
{
    public class FlightRecap
    {
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Speed { get; set; }
        public string AircraftModelCode { get; set; }
        public string Registration { get; set; }
        public DateTime DateTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string AbrFlightCode { get; set; }
        public string FullFlightCode { get; set; }
        public string AirlineICAO { get; set; }
        public Location Location { get { return new Location(Latitude, Longitude); } }

        public override string ToString()
        {
            return Id;
        }
    }
}