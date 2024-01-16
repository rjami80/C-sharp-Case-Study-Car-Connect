using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    public class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException() : base("Reservation not found.")
        {
        }
        public VehicleNotFoundException(string message) : base(message)
        {
        }
    }
}