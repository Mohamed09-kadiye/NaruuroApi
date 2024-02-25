using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using NaruuroApi.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Globalization;

namespace NaruuroApi.Model.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IConfiguration _configuration;

        public ExpenseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeleteExpense(int expenseId)
        {
            throw new NotImplementedException();
        }

        public List<Expense> GetAllExpenses()
        {
            List<Expense> expenses = new List<Expense>();

            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("GetAllExpenses", connection);
                command.CommandType = CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expenses.Add(MapExpenseFromDataReader(reader));
                    }
                }
            }

            return expenses;
        }
        public Expense GetExpenseById(int expenseId)
        {
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("GetExpenseById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_id", expenseId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapExpenseFromDataReader(reader);
                    }
                    return null; // Expense not found
                }
            }
        }

        public void InsertExpense(Expense expense)
        {
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("InsertExpense", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_name", expense.Name);
                command.Parameters.AddWithValue("@p_amount", expense.Amount);
                command.Parameters.AddWithValue("@p_description", expense.Description);
                command.Parameters.AddWithValue("@p_category_id", expense.CategoryId);
                command.Parameters.AddWithValue("@p_user_id", expense.userid);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateExpense(Expense expense)
        {
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("ConStr")))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("UpdateExpense", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_id", expense.Id);
                command.Parameters.AddWithValue("@p_name", expense.Name);
                command.Parameters.AddWithValue("@p_amount", expense.Amount);
                command.Parameters.AddWithValue("@p_description", expense.Description);
                command.Parameters.AddWithValue("@p_category_id", expense.CategoryId);
                command.ExecuteNonQuery();
            }
        }

        private Expense MapExpenseFromDataReader(MySqlDataReader reader)
        {
            DateTime? parsedDate = null;
            DateTime? parsedUpdated = null;

            if (!reader.IsDBNull(reader.GetOrdinal("date")))
            {
                DateTime.TryParseExact(reader.GetString(reader.GetOrdinal("date")), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempDate);
                parsedDate = tempDate;
            }

            if (!reader.IsDBNull(reader.GetOrdinal("updated")))
            {
                DateTime.TryParseExact(reader.GetString(reader.GetOrdinal("updated")), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempUpdated);
                parsedUpdated = tempUpdated;
            }

            return new Expense()
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = reader["name"].ToString(),
                Amount = Convert.ToDecimal(reader["amount"]),
                //CategoryId = Convert.ToInt32(reader["category_id"]),
                Description = reader["description"].ToString(),
                //userid = Convert.ToInt32(reader["uid"]),
                Date = parsedDate,
                updated = parsedUpdated,
                 CategoryId = reader["category_name"].ToString(), // Assuming you have a property for CategoryName
                userid = reader["user_name"].ToString() // Assuming you have a property for UserName
            };
       
        }




        // Map other properties here

    }
}
