using System.Data;
using System;
using System.Data.SqlClient;
using NUnit.Framework;
using CarConnect.DAO;
using CarConnect.Entity;


namespace CarConnectUniTest
{
    [TestFixture]
    internal class VehicleServiceTests
    {
        private IVehicleService _vehicleService;
        [SetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", String.Format("{0}\\App.config", AppDomain.CurrentDomain.BaseDirectory));
            _vehicleService = new VehicleService();
        }

        [Test]
        public void GetAvailableVehiclesTest()
        {
            Assert.IsNotNull(_vehicleService.GetAvailableVehicles());
        }
        [Test]
        public void AddVehicleTest()
        {
            Vehicle addVehicle = new Vehicle("Sonet", "Kia", 2018, "White", "TS07GZI57", false, 1000);
            bool res = _vehicleService.AddVehicle(addVehicle);
            Assert.IsTrue(res);
        }
        [Test]
        public void GetAllVehiclesTest()
        {
            Assert.IsNotNull(_vehicleService.GetAllVehicles());
        }
        [Test]
        
        public void UpdateVehicleTest()
        {
            Vehicle updateVehicle = new Vehicle("Sonet", "Kia", 2018, "White", "ABC666666",true, 3060,4);
            bool res1 = _vehicleService.UpdateVehicle(updateVehicle);
            Assert.IsTrue(res1);
        }

    }
}