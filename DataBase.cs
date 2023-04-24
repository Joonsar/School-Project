using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;

namespace School_Project
{
    public class DataBase
    {
        private string EnemiesKilled;
        private string ItemsCollected;

        private int DamageDealt;

        private int DamageTaken;

        private string PlayerName;
        private int PlayerLevel;

        private int MapLevel;
        private string db;

        public int PlayerID { get; set; }

        public DataBase()
        {
            this.db = "database.db";
        }

        public void CreateDatabase()
        {
            try
            {
                string createDb = @"CREATE TABLE IF NOT EXISTS HighScores (
                PlayerId INTEGER PRIMARY KEY AUTOINCREMENT,
                 Nimi TEXT, Pelaajantaso INTEGER, Kartantaso INTEGER, Tehtyvahinko INTEGER, Otettuvahinko INTEGER, Tapetutviholliset TEXT, Juodutpullot TEXT)";
                var connection = new SqliteConnection($"Data Source ={this.db}");
                connection.Open();
                var command = new SqliteCommand(createDb, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }

        public void SaveToDatabase()
        {
            //test
            var connection = new SqliteConnection($"Data Source ={this.db}");
            connection.Open();
            string insertString = $"INSERT INTO HighScores (Nimi, Pelaajantaso, Kartantaso, Tehtyvahinko, Otettuvahinko, Tapetutviholliset, Juodutpullot) Values (@PlayerName, @PlayerLevel, @MapLevel, @DamageDealt, @DamageTaken, @EnemiesKilled,  @ItemsCollected)";
            var cmd = new SqliteCommand(insertString, connection);
            cmd.Parameters.AddWithValue("@PlayerName", this.PlayerName);
            cmd.Parameters.AddWithValue("@PlayerLevel", this.PlayerLevel);
            cmd.Parameters.AddWithValue("@MapLevel", this.MapLevel);
            cmd.Parameters.AddWithValue("@DamageDealt", this.DamageDealt);
            cmd.Parameters.AddWithValue("@DamageTaken", this.DamageTaken);
            cmd.Parameters.AddWithValue("@EnemiesKilled", this.EnemiesKilled);
            cmd.Parameters.AddWithValue("@ItemsCollected", this.ItemsCollected);
            PlayerID = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
        }

        public void GetData(GameStats gs)
        {
            this.EnemiesKilled = String.Join(",", gs.EnemiesKilled);
            this.ItemsCollected = String.Join(",", gs.ItemsCollected);
            this.MapLevel = gs.MapLevel;
            this.PlayerLevel = gs.PlayerLevel;
            this.DamageDealt = gs.DamageDealt;
            this.DamageTaken = gs.DamageTaken;
            this.PlayerName = gs.PlayerName;
        }

        public void PrintData()
        {
            var connection = new SqliteConnection($"Data Source ={this.db}");
            connection.Open();
            string sql = "SELECT * FROM HighScores";
            var cmd = new SqliteCommand(sql, connection);
            using (SqliteDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Console.WriteLine(rdr.GetString(0) + " " + rdr.GetString(1));
                }
                rdr.Close();
            }
        }
    }
}