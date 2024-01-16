using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Entity
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Customer() { }


        public Customer(string firstName, string lastName, string email, string phoneNumber, string userName, string address, string password,DateTime registrationDate, [Optional] int customerID)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            UserName = userName;
            Address = address;
            Password = password;
            RegistrationDate = registrationDate;
            CustomerID = customerID;
        }

        public override string ToString()
        {
            return $"CustomerID \t: \t{CustomerID}\nFirstName \t:\t{FirstName}\nLastName \t:\t{LastName}\nEmail \t\t:\t{Email}\nPhoneNumber \t:\t{PhoneNumber}\nUserName\t:\t{UserName}\nPassword\t:\t{Password}\nRegistrationDate:\t{RegistrationDate}";

        }
    }
}
