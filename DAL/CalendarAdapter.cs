using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CalendarAdapter
    {
        private const string HebCalURL = @"https://www.hebcal.com/converter?cfg=json&date=";
        private const string EndHebCalURL = @"&g2h=1&strict=1";

        private WebAdapter webAdapter = new WebAdapter();

        //JAI CHANGER KEREN

        public Calendar getCalendar(DateTime start, DateTime end)
        {
            Calendar calendar = new Calendar();
            for (DateTime date = start; date <= end; date.AddDays(1))
            {
                var json = webAdapter.GetDataFromUrl(HebCalURL + date.ToString("yyyy-MM-dd") + EndHebCalURL);
                calendar.days.Add((RootHeb)JsonConvert.DeserializeObject(json, typeof(RootHeb)));
            }
            return calendar;
        }



    }
}
