using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HelperClass
    {

        public HelperClass()
        {

        }
        //helper function to convert from unix epoch time Human DateTime
        // the dal uses this calculator, traanslates the number in the database into a date
        public DateTime GetDateTimeFromEpoch(double EpochTimeStamp)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0); //from start epoch time, time from 1 january 1970, we give number it coverts to date
            start = start.AddSeconds(EpochTimeStamp);//add the seconds to the start DateTime
            return start;
        }
    }
}

