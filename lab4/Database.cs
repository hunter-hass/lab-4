namespace lab4
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows;

    /// <summary>
    /// The data base to keep track of all flights.
    /// </summary>
    public class Database : IDatabase
    {
        /// <summary>
        /// Defines the flights.
        /// </summary>
        List<Flight> flights = new List<Flight>();


        /// <summary>
        /// Defines the connectionStringToDB.
        /// </summary>
        string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;//Connection string

        /// <summary>
        /// Allows the user to create a flight. Checks if all the input is valid and returns an int back to the menu 
        /// showing what went wrong or if the flight was created.
        /// </summary>
        /// <param name="origin"> for the origin.</param>
        /// <param name="destination">for the destination.</param>
        /// <param name="flightIden">for the flight identifier.</param>
        /// <param name="numPassengers">for the number of passengers.</param>
        /// <returns>.</returns>
        public string CreateFlight(string origin, string destination, string flightIden, int numPassengers)
        {
            string feedBack = "valid";

            if (ValidOriginAndDest(origin, destination) != "valid")//Checks if everything entered is valid
            {
                feedBack = ValidOriginAndDest(origin, destination);
            }
            else if (CheckIdenLength(flightIden) != flightIden)
            {
                feedBack = CheckIdenLength(flightIden);
            }
            else if (FindIden(flightIden) >= 0)
            {
                feedBack = "Duplicate";
            }
            else if (ValidPassengers(numPassengers) != "valid")
            {
                feedBack = ValidPassengers(numPassengers);
            }
            else
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(connectionStringToDB);//Create connection
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO Flights(FlightID, Origin, Destination, NumPassengers) VALUES(@FlightID, @Origin, @Destination, @NumPassengers)", conn);
                    cmd.Parameters.AddWithValue("@FlightID", flightIden);//Insert all parameters.
                    cmd.Parameters.AddWithValue("@Origin", origin);
                    cmd.Parameters.AddWithValue("@Destination", destination);
                    cmd.Parameters.AddWithValue("@NumPassengers", numPassengers);
                    cmd.ExecuteNonQuery();
                    Flight flight = new Flight(origin, destination, flightIden, numPassengers);//If valid input then create the flight.

                    flights.Add(flight);
                    conn.Close();
                    string total = "";
                    for (int i = 0; i < flights.Count; i++)
                    {
                        total = total + flights[i] + "\n";
                    }
                    //MessageBox.Show(total);
                }
                catch (Exception e)
                {

                    feedBack = "Error in dataBase access.";
                }
            }
            return feedBack;
        }

        /// <summary>
        /// Will check if the user can update the flight or not, will then update it if 
        /// the flight id is valid.
        /// </summary>
        /// <param name="origin"> for the origin.</param>
        /// <param name="destination">for the destination.</param>
        /// <param name="flightIden">for the flight identifier.</param>
        /// <param name="numPassengers">for the number of passengers.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string UpdateFlight(string origin, string destination, string flightIden, int numPassengers)
        {
            int flightIndex = FindIden(flightIden);
            string feedBack = "valid";

            if (ValidOriginAndDest(origin, destination) != "valid")//Checks if everything entered is valid
            {
                feedBack = ValidOriginAndDest(origin, destination);
            }
            else if (CheckIdenLength(flightIden) != flightIden)
            {
                feedBack = CheckIdenLength(flightIden);
            }
            else if (flights.Count == 0) // check if the flightID fits requirements.
            {
                feedBack = "No flights to update.";
            }
            else if (flightIndex == -1)
            {
                feedBack = "Flight ID does not exist.";
            }
            else if (ValidPassengers(numPassengers) != "valid")
            {
                feedBack = ValidPassengers(numPassengers);
            }
            else
            {
                int rows = 0;
                try
                {
                    MySqlConnection conn = new MySqlConnection(connectionStringToDB);//Create connection and fill in params.
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE Flights SET Origin=@Origin,Destination=@Destination,NumPassengers=@NumPassengers WHERE FlightID=@FlightID", conn);
                    cmd.Parameters.AddWithValue("@FlightID", flightIden);//update database with params.
                    cmd.Parameters.AddWithValue("@Origin", origin);
                    cmd.Parameters.AddWithValue("@Destination", destination);
                    cmd.Parameters.AddWithValue("@NumPassengers", numPassengers);
                    rows = cmd.ExecuteNonQuery();
                    flights[flightIndex].SetOrigin(origin);//If valid then update the flight.
                    flights[flightIndex].SetDestination(destination);
                    flights[flightIndex].SetNumPassengers(numPassengers);
                    conn.Close();
                    string total = "";
                    for (int i = 0; i < flights.Count; i++)
                    {
                        total = total + flights[i] + "\n";
                    }
                    //MessageBox.Show(total);
                }
                catch (Exception e)
                {
                    feedBack = "";
                }

                if (rows == 0)
                {
                    return "No Flights updated";
                }
            }
            return feedBack;
        }

        /// <summary>
        /// will delete a flight if it is a valid flight to be deleted.
        /// </summary>
        /// <param name="flightIden"> flight identifier to be checked.</param>
        /// <returns> returns a boolean showing if a flight was deleted or not .</returns>
        public Boolean DeleteFlight(string flightIden)
        {
            int deleteCheck = 0;
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionStringToDB);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Flights WHERE FlightID=@FlightID", conn);
                cmd.Parameters.AddWithValue("@FlightID", flightIden);
                deleteCheck = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                return false;
            }

            if (deleteCheck > 0)
            {
                int index = FindIden(flightIden);//Finds the index of the flight to be deleted
                if (index >= 0 && flights.Count != 0)//If flight index is found and flight list is not empty then delete that flight.
                {
                    flights.Remove(flights[index]);
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Will print all flights to user.
        /// </summary>
        /// <returns> returns true if flights could be shown and false if flights could not be shown .</returns>
        public string PrintAllFlights()
        {
            string built = "";
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionStringToDB);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * FROM Flights", conn); //Grab all the flights 
                MySqlDataReader reader = cmd.ExecuteReader();
                int count = reader.FieldCount;

                StringBuilder str = new StringBuilder();
                int j = 0;
                while (reader.Read())
                {
                    for (int i = 0; i < count; i++)//Loop through each row.
                    {
                        if (i == 0)
                        {
                            str.Append("FlightID: " + reader.GetValue(i) + " ");
                        }
                        else if (i == 1)
                        {
                            str.Append("Origin: " + reader.GetValue(i) + " ");
                        }
                        else if (i == 2)
                        {
                            str.Append("Destination: " + reader.GetValue(i) + " ");
                        }
                        else
                        {
                            str.Append("Passengers: " + reader.GetValue(i) + " ");
                        }

                    }
                    str.Append("\n");
                    j++;
                }
                built = str.ToString();
                conn.Close();
            }
            catch (Exception e)
            {

            }
            return built;
        }

        /// <summary>
        /// The SaveToList.
        /// </summary>
        public void SaveToList()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionStringToDB);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * FROM Flights", conn); //Grab all the flights 
                MySqlDataReader reader = cmd.ExecuteReader();
                int count = reader.FieldCount;
                string[] arr = new string[4];
                StringBuilder str = new StringBuilder();
                int j = 0;
                while (reader.Read())
                {

                    Flight flight = new Flight(reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), reader.GetValue(0).ToString(), int.Parse(reader.GetValue(3).ToString()));
                    flights.Add(flight);
                }
                string total = "";
                for (int i = 0; i < flights.Count; i++)
                {
                    total = total + flights[i] + "\n";
                }
                //MessageBox.Show(total);

                conn.Close();
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Checks if the passengers entered is valid.
        /// </summary>
        /// <param name="numPassengers"> for number of passengers .</param>
        /// <returns> returns a boolean showing if the number of passengers is valid.</returns>
        public string ValidPassengers(int numPassengers)
        {
            if (numPassengers < 0)//If entered passengers is between 0 and 100 then return true;
            {
                return "Passengers must be greater at least a positive number.";
            }
            else if (numPassengers > 100)
            {
                return "Number of Passengers can't be greater than 100.";
            }
            return "valid";
        }

        /// <summary>
        /// Checks if the origin or destination is valid.
        /// </summary>
        /// <param name="origin">The origin<see cref="string"/>.</param>
        /// <param name="destination">The destination<see cref="string"/>.</param>
        /// <returns> returns the entered string if was valid. Returns string error if invalid. .</returns>
        public string ValidOriginAndDest(string origin, string destination)
        {
            if (origin.Length == 4)
            {
                if (destination.Length == 4)
                {//If string is length of 4 then check if it is a valid string.

                    if (Regex.IsMatch(origin, @"^[a-zA-Z]+$") == false)//Checks if the string is all letters.
                    {
                        return "Origin must contain only letters";
                    }
                    else if (Regex.IsMatch(destination, @"^[a-zA-Z]+$") == false)
                    {
                        return "Destination must contain only letters";
                    }
                }
                else
                {
                    return "Destination length must be exactly four.";
                }
            }
            else
            {
                return "Origin length must be exactly four.";
            }
            return "valid";
        }

        /// <summary>
        /// checks if the entered flight ID has been entered if so return the index. else return -1.
        /// </summary>
        /// <param name="flightIden"> flight ID to be checked .</param>
        /// <returns> returns index of found flight or -1 if flight was not found. .</returns>
        public int FindIden(string flightIden)
        {
            int idenCount = -1;
            int tracker = 0;
            while (idenCount < 0 && flights.Count > 0 && tracker < flights.Count)//While loop runs if a flight was found then returning the index it was found at
            {
                if (flightIden.Equals(flights[tracker].GetflightIden()))//If flight ID was found then return the index it was found at.
                {
                    idenCount = tracker;
                }
                tracker++;
            }
            return idenCount;
        }

        /// <summary>
        /// Checks if entered flight ID is valid.
        /// </summary>
        /// <param name="flightIden"> flight ID to be checked .</param>
        /// <returns> returns the entered string if valid else return false.</returns>
        public string CheckIdenLength(string flightIden)
        {
            if (flightIden.Length != 6)//Check if length is of 6  
            {

                return "Flight ID must be length of six.";
            }
            else if (Regex.IsMatch(flightIden.Substring(0, 3), @"^[a-zA-Z]+$") == false)//Checks if first three characters are letters.
            {
                return "First three chars of Flight ID must consist of letters only.";
            }
            else if ((Regex.IsMatch(flightIden.Substring(3, 3), @"^[0-9]+$") == false))//Checks if last three characters are letters.
            {

                return "Last three chars of Flight ID must consist of letters only.";

            }
            return flightIden;
        }

        public Flight RetrieveFlight(string flightID)
        {
            Flight temp = null;
            for (int i = 0; i < flights.Count; i++)
            {
                if (flights[i].GetflightIden().Equals(flightID))
                {
                    return flights[i];
                }
            }
            return temp;
        }
    }
}
