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
    public class clsFlightManager
    {
        /// <summary>
        /// This Method gets the Flights from the SQL Class
        /// </summary>
        /// <returns></returns>
        public static List<clsFlight> GetFlights()
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRet = 0;
                List<clsFlight> flights = new List<clsFlight>();

                string sSQL;
                sSQL = clsSQL.GetFlights();

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsFlight flight = new clsFlight();
                    flight.sFlightID = dr[0].ToString();
                    flight.sFlightNumber = dr[1].ToString();
                    flight.sAircraftType = dr[2].ToString();
                    flights.Add(flight);
                }

                return flights;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

    }
}
