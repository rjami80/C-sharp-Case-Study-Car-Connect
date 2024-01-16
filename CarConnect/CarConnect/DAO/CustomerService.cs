using System;
using System.Collections.Generic;
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
    public class CustomerService : ICustomerService
    {
        SqlConnection conn;
        SqlCommand cmd = null;

        public CustomerService()
        {
            cmd = new SqlCommand();
        }

        /// <summary>
        /// This is a UpdateCustomer method which updates the Customer details in the Customer table.
        /// </summary>  
        public bool UpdateCustomer(Customer customerData)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {

                    cmd.CommandText = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, UserName = @UserName, Password = @Password WHERE CustomerId = @CustomerId";
                    cmd.Parameters.AddWithValue("@FirstName", customerData.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customerData.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customerData.PhoneNumber);
                    cmd.Parameters.AddWithValue("@UserName", customerData.UserName);
                    cmd.Parameters.AddWithValue("@Password", customerData.Password);
                    cmd.Parameters.AddWithValue("@CustomerId", customerData.CustomerID);
                    cmd.Connection = conn;
                    try
                    {
                        conn.Open();
                        int updatedRows = cmd.ExecuteNonQuery();
                        if (updatedRows > 0)
                        {
                            return true;
                        }
                        else
                        {
                            throw new CustomerNotFoundException("Invalid Username. Please enter a valid username to update the details.");
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
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
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
            catch (CustomerNotFoundException e)
            {
                Console.Write(e.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            return false;
        }
        /// <summary>
        /// This is a DeleteCustomer method which deletes the Customer details in the Customer table.
        /// </summary>  
        public string DeleteCustomer(int customerId)
        {
            string response = null;
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    try
                    {
                        cmd.CommandText = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);
                        cmd.Connection = conn;
                        conn.Open();
                        int removeCustomerId = cmd.ExecuteNonQuery();
                        if (removeCustomerId > 0)
                        {
                            response = $"Data of customer having ID : {customerId} has been deleted succefully.";
                        }
                        else
                        {
                            throw new CustomerNotFoundException($"Customer by ID : {customerId} is not present.");
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
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
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
            catch (CustomerNotFoundException e)
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
            return response;
        }
        /// <summary>
        /// This is a GetAllCustomers method which retrieves all the Customer details in the Customer table.
        /// </summary>  
        public List<Customer> GetAllCustomers()
        {
        try
          {
            List<Customer> customers = new List<Customer>();

                using (conn = DBConnUtil.GetConnection())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from Customer";
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                customers.Add(new Customer() { CustomerID = (int)dr[0], FirstName = dr[1].ToString(), LastName = dr[2].ToString(), Email = dr[3].ToString(), PhoneNumber = dr[4].ToString(), Address = dr[5].ToString(), UserName = dr[6].ToString(), Password = dr[7].ToString(), RegistrationDate = (DateTime)dr[8] });
                            }
                            dr.Close();
                        }
                        else
                        {
                            throw new CustomerNotFoundException("Customer Table is empty.");
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
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                    }
                    return customers;
                }
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (CustomerNotFoundException cnfe)
            {
                Console.WriteLine(cnfe.Message);
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
            return new List<Customer>();
        }
        /// <summary>
        /// This is a GetCustomerById method which retrieves the Customer details in the Customer table using Customer ID.
        /// </summary>  
        public void GetCustomerById(int customerId)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"select * from Customer where customerid={customerId}", conn);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            Console.Clear();
                            Console.WriteLine($"\t\t\t\tDetails of Customer ID : {customerId}\n");
                            while (dr.Read())
                            {
                                DateTime dateValue = (DateTime)dr[8];
                                Console.WriteLine($"  FirstName         = {dr[1]}");
                                Console.WriteLine($"  LastName          = {dr[2]}");
                                Console.WriteLine($"  Email             = {dr[3]}");
                                Console.WriteLine($"  PhoneNumber       = {dr[4]}");
                                Console.WriteLine($"  UserName          = {dr[5]}");
                                Console.WriteLine($"  Address           = {dr[6]}");
                                Console.WriteLine($"  Password          = {dr[7]}");
                                Console.WriteLine($"  RegistrationDate  = {dateValue.ToShortDateString()}");
                            }
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                        else
                        {
                            throw new CustomerNotFoundException($"No customer is present with ID : {customerId}.");
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
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
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
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a GetCustomerByName method which retrieves the Customer details in the Customer table using Username.
        /// </summary>  
        public void GetCustomerByName(string userName)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Customer where Username='{userName}'", conn);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            Console.Clear();
                            Console.WriteLine($"\t\t\t\tDetails of User : {userName}\n");
                            while (dr.Read())
                            {
                                DateTime dateValue = (DateTime)dr[8];
                                Console.WriteLine($"  FirstName         = {dr[1]}");
                                Console.WriteLine($"  LastName          = {dr[2]}");
                                Console.WriteLine($"  Email             = {dr[3]}");
                                Console.WriteLine($"  PhoneNumber       = {dr[4]}");
                                Console.WriteLine($"  UserName          = {dr[5]}");
                                Console.WriteLine($"  Address           = {dr[6]}");
                                Console.WriteLine($"  Password          = {dr[7]}");
                                Console.WriteLine($"  RegistrationDate  = {dateValue.ToShortDateString()}");
                            }
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                        else
                        {
                            throw new CustomerNotFoundException($"No customer is present with username : {userName}.");
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
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
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
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a RegisterCustomer method which registers new Customer details in the Customer table.
        /// </summary>  
        public bool RegisterCustomer(Customer customerData)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Customer OUTPUT INSERTED.CustomerID VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, @UserName, @Password, @RegistrationDate);", conn);
                    cmd.Parameters.AddWithValue("@FirstName", customerData.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customerData.LastName);
                    cmd.Parameters.AddWithValue("@Email", customerData.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customerData.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", customerData.Address);
                    cmd.Parameters.AddWithValue("@UserName", customerData.UserName);
                    cmd.Parameters.AddWithValue("@Password", customerData.Password);
                    cmd.Parameters.AddWithValue("@RegistrationDate", customerData.RegistrationDate);
                    try
                    {
                        cmd.Connection = conn;
                        try
                        {
                            conn.Open();
                            object NewId = cmd.ExecuteScalar();
                            if (NewId != null)
                            {
                                Console.WriteLine("Customer Registered Successfully with ID: " + NewId);
                                return true;
                            }
                            else
                            {
                                throw new CustomerNotFoundException("Error Displayed");
                            }
                        }
                        catch (SqlException se)
                        {
                            if (se.Class == 14)
                            {
                                Console.WriteLine("Customer Username already exists. Please enter another username");
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
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(4000);
            }
            catch (Exception ex)
            {
                Console.Write("An error occured: " + ex.Message);
                Thread.Sleep(2000);
            }
            return false;
        }
        /// <summary>
        /// This is a Authenticate method which authenticates the Customer details in the Customer table during Login.
        /// </summary>  
        public bool Authenticate(string userName, string password)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Customer where Username='{userName}'", conn);
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
                                    Console.Write("Customer authentication successful!. \nLogged IN.");
                                    Thread.Sleep(2000);
                                    return true;
                                }
                                else throw new AuthenticationException("Invalid password.");
                            }
                        }
                        else throw new AuthenticationException("Invalid username.");
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
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
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
                Thread.Sleep(2000);
                return false;
            }
            catch (AuthenticationException ae)
            {
                Console.WriteLine(ae.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
                return false;
            }
            catch (Exception ex)
            {
                Console.Write($"Authentication Error: {ex.Message}");
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
                return false;
            }
        }
    }
}


