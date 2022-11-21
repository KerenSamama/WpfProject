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
        
     

        Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights(); // tous les vols 
        Flight GetFlightData(string Key); //Un vol


      

        List<FlightInfoPartial> GetAllFlightInDB();
        void SaveFlightToDB(FlightInfoPartial flight);
        void DeleteFlight(FlightInfoPartial flight);



    }
}

