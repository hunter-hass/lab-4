using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab4
{
    /// <summary>
    /// Interaction logic for DisplayFlightsWindow.xaml
    /// </summary>
    public partial class DisplayFlightsWindow : Window
    {
        /// <summary>
        /// On opening we will display flights to the user. 
        /// </summary>
        public DisplayFlightsWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            string flights = mainWindow.controller.PrintAllFlights();
            //MessageBox.Show(App.controller.PrintAllFlights());
            if (flights.Length > 0)//If the list returned contains at least one flight then we can print them out.
            {

                displayBox.Text = flights;//display the the flights to the user.
            }
            else
            {
                displayBox.Text = "No flights to print.";

            }
        }
        /// <summary>
        /// Closes out the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
