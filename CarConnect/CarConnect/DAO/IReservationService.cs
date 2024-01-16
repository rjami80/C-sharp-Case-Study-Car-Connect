using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;

namespace CarConnect.DAO
{
    internal interface IReservationService
    {
        List<Reservation> GetAllReservation();
        void GetReservationById(int reservationId);
        void GetReservationByCustomerId(int customerId);
        bool CreateReservation(Reservation reservationData);
        bool UpdateReservation(Reservation reservationData);
        string CancelReservation(int reservationId);
        decimal CalculateTotalCost(DateTime startDate, DateTime endDate, int vehicleId);

    }
}
