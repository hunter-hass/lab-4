using System;
using System.Collections.Generic;
using System.IO;
using lab4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlightTester
{
    [TestClass]
    public class UnitTest1
    {

        /// <summary>
        /// Method to return a controller object.
        /// </summary>
        /// <returns></returns>
        public IController ReturnController()
        {
            IDatabase dataBase = new Database();
            IController controller = new Controller(dataBase);

            return controller;//return controller object. 
        }

        /// <summary>
        /// This method will test creating a flight with all valid input. 
        /// The createFlight method returns a message of what happened
        /// if it created a flight it will return the string "valid".
        /// </summary>
        [TestMethod]
        public void ACreateFlight()
        {

            IController controllerTemp = ReturnController();

            string result = controllerTemp.CreateFlight("meno", "oshh", "asd771", 5);//Create the flight 
            Console.WriteLine(result);

            Assert.AreEqual("valid", result);//If the result returns valid then flight was created. 
        }

        /// <summary>
        /// This Method Will test creating a duplicate flight that already exists.
        /// The test will pass because it will pass with expected error being returned.
        /// </summary>
        [TestMethod]
        public void CreateFlightErrorDuplicate()
        {
            IController controllerTemp = ReturnController();

            string result = controllerTemp.CreateFlight("meno", "oshh", "asd771", 5);//Create the flight 
            Console.WriteLine(result);

            Assert.AreEqual("Duplicate", result);//Test if expected error message was returned.

        }


        /// <summary>
        /// This method puts the first three chars of flight id
        /// to be invalid by containing a number. 
        /// The result will return the error message. 
        /// </summary>
        [TestMethod]
        public void TestCreateInvalidIDFirstChars()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.CreateFlight("meno", "oshh", "as1768", 5);//get message
            Assert.AreEqual("First three chars of Flight ID must consist of letters only.", result);
        }

        /// <summary>
        /// This method will test the last three chars of the flight id
        /// by setting one of them to a later which will then
        /// cause create flight to return an error message.
        /// </summary>
        [TestMethod]
        public void TestCreateInvalidIDLastChars()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.CreateFlight("meno", "oshh", "ashh68", 5);//Flight id has one letter inplace of an int.
            Assert.AreEqual("Last three chars of Flight ID must consist of letters only.", result);
        }


        /// <summary>
        /// This method will try creating a flight with origin being too short
        /// This will cause create flight to return an error message.
        /// </summary>
        [TestMethod]
        public void TestCreateOriginInvalidLengthShort()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.CreateFlight("men", "oshh", "asd777", 5);//get message
            Assert.AreEqual("Origin length must be exactly four.", result);
        }

        /// <summary>
        /// Testing with an invalid length Destination. An error message will 
        /// be returned from create flight stating what went wrong. 
        /// </summary>
        [TestMethod]
        public void TestCreateDestinationInvalidLengthTooShort()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.CreateFlight("men0", "osh", "asd777", 5);//get message
            Assert.AreEqual("Destination length must be exactly four.", result);//If correct message then true.
        }

        /// <summary>
        /// This method will test updating a flight with valid 
        /// inputs. We will then test if the string valid is returned from 
        /// update flight which shows the flight was updated. 
        /// </summary>
        [TestMethod]
        public void TestUpdateFlightValid()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.UpdateFlight("Meno", "jack", "abc123", 18);//get message

            Assert.AreEqual("valid", result);//If result is valid then the test passes. 
        }

        /// <summary>
        /// This test will try updating the origin with a string
        /// over a length of four. The method will pass showing the correct error message. 
        /// </summary>
        [TestMethod]
        public void TestUpdateOriginInvalidLengthTooLong()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.UpdateFlight("menoo", "oshh", "abc123", 5);//get message
            Assert.AreEqual("Origin length must be exactly four.", result); //Test passes if correct error message. 

        }


        /// <summary>
        /// This will test updating destination with a length that is too long
        /// An error message with be returned from updateflight stating what error happened
        /// This will then cause the test to pass as we got the expected message. 
        /// </summary>
        [TestMethod]
        public void TestUpdateDestinationInvalidLengthTooLong()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.UpdateFlight("meno", "oshhh", "abc123", 5);//get message
            Assert.AreEqual("Destination length must be exactly four.", result);//Test passes with correct error message. 

        }

        /// <summary>
        /// This method will test updating a flight with a non existent flight ID
        /// The test will pass with the expected error message. 
        /// </summary>
        [TestMethod]
        public void TestUpdateWithNonExistentFlight()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.UpdateFlight("meno", "oshh", "ggg000", 5);//get message
            Assert.AreEqual("Flight ID does not exist.", result);//Test will pass with expected error message. 
        }

        /// <summary>
        /// This method will test updating a flight with too many passengers
        /// It will pass by getting the expected return value. 
        /// </summary>
        [TestMethod]
        public void TestUpdateTooManyPassengers()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.UpdateFlight("meno", "oshh", "abc123", 101);//get message
            Assert.AreEqual("Number of Passengers can't be greater than 100.", result);//Will pass with expected return string. 
        }

        /// <summary>
        /// This method will Test updating a flight with not enough passengers
        /// It will pass the test by getting the expected error message from updateFlight. 
        /// </summary>
        [TestMethod]
        public void TestUpdateNotEnoughPassengers()
        {
            IController controllerTemp = ReturnController();
            string result = controllerTemp.UpdateFlight("meno", "oshh", "abc123", -1);//get message
            Assert.AreEqual("Passengers must be greater at least a positive number.", result);
        }

        /// <summary>
        /// This Method will test deleting a valid flight
        /// The Test will pass when the result returns true, which is expected.
        /// </summary>
        [TestMethod]
        public void TestDeleteFlightSuccess()
        {
            // IDatabase dataBase = new Database();
            // IController controller = new Controller(dataBase);
            IController controllerTemp = ReturnController();
            Boolean result = controllerTemp.DeleteFlight("asd771");//get message

            Assert.IsTrue(result);//Test will pass expecting true to return. 

        }


        /// <summary>
        /// This method will test deleting a flight with an non existent flight ID.
        /// The test will pass by getting the expected return value of false.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithNonExistentFlight()
        {
            IController controllerTemp = ReturnController();
            Boolean result = controllerTemp.DeleteFlight("ajk090");//get message

            Assert.IsFalse(result);
        }

        /// <summary>
        /// This method will Test deleting a flight with an empty string. 
        /// The test will pass by getting the expected return value of false.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithEmptyID()
        {
            IController controllerTemp = ReturnController();
            Boolean result = controllerTemp.DeleteFlight("");//get message

            Assert.IsFalse(result);//Expecting result to be false. 
        }

        /// <summary>
        /// This method will test The displaying of flights by 
        /// adding two new flights to the database
        /// and then we will test it by retrieving those exact flights.
        /// We will pass the test as the sent in flights will 
        /// be retrieved and then compared to what we are expecting. 
        /// </summary>
        [TestMethod]
        public void TestDisplay()
        {
            List<Flight> flights = new List<Flight>();

            IController controllerTemp = ReturnController();


            Flight temp1 = new Flight("mari", "oshh", "asd888", 5);//Temp flight objects
            Flight temp2 = new Flight("meno", "mari", "asd453", 10);

            controllerTemp.CreateFlight("mari", "oshh", "asd888", 5);//Flights added to database.
            controllerTemp.CreateFlight("meno", "mari", "asd453", 10);


            Assert.AreEqual(temp1, controllerTemp.RetrieveFlight("asd888"));//Grabbing flights from database. and then checking if they equal what we expected. 
            Assert.AreEqual(temp2, controllerTemp.RetrieveFlight("asd453"));

            controllerTemp.DeleteFlight("asd888");//Delete flights. 
            controllerTemp.DeleteFlight("asd453");




        }
    }
}
