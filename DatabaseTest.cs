//using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite;

namespace School_Project
{
    public class DatabaseTest
    {
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
    }
}