using NaruuroApi.Model.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace NaruuroApi.Model.Repository
{
    public class StaffRepo : IStaff
    {
        private readonly IConfiguration _configuration;

        public StaffRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<StaffM> GetAllStaff()
        {
            List<StaffM> staffList = new List<StaffM>();

            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("GetStaff", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StaffM staff = new StaffM();
                        staff.Id = Convert.ToInt32(reader["Id"]);
                        staff.Name = reader["Name"].ToString();
                        staff.Telephone = reader["Telephone"].ToString();
                        staff.Address = reader["Address"].ToString();
                        staff.Gender = reader["Gender"].ToString();
                        staff.RoleId = reader["Title"].ToString();

                        if (DateTime.TryParseExact(reader["RegisteredDate"].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime registeredDate))
                        {
                            staff.RegisteredDate = registeredDate;
                        }

                        if (DateTime.TryParseExact(reader["UpdatedAt"].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime updatedAt))
                        {
                            staff.UpdatedAt = updatedAt;
                        }

                        staffList.Add(staff);
                    }
                }
            }

            return staffList;
        }


        // Example implementation of AddStaff method using stored procedure
        public void AddStaff(StaffM staff)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("InsertStaff", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_Name", staff.Name);
                cmd.Parameters.AddWithValue("p_Telephone", staff.Telephone);
                cmd.Parameters.AddWithValue("p_Address", staff.Address);
                cmd.Parameters.AddWithValue("p_Gender", staff.Gender);
                cmd.Parameters.AddWithValue("p_role_id", staff.RoleId);
               

                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateStaff(StaffM staff)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("UpdateStaff", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_Id", staff.Id);
                cmd.Parameters.AddWithValue("p_Name", staff.Name);
                cmd.Parameters.AddWithValue("p_Telephone", staff.Telephone);
                cmd.Parameters.AddWithValue("p_Address", staff.Address);
                cmd.Parameters.AddWithValue("p_Gender", staff.Gender);
                cmd.Parameters.AddWithValue("p_role_id", staff.RoleId);
               

                cmd.ExecuteNonQuery();
            }
        }
       
        public void DeleteStaff(int id)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("DeleteStaff", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Id", id);

                cmd.ExecuteNonQuery();
            }
        }
        public StaffM GetStaffById(int id)
        {
            StaffM staff = null;
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("GetStaffById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        staff = new StaffM();
                        staff.Id = Convert.ToInt32(reader["Id"]);
                        staff.Name = reader["Name"].ToString();
                        staff.Telephone = reader["Telephone"].ToString();
                        staff.Address = reader["Address"].ToString();
                        staff.Gender = reader["Gender"].ToString();
                        staff.RoleId = reader["Title"].ToString();
                        //staff.RegisteredDate = Convert.ToDateTime(reader["RegisteredDate"].ToString());
                       // staff.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                    }
                }
            }
            return staff;
        }
    }
}
