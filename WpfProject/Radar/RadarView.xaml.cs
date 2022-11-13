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

namespace WpfProject.Radar
{
    /// <summary>
    /// Interaction logic for RadarView.xaml
    /// </summary>
    public partial class RadarView : UserControl
    {
        public BE.Flight Flight = new BE.Flight();
        public RadarView()
        {
            InitializeComponent();
            WeatherGrid.Visibility = Visibility.Hidden;
            Border.Visibility = Visibility.Hidden;
            UpdateWeatherTLV();
            Border_Copy.Visibility = Visibility.Hidden;
            WeatherGrid1.Visibility = Visibility.Hidden;
        }

        public RadarViewModel radarViewModel = new RadarViewModel();

        FlightInfoPartial SelectedFlight = null; //Selected Flight
        TrafficAdapter dal = new TrafficAdapter();

        private void ReadAllButton_Click(object sender, RoutedEventArgs e)
        {
            var FlightKeys = dal.GetCurrentFlights(); // gets the flight according to the key

            // this.DataContext = FlightKeys;
            InFlightsListBox.DataContext = FlightKeys["Incoming"];
            OutFlightsListBox.DataContext = FlightKeys["Outgoing"];

            foreach (FlightInfoPartial flight in InFlightsListBox.Items)
            {
                try
                {
                    UpdateMap(flight);
                }
                catch (Exception) { }
            }
        }

        private void FlightsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedFlight = e.AddedItems[0] as BE.FlightInfoPartial; //InFlightsListBox.SelectedItem as FlightInfoPartial;
                UpdateFlight(SelectedFlight);
                Flight = radarViewModel.ConvertFlightIPToFlighInfo(SelectedFlight);
                radarViewModel.SaveFlightToDB(SelectedFlight);
                UpdateWeather(SelectedFlight);
                Border_Copy.Visibility = Visibility.Visible;
                WeatherGrid1.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async void UpdateWeather(FlightInfoPartial selected)
        {
            using (var webClient = new System.Net.WebClient())
            {
                string URL = $"https://api.openweathermap.org/data/2.5/forecast?lat={selected.Lat}&lon={selected.Long}&cnt=1&appid=88107aa04053a4cf2c34e7962481c7e3&units=metric";
                var json = await webClient.DownloadStringTaskAsync(URL);

                string degrees = json.Substring(json.IndexOf("temp") + 6, 4);
                DegreeLabel.Content = degrees + "°C - ";

                json = json.Substring(json.IndexOf("weather") + 6);
                json = json.Substring(json.IndexOf("main") + 7);

                string weather = json.Substring(0, json.IndexOf(",") - 1);

                Weatherlabel.Content = weather;
                Border.Visibility = Visibility.Visible;
                WeatherGrid.Visibility = Visibility.Visible;

                string imagePath;
                switch (weather)
                {
                    case "Clear":
                        imagePath = (selected.DateAndTime.Hour >= 20 || selected.DateAndTime.Hour <= 5) ? @"../images/Night.PNG" : @"../images/Sun2.PNG"; break;
                    case "Rain": imagePath = @"../images/Pluie.PNG"; break;
                    case "Clouds": imagePath = @"../images/NuagesSeuls.PNG"; break;
                    case "Snow": imagePath = @"../images/Neige.PNG"; break;
                    case "Extreme": imagePath = @"../images/Tonnerre.PNG"; break;
                    default: imagePath = @"../images/Sun2.PNG"; break;
                }
                WeatherIMG.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            }
        }
        private async void UpdateWeatherTLV()
        {
            using (var webClient = new System.Net.WebClient())
            {
                string URL = $"https://api.openweathermap.org/data/2.5/forecast?lat=32.109333&lon=34.855499&cnt=1&appid=88107aa04053a4cf2c34e7962481c7e3&units=metric";
                var json = await webClient.DownloadStringTaskAsync(URL);

                string degrees = json.Substring(json.IndexOf("temp") + 6, 4);
                DegreeLabel1.Content = degrees + "°C - ";

                json = json.Substring(json.IndexOf("weather") + 6);
                json = json.Substring(json.IndexOf("main") + 7);

                string weather = json.Substring(0, json.IndexOf(",") - 1);

                Weatherlabel1.Content = weather;

                string imagePath;
                switch (weather)
                {
                    case "Clear":
                        imagePath = (DateTime.Now.Hour >= 20 || DateTime.Now.Hour <= 5) ? @"../images/Night.PNG" : @"../images/Sun2.PNG"; break;
                    case "Rain": imagePath = @"../images/Pluie.PNG"; break;
                    case "Clouds": imagePath = @"../images/NuagesSeuls.PNG"; break;
                    case "Snow": imagePath = @"../images/Neige.PNG"; break;
                    case "Extreme": imagePath = @"../images/Tonnerre.PNG"; break;
                    default: imagePath = @"../images/Sun2.PNG"; break;
                }
                WeatherIMG1.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            }
        }
        private async void UpdateFlight(FlightInfoPartial selected)
        {
           try { 
                var Flight = dal.GetFlightData(selected.SourceId);

                DetailsPanel.DataContext = Flight;

                    // Update map
                    if (Flight != null)
                    {
                        List<Trail> OrderedPlaces = (from f in Flight.trail
                                                     orderby f.ts
                                                     select f).ToList<Trail>();
                        // UpdateMap(selected);
                        addNewPolyLine(OrderedPlaces);
                     }

            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
        }

        private void UpdateMap(FlightInfoPartial selected)
        {
            var Flight = dal.GetFlightData(selected.SourceId);
            if (Flight != null)
            {
                var OrderedPlaces = (from f in Flight.trail
                                     orderby f.ts
                                     select f).ToList<Trail>();

                //      addNewPolyLine(OrderedPlaces);

                //MessageBox.Show(Flight.airport.destination.code.iata);

                Pushpin PinCurrent = new Pushpin { ToolTip = selected.FlightCode },
                        PinOrigin = new Pushpin { ToolTip = Flight.airport.origin.name };


                PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };
                MapLayer.SetPositionOrigin(PinCurrent, origin);

                //Better to use RenderTransform

                if (Flight.airport.destination.code.iata == "TLV")
                    PinCurrent.Style = (Style)Resources["ToIsrael"];

                else
                    PinCurrent.Style = (Style)Resources["FromIsrael"];


                Trail CurrentPlace = OrderedPlaces.Last<Trail>();
                var PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                PinCurrent.Location = PlaneLocation;


                CurrentPlace = OrderedPlaces.First<Trail>();
                PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                PinOrigin.Location = PlaneLocation;

                myMap.Children.Add(PinOrigin);
                //ajouter le event qui trace la ligne qd la souris passe dessus
                //myMap.Children[myMap.Children.Count-1].MouseEnter += FlightMouseEnter;

                myMap.Children.Add(PinCurrent);
                //ajouter le event qui trace la ligne qd la souris passe dessus

                //myMap.Children[myMap.Children.Count - 1].MouseEnter += FlightMouseEnter;
            }
        }
        private void FlightMouseEnter(object sender, MouseEventArgs e)
        {
            var Flight = dal.GetFlightData((InFlightsListBox.SelectedItem as FlightInfoPartial).SourceId);
            var OrderedPlaces = (from f in Flight.trail
                                 orderby f.ts
                                 select f).ToList<Trail>();
            addNewPolyLine(OrderedPlaces);
        }
        private void Pin_MouseDown(object sender, MouseButtonEventArgs e) //MouseDown="Pin_MouseDown"
        {
            Pushpin pin = e.OriginalSource as Pushpin;
            if (pin != null)
                MessageBox.Show(pin.ToolTip.ToString());
        }

        void addNewPolyLine(List<Trail> Route)
        {
            // design of the route


            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new SolidColorBrush(Colors.Green);
            polyline.StrokeThickness = 1;
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection();
            foreach (var item in Route)
            {
                polyline.Locations.Add(new Location(item.lat, item.lng));
            }

            // myMap.Children.Clear(); // clear every line in the map
            myMap.Children.Remove(myMap.Children[myMap.Children.Count - 1]);
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

        //private void ComboBox_Loaded_Outgoing(object sender, RoutedEventArgs e)
        //{
        //    var FlightKeys = dal.GetCurrentFlights();
        //    List<string> data = new List<string>();
        //    foreach (var flight in FlightKeys["Outgoing"])
        //    {
        //        data.Add(flight.Destination);
        //    }
        //    var combo = sender as ComboBox;
        //    combo.ItemsSource = data;
        //    combo.SelectedIndex = 0;
        //}

        //private void ComboBox_Loaded_Incomimg(object sender, RoutedEventArgs e)
        //{
        //    var FlightKeys = dal.GetCurrentFlights();
        //    List<string> data = new List<string>();
        //    foreach (var flight in FlightKeys["Incoming"])
        //    {
        //        data.Add(flight.Source);
        //    }
        //    var combo = sender as ComboBox;
        //    combo.ItemsSource = data;
        //    combo.SelectedIndex = 0;

        //}
        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        // var selectedcomboitem = sender as ComboBox;
        //    string name = selectedcomboitem.SelectedItem as string;
        //    MessageBox.Show(name);
        //}
    }
}
