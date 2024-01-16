using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Entity
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime JoinDate { get; set; }

        public Admin() { }

        public Admin(int adminID, string firstName, string lastName, string email, string phoneNumber, string userName, string password, string role, DateTime joinDate)
        {
            AdminID = adminID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            UserName = userName;
            Password = password;
            Role = role;
            JoinDate = joinDate;
        }

        public override string ToString()
        {
            return $"AdminID \t: \t{AdminID}\nFirstName \t:\t{FirstName}\nLastName \t:\t{LastName}\nEmail \t\t:\t{Email}\nPhoneNumber \t:\t{PhoneNumber}\nUserName\t:\t{UserName}\nPassword\t:\t{Password}\nRole\t\t:\t{Role}\nJoinDate\t:\t{JoinDate}";
        }
    }
}
