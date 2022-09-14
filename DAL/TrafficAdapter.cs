using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using BE;

namespace DAL
{
    public class TrafficAdapter 
    {
        // link of a list with all the flights, untight, we dont know where is the info
        private const string AllURL = @" https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=38.805%2C24.785%2C29.014%2C40.505&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";
        // link that gives to us the details of the flights which is after the =, tight serialization
        private const string FlightURL = @"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";
        
        public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights()
        {

            Dictionary<string, List<FlightInfoPartial>> Result = new Dictionary<string, List<FlightInfoPartial>>();//belongs to BL
            JObject AllFlightData = null;
            IList<string> Keys = null;
            IList<Object> Values = null;

            List<FlightInfoPartial> Incoming = new List<FlightInfoPartial>();
            List<FlightInfoPartial> Outgoing = new List<FlightInfoPartial>();

            using (var webClient = new System.Net.WebClient()) // able to do a http request
            {
                var json = webClient.DownloadString(AllURL);

                HelperClass Helper = new HelperClass(); 
                AllFlightData = JObject.Parse(json); // convert the jason to data

                try
                {
                    foreach (var item in AllFlightData) // for each item in the data, for each flight
                    {
                        var key = item.Key;
                        if (key == "full_count") continue;
                        if (key == "version") continue;
                        // 11 is the source in the array, we create an object with every 
                        if (item.Value[11].ToString() == "TLV") Outgoing.Add(new FlightInfoPartial { Id = -1, Source = item.Value[11].ToString(), Destination = item.Value[12].ToString(), SourceId = key, Long = Convert.ToDouble(item.Value[1]), Lat = Convert.ToDouble(item.Value[2]), DateAndTime = Helper.GetDateTimeFromEpoch(Convert.ToDouble(item.Value[10])), FlightCode = item.Value[13].ToString() });
                        // incomimg flights, checks if the destination is tlv
                        if (item.Value[12].ToString() == "TLV") Incoming.Add(new FlightInfoPartial { Id = -1, Source = item.Value[11].ToString(), Destination = item.Value[12].ToString(), SourceId = key, Long = Convert.ToDouble(item.Value[1]), Lat = Convert.ToDouble(item.Value[2]), DateAndTime = Helper.GetDateTimeFromEpoch(Convert.ToDouble(item.Value[10])), FlightCode = item.Value[13].ToString() });
                        
                    }
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            //Add the flights
            Result.Add("Incoming", Incoming);
            Result.Add("Outgoing", Outgoing);

            return Result;
        }

        //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        //change root name to FlightData

        public Root GetFlightData(string Key)
        {
            var CurrentUrl = FlightURL + Key;
            Root CurrentFlight = null;
            //must use try-catch
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(CurrentUrl);
                try
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    CurrentFlight = serializer.Deserialize<Root>(json);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

            }
            return CurrentFlight;
        }
}
