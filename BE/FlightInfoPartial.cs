using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Cette classe nous donne des infos partielle pour le vol c'est ce qui s'affiche sur la page
namespace BE
{
    // Translation of the json of flighradar24, helps to know which flights are interesting for us
    public class FlightInfoPartial
    {
        public int Id { get; set; }
        public string SourceId { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Source { get; set; } // we have to check if the source or the destination is tlv
        public string Destination { get; set; }
        public string FlightCode { get; set; }
      

    }
}
