using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace School_Project
{
    public class DataBase
    {
        private List<Entity> EnemiesKilled;
        private List<Entity> ItemsCollected;

        private int DamageDealt;

        private int DamageTaken;

        private string PlayerName;
        private int PlayerLevel;

        private int MapLevel;
        public DataBase db;
        public DataBase()
        {
            this.EnemiesKilled = GameStats.EnemiesKilled;


        }


        private void CreateDatabase()
        {
            try
            {
                string createDb = @"CREATE TABLE IF NOT EXISTS HighScores (
                PlayerId INTEGER PRIMARY KEY AUTOINCREMENT,
                 Nimi TEXT, Taso INTEGER, Tehtyvahinko INTEGER, Otettuvahinko INTEGER, Tapetutviholliset INTEGER, Juodutpullot INTEGER)";
                var connection = new SqliteConnection("database.db");
                connection.Open();
                var command =new SqliteCommand(createDb, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void SaveToDatabase(string dbName, string PlayerName, int playerLevel, int mapLevel, int damageDealt, int damageTaken, List<Enemy> enemiesKilled, List<Item> itemsCollected)
        {
            //test
            var connection = new SqliteConnection(dbName);
            connection.Open();
            string insertString = $"INSERT INTO HighScores (PlayerID, GameStats) Values (@PlayerID, @GameStats)";
            var cmd = new SqliteCommand(insertString, connection);
            cmd.Parameters.AddWithValue("@PlayerID", 1);
            cmd.Parameters.AddWithValue("@Gamestats", jsonString);
            cmd.ExecuteNonQuery();
            connection.Close();
        }


    }
}
