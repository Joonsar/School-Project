//using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;

namespace School_Project
{
    public class DatabaseTest
    {
        private readonly string connectionString;

        public DatabaseTest()
        {
            connectionString = PW.cString;
        }

        public void UploadToServer(string jsonString)
        {
            try
            {
                var connection = new MySqlConnection(connectionString);
                connection.Open();
                string sql = "INSERT INTO GameStats (GameStats, Name, Level, Score) VALUES (@value1, @value2, @value3, @value4)";
                MySqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@value1", jsonString);
                command.Parameters.AddWithValue("@value2", GameController.Instance.GameStats.PlayerName);
                command.Parameters.AddWithValue("@value3", GameController.Instance.Player.Level);
                command.Parameters.AddWithValue("@value4", GameController.Instance.GameStats.Scores);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Virhe yhdistäessä tietokantaan: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Virhe datan lähettämisessä tietokantaan: " + ex.Message);
            }
        }
    }
}