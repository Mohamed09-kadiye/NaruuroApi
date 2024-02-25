namespace NaruuroApi.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MySql.Data.MySqlClient;
    using NaruuroApi.Model.Interface;

    public class CustomerRepository : ICustomer
    {
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (MySqlConnection listconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                listconnection.Open();

                MySqlCommand command = new MySqlCommand("GetAllCustomers", listconnection);
                command.CommandType = CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(MapCustomerFromDataReader(reader));
                    }
                }
            }

            return customers;
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer? customer = null;

            using (MySqlConnection getconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                getconnection.Open();

                MySqlCommand command = new MySqlCommand("GetCustomerById", getconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@customerId", customerId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        customer = MapCustomerFromDataReader(reader);
                    }
                }
            }

            return customer;
        }
        public void AddCustomer(Customer customer)
        {
            using (MySqlConnection addconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                addconnection.Open();

                MySqlCommand command = new MySqlCommand("InsertCustomer", addconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@names", customer.Names);
                command.Parameters.AddWithValue("@gender", customer.Gender);
                command.Parameters.AddWithValue("@tell", customer.Tell);
                command.Parameters.AddWithValue("@address", customer.Address);
                command.Parameters.AddWithValue("@iD_Documents", customer.ID_Documents);
                command.Parameters.AddWithValue("@drivelinkid", customer.drivelinkid);

                command.Parameters.AddWithValue("@registeredBy", customer.RegisteredBy);


                command.ExecuteNonQuery();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (MySqlConnection updateconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                updateconnection.Open();

                MySqlCommand command = new MySqlCommand("UpdateCustomer", updateconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@customerId", customer.ID);
                command.Parameters.AddWithValue("@newName", customer.Name);
                command.Parameters.AddWithValue("@newNames", customer.Names);
                command.Parameters.AddWithValue("@newGender", customer.Gender);
                command.Parameters.AddWithValue("@newTell", customer.Tell);
                command.Parameters.AddWithValue("@newAddress", customer.Address);
                command.Parameters.AddWithValue("@newID_Documents", customer.ID_Documents);
                command.Parameters.AddWithValue("@drivelinkid", customer.drivelinkid);


                command.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (MySqlConnection delconnection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                delconnection.Open();

                MySqlCommand command = new MySqlCommand("DeleteCustomer", delconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@customerId", customerId);

                command.ExecuteNonQuery();
            }
        }

        private Customer MapCustomerFromDataReader(MySqlDataReader reader)
        {
        
            return new Customer()
            {
                ID = Convert.ToInt32(reader["ID"]),
                Name = reader["Name"].ToString(), 
                Names = reader["Names"].ToString(),
                Gender = reader["gender"].ToString(),
                Tell = reader["Tell"].ToString(),
                Address = reader["Address"].ToString(),
                ID_Documents = reader["ID_Documents"].ToString(),
                drivelinkid = reader["drivelinkid"].ToString(),
                RegisteredDate = Convert.ToDateTime(reader["RegisteredDate"]),
                Updated = Convert.ToDateTime(reader["updated"]),
                RegisteredBy = reader["registeredBy"].ToString(),
            };
        }

    }

}
