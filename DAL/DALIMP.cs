using System;
using System.Collections.Generic;
using BE;


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
       // public DALIMP() { }
      
            TrafficAdapter trafficAdapter = new TrafficAdapter();
        //  CalendarAdapter calendarAdapter = new CalendarAdapter();



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
         


        //LA FONCTION POUR RECUPERER LE CALENDRIER
        /*
            public Calendar getCalendar(DateTime start, DateTime end)
            {
                return calendarAdapter.getCalendar(start, end);
            }
        */

        //LA FONCTION JAI PAS ENCORE REFLECHIS C'ETAIT QUOI

        /*
        public string getEvent(DateTime start, DateTime end)
        {
        Calendar calendar = getCalendar(start, end);

        if (calendar.days != null)
        {
            foreach (HebDate day in calendar.days)
                if (!day.events[0].StartsWith("Parashat") && !day.events[0].StartsWith("Erev"))
                    return day.events[0];
        }
        return null;
        */


    }
}




