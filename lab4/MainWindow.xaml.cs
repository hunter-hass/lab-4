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

namespace lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IController controller;
        public MainWindow()
        {
            InitializeComponent();
            IDatabase dataBase = new Database();//Create database and controller objects.
            controller = new Controller(dataBase);


        }
        /// <summary>
        /// This is the create Flight Button on click it show the create flight window. 
        /// </summary>
        /// <param name="sender">default object</param>
        /// <param name="e">default peram</param>
        private void CreateFlightClick(object sender, RoutedEventArgs e)
        {
            CreateFlight flightWindow = new CreateFlight(this);
            flightWindow.Show();
        }


        /// <summary>
        /// uppon flight update button click it will then show teh update flight window.
        /// </summary>
        /// <param name="sender">default param</param>
        /// <param name="e">default param</param>
        private void UpdateFlightClick(object sender, RoutedEventArgs e)
        {
            UpdateFlightW updateWindow = new UpdateFlightW(this);
            updateWindow.Show();//show update window
        }

        /// <summary>
        /// On delete flight button click it will show the user the delete flight window.
        /// </summary>
        /// <param name="sender">default param for button</param>
        /// <param name="e">default param for event args</param>
        private void DeleteBT_Click_1(object sender, RoutedEventArgs e)
        {

            DeleteFlightWindow deleteWindow = new DeleteFlightWindow(this);
            deleteWindow.Show();//show delete window.
        }

        /// <summary>
        /// On button click the display flight window will show. Then displaying the flights if there is any.
        /// </summary>
        /// <param name="sender">default param for button</param>
        /// <param name="e">default param for event args</param>
        private void displayBT_Click(object sender, RoutedEventArgs e)
        {
            DisplayFlightsWindow displayWindow = new DisplayFlightsWindow(this);
            displayWindow.Show();
        }

        /// <summary>
        /// Closes the main program window on click.
        /// </summary>
        /// <param name="sender">default param for button</param>
        /// <param name="e">default param for event args</param>
        private void end_programBT_Click(object sender, RoutedEventArgs e)
        {
            controller.EndProgram();
        }
    }
}
