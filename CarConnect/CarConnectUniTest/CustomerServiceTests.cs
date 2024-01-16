using System.Data;
using System;
using System.Data.SqlClient;
using NUnit.Framework;
using CarConnect.DAO;
using CarConnect.Entity;


namespace CarConnectUniTest
{
    [TestFixture]
    internal class CustomerServiceTests
    {
        private ICustomerService _customerService;
        [SetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", String.Format("{0}\\App.config", AppDomain.CurrentDomain.BaseDirectory));
            _customerService = new CustomerService();
        }

        [Test]
        public void UpdateCustomerTest()
        {
            Customer updateCustomer = new Customer("Rohan", "Jami", "murali_mukesh@gmail.com", "7032530259", "rohan_jami", "789 Pine St, Villageton", "rjami@86", Convert.ToDateTime("2023-03-20"),3);
            bool res = _customerService.UpdateCustomer(updateCustomer);
            Assert.IsTrue(res);
        }
        [Test]
        public void AuthenticationCustomerTest()
        {
            bool res1 = _customerService.Authenticate("username","password");
            Assert.IsFalse(res1);
        }
    }
}
