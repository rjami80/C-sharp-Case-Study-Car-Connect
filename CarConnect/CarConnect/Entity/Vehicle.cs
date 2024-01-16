using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Entity
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string RegistrationNumber { get; set; }
        public bool Availability { get; set; }
        public double DailyRate { get; set; }

        public Vehicle() { }
        public Vehicle(string model, string make, int year, string color, string registrationNumber, bool availability, double dailyRate,[Optional] int vehicleID)
        {
            Model = model;
            Make = make;
            Year = year;
            Color = color;
            RegistrationNumber = registrationNumber;
            Availability = availability;
            DailyRate = dailyRate;
            VehicleID = vehicleID;
        }
        public override string ToString()
        {
            return $"VehicleID \t: \t{VehicleID}\nModel \t\t:\t{Model}\nMake \t\t:\t{Make}\nYear \t\t:\t{Year}\nColor \t\t:\t{Color}\nRegistrationNumber:\t{RegistrationNumber}\nAvailability\t:\t{Availability}\nDailyRate\t:\t{DailyRate}";
        }
    }
}
