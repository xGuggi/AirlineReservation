using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    public class clsFlight
    {
        /// <summary>
        /// FlightID property
        /// </summary>
        public string sFlightID { get; set; }
        /// <summary>
        /// Flight Number property
        /// </summary>
        public string sFlightNumber { get; set; }
        /// <summary>
        /// AircraftType property
        /// </summary>
        public string sAircraftType { get; set; }

        /// <summary>
        /// Method overrides the ToString() method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return sFlightID + " " + sFlightNumber + " " + sAircraftType;
        }
    }
}
