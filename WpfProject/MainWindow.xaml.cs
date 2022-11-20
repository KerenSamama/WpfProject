﻿using System.Windows;
using System.Windows.Input;
using WpfProject.Radar;
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





namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RadarView radarView;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!(MainWindowUC.Content is RadarView) )
            {
                radarView = new RadarView();
            }
            startButton.Visibility = Visibility.Hidden;
            ViewCalendar.Visibility = Visibility.Hidden;
            stp.Visibility = Visibility.Hidden;
            deco.Visibility = Visibility.Hidden;
            welcome.Visibility = Visibility.Hidden;
            to.Visibility = Visibility.Hidden;
            airplane.Visibility = Visibility.Hidden;
            MainWindowUC.Content = radarView;
            

        }
    }


    }


