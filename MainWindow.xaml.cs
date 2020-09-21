﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace DogAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            raceBox.Items.Add("germanshepherd");
            raceBox.Items.Add("chow");

        }

        //Sletter start teksten når den bliver trykket på
        private void raceSoeg_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "Søg efter race")
                txtBox.Text = string.Empty;
        }

        //Søger efter teksten indtastet i search boxen 
        private void Soeg_Click(object sender, RoutedEventArgs e)
        {
            string race = raceSoeg.Text;
 
            string apistring = "https://dog.ceo/api/breed/" + raceSoeg.Text  + "/images/random";

            try
            {
                doggoPic.Source = new BitmapImage(new Uri(hentBillede(apistring)));
            }
            catch (Exception)
            {
            }
        }

        //Henter billede af racen valgt på listen
        private void raceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string apistring = "https://dog.ceo/api/breed/"+ raceBox.SelectedItem + "/images/random";

            try
            {
                doggoPic.Source = new BitmapImage(new Uri(hentBillede(apistring)));
            }
            catch (Exception)
            {
            }

        }

        private string hentBillede(string API)
        {
            try
            {
                var Json = new WebClient().DownloadString(API);
                string[] temp = Json.Split('"');
                temp[3] = temp[3].Replace(@"\", "");
                return temp[3];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Dog may not exist");
            }
            return null;
        }
    }
}
