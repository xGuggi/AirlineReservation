using Assignment6AirlineReservation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    public class clsPassengerManager
    {
        /// <summary>
        /// this Method Gets the Passengers from the SQL class
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public static List<clsPassenger> GetPassengers(string sFlightID)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRet = 0;
                List<clsPassenger> passengers = new List<clsPassenger>();

                string sSQL;
                sSQL = clsSQL.GetPassengers(sFlightID);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsPassenger passenger = new clsPassenger();
                    passenger.sPassengerID = dr[0].ToString();
                    passenger.sFirstName = dr[1].ToString();
                    passenger.sLastName = dr[2].ToString();
                    passenger.sSeatNumber = dr[3].ToString();
                    passengers.Add(passenger);
                }

                return passengers;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// this method inserts new passengers into the database
        /// </summary>
        /// <param name="sFirstName"></param>
        /// <param name="sLastName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static void InsertNewPassenger(string sFirstName, string sLastName, string sFlightID, string sSeatNumber)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                string sSQL;

                sSQL = clsSQL.InsertPassenger(sFirstName, sLastName);
                db.ExecuteNonQuery(sSQL);

                sSQL = clsSQL.GetPassengerID(sFirstName, sLastName);
                string sPassengerID = db.ExecuteScalarSQL(sSQL);

                sSQL = clsSQL.InsertFlightPassengerLink(sFlightID, sPassengerID, sSeatNumber);
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }
        /// <summary>
        /// This method updates the seat for the passenger.
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sSeatNumber"></param>
        /// <param name="sFirstName"></param>
        /// <param name="sLastName"></param>
        /// <exception cref="Exception"></exception>
        public static void UpdateSeat(string sFlightID, string sSeatNumber, string sFirstName, string sLastName)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                string sSQL;

                sSQL = clsSQL.GetPassengerID(sFirstName, sLastName);
                string sPassengerID = db.ExecuteScalarSQL(sSQL);

                sSQL = clsSQL.UpdateSeatNumber(sFlightID, sPassengerID, sSeatNumber);
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// this method deletes a passenger from the database
        /// </summary>
        public static void DeletePassenger(string sFlightID, string sFirstName, string sLastName)
        {
            clsDataAccess db = new clsDataAccess();
            string sSQL;

            sSQL = clsSQL.GetPassengerID(sFirstName, sLastName);
            string sPassengerID = db.ExecuteScalarSQL(sSQL);

            sSQL = clsSQL.DeleteLink(sFlightID, sPassengerID);
            db.ExecuteNonQuery(sSQL);

            sSQL = clsSQL.DeletePassenger(sPassengerID);
            db.ExecuteNonQuery(sSQL);
        }
        /// <summary>
        /// This method inserts new passenger in the link table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="iSeatNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /*public static List<clsPassenger> InsertNewPassengerLink(string sFlightID, string sPassengerID, string sSeatNumber)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRet = 0;
                List<clsPassenger> passengers = new List<clsPassenger>();

                string sSQL;
                sSQL = clsSQL.InsertFlightPassengerLink(sFlightID, sPassengerID, sSeatNumber);

                iRet = db.ExecuteNonQuery(sSQL);

                return passengers;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }*/
        public static List<clsPassenger> GetPassengerID(string sFirstName, string sLastName)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRet = 0;
                List<clsPassenger> passengers = new List<clsPassenger>();

                string sSQL;
                sSQL = clsSQL.GetPassengerID(sFirstName, sLastName);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsPassenger passenger = new clsPassenger();
                    passenger.sPassengerID = dr[0].ToString();
                    passengers.Add(passenger);
                }

                return passengers;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
