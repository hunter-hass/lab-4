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
    /// Interaction logic for DeleteFlightWindow.xaml
    /// </summary>
    public partial class DeleteFlightWindow : Window
    {
        private MainWindow mainWindow;
        public DeleteFlightWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        /// <summary>
        /// On button click we will try to delete the flight the user asked to delete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteFLBT_Click(object sender, RoutedEventArgs e)
        {
            string flightIden = DeleteFlightBox.Text;
            Boolean deleteCheck = mainWindow.controller.DeleteFlight(flightIden);
            if (deleteCheck)//If true then print you deleted flight.
            {
                MessageBox.Show("Yay you deleted the flight " + flightIden);
            }
            else//Else print could not create flight.
            {
                MessageBox.Show("Error flight could not be deleted either not found or invalid input");
            }
            this.Close();
        }
        /// <summary>
        /// Will close out the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
