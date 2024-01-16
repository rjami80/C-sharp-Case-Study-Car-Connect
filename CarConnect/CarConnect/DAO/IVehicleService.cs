using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;

namespace CarConnect.DAO
{
    public interface IVehicleService
    {
        List<Vehicle> GetAllVehicles();
        void GetVehicleById(int vehicleId);
        List <Vehicle> GetAvailableVehicles();
        bool AddVehicle(Vehicle vehicleData);
        bool UpdateVehicle(Vehicle vehicleData);
        string RemoveVehicle(int vehicleId);
    }
}
