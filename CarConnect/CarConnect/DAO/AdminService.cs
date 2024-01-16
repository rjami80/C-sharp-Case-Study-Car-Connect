using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;
using CarConnect.Util;
using CarConnect.Exceptions;
using System.Security.Authentication;
using System.Threading;


namespace CarConnect.DAO
{
    public class AdminService : IAdminService
    {
        SqlConnection conn;
        SqlCommand cmd = null;

        public AdminService()
        {
            cmd = new SqlCommand();
        }
        /// <summary>
        /// This is a DeleteAdmin method which deletes the admin details from the Admin table.
        /// </summary>  
        public string  DeleteAdmin(int adminId)
        {
            string response = null;
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    try
                    {
                        cmd.CommandText = "DELETE FROM Admin WHERE AdminID = @AdminId";
                        cmd.Parameters.AddWithValue("@AdminId", adminId);
                        cmd.Connection = conn;
                        conn.Open();
                        int removeAdminId = cmd.ExecuteNonQuery();
                        if (removeAdminId > 0)
                        {
                            response = $"Data of admin having ID : {adminId} has been deleted succefully.";
                        }
                        else
                        {
                            throw new AdminNotFoundException($"Customer by ID : {adminId} is not present.");
                        }
                    }
                    catch (SqlException se)
                    {
                        if (se.Class == 11)
                        {
                            throw new DatabaseConnectionException("Error connecting to Database.\nName of the database is not present in the sql server.");
                        }
                        else if (se.Class == 20)
                        {
                            throw new DatabaseConnectionException("Incorrect Server Name.");
                        }
                        else
                        {
                            Console.WriteLine("An error occured : " + se.Message);
                            Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                    }
                }
            }

            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (AdminNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
            }
            return response;
        }
        /// <summary>
        /// This is a GetAdminById method which retrieves the admin details from the Admin table using Admin Id.
        /// </summary>  
        public void GetAdminById(int adminId)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"select * from Admin where adminid={adminId}", conn);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            Console.Clear();
                            Console.WriteLine($"\t\t\t\tDetails of Admin ID : {adminId}\n");
                            while (dr.Read())
                            {
                                DateTime dateValue = (DateTime)dr[8];
                                Console.WriteLine($"  AdminID     = {dr[0]}");
                                Console.WriteLine($"  FirstName   = {dr[1]}");
                                Console.WriteLine($"  LastName    = {dr[2]}");
                                Console.WriteLine($"  Email       = {dr[3]}");
                                Console.WriteLine($"  PhoneNumber = {dr[4]}");
                                Console.WriteLine($"  Password    = {dr[6]}");
                                Console.WriteLine($"  Role        = {dr[7]}");
                                Console.WriteLine($"  JoinDate    = {dateValue.ToShortDateString()}");
                            }
                            Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                        else
                        {
                            throw new AdminNotFoundException($"No admin is present with ID : {adminId}.");
                        }
                    }
                    catch (SqlException se)
                    {
                        if (se.Class == 11)
                        {
                            throw new DatabaseConnectionException("Error connecting to Database.\nName of the database is not present in the sql server.");
                        }
                        else if (se.Class == 20)
                        {
                            throw new DatabaseConnectionException("Incorrect Server Name.");
                        }
                        else
                        {
                            Console.WriteLine("An error occured : " + se.Message);
                            Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (AdminNotFoundException ex)
            { 
            Console.WriteLine(ex.Message);
            Console.Write("Returning to previous menu.");
            Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a GetAdminByUserName method which retrieves the admin details from the Admin table using Username.
        /// </summary>  
        public void GetAdminByUserName(string userName)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"select * from Admin where Username='{userName}'", conn);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            Console.Clear();
                            Console.WriteLine($"\t\t\t\tDetails of Admin : {userName}\n");
                            while (dr.Read())
                            {
                                DateTime dateValue = (DateTime)dr[8];
                                Console.WriteLine($"  AdminID     = {dr[0]}");
                                Console.WriteLine($"  FirstName   = {dr[1]}");
                                Console.WriteLine($"  LastName    = {dr[2]}");
                                Console.WriteLine($"  Email       = {dr[3]}");
                                Console.WriteLine($"  PhoneNumber = {dr[4]}");
                                Console.WriteLine($"  Password    = {dr[6]}");
                                Console.WriteLine($"  Role        = {dr[7]}");
                                Console.WriteLine($"  JoinDate    = {dateValue.ToShortDateString()}");
                            }
                            Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                        else
                        {
                            throw new AdminNotFoundException($"No admin is present with username : {userName}.");
                        }
                    }
                    catch (SqlException se)
                    {
                        if (se.Class == 11)
                        {
                            throw new DatabaseConnectionException("Error connecting to Database.\nName of the database is not present in the sql server.");
                        }
                        else if (se.Class == 20)
                        {
                            throw new DatabaseConnectionException("Incorrect Server Name.");
                        }
                        else
                        {
                            Console.WriteLine("An error occured : " + se.Message);
                            Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (AdminNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a GetAllAdmins method which retrieves all the admin details from the Admin table.
        /// </summary>  
        public List<Admin> GetAllAdmins()
        {
            try
            {
                List<Admin> admins = new List<Admin>();
                using (conn = DBConnUtil.GetConnection())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from Admin";
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Admin admin = new Admin()
                                {
                                    AdminID = Convert.ToInt32(dr["AdminID"]),
                                    FirstName = dr["FirstName"].ToString(),
                                    LastName = dr["LastName"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    PhoneNumber = dr["PhoneNumber"].ToString(),
                                    UserName = dr["Username"].ToString(),
                                    Password = dr["Password"].ToString(),
                                    Role = dr["Role"].ToString(),
                                    JoinDate = Convert.ToDateTime(dr["JoinDate"])
                                };
                                admins.Add(admin);
                            }
                            dr.Close();
                        }
                        else
                        {
                            throw new AdminNotFoundException("Admin Table is empty.");
                        }
                    }
                    catch (SqlException se)
                    {
                        if (se.Class == 11)
                        {
                            throw new DatabaseConnectionException("Error connecting to Database.\nName of the database is not present in the sql server.");
                        }
                        else if (se.Class == 20)
                        {
                            throw new DatabaseConnectionException("Incorrect Server Name.");
                        }
                        else
                        {
                            Console.WriteLine("An error occured : " + se.Message);
                            Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                    }
                    return admins;
                }
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (AdminNotFoundException anfe)
            {
                Console.WriteLine(anfe.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            finally
            {
                conn.Close();
            }
            return new List<Admin>();
        }
        /// <summary>
        /// This is a RegisterAdmin method which registers the new admin details into the Admin table.
        /// </summary>  
        public bool RegisterAdmin(Admin adminData)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO admin OUTPUT INSERTED.adminid VALUES (@FirstName,@LastName,@Email,@PhoneNumber,@UserName,@Password,@Role,@JoinDate);", conn);
                    cmd.Parameters.AddWithValue("@FirstName", adminData.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", adminData.LastName);
                    cmd.Parameters.AddWithValue("@Email", adminData.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", adminData.PhoneNumber);
                    cmd.Parameters.AddWithValue("@UserName", adminData.UserName);
                    cmd.Parameters.AddWithValue("@Password", adminData.Password);
                    cmd.Parameters.AddWithValue("@Role", adminData.Role);
                    cmd.Parameters.AddWithValue("@JoinDate", adminData.JoinDate);
                    try
                    {
                        cmd.Connection = conn;
                        try
                        {
                            conn.Open();
                            object NewId = cmd.ExecuteScalar();
                            if (NewId != null)
                            {
                                Console.WriteLine("Admin Registration Successfull with ID : " + NewId);
                                return true;
                            }
                            else
                            {
                                throw new AdminNotFoundException("Error Displayed");
                            }
                        }
                        catch (SqlException se)
                        {
                            if (se.Class == 14)
                            {
                                Console.WriteLine("Admin Username already exists. Please enter another username");
                                Console.Write("\nReturning to previous menu...");
                                Thread.Sleep(3000);
                            }
                            else throw se;
                        }
                    }
                    catch (SqlException se)
                    {
                        if (se.Class == 11)
                        {
                            throw new DatabaseConnectionException("Error connecting to Database.\nName of the database is not present in the sql server.");
                        }
                        else if (se.Class == 20)
                        {
                            throw new DatabaseConnectionException("Incorrect Server Name.");
                        }
                        else
                        {
                            Console.WriteLine("An error occured : " + se.Message);
                            Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Console.Write("An error occured: " + ex.Message);
                Thread.Sleep(2000);
            }
            return false;
        }
        /// <summary>
        /// This is a UpdateAdmin method which updates the admin details from the Admin table.
        /// </summary>  
        public bool UpdateAdmin(Admin adminData)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "UPDATE Admin SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, UserName = @UserName WHERE AdminId = @AdminId";
                    cmd.Parameters.AddWithValue("@FirstName", adminData.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", adminData.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", adminData.PhoneNumber);
                    cmd.Parameters.AddWithValue("@UserName", adminData.UserName);
                    cmd.Parameters.AddWithValue("@AdminId", adminData.AdminID);
                    cmd.Connection = conn;
                    try
                    {
                        conn.Open();
                        int updateStatus = cmd.ExecuteNonQuery();
                        return updateStatus > 0;
                    }
                    catch (SqlException se)
                    {
                        if (se.Class == 11)
                        {
                            throw new DatabaseConnectionException("Error connecting to Database.\nName of the database is not present in the sql server.");
                        }
                        else if (se.Class == 20)
                        {
                            throw new DatabaseConnectionException("Incorrect Server Name.");
                        }
                        else
                        {
                            Console.WriteLine("An error occured : " + se.Message);
                            Console.Write("\n\n\n\nPress any key to return to previous menu...");
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (AdminNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(3000);
            }
            return false;
        }
        /// <summary>
        /// This is a Authenticate method which authenticates the admin details from the Admin table during Login.
        /// </summary>  
        public bool Authenticate(string userName, string password)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"select * from Admin where Username='{userName}'", conn);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string stored_password = dr["Password"].ToString();
                                if (stored_password == password)
                                {
                                    Console.WriteLine("Admin authentication successful! \nLogged IN");
                                    Thread.Sleep(2000);
                                    return true;
                                }
                            }
                        }
                        else throw new AuthenticationInvalidException("Invalid username or password");
                    }
                    catch (SqlException se)
                    {
                        if (se.Class == 11)
                        {
                            throw new DatabaseConnectionException("Error connecting to Database.\nName of the database is not present in the sql server.");
                        }
                        else if (se.Class == 20)
                        {
                            throw new DatabaseConnectionException("Incorrect Server Name.");
                        }
                        else
                        {
                            Console.WriteLine("An error occured : " + se.Message);
                            Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                    }
                }
                return false;
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
                return false;
            }
            catch (AuthenticationInvalidException ae)
            {
                Console.WriteLine(ae.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authentication Error: {ex.Message}");
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
                return false;
            }
        }
    }
}
