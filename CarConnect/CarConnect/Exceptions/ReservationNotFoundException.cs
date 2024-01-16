using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    public class ReservationNotFoundException : Exception
    {
        public ReservationNotFoundException() : base("Reservation not found.")
        {
        }
        public ReservationNotFoundException(string message) : base(message)
        {
        }
    }
}