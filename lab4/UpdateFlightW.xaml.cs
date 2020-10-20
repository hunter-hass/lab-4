using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab4
{
    /// <summary>
    /// Interaction logic for updateFlightW.xaml
    /// </summary>
    public partial class UpdateFlightW : Window
    {
        private MainWindow mainWindow;
        public UpdateFlightW(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// On update button click it will save the user inputs from the text box.
        /// then will try to create a flight with the input. 
        /// </summary>
        /// <param name="sender">default param for button</param>
        /// <param name="e">default param for event args</param>
        private void UpdateFlightB(object sender, RoutedEventArgs e)
        {
            try//Chech if entered in passengers is an int.
            {

                string origin = OriginTB2.Text;//Stores user input
                string destination = DestinationTB2.Text;
                string flightIden = FlightIdenTB2.Text;
                int numPassengers = int.Parse(NumPassTB2.Text);
                string feedBack = mainWindow.controller.UpdateFlight(origin, destination, flightIden, numPassengers);//Sends user input to update flight, gets feedback showing what happened.
                if (feedBack == "valid")
                {
                    MessageBox.Show("Success Flight was updated");
                }
                else
                {
                    MessageBox.Show(feedBack);
                }
            }
            catch (FormatException mes)
            {
                MessageBox.Show(mes.Message);
            }
            this.Close();

        }
        /// <summary>
        /// On click it will close out the update flight window.
        /// </summary>
        /// <param name="sender">Default paramater</param>
        /// <param name="e">default param</param>
        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
