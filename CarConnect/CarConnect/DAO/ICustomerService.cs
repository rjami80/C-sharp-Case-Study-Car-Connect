using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;

namespace CarConnect.DAO
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        void GetCustomerById(int customerId);
        void GetCustomerByName(string userName);
        bool RegisterCustomer(Customer customerData);
        bool UpdateCustomer(Customer customerData);
        string DeleteCustomer(int customerId);
        bool Authenticate(string userName, string password);

    }
}
