using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public interface IController
    {
        /// <summary>
        /// The CreateFlight method that will create the flight.
        /// </summary>
        /// <param name="origin"> for the origin.</param>
        /// <param name="destination">for the destination.</param>
        /// <param name="flightIden">for the flight identifier.</param>
        /// <param name="numPassengers">for the number of passengers.</param>
        /// <returns>returns an int to interface to show if the flight was created </returns>
        public string CreateFlight(string origin, string destination, string flightIden, int numPassengers);


        /// <summary>
        /// Will update all the flights credentials. 
        /// </summary>
        /// <param name="origin"> for the origin.</param>
        /// <param name="destination">for the destination.</param>
        /// <param name="flightIden">for the flight identifier.</param>
        /// <param name="numPassengers">for the number of passengers.</param>
        /// <returns> returns an int to interface </returns>
        public string UpdateFlight(string origin, string destination, string flightIden, int numPassengers);

        /// <summary>
        /// passes a string to deleteflight method to search for a flight to be deleted.
        /// </summary>
        /// <param name="flightIden"> the flight identifier to be checked .</param>
        /// <returns> a boolean if a flight has been deleted or not./>.</returns>
        public Boolean DeleteFlight(string flightIden);


        /// <summary>
        /// calls the PrintAllflights method in database and returns all the flights to user.
        /// </summary>
        /// <returns> returns a list of flights to user</returns>
        public string PrintAllFlights();

        public Flight RetrieveFlight(string flightID);

        /// <summary>
        /// The method that will end the program.
        /// </summary>
        public void EndProgram();


    }
}
