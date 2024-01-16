using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CarConnect.DAO;
using CarConnect.Entity;
using CarConnect.Exceptions;
using CarConnect.Util;

namespace CarConnect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t\tCar Connect Home Page");
                    Console.WriteLine("1. Customer");
                    Console.WriteLine("2. Admin");
                    Console.WriteLine("3. Vehicle");
                    Console.WriteLine("4. Reservation");
                    Console.WriteLine("0. exit");
                    Console.Write("\nPlease enter your choice : ");
                    string choice = Console.ReadLine();
                    Console.WriteLine();

                    switch (choice)
                    {
                        case "1":
                            CustomerHomeScreen();
                            break;

                        case "2":
                            AdminHomeScreen();
                            break;

                        case "3":
                            VehicleMenu();
                            break;

                        case "4":
                            ReservationMenu();
                            break;

                        case "0":
                            Exit();
                            break;

                        default:
                            Console.Write("Invalid choice. Please enter a number as displayed.");
                            Thread.Sleep(2000);
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a AuthenticateCustomer method which displays the Login page for Customer.
        /// </summary>
        public static void AuthenticateCustomer(ICustomerService customerService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tCustomer Login Page");
                Console.Write("Enter Username: ");
                string uname = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Enter Password: ");
                string pwd = Console.ReadLine();
                Console.WriteLine();
                bool AuthToken = customerService.Authenticate(uname, pwd);
                if (AuthToken)
                {
                    CustomerMenu();
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a CustomerHomeScreen method which displays the customer home screen details.
        /// </summary>  
        public static void CustomerHomeScreen()
        {
            try
            {
                bool customerHomeScreenReturnToMainMenu = false;
                while (!customerHomeScreenReturnToMainMenu)
                {
                    ICustomerService customerService;
                    customerService = new CustomerService();
                    Console.Clear();
                    Console.WriteLine("\t\t\t\tCustomer Home Screen");
                    Console.WriteLine("1. Login");
                    Console.WriteLine("2. Register");
                    Console.WriteLine("3. Return to Main Menu");
                    Console.WriteLine("0. Exit");
                    Console.Write("\nPlease enter your choice : ");

                    string customerHomeScreenChoice = Console.ReadLine();
                    Console.WriteLine();

                    switch (customerHomeScreenChoice)
                    {
                        case "1":
                            AuthenticateCustomer(customerService);
                            break;

                        case "2":
                            RegisterCustomer(customerService);
                            break;

                        case "3":
                            customerHomeScreenReturnToMainMenu = true;
                            break;

                        case "0":
                            Exit();
                            break;

                        default:
                            Console.Write("Invalid choice. Please enter a number as displayed.");
                            Thread.Sleep(2000);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a AdminHomeScreen method which displays the admin home screen details.
        /// </summary>  
        public static void AdminHomeScreen()
        {
            try
            {
                bool adminHomeScreenReturnToMainMenu = false;
                while (!adminHomeScreenReturnToMainMenu)
                {
                    IAdminService adminService;
                    adminService = new AdminService();
                    Console.Clear();
                    Console.WriteLine("\t\t\t\tAdmin Home Screen");
                    Console.WriteLine("1. Login");
                    Console.WriteLine("2. Register");
                    Console.WriteLine("3. Return to Main Menu");
                    Console.WriteLine("0. Exit");
                    Console.Write("\nPlease enter your choice : ");

                    string adminHomeScreenChoice = Console.ReadLine();
                    Console.WriteLine();

                    switch (adminHomeScreenChoice)
                    {
                        case "1":
                            AuthenticateAdmin(adminService);
                            break;

                        case "2":
                            RegisterAdmin(adminService);
                            break;

                        case "3":
                            adminHomeScreenReturnToMainMenu = true;
                            break;

                        case "0":
                            Exit();
                            break;

                        default:
                            Console.Write("Invalid choice. Please enter a number as displayed.");
                            Console.ReadLine();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a AuthenticateAdmin method which displays the Login page for Admin.
        /// </summary>
        public static void AuthenticateAdmin(IAdminService adminService)
        {
            try
            {
                Console.Clear();
                Console.Write("Enter Username: ");
                string uname = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Enter Password: ");
                string pwd = Console.ReadLine();
                Console.WriteLine();
                bool AuthToken = adminService.Authenticate(uname, pwd);
                if (AuthToken)
                {
                    AdminMenu();
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a CustomerMenu method which displays the menu options for Customer.
        /// </summary>
        public static void CustomerMenu()
        {
            try
            {
                ICustomerService customerService;
                customerService = new CustomerService();
                bool customerReturnToMainMenu = false;
                while (!customerReturnToMainMenu)
                {
                    Console.Clear();
                    Console.WriteLine("\t\tCustomer Menu");
                    Console.WriteLine("1. Get all customers");
                    Console.WriteLine("2. Get customer details using customer ID");
                    Console.WriteLine("3. Get customer details using customer user-name");
                    Console.WriteLine("4. Register a new customer");
                    Console.WriteLine("5. Update Customer");
                    Console.WriteLine("6. Delete an existing customer data");
                    Console.WriteLine("7. Logout");
                    Console.WriteLine("0. Exit");
                    Console.Write("\nPlease enter your choice : ");
                    string customerMenuChoice = Console.ReadLine();
                    Console.WriteLine();
                    switch (customerMenuChoice)
                    {
                        case "1":
                            GetAllCustomers(customerService);
                            break;

                        case "2":
                            GetCustomerById(customerService);
                            break;

                        case "3":
                            GetCustomerByName(customerService);
                            break;

                        case "4":
                            RegisterCustomer(customerService);
                            break;

                        case "5":
                            UpdateCustomer(customerService);
                            break;

                        case "6":
                            DeleteCustomer(customerService);
                            break;

                        case "7":
                            Console.WriteLine("Logged out successfully.");
                            customerReturnToMainMenu = true;
                            break;

                        case "0":
                            Exit();
                            break;

                        default:
                            Console.Write("Invalid choice. Please enter a number as displayed.");
                            Thread.Sleep(1500);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a GetAllCustomers method which retrieves all the Customer details in the Customer table.
        /// </summary>  
        public static void GetAllCustomers(ICustomerService customerService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tDetails of All Customers:\n");
                List<Customer> customers = customerService.GetAllCustomers();
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer);
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a GetCustomerById method which retrieves the Customer details in the Customer table using Customer ID.
        /// </summary>  
        public static void GetCustomerById(ICustomerService customerService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tCustomer Details using Customer ID\n");
                Console.Write("\t\t\t\tEnter the ID of the customer you want to retrive the data:\n ");
                int a = int.Parse(Console.ReadLine());
                Console.WriteLine();
                customerService.GetCustomerById(a);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message + " The ID should be a numeric value.");
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a GetCustomerByName method which retrieves the Customer details in the Customer table using Username.
        /// </summary>  
        public static void GetCustomerByName(ICustomerService customerService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tCustomer Details using Customer username\n");
                Console.Write("Enter the username of the customer you want to retrive the data: ");
                string customerName = Console.ReadLine();
                Console.WriteLine();
                customerService.GetCustomerByName(customerName);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a RegisterCustomer method which registers new Customer details in the Customer table.
        /// </summary>  
        public static void RegisterCustomer(ICustomerService customerService)
        {
            try
            {
                Console.Clear();
                Customer registerCustomer = new Customer();
                Console.WriteLine("\t\t\t\tNew User Regesitration");
                Console.WriteLine("Enter First Name: ");
                registerCustomer.FirstName = Console.ReadLine();
                if (Regex.IsMatch(registerCustomer.FirstName, "^[a-zA-Z ]+$"))
                {
                    Console.WriteLine("Enter Last Name: ");
                    registerCustomer.LastName = Console.ReadLine();
                    if (Regex.IsMatch(registerCustomer.LastName, "^[a-zA-Z ]+$"))
                    {
                        Console.WriteLine("Enter Phone Number: ");
                        registerCustomer.PhoneNumber = Console.ReadLine();
                        if (Regex.IsMatch(registerCustomer.PhoneNumber, @"^\d+$"))
                        {
                            if (registerCustomer.PhoneNumber.Length <= 10)
                            {
                                Console.WriteLine("Enter Address: ");
                                registerCustomer.Address = Console.ReadLine();
                                Console.WriteLine("Enter Email-id: ");
                                registerCustomer.Email = Console.ReadLine();
                                if (registerCustomer.Email.Contains("@"))
                                {
                                    Console.WriteLine("Enter Username: ");
                                    registerCustomer.UserName = Console.ReadLine();
                                    Console.WriteLine("Password");
                                    registerCustomer.Password = Console.ReadLine();
                                    registerCustomer.RegistrationDate = DateTime.Now;
                                    Console.WriteLine();
                                    bool reg = customerService.RegisterCustomer(registerCustomer);
                                    if (reg != false)
                                    {
                                        Console.Write("Returning to previous menu.");
                                        Thread.Sleep(2000);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Customer Registration UnSuccessful");
                                        Console.Write("Returning to previous menu.");
                                        Thread.Sleep(2000);
                                    }
                                    Thread.Sleep(1500);
                                }
                                else throw new InvalidInputException("Invalid E-mail address.");
                            }
                            else throw new InvalidInputException("Phone number should be minimum of 10 digits.");
                        }
                        else throw new InvalidInputException("Phone number should only contain digits.");
                    }
                    else throw new InvalidInputException("Cannot use special characters and numbers.");
                }
                else throw new InvalidInputException("Cannot use special characters and numbers.");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a UpdateCustomer method which updates the Customer details in the Customer table.
        /// </summary>  
        public static void UpdateCustomer(ICustomerService customerService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tCustomer Details Updation");
                Customer updateCustomer = new Customer();
                Console.WriteLine("Enter new First Name: ");
                updateCustomer.FirstName = Console.ReadLine();
                if (Regex.IsMatch(updateCustomer.FirstName, "^[a-zA-Z ]+$"))
                {
                    Console.WriteLine("Enter Last Name: ");
                    updateCustomer.LastName = Console.ReadLine();
                    if (Regex.IsMatch(updateCustomer.LastName, "^[a-zA-Z ]+$"))
                    {
                        Console.WriteLine("Enter new phone number: ");
                        updateCustomer.PhoneNumber = Console.ReadLine();
                        if (Regex.IsMatch(updateCustomer.PhoneNumber, @"^\d+$"))
                        {
                            if (updateCustomer.PhoneNumber.Length <= 10)
                            {
                                Console.WriteLine("Enter the Username: ");
                                updateCustomer.UserName = Console.ReadLine();
                                Console.WriteLine("Enter the Password");
                                updateCustomer.Password = Console.ReadLine();
                                Console.WriteLine("Enter the Customer ID you want to update:");
                                updateCustomer.CustomerID = int.Parse(Console.ReadLine());
                                Console.WriteLine();
                                bool res = customerService.UpdateCustomer(updateCustomer);
                                if (res != false)
                                {
                                    Console.WriteLine("Customer Updated Successfully");
                                    Console.Write("Returning to previous menu.");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    Console.WriteLine("Customer Updation UnSuccessful");
                                    Console.Write("Returning to previous menu.");
                                    Thread.Sleep(2000);
                                }
                                Thread.Sleep(1500);
                            }
                            else throw new InvalidInputException("Phone number should be of 10 digits.");
                        }
                        else throw new InvalidInputException("Phone number should only contain digits.");
                    }
                    else throw new InvalidInputException("Cannot use special characters and numbers.");
                }
                else throw new InvalidInputException("Cannot use special characters and numbers.");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a DeleteCustomer method which deletes the Customer details in the Customer table.
        /// </summary>  
        public static void DeleteCustomer(ICustomerService customerService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tCustomer Details Deletion\n");
                Console.WriteLine("Enter the ID of the customer you want to delete: ");
                int deleteCustomerID = int.Parse(Console.ReadLine());
                Console.WriteLine();
                string updateCustomerRes = customerService.DeleteCustomer(deleteCustomerID);
                if (updateCustomerRes != null)
                {
                    Console.WriteLine(updateCustomerRes);
                    Console.Write("\nReturning to previous menu...");
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message + " The ID should be a numeric value.");
                Console.Write("\nReturning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a AdminMenu method which displays the menu options for Admin.
        /// </summary>
        public static void AdminMenu()
        {
            IAdminService adminService;
            adminService = new AdminService();
            bool adminReturnToMainMenu = false;
            while (!adminReturnToMainMenu)
            {
                Console.Clear();
                Console.WriteLine("\t\tAdmin Menu");
                Console.WriteLine("1. Get all admins");
                Console.WriteLine("2. Get admin details using admin ID");
                Console.WriteLine("3. Get admin details using admin user-name");
                Console.WriteLine("4. Register a new admin");
                Console.WriteLine("5. Update admin");
                Console.WriteLine("6. Delete an existing admin data");
                Console.WriteLine("7. Logout");
                Console.WriteLine("0. Exit");
                Console.WriteLine("\nPlease enter your choice : ");

                string adminMenuChoice = Console.ReadLine();
                Console.WriteLine();

                switch (adminMenuChoice)
                {
                    case "1":
                        GetAllAdmins(adminService);
                        break;

                    case "2":
                        GetAdminById(adminService);
                        break;

                    case "3":
                        GetAdminByUserName(adminService);
                        break;

                    case "4":
                        RegisterAdmin(adminService);
                        break;

                    case "5":
                        UpdateAdmin(adminService);
                        break;

                    case "6":
                        DeleteAdmin(adminService);
                        break;

                    case "7":
                        Console.WriteLine("Logged out successfully.");
                        adminReturnToMainMenu = true;
                        break;

                    case "0":
                        Exit();
                        break;

                    default:
                        Console.Write("Invalid choice. Please enter a number as displayed.");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }
        /// <summary>
        /// This is a GetAllAdmins method which retrieves all the admin details from the Admin table.
        /// </summary> 
        public static void GetAllAdmins(IAdminService adminService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tDetails of All Admins\n");
                List<Admin> admins1 = adminService.GetAllAdmins();
                foreach (Admin admin0 in admins1)
                {
                    Console.WriteLine(admin0);
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a GetAdminById method which retrieves the admin details from the Admin table using Admin Id.
        /// </summary>  
        public static void GetAdminById(IAdminService adminService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tAdmin Details using Admin ID\n");
                Console.WriteLine("Enter the ID of the Admin you want to retrive the data: ");
                int a = int.Parse(Console.ReadLine());
                Console.WriteLine();
                adminService.GetAdminById(a);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message + " The ID should be a numeric value.");
                Console.Write("Returning to previous menu.");
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a GetAdminByUserName method which retrieves the admin details from the Admin table using Username.
        /// </summary>  
        public static void GetAdminByUserName(IAdminService adminService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tAdmin Details using username\n");
                Console.WriteLine("Enter the username of the admin you want to retrive the data: ");
                string adminName = Console.ReadLine();
                Console.WriteLine();
                adminService.GetAdminByUserName(adminName);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a RegisterAdmin method which registers the new admin details into the Admin table.
        /// </summary> 
        public static void RegisterAdmin(IAdminService adminService)
        {
            try
            {
                Console.Clear();
                Admin registerAdmin = new Admin();
                Console.WriteLine("\t\t\t\tNew Admin Registeration");
                Console.WriteLine("Enter First Name: ");
                registerAdmin.FirstName = Console.ReadLine();
                if (Regex.IsMatch(registerAdmin.FirstName, "^[a-zA-Z ]+$"))
                {
                    Console.WriteLine("Enter Last Name: ");
                    registerAdmin.LastName = Console.ReadLine();
                    if (Regex.IsMatch(registerAdmin.LastName, "^[a-zA-Z ]+$"))
                    {
                        Console.WriteLine("Enter Phone Number: ");
                        registerAdmin.PhoneNumber = Console.ReadLine();
                        if (Regex.IsMatch(registerAdmin.PhoneNumber, @"^\d+$"))
                        {
                            if (registerAdmin.PhoneNumber.Length <= 10)
                            {
                                Console.WriteLine("Enter Email-id: ");
                                registerAdmin.Email = Console.ReadLine();
                                if (registerAdmin.Email.Contains("@"))
                                {
                                    Console.WriteLine("Enter Username: ");
                                    registerAdmin.UserName = Console.ReadLine();
                                    Console.WriteLine("Enter Password");
                                    registerAdmin.Password = Console.ReadLine();
                                    Console.WriteLine("Enter Role: ");
                                    registerAdmin.Role = Console.ReadLine();
                                    registerAdmin.JoinDate = DateTime.Now;
                                    Console.WriteLine();
                                    bool res = adminService.RegisterAdmin(registerAdmin);
                                    if (res != false)
                                    {
                                        Console.Write("\nReturning to previous menu...");
                                        Thread.Sleep(2000);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Admin Registration UnSuccessful");
                                        Console.Write("\nReturning to previous menu...");
                                        Thread.Sleep(2000);
                                    }
                                    Thread.Sleep(1500);
                                }
                                else throw new InvalidInputException("Invalid E-mail address.");
                            }
                            else throw new InvalidInputException("Phone number should min be of 10 digits.");
                        }
                        else throw new InvalidInputException("Phone number should only contain digits.");
                    }
                    else throw new InvalidInputException("Cannot use special characters and numbers.");
                }
                else throw new InvalidInputException("Cannot use special characters and numbers.");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a UpdateAdmin method which updates the admin details from the Admin table.
        /// </summary>  
        public static void UpdateAdmin(IAdminService adminService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tAdmin Details Updation");
                Admin updateAdmin = new Admin();
                Console.WriteLine("Enter new First Name: ");
                updateAdmin.FirstName = Console.ReadLine();
                if (Regex.IsMatch(updateAdmin.FirstName, "^[a-zA-Z ]+$"))
                {
                    Console.WriteLine("Enter Last Name: ");
                    updateAdmin.LastName = Console.ReadLine();
                    if (Regex.IsMatch(updateAdmin.LastName, "^[a-zA-Z ]+$"))
                    {
                        Console.WriteLine("Enter new phone number: ");
                        updateAdmin.PhoneNumber = Console.ReadLine();
                        if (Regex.IsMatch(updateAdmin.PhoneNumber, @"^\d+$"))
                        {
                            if (updateAdmin.PhoneNumber.Length <= 10)
                            {
                                Console.WriteLine("Enter the Username: ");
                                updateAdmin.UserName = Console.ReadLine();
                                Console.WriteLine("Enter the Admin ID you want to update:");
                                updateAdmin.AdminID = int.Parse(Console.ReadLine());
                                Console.WriteLine();
                                bool updateAdminRes = adminService.UpdateAdmin(updateAdmin);
                                if (updateAdminRes != false)
                                {
                                    Console.WriteLine("Admin Updated Successfully");
                                    Console.Write("\nReturning to previous menu...");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    Console.WriteLine("Admin Updation UnSuccessful");
                                    Console.Write("\nReturning to previous menu...");
                                    Thread.Sleep(2000);
                                }
                                Thread.Sleep(2000);
                            }
                            else throw new InvalidInputException("Phone number should be of 10 digits.");
                        }
                        else throw new InvalidInputException("Phone number should only contain digits.");
                    }
                    else throw new InvalidInputException("Cannot use special characters and numbers.");
                }
                else throw new InvalidInputException("Cannot use special characters and numbers.");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);

            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a DeleteAdmin method which deletes the admin details from the Admin table.
        /// </summary>  
        public static void DeleteAdmin(IAdminService adminService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\\Admin Details Deletion\n");
                Console.WriteLine("Enter the ID of the admin you want to delete: ");
                int deleteAdminID = int.Parse(Console.ReadLine());
                Console.WriteLine();
                string updateAdminRes = adminService.DeleteAdmin(deleteAdminID);
                if (updateAdminRes != null)
                {
                    Console.WriteLine(updateAdminRes);
                    Console.Write("\nReturning to previous menu...");
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message + " The ID should be a numeric value.");
                Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a VehicleMenu method which displays the menu options for Vehicle.
        /// </summary>
        public static void VehicleMenu()
        {
            IVehicleService vehicleService;
            vehicleService = new VehicleService();
            bool vehicleReturnToMainMenu = false;
            while (!vehicleReturnToMainMenu)
            {
                Console.Clear();
                Console.WriteLine("\t\tVehicle Menu");
                Console.WriteLine("1. Get all vehicles");
                Console.WriteLine("2. Get vehicle details using vehicle ID");
                Console.WriteLine("3. Get Available vehicles");
                Console.WriteLine("4. Register a new vehicle");
                Console.WriteLine("5. Update vehicle details");
                Console.WriteLine("6. Delete an existing vehicle data");
                Console.WriteLine("7. Return to Main Menu");
                Console.WriteLine("0. Exit");
                Console.WriteLine("\nPlease enter your choice : ");

                string vehicleMenuChoice = Console.ReadLine();
                Console.WriteLine();

                switch (vehicleMenuChoice)
                {
                    case "1":
                        GetAllVehicles(vehicleService);
                        break;

                    case "2":
                        GetVehicleById(vehicleService);
                        break;

                    case "3":
                        GetAvailableVehicles(vehicleService);
                        break;

                    case "4":
                        AddVehicle(vehicleService);
                        break;

                    case "5":
                        UpdateVehicle(vehicleService);
                        break;

                    case "6":
                        RemoveVehicle(vehicleService);
                        break;

                    case "7":
                        vehicleReturnToMainMenu = true;
                        break;

                    case "0":
                        Exit();
                        break;

                    default:
                        Console.Write("Invalid choice. Please enter a number as displayed.");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }
        /// <summary>
        /// This is a GetAllVehicles method which retrieves all the vehicle details in the Vehicle table.
        /// </summary>   
        public static void GetAllVehicles(IVehicleService vehicleService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tDetails of All Vehicles");
                List<Vehicle> vehicles = vehicleService.GetAllVehicles();
                foreach (Vehicle vehicle in vehicles)
                {
                    Console.WriteLine(vehicle);
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a GetVehicleById method which retrieves the  vehicle details from the Vehicle table using Vehicle ID.
        /// </summary>  
        public static void GetVehicleById(IVehicleService vehicleService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tVehicle Details using Vehicle ID\n");
                Console.Write("Enter the ID of the vehicle you want to retrive the data: ");
                int a = int.Parse(Console.ReadLine());
                Console.WriteLine();
                vehicleService.GetVehicleById(a);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message + " The ID should be a numeric value.");
                Console.Write("Returning to previous menu.");
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a GetAvailableVehicles method which retrieves all the  available vehicle details into the Vehicle table.
        /// </summary>
        public static void GetAvailableVehicles(IVehicleService vehicleService)
        {
            try
            {
                Console.Clear();
                List<Vehicle> vehicless = vehicleService.GetAvailableVehicles();
                foreach (Vehicle vehicle in vehicless)
                {
                    Console.WriteLine(vehicle);
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a AddVehicle method which adds the new vehicle details into the Vehicle table.
        /// </summary>  
        public static void AddVehicle(IVehicleService vehicleService)
        {
            try
            {
                Console.Clear();
                Vehicle addVehicle = new Vehicle();
                Console.WriteLine("\t\t\t\tNew Vehicle Registeration:");
                Console.WriteLine("Enter Vehicle Model Name: ");
                addVehicle.Model = Console.ReadLine();
                Console.WriteLine("Enter Vehicle Maker Name: ");
                addVehicle.Make = Console.ReadLine();
                Console.WriteLine("Enter Vehicle Color: ");
                addVehicle.Color = Console.ReadLine();
                if (Regex.IsMatch(addVehicle.Color, "^[a-zA-Z ]+$"))
                {
                    Console.WriteLine("Enter Year: ");
                    if (int.TryParse(Console.ReadLine(), out int year))
                    {

                        addVehicle.Year = year;
                        if (Regex.IsMatch(addVehicle.Year.ToString(), @"^\d{4}$"))
                        {
                            Console.WriteLine("Enter Vehicle Registration Number: ");
                            addVehicle.RegistrationNumber = Console.ReadLine();
                            if (Regex.IsMatch(addVehicle.RegistrationNumber, @"^[a-zA-Z0-9 ]+$"))
                            {
                                Console.WriteLine("Enter Vehicle Availability (true / false): ");
                                if (bool.TryParse(Console.ReadLine(), out bool availability))
                                {
                                    addVehicle.Availability = availability;
                                    Console.WriteLine("Enter Daily Rate of the Vehicle: ");
                                    if (double.TryParse(Console.ReadLine(), out double dailyRate))
                                    {
                                        addVehicle.DailyRate = dailyRate;
                                        Console.WriteLine();
                                        bool res = vehicleService.AddVehicle(addVehicle);
                                        if (res != false)
                                        {
                                            Console.Write("\nReturning to previous menu...");
                                            Thread.Sleep(2000);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Vehicle Registration Unsuccessful");
                                            Console.Write("\nReturning to previous menu...");
                                            Thread.Sleep(2000);
                                        }
                                    }
                                    else throw new InvalidInputException("Input should only contain numeric values.");
                                }
                                else throw new InvalidInputException("Input should only be - True / False.");
                            }
                            else throw new InvalidInputException("Cannot use special characters");
                        }
                        else throw new InvalidInputException("Year should be of 4 digits.");
                    }
                    else throw new InvalidInputException("Year should only contain digits.");
                }
                else throw new InvalidInputException("Cannot use special characters and numbers");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a UpdateVehicle method which updates the vehicle details from the Vehicle table.
        /// </summary>   
        public static void UpdateVehicle(IVehicleService vehicleService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tVehicle Details Updation");
                Vehicle updateVehicle = new Vehicle();
                Console.WriteLine("Enter availability status (true or false): ");
                if (bool.TryParse(Console.ReadLine(), out bool availability))
                {
                    updateVehicle.Availability = availability;
                    Console.WriteLine("Enter new Daily Rate: ");
                    if (double.TryParse(Console.ReadLine(), out double dailyRate))
                    {
                        updateVehicle.DailyRate = dailyRate;
                        Console.WriteLine("Enter the Vehicle Registration Number: ");
                        updateVehicle.RegistrationNumber = Console.ReadLine();
                        if (Regex.IsMatch(updateVehicle.RegistrationNumber, @"^[a-zA-Z0-9 ]+$"))
                        {
                            Console.WriteLine("Enter the Vehicle ID you want to update:");
                            updateVehicle.VehicleID = int.Parse(Console.ReadLine());
                            Console.WriteLine();
                            bool updateVehicleRes = vehicleService.UpdateVehicle(updateVehicle);
                            if (updateVehicleRes != false)
                            {
                                Console.WriteLine("Vehicle updation Successful");
                                Console.Write("\nReturning to previous menu...");
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                Console.WriteLine("Vehicle updation Unsuccessful");
                                Console.Write("\nReturning to previous menu...");
                                Thread.Sleep(2000);
                            }
                        }
                        else throw new InvalidInputException("Cannot use special characters");
                    }
                    else throw new InvalidInputException("Input should only contain numeric values.");
                }
                else throw new InvalidInputException("Input should only be - True / False.");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.Write(ex.Message);
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a RemoveVehicle method which removes the vehicle details from the Vehicle table.
        /// </summary>
        public static void RemoveVehicle(IVehicleService vehicleService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tVehicle Details Deletion\n");
                Console.WriteLine("Enter the ID of the vehicle you want to delete: ");
                int deleteVehicleID = int.Parse(Console.ReadLine());
                Console.WriteLine();
                string updateVehicleRes = vehicleService.RemoveVehicle(deleteVehicleID);
                if (updateVehicleRes != null)
                {
                    Console.WriteLine(updateVehicleRes);
                    Console.Write("\nReturning to previous menu...");
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message + " The ID should be a numeric value.");
                Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a ReservationMenu method which displays the menu options for Reservation.
        /// </summary>
        public static void ReservationMenu()
        {
            IReservationService reservationService;
            reservationService = new ReservationService();
            bool reservationReturnToMainMenu = false;
            while (!reservationReturnToMainMenu)
            {
                Console.Clear();
                Console.WriteLine("\t\tReservation Menu");
                Console.WriteLine("1. Get all reservations");
                Console.WriteLine("2. Get reservation details using reservation ID");
                Console.WriteLine("3. Get reservation details using customer ID");
                Console.WriteLine("4. Register a new reservation");
                Console.WriteLine("5. Update reservation");
                Console.WriteLine("6. Cancel an existing reservation data");
                Console.WriteLine("7. Return to Main Menu");
                Console.WriteLine("0. Exit");
                Console.WriteLine("\nPlease enter your choice : ");

                string reservationMenuChoice = Console.ReadLine();
                Console.WriteLine();

                switch (reservationMenuChoice)
                {
                    case "1":
                        GetAllReservations(reservationService);
                        break;

                    case "2":
                        GetReservationById(reservationService);
                        break;

                    case "3":
                        GetReservationByCustomerId(reservationService);
                        break;

                    case "4":
                        CreateReservation(reservationService);
                        break;

                    case "5":
                        UpdateReservation(reservationService);
                        break;

                    case "6":
                        CancelReservation(reservationService);
                        break;

                    case "7":
                        reservationReturnToMainMenu = true;
                        break;

                    case "0":
                        Exit();
                        break;

                    default:
                        Console.Write("Invalid choice. Please enter a number as displayed.");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }
        /// <summary>
        /// This is a GetAllReservation method which retrieves all the reservation details from the Reservation table.
        /// </summary> 
        public static void GetAllReservations(IReservationService reservationService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tDetails of All Reservations");
                List<Reservation> reservations = reservationService.GetAllReservation();
                foreach (Reservation reservation1 in reservations)
                {
                    Console.WriteLine(reservation1);
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Returning to previous menu.");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a GetReservationByCustomerId method which retrieves the reservation details from the Reservation table using Reservation ID.
        /// </summary> 
        public static void GetReservationById(IReservationService reservationService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tReservation Details using Reservation ID\n");
                Console.Write("Enter the ReservationID of the reservation you want to retrive the data: ");
                int a = int.Parse(Console.ReadLine());
                Console.WriteLine();
                reservationService.GetReservationById(a);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message + " The ID should be a numeric value.");
                Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a GetReservationByCustomerId method which retrieves the reservation details from the Reservation table using Customer ID.
        /// </summary>
        public static void GetReservationByCustomerId(IReservationService reservationService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tReservation Details using Customer ID\n");
                Console.Write("Enter the CustomerID of the reservation you want to retrive the data: ");
                int z = int.Parse(Console.ReadLine());
                Console.WriteLine();
                reservationService.GetReservationByCustomerId(z);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message + " The ID should be a numeric value.");
                Console.WriteLine("\n\n\n\nPress enter key to return to previous menu...");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a CreateReservation method which creates the new reservation details into the Reservation table.
        /// </summary>  
        public static void CreateReservation(IReservationService reservationService)
        {
            try
            {
                Console.Clear();
                Reservation registerReservation = new Reservation();
                Console.WriteLine("\t\t\t\tNew Reservation");
                Console.WriteLine("Enter Customer ID: ");
                registerReservation.CustomerID = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Vehicle ID: ");
                registerReservation.VehicleID = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Start Date (dd-MM-yyyy): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime parsedStartDate))
                {
                    registerReservation.StartDate = parsedStartDate;
                    if (registerReservation.StartDate >= DateTime.Now.Date)
                    {
                        Console.WriteLine("Enter End Date (dd-MM-yyyy): ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime parsedEndDate))
                        {
                            registerReservation.EndDate = parsedEndDate;
                            if (registerReservation.EndDate > registerReservation.StartDate)
                            {
                                registerReservation.TotalCost = reservationService.CalculateTotalCost(registerReservation.StartDate, registerReservation.EndDate, registerReservation.VehicleID);
                                Console.WriteLine("Enter status: ");
                                registerReservation.Status = Console.ReadLine();
                                Console.WriteLine();
                                bool res = reservationService.CreateReservation(registerReservation);
                                if (res != false)
                                {
                                    Console.Write("\nReturning to previous menu...");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    Console.WriteLine("\nRegistration Unsuccessful");
                                    Console.Write("\nReturning to previous menu...");
                                    Thread.Sleep(2000);
                                }
                            }
                            else throw new InvalidInputException($"End Date cannot be lesser than 24 hours");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\nInvalid date format.");
                            Console.Write("\nReturning to previous menu...");
                            Thread.Sleep(2000);
                        }
                    }
                    else throw new InvalidInputException($"Start Date cannot be lesser than {DateTime.Now.ToShortDateString()}");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\nInvalid date format.");
                    Console.Write("\nReturning to previous menu...");
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.Write(ex.Message);
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a UpdateReservation method which updates the reservation details from the Reservation table.
        /// </summary>   
        public static void UpdateReservation(IReservationService reservationService)
        {
            try
            {
                Reservation updateReservation = new Reservation();
                Console.WriteLine("\t\t\t\tUpdate Reservation");
                Console.WriteLine("Enter Status of the reservation: ");
                updateReservation.Status = Console.ReadLine();
                Console.Write("Enter reservation id: ");
                updateReservation.ReservationID = int.Parse(Console.ReadLine());
                Console.WriteLine();
                bool updateReservationRes = reservationService.UpdateReservation(updateReservation);
                if (updateReservationRes != false)
                {
                    Console.WriteLine("Reservation Registered Successfully");
                    Console.Write("Returning to previous menu.");
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("Reservation Registration UnSuccessful");
                    Console.Write("Returning to previous menu.");
                    Thread.Sleep(2000);
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a CancelReservation method which cancels the reservation details from the Reservation table.
        /// </summary>  
        public static void CancelReservation(IReservationService reservationService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Reservation Details Cancellation \n");
                Console.WriteLine("Enter the ID of the reservation you want to cancel: ");
                int deleteReservationID = int.Parse(Console.ReadLine());
                Console.WriteLine();
                string deleteCustomerRes = reservationService.CancelReservation(deleteReservationID);
                if (deleteCustomerRes != null)
                {
                    Console.WriteLine(deleteCustomerRes);
                    Console.Write("\nReturning to previous menu...");
                    Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message + " The ID should be a numeric value.");
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// This is a Exit method which exits the code and close the console window.
        /// </summary>  
        public static void Exit()
        {
            Console.WriteLine("Exiting.");
            Thread.Sleep(2000);
            Console.WriteLine();
            Environment.Exit(0);
        }
    }
}