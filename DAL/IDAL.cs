using System;
using System.Data;
using System.Collections.Generic;
using BE;
//using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// le nom des fonctions
namespace DAL
{
    public interface IDAL
    {

        #region Flights

        Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights(); // tous les vols 
        Flight GetFlightData(string Key); //Un vol

        
        #endregion

        // List<FlightRecap> getAllFlightsSummarize();

        //    Flight GetFlightByKey(string key);

        //   string getEvent(DateTime start, DateTime end);
        //Weather getWeather(Location location)();
        //HebEvent getHebevent(Date date)();

        
    }
}

