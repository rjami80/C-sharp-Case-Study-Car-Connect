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
using System.Threading;

namespace CarConnect.DAO
{
    public class VehicleService : IVehicleService
    {
        SqlConnection conn;
        SqlCommand cmd = null;

        public VehicleService()
        {
            cmd = new SqlCommand();
        }
        /// <summary>
        /// This is a AddVehicle method which adds the new vehicle details into the Vehicle table.
        /// </summary>  
        public bool AddVehicle(Vehicle vehicleData)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"insert into vehicle OUTPUT INSERTED.vehicleid values (@Model,@Make,@Year,@Color,@RegistrationNumber,@Availability,@DailyRate);", conn);
                    cmd.Parameters.AddWithValue("@Model", vehicleData.Model);
                    cmd.Parameters.AddWithValue("@Make", vehicleData.Make);
                    cmd.Parameters.AddWithValue("@Year", vehicleData.Year);
                    cmd.Parameters.AddWithValue("@Color", vehicleData.Color);
                    cmd.Parameters.AddWithValue("@RegistrationNumber", vehicleData.RegistrationNumber);
                    cmd.Parameters.AddWithValue("@Availability", vehicleData.Availability);
                    cmd.Parameters.AddWithValue("@DailyRate", vehicleData.DailyRate);
                    cmd.Connection = conn;
                    try
                    {
                        conn.Open();
                        object NewId = cmd.ExecuteScalar();
                        if (NewId != null)
                        {
                            Console.WriteLine("Vehicle Registered Successfully with ID: " + NewId);
                            return true;
                        }
                        else
                        {
                            throw new VehicleNotFoundException("Error Displayed");
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
                Thread.Sleep(4000);
            }
            catch (VehicleNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(4500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(4500);
            }
            return false;
        }
        /// <summary>
        /// This is a GetAllVehicles method which retrieves all the vehicle details in the Vehicle table.
        /// </summary>   
        public List<Vehicle> GetAllVehicles()
        {
            try
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                try
                {
                    using (conn = DBConnUtil.GetConnection())
                        cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from Vehicle";
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Vehicle vehicle = new Vehicle()
                            {
                                VehicleID = Convert.ToInt32(dr["VehicleId"]),
                                Model = dr["Model"].ToString(),
                                Make = dr["Make"].ToString(),
                                Year = Convert.ToInt32(dr["Year"]),
                                Color = dr["Color"].ToString(),
                                RegistrationNumber = dr["RegistrationNumber"].ToString(),
                                Availability = (bool)dr["Availability"],
                                DailyRate = Convert.ToDouble(dr["DailyRate"]),
                            };
                            vehicles.Add(vehicle);
                        }
                        dr.Close();
                    }
                    else
                    {
                        throw new VehicleNotFoundException("Vehicle Table is empty.");
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
                return vehicles;
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (VehicleNotFoundException vnfe)
            {
                Console.WriteLine(vnfe.Message);
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
            return new List<Vehicle>();
        }
        /// <summary>
        /// This is a GetAvailableVehicles method which retrieves all the  available vehicle details into the Vehicle table.
        /// </summary>  
        public List<Vehicle> GetAvailableVehicles()
        {
            try
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                try
                {
                    using (conn = DBConnUtil.GetConnection())
                        cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from Vehicle where Availability = 1";
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            Vehicle vehicle = new Vehicle()
                            {
                                VehicleID = Convert.ToInt32(dr["VehicleId"]),
                                Model = dr["Model"].ToString(),
                                Make = dr["Make"].ToString(),
                                Year = Convert.ToInt32(dr["Year"]),
                                Color = dr["Color"].ToString(),
                                RegistrationNumber = dr["RegistrationNumber"].ToString(),
                                Availability = (bool)dr["Availability"],
                                DailyRate = Convert.ToDouble(dr["DailyRate"]),
                            };
                            vehicles.Add(vehicle);
                        }
                        dr.Close();
                    }
                    else
                    {
                        throw new VehicleNotFoundException("Vehicle Table is empty.");
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
                return vehicles;
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (VehicleNotFoundException vnfe)
            {
                Console.WriteLine(vnfe.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            finally
            {
                conn.Close();
            }
            return new List<Vehicle>();
        }
        /// <summary>
        /// This is a GetVehicleById method which retrieves the  vehicle details from the Vehicle table using Vehicle ID.
        /// </summary>  
        public void GetVehicleById(int vehicleId)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"select * from vehicle where vehicleid={vehicleId}", conn);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            Console.Clear();
                            Console.WriteLine($"\t\t\t\tDetails of VehicleId ID : {vehicleId}\n");
                            while (dr.Read())
                            {
                                Console.WriteLine($"  Model                 = {dr[1]} ");
                                Console.WriteLine($"  Make                  = {dr[2]} ");
                                Console.WriteLine($"  Year                  = {dr[3]} ");
                                Console.WriteLine($"  Color                 = {dr[4]} ");
                                Console.WriteLine($"  RegistrationNumber    = {dr[5]} ");
                                Console.WriteLine($"  Availability          = {dr[6]} ");
                                Console.WriteLine($"  DailyRate             = {dr[7]} ");
                            }
                            Console.Write("\n\n\n\nPress enter key to return to previous menu...");
                            Console.ReadLine();
                        }
                        else
                        {
                            throw new VehicleNotFoundException($"No customer is present with ID : {vehicleId}.");
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
            catch (VehicleNotFoundException ex)
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
        /// This is a RemoveVehicle method which removes the vehicle details from the Vehicle table.
        /// </summary>   
        public string RemoveVehicle(int vehicleId)
        {
            string response = null;
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    try
                    {
                        cmd.CommandText = "DELETE FROM Vehicle WHERE VehicleID = @VehicleId";
                        cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                        cmd.Connection = conn;
                        conn.Open();
                        int removeVehicleId = cmd.ExecuteNonQuery();
                        if (removeVehicleId > 0)
                        {
                            response = $"Data of Vehicle having ID : {vehicleId} has been deleted succefully.";
                        }
                        else
                        {
                            throw new CustomerNotFoundException($"Customer by ID : {vehicleId} is not present.");
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
        /// This is a UpdateVehicle method which updates the vehicle details from the Vehicle table.
        /// </summary>    
        public bool UpdateVehicle(Vehicle vehicleData)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "UPDATE Vehicle SET Availability = @Availability, DailyRate = @DailyRate, RegistrationNumber = @RegistrationNumber WHERE VehicleID = @VehicleId";
                    cmd.Parameters.AddWithValue("@Availability", vehicleData.Availability);
                    cmd.Parameters.AddWithValue("@DailyRate", vehicleData.DailyRate);
                    cmd.Parameters.AddWithValue("@RegistrationNumber", vehicleData.RegistrationNumber);
                    cmd.Parameters.AddWithValue("@VehicleID", vehicleData.VehicleID);
                    cmd.Connection = conn;
                    try
                    {
                        conn.Open();
                        int updateVehicleStatus = cmd.ExecuteNonQuery();
                        if (updateVehicleStatus > 0)
                        {
                            return true;
                        }
                        else
                        {
                            throw new VehicleNotFoundException("Error Displayed");
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
            catch (VehicleNotFoundException e)
            {
                Console.WriteLine(e.Message);
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
    }
}
