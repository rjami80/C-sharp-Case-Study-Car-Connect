using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Entity
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int CustomerID { get; set; }
        public int VehicleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public string Status { get; set; }

        public Reservation() { }
        public Reservation(int reservationID, int customerID, int vehicleID, DateTime startDate, DateTime endDate, decimal totalCost, string status)
        {
            ReservationID = reservationID;
            CustomerID = customerID;
            VehicleID = vehicleID;
            StartDate = startDate;
            EndDate = endDate;
            TotalCost = totalCost;
            Status = status;
        }

       
        public override string ToString()
        {
            return $"VehicleID \t: \t{VehicleID}\nReservationID \t:\t{ReservationID}\nCustomerID \t:\t{CustomerID}\nStartDate \t:\t{StartDate}\nEndDate \t:\t{EndDate}\nTotalCost\t:\t{TotalCost}\nStatus\t\t:\t{Status}";

        }

    }
}
