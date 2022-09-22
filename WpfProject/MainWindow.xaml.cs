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



namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FlightInfoPartial SelectedFlight = null; //Selected Flight
        TrafficAdapter dal = new TrafficAdapter();
        
        public MainWindow()
        {
            
            InitializeComponent();
     
        }

        private void ReadAllButton_Click(object sender, RoutedEventArgs e)
        {
             var FlightKeys = dal.GetCurrentFlights(); // gets the flight according to the key

            // this.DataContext = FlightKeys;
            InFlightsListBox.DataContext  = FlightKeys["Incoming"];
            OutFlightsListBox.DataContext = FlightKeys["Outgoing"];
            //load current data
        }

        private void FlightsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFlight = e.AddedItems[0] as FlightInfoPartial; //InFlightsListBox.SelectedItem as FlightInfoPartial;
            UpdateFlight(SelectedFlight);
        }

        private async void UpdateFlight(FlightInfoPartial selected)
        {
            var Flight = dal.GetFlightData(selected.SourceId);

            DetailsPanel.DataContext = Flight;

            // Update map
            if (Flight != null)
            {
                var OrderedPlaces = (from f in Flight.trail
                                     orderby f.ts
                                     select f).ToList<Trail>();

                addNewPolyLine(OrderedPlaces);

                //MessageBox.Show(Flight.airport.destination.code.iata);
                Trail CurrentPlace = null;

                Pushpin PinCurrent = new Pushpin { ToolTip = selected.FlightCode },
                        PinOrigin  = new Pushpin { ToolTip = Flight.airport.origin.name };

                PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };
                MapLayer.SetPositionOrigin(PinCurrent, origin);

                //Better to use RenderTransform
                
                if (Flight.airport.destination.code.iata == "TLV")
                    PinCurrent.Style = (Style)Resources["ToIsrael"];
                
                else
                    PinCurrent.Style = (Style)Resources["FromIsrael"];

                CurrentPlace = OrderedPlaces.Last<Trail>();
                var PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                PinCurrent.Location = PlaneLocation;


                CurrentPlace = OrderedPlaces.First<Trail>();
                PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                PinOrigin.Location = PlaneLocation;

                //PinCurrent.MouseDown += Pin_MouseDown;

                myMap.Children.Add(PinOrigin);
                myMap.Children.Add(PinCurrent); 


            /*weather*/
            using (var webClient = new System.Net.WebClient())
            {
            string URL = $"https://api.openweathermap.org/data/2.5/forecast?lat={selected.Lat}&lon={selected.Long}&cnt=1&appid=88107aa04053a4cf2c34e7962481c7e3&units=metric"; 
            var json = await webClient.DownloadStringTaskAsync(URL);
            string temp = json.Substring(json.IndexOf("temp")+6,4);
            TemperatureLabel.Content = "Weather : " + temp + "°C - ";
            json = json.Substring(json.IndexOf("weather")+6);
            json = json.Substring(json.IndexOf("main")+7);
            string weather = json.Substring(0,json.IndexOf(",")-1);
            TemperatureLabel.Content += weather;


}



            }
        }

        private void Pin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Pushpin pin = e.OriginalSource as Pushpin;
            if(pin!=null)
                MessageBox.Show(pin.ToolTip.ToString());
        }

        void addNewPolyLine(List<Trail> Route)
        {
            // design of the route
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            polyline.StrokeThickness = 1;
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection();
            foreach (var item in Route)
            {
                polyline.Locations.Add(new Location(item.lat, item.lng));
            }

            myMap.Children.Clear(); // clear every line in the map
            myMap.Children.Add(polyline); // add the new line
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateFlight(SelectedFlight);
            Counter.Text = (Convert.ToInt32(Counter.Text) + 1).ToString();
        }
    }
}

