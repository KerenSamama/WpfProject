using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Collections.ObjectModel;

namespace BL
{
    public class BLIMP : IBL
    {

        public IDAL dal { get; set; }
        public BLIMP()
        {
            dal = new DALIMP();
        }

      

        public
        Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights()

         {
             return dal.GetCurrentFlights();
         }
        

        public Flight GetFlightData(string Key)
        {
            return dal.GetFlightData(Key);
        }

        public void SaveFlight(FlightInfoPartial flightInfoPartial)
        {
            dal.SaveFlightToDB(flightInfoPartial);
        }
       
        public ObservableCollection<FlightInfoPartial>GetFlightBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            List<FlightInfoPartial> ListOfFlightInDB = dal.GetAllFlightInDB();
            List<FlightInfoPartial> ListPred = ListOfFlightInDB.FindAll(p => p.DateAndTime.Date <= dateTo.Date && p.DateAndTime.Date >= dateFrom.Date);

            ObservableCollection<FlightInfoPartial> Obs = new ObservableCollection<FlightInfoPartial>(ListPred);
            return Obs;

        }



      

       
       
    }
}
