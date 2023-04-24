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
        private List<Entity> EnemiesKilled;
        private List<Entity> ItemsCollected;

        private List<String> enemiesNames;
        private List<String> itemNames;

        private string enemies;
        private string items;

        private int DamageDealt;

        private int DamageTaken;

        private string PlayerName;
        private int PlayerLevel;

        private int MapLevel;
        private string db;

        private int Scores;
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
                 Nimi TEXT, Pisteet INTEGER, Pelaajantaso INTEGER, Kartantaso INTEGER, Tehtyvahinko INTEGER, Otettuvahinko INTEGER, Tapetutviholliset TEXT, Juodutpullot TEXT)";
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
            this.getNamesForEntities();
            var connection = new SqliteConnection($"Data Source ={this.db}");
            connection.Open();
            string insertString = $"INSERT INTO HighScores (Nimi, Pisteet, Pelaajantaso, Kartantaso, Tehtyvahinko, Otettuvahinko, Tapetutviholliset, Juodutpullot) Values (@PlayerName, @Scores, @PlayerLevel, @MapLevel, @DamageDealt, @DamageTaken, @EnemiesKilled,  @ItemsCollected)";
            var cmd = new SqliteCommand(insertString, connection);
            cmd.Parameters.AddWithValue("@PlayerName", this.PlayerName);
            cmd.Parameters.AddWithValue("@Scores", this.Scores);
            cmd.Parameters.AddWithValue("@PlayerLevel", this.PlayerLevel);
            cmd.Parameters.AddWithValue("@MapLevel", this.MapLevel);
            cmd.Parameters.AddWithValue("@DamageDealt", this.DamageDealt);
            cmd.Parameters.AddWithValue("@DamageTaken", this.DamageTaken);
            cmd.Parameters.AddWithValue("@EnemiesKilled", this.enemies);
            cmd.Parameters.AddWithValue("@ItemsCollected", this.items);
            PlayerID = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
        }

        public void GetData(GameStats gs)
        {
            this.EnemiesKilled = gs.EnemiesKilled;
            this.ItemsCollected = gs.ItemsCollected;
            this.MapLevel = gs.MapLevel;
            this.PlayerLevel = gs.PlayerLevel;
            this.DamageDealt = gs.DamageDealt;
            this.DamageTaken = gs.DamageTaken;
            this.PlayerName = gs.PlayerName;
            this.Scores = gs.Scores;
        }

        public void PrintData()
        {
            Console.WriteLine();
            Console.WriteLine();
            String lines = new String('-', Console.LargestWindowWidth);
            Console.WriteLine($"{"Nimi",-15}{"Pisteet",-15}{"Tehty vahinko",-25}{"Otettu vahinko",-25}{"Tapetut viholliset",-25}{"Juodut pullot",-25}");
            var connection = new SqliteConnection($"Data Source ={this.db}");
            connection.Open();
            string sql = "SELECT * FROM HighScores";
            var cmd = new SqliteCommand(sql, connection);
            using (SqliteDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Console.WriteLine(lines);
                    string[] enemies = rdr.GetString(7).Split(",");
                    string[] items = rdr.GetString(8).Split(",");
                     
                    Console.WriteLine($"{rdr.GetString(1),-15}{rdr.GetString(2),-15}{rdr.GetString(5),-25}{rdr.GetString(6),-25}{enemies.Length,-25}{items.Length,-25}");
                }
                Console.WriteLine(lines);
                rdr.Close();
            }
        }

        private void getNamesForEntities()
        {
            this.enemiesNames = new List<String>();
            foreach (Entity e in EnemiesKilled)
            {
                enemiesNames.Add(e.Name);
            }

            this.itemNames = new List<String>();
            foreach (Entity i in ItemsCollected)
            {
                itemNames.Add(i.Name);
            }
            this.enemies = string.Join(",", enemiesNames);
            this.items = string.Join(",", itemNames);
        }
    }
}