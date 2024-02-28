using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    public class clsPassenger
    {
        /// <summary>
        /// PassengerID Property
        /// </summary>
        public string sPassengerID { get; set; }
        /// <summary>
        /// Passenger First Name Property
        /// </summary>
        public string sFirstName { get; set; }
        /// <summary>
        /// Passenger Last Name Property
        /// </summary>
        public string sLastName { get; set; }
        /// <summary>
        /// Seat Number Property
        /// </summary>
        public string sSeatNumber {  get; set; }
        /// <summary>
        /// FlightID Property
        /// </summary>
        public string sFlightID { get; set; }   

        /// <summary>
        /// Method overrides the ToString() method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return sSeatNumber + " " + sFirstName + " " + sLastName;
        }
    }
}
