using System;
using System.Collections.Generic;
using System.Windows;

namespace lab4
{
    /// <summary>
    /// The controller for the flights />.
    /// </summary>
    public class Controller : IController
    {
        /// <summary>
        /// Defines the dataBase.
        /// </summary>
        private IDatabase dataBase;

        /// <summary>
        /// Initializes a new instance of database.
        /// </summary>
        /// <param name="dataBase"> data base object </param>
        public Controller(IDatabase dataBase)
        {
            this.dataBase = dataBase;
            dataBase.SaveToList();
        }

        /// <summary>
        /// The CreateFlight method that will create the flight.
        /// </summary>
        /// <param name="origin"> for the origin.</param>
        /// <param name="destination">for the destination.</param>
        /// <param name="flightIden">for the flight identifier.</param>
        /// <param name="numPassengers">for the number of passengers.</param>
        /// <returns>returns an int to interface to show if the flight was created </returns>
        public string CreateFlight(string origin, string destination, string flightIden, int numPassengers)
        {

            return dataBase.CreateFlight(origin, destination, flightIden, numPassengers);
        }

        /// <summary>
        /// Will update all the flights credentials. 
        /// </summary>
        /// <param name="origin"> for the origin.</param>
        /// <param name="destination">for the destination.</param>
        /// <param name="flightIden">for the flight identifier.</param>
        /// <param name="numPassengers">for the number of passengers.</param>
        /// <returns> returns an int to interface </returns>
        public string UpdateFlight(string origin, string destination, string flightIden, int numPassengers)
        {

            return dataBase.UpdateFlight(origin, destination, flightIden, numPassengers);
        }

        /// <summary>
        /// passes a string to deleteflight method to search for a flight to be deleted.
        /// </summary>
        /// <param name="flightIden"> the flight identifier to be checked .</param>
        /// <returns> a boolean if a flight has been deleted or not./>.</returns>
        public Boolean DeleteFlight(string flightIden)
        {
            return dataBase.DeleteFlight(flightIden);
        }

        /// <summary>
        /// calls the PrintAllflights method in database and returns all the flights to user.
        /// </summary>
        /// <returns> returns a list of flights to user</returns>
        public string PrintAllFlights()
        {
            return dataBase.PrintAllFlights();
        }

        public Flight RetrieveFlight(string flightID)
        {
            return dataBase.RetrieveFlight(flightID);
        }

        /// <summary>
        /// The method that will exit the program.
        /// </summary>
        public void EndProgram()
        {
            Environment.Exit(0);//close the main window.
        }
    }
}
