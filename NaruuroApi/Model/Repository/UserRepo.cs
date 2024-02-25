using NaruuroApi.Model.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace NaruuroApi.Model.Repository
{
    public class UserRepo : IUser
    {
        private readonly IConfiguration _configuration;

        public UserRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public List<UserM> GetAllUsers()
        {
            List<UserM> userList = new List<UserM>();

            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {

                con.Open();
                MySqlCommand cmd = new MySqlCommand("Getuser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserM user = new UserM();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.Stafid = reader["StaffName"].ToString();
                        user.Roleid = reader["RoleTitle"].ToString();
                        user.UserName = reader["username"].ToString();
                        user.Password = reader["Password"].ToString();
                        //user.Created_at = Convert.ToDateTime(reader["Created_at"]);
                        //user.Updated_at = Convert.ToDateTime(reader["Updated_at"]);

                        userList.Add(user);
                    }
                }
            }

            return userList;
        }

        public void Add(UserM user)
        {
            // Check for duplicate user before attempting to add
            if (IsDuplicateUser(user))
            {
                // Handle duplicate user scenario (e.g., display error message)
                Console.WriteLine("Duplicate user detected. Please provide unique data.");
                // You can also throw an exception here if you want to stop execution
                return;
            }

            // Continue with adding the user
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("InsertuserM", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_StaffID", user.Stafid);
                cmd.Parameters.AddWithValue("p_role_id", user.Roleid);
                cmd.Parameters.AddWithValue("p_username", user.UserName);
                cmd.Parameters.AddWithValue("p_Password", user.Password);

                cmd.ExecuteNonQuery();
            }
        }
        private bool IsDuplicateUser(UserM user)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE StaffID = @StaffID", con);

                cmd.Parameters.AddWithValue("@StaffID", user.Stafid);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }


        public void UpdateUsers(UserM user)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("UpdateUserM", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_Id", user.Id);
                cmd.Parameters.AddWithValue("p_StaffID", user.Stafid);
                cmd.Parameters.AddWithValue("p_role_id", user.Roleid);
                cmd.Parameters.AddWithValue("p_username", user.UserName);
                cmd.Parameters.AddWithValue("p_Password", user.Password);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUsers(int id)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("DeleteUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("userId", id);

                cmd.ExecuteNonQuery();
            }
        }

        public UserM GetUsersbyid(int id)
        {
            UserM user = null;

            using (MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("GetUserbyId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("userId", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserM();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.Stafid = reader["StaffName"].ToString();
                        user.Roleid = reader["RoleTitle"].ToString();
                        user.UserName = reader["username"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.Created_at = Convert.ToDateTime(reader["Created_at"]);
                      //  user.Updated_at = Convert.ToDateTime(reader["Updated_at"]);
                    }
                }
            }

            return user;
        }
    }
}

