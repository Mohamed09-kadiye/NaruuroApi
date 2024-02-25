using NaruuroApi.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace NaruuroApi.Model.Repository
{
    public class BookingRepository : IBooking
    {
        private readonly IConfiguration _configuration;

        public BookingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Booking> GetBookings()
        {
            List<Booking> bookings = new List<Booking>();

            using (MySqlConnection connectionlist = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
               connectionlist.Open();

                MySqlCommand command = new MySqlCommand("GET_ALL_BOOKINGS", connectionlist);
                command.CommandType = CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookings.Add(MapBookingFromDataReader(reader));
                    } 
                }
            }

            return bookings;
        }

        public Booking GetBookingById(int ID)
        {
            Booking? booking = null;

            using (MySqlConnection Getconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                Getconnection.Open();

                MySqlCommand command = new MySqlCommand("GET_BOOKING_BY_ID", Getconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", ID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        booking = MapBookingFromDataReader(reader);
                    }
                }
            }

            return  booking;
        }

        public void UpdateBooking(Booking booking)
        {
            using (MySqlConnection Updateconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                Updateconnection.Open();

                MySqlCommand command = new MySqlCommand("UPDATE_BOOKING", Updateconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", booking.ID);
                command.Parameters.AddWithValue("@customerId", booking.CUSTOMER_TELL);
                command.Parameters.AddWithValue("@amount", booking.Amount);
                command.Parameters.AddWithValue("@roomNumber", booking.ROOM_NUMBER);
                command.Parameters.AddWithValue("@discription", booking.Discription);
                command.Parameters.AddWithValue("@userId", booking.USERNAME);

                command.ExecuteNonQuery();
            }
        }

        public void ExecuteRefreshProcedure()
        {
            using (MySqlConnection Executeconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                Executeconnection.Open();

                using (MySqlCommand command = new MySqlCommand("RefreshBookings", Executeconnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("refresh fialure :",ex);
                    }
                }
            }
        }



        public void AddBooking(Booking booking)
        {
            try
            {
                using (MySqlConnection Addconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
                {
                    Addconnection.Open();

                    MySqlCommand command = new MySqlCommand("INSERT_BOOKING", Addconnection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters for inserting new booking details here
                    command.Parameters.AddWithValue("@customerId", booking.CUSTOMER_TELL);
                    command.Parameters.AddWithValue("@amount", booking.Amount);
                    command.Parameters.AddWithValue("@roomNumber", booking.ROOM_NUMBER);
                    command.Parameters.AddWithValue("@bookingDateTime", booking.DATE_TIME);
                    command.Parameters.AddWithValue("@userId", booking.USERNAME);
                    command.Parameters.AddWithValue("@discription", booking.Discription);

                    command.ExecuteNonQuery();

                }

            }
            catch(MySqlException ex)
            {
                Console.WriteLine($" failed to add booking due to missing params  MySQL Exception: {ex.Message}");
            }
            
        }


        public void DeleteBooking(int ID)
        {
            using (MySqlConnection Delconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                Delconnection.Open();

                MySqlCommand command = new MySqlCommand("DeleteBooking", Delconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", ID);

                command.ExecuteNonQuery();
            }
        }
        private Booking MapBookingFromDataReader(MySqlDataReader reader)
        {
            return new Booking()
            {
                ID = Convert.ToInt32(reader["ID"]),
                CUSTOMER_TELL = reader["CUSTOMER_TELL"].ToString(),
                Amount = Convert.ToInt64(reader["Amount"]),
                ROOM_NUMBER = reader["ROOM_NUMBER"].ToString(),
                
                DATE_TIME = Convert.ToDateTime(reader["DATE_TIME"]),
                CHECKOUT = Convert.IsDBNull(reader["CHECKOUT"]) ? (DateTime?)null : Convert.ToDateTime(reader["CHECKOUT"]),
                Discription = reader["Discription"].ToString(),
                updated = Convert.IsDBNull(reader["updated"]) ? (DateTime?)null : Convert.ToDateTime(reader["updated"]),
                USERNAME = reader["USERNAME"].ToString(),
                Status = reader["status"].ToString()
            };
        }

    }
}
