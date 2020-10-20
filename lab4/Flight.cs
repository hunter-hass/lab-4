using System;
using System.Collections.Generic;
using System.Text;

namespace lab4
{

    public class Flight : IComparable<Flight>
    {
        //Create variables. 
        private string destination;
        private string flightIden;
        private string origin;
        private int numPassengers;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Flight r = (Flight)obj;
            return (this.flightIden.Equals(r.flightIden)) && (this.origin.Equals(r.origin)) && (this.destination.Equals(r.destination) && (this.numPassengers == r.numPassengers));
        }

        /// <summary>
        /// Will return if the instance we are on is smaller or greater than what we 
        /// are comparing it to.
        /// </summary>
        /// <param name="obj"> The object we are passing in to compare </param>
        /// <returns> an int on how it should be sorted </returns>
        public int CompareTo(Flight obj)
        {
            return this.flightIden.CompareTo(obj.flightIden);
        }
        /// <summary>
        /// Sets all the data for the flight class
        /// </summary>
        /// <param name="origin"> origin</param>
        /// <param name="destination">destination</param>
        /// <param name="flightIden">flight ID</param>
        /// <param name="numPassengers"> Number of passengers </param>
        public Flight(String origin, String destination, String flightIden, int numPassengers)
        {
            this.origin = origin;
            this.destination = destination;
            this.flightIden = flightIden;
            this.numPassengers = numPassengers;
        }
        /// <summary>
        /// gets origin
        /// </summary>
        /// <returns> origin </returns>
        public string GetOrigin()
        {
            return origin;
        }
        /// <summary>
        /// sets origin
        /// </summary>
        /// <param name="origin"> to set origin</param>
        public void SetOrigin(String origin)
        {
            this.origin = origin;

        }
        /// <summary>
        /// gets destination
        /// </summary>
        /// <returns> returns destination </returns>
        public string GetDestination()
        {
            return destination;
        }
        /// <summary>
        /// Sets the destination
        /// </summary>
        /// <param name="destination"></param>
        public void SetDestination(string destination)
        {
            this.destination = destination;
        }
        /// <summary>
        /// Get flight ID
        /// </summary>
        /// <returns> returns flight ID </returns>
        public string GetflightIden()
        {
            return flightIden;
        }
        /// <summary>
        /// Sets the flight ID
        /// </summary>
        /// <param name="flightIden"> flight ID to set</param>
        public void SetflightIden(String flightIden)
        {
            this.flightIden = flightIden;

        }
        /// <summary>
        /// Returns passengers
        /// </summary>
        /// <returns> returns passengers</returns>
        public int GetNumPassengers()
        {

            return numPassengers;
        }
        /// <summary>
        /// Sets passengers
        /// </summary>
        /// <param name="numPassengers">Set num passengers</param>
        public void SetNumPassengers(int numPassengers)
        {
            this.numPassengers = numPassengers;
        }
        /// <summary>
        /// Create a string to print when we want to display the flights credentials
        /// </summary>
        /// <returns> returns a string to user of what the flight is</returns>
        public override string ToString()
        {

            return "Flight ID: " + this.flightIden + ", Origin: " + this.origin + ", Destination: " + this.destination + ", Number of Passengers " + this.numPassengers;

        }
    }

}
