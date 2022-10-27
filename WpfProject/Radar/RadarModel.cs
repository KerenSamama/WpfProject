using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;

namespace WpfProject.Radar
{
    public class RadarModel
    {
        IBL BL;
        public RadarModel()
        {
            BL = new BLIMP();
        }
        public Flight radarModelFlightInfo(string key)
        {
            return BL.GetFlightData(key);
        }

        public void radarModelSave( FlightInfoPartial flightInfoPartial)
        {
            BL.SaveFlight(flightInfoPartial);
        }
    }
}
