using System;
using System.Data;
using System.Collections.Generic;
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
    public class ReservationService : IReservationService
    {
        SqlConnection conn;
        SqlCommand cmd = null;
        public ReservationService()
        {
            cmd = new SqlCommand();
        }
        /// <summary>
        /// This is a CreateReservation method which creates the new reservation details into the Reservation table.
        /// </summary>   
        public bool CreateReservation(Reservation reservationData)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into Reservation OUTPUT INSERTED.ReservationID values (@CustomerID, @VehicleID, @StartDate, @EndDate, @TotalCost, @Status);";
                    cmd.Parameters.AddWithValue("@CustomerID", reservationData.CustomerID);
                    cmd.Parameters.AddWithValue("@VehicleID", reservationData.VehicleID);
                    cmd.Parameters.AddWithValue("@StartDate", reservationData.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", reservationData.EndDate);
                    cmd.Parameters.AddWithValue("@TotalCost", reservationData.TotalCost);
                    cmd.Parameters.AddWithValue("@Status", reservationData.Status);
                    try
                    {
                        conn.Open();
                        object NewId = cmd.ExecuteScalar();
                        if (NewId != null)
                        {
                            Console.WriteLine("Reservation Registered Successfully with ID: " + NewId);
                            return true;
                        }
                        else
                        {
                            throw new ReservationNotFoundException("Error Displayed");
                        }
                    }
                    catch (SqlException se)
                    {
                        if (se.Class == 11)
                        {
                            throw new DatabaseConnectionException("Error connecting to Database.\nName of the database is not present in the sql server.");
                        }
                        else if (se.Class == 16)
                        {
                            throw new DatabaseConnectionException("Invalid vehicle or customer id.");
                        }
                        else if (se.Class == 20)
                        {
                            throw new DatabaseConnectionException("Incorrect Server Name.");
                        }
                        else
                        {
                            Console.WriteLine("An error occured : " + se.Message);
                            Console.Write("\n\n\n\nPress any key to return to previous menu...");
                            Console.ReadKey(); ;
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
                Thread.Sleep(3000);
            }
            return false;
        }
        /// <summary>
        /// This is a GetAllReservation method which retrieves all the reservation details from the Reservation table.
        /// </summary>     
        public List<Reservation> GetAllReservation()
        {
            try
            {
                List<Reservation> reservations = new List<Reservation>();
                try
                {
                    using (conn = DBConnUtil.GetConnection())
                        cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from reservation";
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            reservations.Add(new Reservation() { ReservationID = (int)dr[0], CustomerID = (int)dr[1], VehicleID = (int)dr[2], StartDate = (DateTime)dr[3], EndDate = (DateTime)dr[4], TotalCost = (decimal)dr[5], Status = dr[6].ToString() });
                        }
                        dr.Close();
                    }
                    else
                    {
                        throw new ReservationNotFoundException("Reservation Table is empty.");
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
                        Console.ReadKey(); ;
                    }
                }
                return reservations;
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(4000);
            }
            catch (ReservationNotFoundException re)
            {
                Console.WriteLine(re.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(4000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(4000);
            }
            finally
            {
                conn.Close();
            }
            return new List<Reservation>();
        }
        /// <summary>
        /// This is a GetReservationByCustomerId method which retrieves the reservation details from the Reservation table using Customer ID.
        /// </summary>     
        public void GetReservationByCustomerId(int customerId)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"select * from reservation where CustomerID={customerId}", conn);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            Console.Clear();
                            Console.WriteLine($"\t\t\t\tDetails of Reservation of Customer ID : {customerId}\n");
                            while (dr.Read())
                            {
                                DateTime startDate = (DateTime)dr["StartDate"];
                                DateTime endDate = (DateTime)dr["EndDate"];
                                Console.WriteLine($"  ReservationID     = {dr["ReservationID"]}");
                                Console.WriteLine($"  VehicleID         = {dr["VehicleID"]}");
                                Console.WriteLine($"  StartDate         = {startDate.ToShortDateString()}");
                                Console.WriteLine($"  EndDate           = {endDate.ToShortDateString()}");
                                Console.WriteLine($"  TotalCost         = {dr["TotalCost"]}");
                                Console.WriteLine($"  Status            = {dr["Status"]}");
                                Console.WriteLine();
                            }
                            Console.Write("\n\n\n\nPress any key to return to previous menu...");
                            Console.ReadKey(); ;
                        }
                        else
                        {
                            throw new ReservationNotFoundException($"No reservation for customer with ID : {customerId}.");
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
                            Console.ReadKey(); ;
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
            catch (ReservationNotFoundException re)
            {
                Console.WriteLine(re.Message); Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); Console.Write("\nReturning to previous menu...");
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// This is a GetReservationByCustomerId method which retrieves the reservation details from the Reservation table using Reservation ID.
        /// </summary>   
        public void GetReservationById(int reservationId)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"select * from reservation where ReservationID={reservationId}", conn);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            Console.Clear();
                            Console.WriteLine($"\t\t\t\tDetails of Reservation ID : {reservationId}\n");
                            while (dr.Read())
                            {
                                DateTime startDate = (DateTime)dr["StartDate"];
                                DateTime endDate = (DateTime)dr["EndDate"];
                                Console.WriteLine($"  CustomerID     = {dr["CustomerID"]}");
                                Console.WriteLine($"  VehicleID      = {dr["VehicleID"]}");
                                Console.WriteLine($"  StartDate      = {startDate.ToShortDateString()}");
                                Console.WriteLine($"  EndDate        = {endDate.ToShortDateString()}");
                                Console.WriteLine($"  TotalCost      = {dr["TotalCost"]}");
                                Console.WriteLine($"  Status         = {dr["Status"]}");
                            }
                            Console.Write("\n\n\n\nPress any key to return to previous menu...");
                            Console.ReadKey();
                        }
                        else
                        {
                            throw new ReservationNotFoundException($"No reservation is present with ID : {reservationId}.");
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
                            Console.ReadKey(); ;
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
            catch (ReservationNotFoundException re)
            {
                Console.WriteLine(re.Message);
                Console.Write("\nReturning to previous menu...");
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
        /// This is a UpdateReservation method which updates the reservation details from the Reservation table.
        /// </summary>   
        public bool UpdateReservation(Reservation reservationData)
        {
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE reservation SET Status=@Status WHERE ReservationID=@ReservationID;";
                        cmd.Parameters.AddWithValue("@Status", reservationData.Status);
                        cmd.Parameters.AddWithValue("@ReservationID", reservationData.ReservationID);

                        conn.Open();
                        int updateReservationStatus = cmd.ExecuteNonQuery();
                        if (updateReservationStatus > 0)
                        {
                            return true;
                        }
                        else
                        {
                            throw new ReservationNotFoundException("Invalid Reservation ID. Please enter a valid reservation ID to update the details.");
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
                            Console.ReadKey(); ;
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
            catch (ReservationNotFoundException re)
            {
                Console.WriteLine(re.Message);
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
        /// This is a CancelReservation method which cancels the reservation details from the Reservation table.
        /// </summary>    
        public string CancelReservation(int reservationId)
        {
            string response = null;
            try
            {
                conn = DBConnUtil.GetConnection();
                SqlCommand cmd = new SqlCommand($"select Status, VehicleID from Reservation where ReservationID = {reservationId}", conn);
                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        string status = dr["Status"].ToString();
                        int vehicleId = (int)dr["VehicleID"];
                        dr.Close();
                        conn.Close();
                        if (status.ToLower() == "pending" || status.ToLower() == "confirmed")
                        {
                            cmd = new SqlCommand($"update reservation set Status = 'Cancelled' where ReservationID = {reservationId}; update vehicle set Availability = 1 where VehicleID = {vehicleId};", conn);
                            conn.Open();
                            int updatedRows = cmd.ExecuteNonQuery();
                            if (updatedRows > 0)
                            {
                                response = "Reservation Cancelled successfully.";
                            }
                            conn.Close();
                        }
                        else if (status.ToLower() == "completed" || status.ToLower() == "cancelled")
                        {
                            throw new ReservationNotFoundException($"Cannot cancel a reservation where status is {status}.");
                        }
                        else
                        {
                            throw new ReservationNotFoundException($"Corrupted status.");
                        }
                    }
                    else
                    {
                        throw new ReservationNotFoundException($"Reservation by ID : {reservationId} is not present.");
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
                        Console.ReadKey(); ;
                    }
                }
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(4000);
            }
            catch (ReservationNotFoundException re)
            {
                Console.WriteLine(re.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(4500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(4500);
            }
            return response;
        }
        /// <summary>
        /// This is a CalculateTotalCost method which calculates the total cost for the reservation details from the Reservation table.
        /// </summary>     
        public decimal CalculateTotalCost(DateTime startDate, DateTime endDate, int vehicleId)
        {
            decimal totalCost = 0;
            try
            {
                using (conn = DBConnUtil.GetConnection())
                {
                    try
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.CommandText = "SELECT DailyRate FROM vehicle WHERE VehicleID = @VehicleID";
                        
                            cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        decimal dailyRate = (decimal)dr["DailyRate"];
                                        var numberOfDays = (endDate - startDate).Days;
                                        totalCost = dailyRate * numberOfDays;
                                    }
                                }
                            }                      
                        return totalCost;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("\nReturning to previous menu...");
                Thread.Sleep(3000);
            }
            return totalCost;
        }
    }
}
