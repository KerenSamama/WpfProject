﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HebDates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string URL1 = "https://www.hebcal.com/converter?cfg=json&date=2021-09-07&g2h=1&strict=1";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = OriginalDate.SelectedDate.Value;
            using (var webClient = new System.Net.WebClient())
            {
                var yyyy = date.ToString("yyyy");
                var mm = date.ToString("MM");
                var dd = date.ToString("dd");
                string URL = $"https://www.hebcal.com/converter?cfg=json&date={yyyy}-{mm}-{dd}&g2h=1&strict=1"; 
                var json = await webClient.DownloadStringTaskAsync(URL);
                Root Data = JsonConvert.DeserializeObject<Root>(json);
                MessageBox.Show(Data.events[0].Contains("Erev") ? "ערב חג" : "יום רגיל");
            }
        }
    }
}
