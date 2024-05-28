using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Task4Menu.Models;

namespace Task4Menu.Queries
{
    public class FarmDbHelper
    {
        private static string connectionString = @"Data Source=C:\Users\canel\OneDrive\Pictures\Task4\Task4\Database\FarmDataOriginal.db;";

        // Method to get all cows from the database
        public static List<Cow> GetAllCows()
        {
            List<Cow> cows = new List<Cow>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Cow;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cows.Add(new Cow
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Cost = decimal.Parse(reader["Cost"].ToString()),
                                Weight = decimal.Parse(reader["Weight"].ToString()),
                                Colour = reader["Colour"].ToString(),
                                Milk = decimal.Parse(reader["Milk"].ToString())
                            });
                        }
                    }
                }
                connection.Close();
            }
            return cows;
        }
        public static List<Sheep> GetAllSheep()
        {
            List<Sheep> sheep = new List<Sheep>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Sheep;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sheep.Add(new Sheep
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Cost = decimal.Parse(reader["Cost"].ToString()),
                                Weight = decimal.Parse(reader["Weight"].ToString()),
                                Colour = reader["Colour"].ToString(),
                                Wool = decimal.Parse(reader["Wool"].ToString())
                            });
                        }
                    }
                }
                connection.Close();
            }
            return sheep;

        }
        public static void InsertCow(decimal cost, decimal weight, string colour, decimal milk)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Cow (Cost, Weight, Colour, Milk) VALUES (@Cost, @Weight, @Colour, @Milk);";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cost", cost);
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.Parameters.AddWithValue("@Colour", colour);
                    command.Parameters.AddWithValue("@Milk", milk);
                    command.ExecuteNonQuery();
                }
            }
        }


        // Insert a new Sheep into the database
        public static void InsertSheep(decimal cost, decimal weight, string colour, decimal wool)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Sheep (Cost, Weight, Colour, Wool) VALUES (@Cost, @Weight, @Colour, @Wool);";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cost", cost);
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.Parameters.AddWithValue("@Colour", colour);
                    command.Parameters.AddWithValue("@Wool", wool);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteRecord(int id, string livestockType)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string table = livestockType.Equals("cow", StringComparison.OrdinalIgnoreCase) ? "Cow" : "Sheep";
                string query = $"DELETE FROM {table} WHERE Id = @Id;";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine($"Record deleted: {table} with ID {id}");
                    }
                    else
                    {
                        Console.WriteLine($"No {table} record found with ID {id}.");
                    }
                }
            }
        }

        // Method to update a Cow or Sheep record in the database
        public static void UpdateRecord(int id, decimal cost, decimal weight, string colour, decimal milkOrWool, string livestockType)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string table = livestockType.Equals("cow", StringComparison.OrdinalIgnoreCase) ? "Cow" : "Sheep";
                string productColumn = livestockType.Equals("cow", StringComparison.OrdinalIgnoreCase) ? "Milk" : "Wool";
                string query = $"UPDATE {table} SET Cost = @Cost, Weight = @Weight, Colour = @Colour, {productColumn} = @Product WHERE Id = @Id;";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cost", cost);
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.Parameters.AddWithValue("@Colour", colour);
                    command.Parameters.AddWithValue($"@Product", milkOrWool);
                    command.Parameters.AddWithValue("@Id", id);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine($"Record updated: {table} with ID {id}");
                    }
                    else
                    {
                        Console.WriteLine($"No {table} record found with ID {id}.");
                    }
                }
            }
        }
        public int GetLastInsertedId()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT last_insert_rowid();", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
    }
}
