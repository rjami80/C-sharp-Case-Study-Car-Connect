using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;

namespace CarConnect.DAO
{
    internal interface IAdminService
    {
        bool Authenticate(string userName, string password);
        List<Admin> GetAllAdmins();
        void GetAdminById(int adminId);
        void GetAdminByUserName(string userName);
        bool RegisterAdmin(Admin adminData);
        bool UpdateAdmin(Admin adminData);
        string DeleteAdmin(int adminId);

    }
}
