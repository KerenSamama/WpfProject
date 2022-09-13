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
       Dictionary<string, List<BE.FlightInfoPartial>> GetCurrentFlights();
       BE.Root GetFlightData(string Key);


           // List<FlightSummarize> getAllFlightsSummarize();
        //    Flight GetFlightByKey(string key);

         //   string getEvent(DateTime start, DateTime end);
            //Weather getWeather(Location location)();
            //HebEvent getHebevent(Date date)();
        







    }  
}

