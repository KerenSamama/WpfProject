using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public interface IBL
    {
        //GetWeatherInfo
        //GetCalendarInfo
        FlightVisibleInfo GetVisibleInfo(string key);
        FlightVisibleInfo getVisibleFromFlight(Flight myFlight);

        List<FlightSummarize> getAllFlights();
        Flight GetFlightByKey(string key);
        string getHoliday();

    }
}
