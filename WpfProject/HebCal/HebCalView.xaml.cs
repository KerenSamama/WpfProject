using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using BE;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Threading;
using DAL;
using System.Diagnostics;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public  partial class HebCalView: UserControl
    {
        public HebCalViewModel hcViewModel = new HebCalViewModel();
        public BE.Calendar Calendar = new BE.Calendar();
        public HebCalView()
        {  
            InitializeComponent();
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DateTime date = OriginalDate.SelectedDate.Value;

            using (var webClient = new System.Net.WebClient())
            {
                int i = 0;
                string yyyy,mm,dd,URL;
                RootHeb Data;

                for(DateTime date1 = date.AddDays(7) ; date.CompareTo(date1) != 1 ; date = date.AddDays(1),i++) { 
                    yyyy = date.ToString("yyyy");
                    mm = date.ToString("MM");
                    dd = date.ToString("dd");

                    URL = $"https://www.hebcal.com/converter?cfg=json&date={yyyy}-{mm}-{dd}&g2h=1&strict=1"; 
                   var json = await webClient.DownloadStringTaskAsync(URL);
                
                    Data = JsonConvert.DeserializeObject<RootHeb>(json);
                    if(Data.events.Count > 1) { 
                        
                        if (Data.events[0].Contains("Erev")) 
                              MessageBox.Show( "ערב חג : " + (i>0? " חג בעוד "+(i+1)+" ימים ":"") +"\n"+ Data.events[0].Substring(5));
                             
                        else 
                              MessageBox.Show( " חג היום :\n " + Data.events[0]);
                         return;
                        
                    
                }
            }
            MessageBox.Show("יום רגיל");
        }
    }
}
    }
