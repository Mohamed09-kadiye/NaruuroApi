using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using NaruuroApi.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace NaruuroApi.Model.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IConfiguration _configuration;

        public RoomRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();

            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("GetAllRooms", connection);
                command.CommandType = CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rooms.Add(MapRoomFromDataReader(reader));
                    }
                }
            }

            return rooms;
        }

        private Room MapRoomFromDataReader(MySqlDataReader reader) => new Room()
        {
            ROOM_ID = Convert.ToInt32(reader["ROOM_ID"]),
            ROOM_NUMBER = Convert.ToInt32(reader["ROOM_NUMBER"]),
            availability = reader["availability"].ToString()
        };
        public Room GetRoomById(int roomId)
        {
            Room room = null;

            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("GetRoomById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_ROOM_ID", roomId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        room = MapRoomFromDataReader(reader);
                    }
                }
            }

            return room;
        }

        public void InsertRoom(Room room)
        {
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("CreateRoom", connection);
                command.CommandType = CommandType.StoredProcedure;
                // Add parameters for inserting new room details here
                command.Parameters.AddWithValue("@p_ROOM_NUMBER", room.ROOM_NUMBER);
              

                command.ExecuteNonQuery();
            }
        }

        public void UpdateRoom(Room room)
        {
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("UpdateRoom", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_ROOM_ID", room.ROOM_ID);
                command.Parameters.AddWithValue("@p_ROOM_NUMBER", room.ROOM_NUMBER);
             

                command.ExecuteNonQuery();
            }
        }

        public void DeleteRoom(int roomId)
        {
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("DeleteRoom", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_ROOM_ID", roomId);

                command.ExecuteNonQuery();
            }
        }
    }
}
