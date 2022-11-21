using System;
using System.Collections.Generic;
using BE;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.Collections.ObjectModel;
using Nancy.Json;
using Newtonsoft.Json.Linq;
//Fonction + memoumachim


namespace DAL
{
    public class DALIMP : IDAL
    {
        public DALIMP() { }
      
            TrafficAdapter trafficAdapter = new TrafficAdapter();
            CalendarAdapter calendarAdapter = new CalendarAdapter();



        //LA FONCTION POUR RECUPERER TOUS LES VOLS, IL FAUT CHANGER LE NOM

        public
            
        Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights()
           {
               return trafficAdapter.GetCurrentFlights();
           }

       

        //LA FONCTION POUR RECUPERER UN, IL FAUT CHANGER LE NOM
         public Flight GetFlightData(string Key)
             {
               return trafficAdapter.GetFlightData(Key);
              }



       
        // Function that returns all the flights between two dates
        public List<FlightInfoPartial> GetAllFlightInDB()
        {
            List<FlightInfoPartial> flights = new List<FlightInfoPartial>();
            using (var ctx = new FlightContext())
            {
                flights = (from f in ctx.Flights
                           select f).ToList<FlightInfoPartial>();
            }
            return flights;
        }
        public void SaveFlightToDB(FlightInfoPartial flight)
        {
            List<FlightInfoPartial> listOfFlights = new List<FlightInfoPartial>();

            using (var ctx = new FlightContext())
            {
                listOfFlights = (from f in ctx.Flights
                                 where f.SourceId == flight.SourceId
                                 select f).ToList<FlightInfoPartial>();
                if (listOfFlights.Count == 0)
                {
                    ctx.Flights.Add(flight);
                    ctx.SaveChanges();
                }
            }
        }

        public void DeleteFlight (FlightInfoPartial flight)
        {
            List<FlightInfoPartial> listOfFlights = new List<FlightInfoPartial>();

            using (var ctx = new FlightContext())
            {
                listOfFlights = (from f in ctx.Flights
                                 where f.SourceId == flight.SourceId
                                 select f).ToList<FlightInfoPartial>();
                if (listOfFlights.Count != 0)
                {
                    ctx.Flights.Remove(listOfFlights.First());
                    ctx.SaveChanges();
                }
            }

        }

        public class FlightContext : DbContext
        {
            public FlightContext() : base("DataBase")
            {

            }
            public DbSet<FlightInfoPartial> Flights { get; set; }
        }

        

        


    }
}




