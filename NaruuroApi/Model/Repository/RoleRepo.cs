using NaruuroApi.Model.Interface;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;
using MySqlDataReader = MySql.Data.MySqlClient.MySqlDataReader;

namespace NaruuroApi.Model.Repository
{
    public class RoleRepo : IRole
    {
        private readonly IConfiguration _configuration;

        public RoleRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void AddRole(Role role)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                // Save a role using stored procedure
                MySqlCommand cmd = new MySqlCommand("InsertRole", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
              
                cmd.Parameters.AddWithValue("title_param", role.Title);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRole(int id)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                // Delete a role using stored procedure
                MySqlCommand cmd = new MySqlCommand("DeleteRole", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("role_id_param", id);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Role> GetAllRole()
        {
            List<Role> roles = new List<Role>();

            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {



                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM role", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var role = new Role
                        {
                            id = Convert.ToInt32(reader["role_id"]),
                            Title = Convert.ToString(reader["Title"]),
                        };

                        roles.Add(role);
                    }
                }
            }

            return roles;
        }

        public Role GetById(int id)
        {
            Role role = new Role();
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM role WHERE role_id=@id", con);
                cmd.Parameters.AddWithValue("id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    role.id = Convert.ToInt32(reader["role_id"]);
                    role.Title = reader["Title"].ToString();
                }
                reader.Close();
            }
            return role;
        }

        public void UpdateRole(Role role)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                // Update a role using stored procedure
                MySqlCommand cmd = new MySqlCommand("UpdateRole", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("role_id_param", role.id);
                cmd.Parameters.AddWithValue("new_title_param", role.Title);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
