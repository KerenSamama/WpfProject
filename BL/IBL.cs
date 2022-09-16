using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;


namespace BL
{
    
    public interface IBL
    {
       

        Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights();

        Flight GetFlightData(string Key);


        //GetWeatherInfo
        //GetCalendarInfo
        //  FlightInfoPartial GetVisibleInfo(string key);
        //  FlightInfoPartial getVisibleFromFlight(Root myFlight);

        //   List<FlightRecap> getAllFlights();
        // Root GetFlightByKey(string key);
        //  string getHoliday();

    }
    
}
