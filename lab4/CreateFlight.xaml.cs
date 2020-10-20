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
    /// Interaction logic for CreateFlight.xaml
    /// </summary>
    public partial class CreateFlight : Window
    {
        private MainWindow mainWindow;
        public CreateFlight(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        /// <summary>
        /// On create button click it will save the user inputs from the text box.
        /// then will try to create a flight with the input. 
        /// </summary>
        /// <param name="sender">default param for button</param>
        /// <param name="e">default param for event args</param>
        private void CreateFlightB(object sender, RoutedEventArgs e)
        {

            try//Check if the entered in number of passengers is an int.
            {
                string origin = OriginTB.Text;//Save all the texts from the boxes.
                string destination = DestinationTB.Text;
                string flightIden = FlightIdenTB.Text;

                int numPassengers = int.Parse(NumPassTB.Text);

                string feedBack = mainWindow.controller.CreateFlight(origin, destination, flightIden, numPassengers);//Try to create flight and get feedback.
                if (feedBack == "valid")
                {
                    MessageBox.Show("Success Flight was added");
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
        /// On click will close the create flight window..
        /// </summary>
        /// <param name="sender">default param for button</param>
        /// <param name="e">default param for event args</param>
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//Close the delete flight window.
        }
    }
}
