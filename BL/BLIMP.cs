using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BL
{
    public class BLIMP : IBL
    {

        public IDAL dal { get; set; }
        public BLIMP()
        {
            dal = new DALIMP();
        }

        #region Flights

        public
           Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights()
         {
             return dal.GetCurrentFlights();
         }
        

        public Flight GetFlightData(string Key)
        {
            return dal.GetFlightData(Key);
        }
       
        



        /*
        public FlightInfoPartial GetVisibleInfo(string key)
{
    return getVisibleFromFlight(dal.GetFlightByKey(key));
        }
*/



        /*
public FlightInfoPartial getVisibleFromFlight(Root myFlight)
{
    FlightInfoPartial flightVisibleInfo = new FlightInfoPartial();
    HelperClass helperClass = new HelperClass();
    try
    {
        if (myFlight.identification != null)
        {
            flightVisibleInfo.Id = myFlight.identification.id;
            flightVisibleInfo.FlightCode = myFlight.identification.number.@default;
            flightVisibleInfo.FullFlightCode = myFlight.identification.callsign;
        }
        if (myFlight.airline != null)
        {
            flightVisibleInfo.AirlineName = myFlight.airline.name;
        }
        if (myFlight.airport != null)
        {
            if (myFlight.airport.origin != null)
            {
                flightVisibleInfo.OriginAirportIATA = myFlight.airport.origin.code.iata;
                flightVisibleInfo.OriginAirportCity = myFlight.airport.origin.position.region.city;
            }
            if (myFlight.airport.destination != null)
            {
                flightVisibleInfo.DestinationAirportIATA = myFlight.airport.destination.code.iata;
                flightVisibleInfo.DestinationAirportCity = myFlight.airport.destination.position.region.city;
            }
        }

        flightVisibleInfo.Longitude = myFlight.trail[0].lng;
        flightVisibleInfo.Latitude = myFlight.trail[0].lat;
        flightVisibleInfo.ImgUrl = myFlight.aircraft.images.thumbnails[0].link;

        if (myFlight.time.scheduled != null)
            flightVisibleInfo.ScheduledDeparture = helperClass.EpochToDateTime(myFlight.time.scheduled.departure);
        if (myFlight.time.real != null)
            if (myFlight.time.real.departure != null)
                flightVisibleInfo.RealDeparture = helperClass.EpochToDateTime((double)myFlight.time.real.departure);
        if (myFlight.time.scheduled != null)
            flightVisibleInfo.ScheduledArrival = helperClass.EpochToDateTime(myFlight.time.scheduled.arrival);
        if (myFlight.time.estimated != null)
            if (myFlight.time.estimated.arrival != null)
                flightVisibleInfo.EstimatedArrival = helperClass.EpochToDateTime((double)myFlight.time.estimated.arrival);

    }
    catch (Exception e)
    {
        Debug.Print(e.Message);
    }

    return flightVisibleInfo;
        }
*/
        #endregion

        #region weather
        #endregion
        /*
        #region Calendar
        public string getHoliday()
        {
            return dal.getEvent(DateTime.Today, DateTime.Today.AddDays(7));
        }
        #endregion
        */
    }
}
