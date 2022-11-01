using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace WpfProject.Radar
{
    public class RadarViewModel
    {
        public RadarModel radarModel { get; set; }
        
        public RadarViewModel()
        {
            radarModel = new RadarModel();
        }
        public Flight ConvertFlightIPToFlighInfo(FlightInfoPartial flightInfoPartial)
        {
            Flight flight = new Flight();
            string key = flightInfoPartial.SourceId;
            flight = radarModel.radarModelFlightInfo(key);
            return flight;
        }

        public void SaveFlightToDB (FlightInfoPartial flightInfoPartial)
        {
            radarModel.radarModelSave(flightInfoPartial);
        }
    }
}
