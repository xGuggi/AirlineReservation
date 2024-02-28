using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    public class clsSQL
    {
        /// <summary>
        /// Method Gets Flight Info
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFlights()
        {
            try
            {
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method Gets Passenger Info
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassengers(string sFlightID)
        {
            try
            {
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                                "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
                                "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
                                "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
                                "FLIGHT.FLIGHT_ID = " + sFlightID;
                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method Updates Seat Number
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateSeatNumber(string sFlightID, string sPassengerID, string sSeatNumber)
        {
            try
            {
                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
                                "SET Seat_Number = '" + sSeatNumber + "' " +
                                "WHERE FLIGHT_ID = " + sFlightID + "AND PASSENGER_ID =" + sPassengerID;
                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method Inserts Passenger
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertPassenger(string sFirstName, string sLastName)
        {
            try
            {
                string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('" + sFirstName + "','" + sLastName + "')";
                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Insert Into the link table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="iSeatNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertFlightPassengerLink(string sFlightID, string sPassengerID, string sSeatNumber)
        {
            try
            {
                string sSQL = "INSERT INTO FLIGHT_PASSENGER_LINK(Flight_ID, Passenger_ID, Seat_Number) " +
                                "VALUES(" + sFlightID + "," + sPassengerID + "," + sSeatNumber + ")";
                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Deleting the link
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteLink(string sFlightID, string sPassengerID)
        {
            try
            {
                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
                                "WHERE FLIGHT_ID = " + sFlightID + "AND " +
                                "PASSENGER_ID = " + sPassengerID;
                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Delete the Passenger Method
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeletePassenger(string sPassengerID)
        {
            try
            {
                string sSQL = "Delete FROM PASSENGER " +
                        "WHERE PASSENGER_ID = " + sPassengerID + "";
                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method gets passengerID
        /// </summary>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassengerID(string sFirstName, string sLastName)
        {
            try
            {
                string sSQL = "SELECT Passenger_ID from Passenger where First_Name = '" + sFirstName + "' AND Last_Name = '" + sLastName + "'";
                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
