//using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace School_Project
{
    public class DatabaseTest
    {
        private string connectionString = "server=henkka.sytes.net;user id=juoppopeli;password=Juoppopeli12kossu14;database=HighScores";

        public DatabaseTest()
        {
        }

        public void SaveToDatabase(string dbName, string jsonString)
        {
            //test
            var connection = new SqliteConnection($"Data Source ={dbName}");
            connection.Open();
            string insertString = $"INSERT INTO HighScores (PlayerID, GameStats) Values (@PlayerID, @GameStats)";
            var cmd = new SqliteCommand(insertString, connection);
            cmd.Parameters.AddWithValue("@PlayerID", 1);
            cmd.Parameters.AddWithValue("@Gamestats", jsonString);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void CreateDatabase(string dbName)
        {
            string createTableSql = @"CREATE TABLE IF NOT EXISTS HighScores (
            PlayerId INTEGER PRIMARY KEY AUTOINCREMENT,
            GameStats TEXT NOT NULL)";
            var connection = new SqliteConnection($"Data Source ={dbName}");
            connection.Open();
            var command = new SqliteCommand(createTableSql, connection);
            command.ExecuteNonQuery();
        }

        public void UploadToServer(string jsonString)
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            string sql = "INSERT INTO GameStats (GameStats, Name, Level) VALUES (@value1, @value2, @value3)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@value1", jsonString);
            command.Parameters.AddWithValue("@value2", GameController.Instance.GameStats.PlayerName);
            command.Parameters.AddWithValue("@value3", GameController.Instance.GameStats.PlayerLevel);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}