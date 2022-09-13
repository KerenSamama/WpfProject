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
        public DALIMP() { }
        private const string AllURL = @" https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=38.805%2C24.785%2C29.014%2C40.505&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";
        private const string FlightURL = @"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";

       
        public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights()
        {
            //c'est la liste des avions dans la database en partance et en provenance de TLV

            Dictionary<string, List<FlightInfoPartial>> Result = new Dictionary<string, List<FlightInfoPartial>>();//belongs to BL
            //On cree une liste de Flight Info partial, et de string
            JObject AllFlightData = null; // 
            IList<string> Keys = null;
            IList<Object> Values = null;

            //JSON: JavaScript Object Notation :  Il est principalement utilisé pour 
            //transmettre les données d’une application web entre un serveur virtuel hôte et un client.

            List<FlightInfoPartial> Incoming = new List<FlightInfoPartial>();// liste des flights qui entre en israel
            List<FlightInfoPartial> Outgoing = new List<FlightInfoPartial>();// liste  des flights qui sort d'israel

            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(AllURL); 
                //cree un michtane json qui donne toutes les donnees actuelles


                HelperClass Helper = new HelperClass();
                //helper epoch time
                AllFlightData = JObject.Parse(json);
                //Parse : Chargez un JObject à partir d'une chaîne contenant JSON.


                try
                {
                    foreach (var item in AllFlightData) // pour chaque item de la base de donnees
                    {
                        var key = item.Key; 
                        if (key == "full_count") continue;//premiere ligne
                        if (key == "version") continue;// premiere ligne
                        // il a mis id=-1 parce qu'on doit rajouter le id et savoir le gerer
                        if (item.Value[11].ToString() == "TLV") Outgoing.Add(new FlightInfoPartial { Id = -1, Source = item.Value[11].ToString(), Destination = item.Value[12].ToString(), SourceId = key, Long = Convert.ToDouble(item.Value[1]), Lat = Convert.ToDouble(item.Value[2]), DateAndTime = Helper.GetDateTimeFromEpoch(Convert.ToDouble(item.Value[10])), FlightCode = item.Value[13].ToString() });
                        if (item.Value[12].ToString() == "TLV") Incoming.Add(new FlightInfoPartial { Id = -1, Source = item.Value[11].ToString(), Destination = item.Value[12].ToString(), SourceId = key, Long = Convert.ToDouble(item.Value[1]), Lat = Convert.ToDouble(item.Value[2]), DateAndTime = Helper.GetDateTimeFromEpoch(Convert.ToDouble(item.Value[10])), FlightCode = item.Value[13].ToString() });
                        // la on rajoute a la list d'avion rentrant et sortant de TLV
                    }
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            Result.Add("Incoming", Incoming);
            Result.Add("Outgoing", Outgoing);

            return Result;
        }




        public BE.Root GetFlightData(string Key)
        {
            // un seul vol 

            var CurrentUrl = FlightURL + Key; // toutes les infos d1 vol
            Root CurrentFlight = null;// cree un object CurrentFlight vide
            //must use try-catch
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(CurrentUrl);
                try
                {
                    //Serializer : La sérialisation est une opération qui consiste à transformer une variable
                    //composite (comme un objet ou un tableau, qui est constitué de plusieurs éléments) 
                    //en une variable scalaire (on dit aussi atomique), 
                    //généralement une chaîne de caractère.
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
}

