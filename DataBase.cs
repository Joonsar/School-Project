using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Data.Sqlite;

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
                connection.Close();
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
            cmd.ExecuteNonQuery();
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

        public void PrintData(string sql, string name)
        {
            Console.WriteLine();
            Console.WriteLine();
            string lines = new String('-', Console.LargestWindowWidth);
            Console.WriteLine($"{"Sija",-7}{"Nimi",-15}{"Pisteet",-15}{"Tehty vahinko",-20}{"Otettu vahinko",-20}{"Tapetut viholliset",-25}{"Juodut pullot",-20}{"Id",-5}");
            var connection = new SqliteConnection($"Data Source ={this.db}");
            connection.Open();
            var cmd = new SqliteCommand(sql, connection);
            if (name != null)
            {
                cmd.Parameters.AddWithValue("@Nimi", name);
            }
            using (SqliteDataReader rdr = cmd.ExecuteReader())
            {
                int sija = 1;
                int i = 0;
                while (rdr.Read())
                {
                    Console.WriteLine(lines);
                    string[] enemies = rdr.GetString(7).Split(",");
                    string[] items = rdr.GetString(8).Split(",");
                    if (i == 0)
                    {
                        PlayerID = rdr.GetInt32(0);
                        i++;
                    }
                    Console.WriteLine($"{sija,-7}{rdr.GetString(1),-15}{rdr.GetString(2),-15}{rdr.GetString(5),-20}{rdr.GetString(6),-20}{enemies.Length,-25}{items.Length,-20}{rdr.GetInt32(0),-5}");
                    sija++;
                }
                Console.WriteLine(lines);
                rdr.Close();
            }
            connection.Close();
        }

        private void getNamesForEntities()
        {
            this.enemiesNames = new List<String>();
            foreach (Entity e in EnemiesKilled)
            {
                enemiesNames.Add(e.Name + ";" + e.Description);
            }

            this.itemNames = new List<String>();
            foreach (Entity i in ItemsCollected)
            {
                itemNames.Add(i.Name + ";" + i.Description);
            }
            this.enemies = string.Join(",", enemiesNames);
            this.items = string.Join(",", itemNames);
        }

        public void PrintPlayerStats(int id)
        {
            Console.WriteLine();
            Console.WriteLine();
            var connection = new SqliteConnection($"Data Source ={this.db}");
            connection.Open();
            string sql = "SELECT * FROM HighScores WHERE @PlayerID = PlayerID";
            var cmd = new SqliteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@PlayerID", id);
            using (SqliteDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Console.WriteLine("Nimi: " + rdr.GetString(1));
                    Console.WriteLine("Pisteet: " + rdr.GetString(2));
                    Console.WriteLine("Taso: " + rdr.GetString(3));
                    Console.WriteLine("Tehty vahinko: " + rdr.GetString(5));
                    Console.WriteLine("Otettu vahinko: " + rdr.GetString(6));
                    Console.WriteLine("Kuolit lähiön tasolla: " + rdr.GetString(4));
                    string s = "Tapetut viholliset: ";
                    string spaces = new string(' ', s.Length);
                    Console.WriteLine("Tapetut viholliset: ");
                    string[] enemies = rdr.GetString(7).Split(",");
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        Console.WriteLine(spaces + enemies[i].Replace(";", " - "), Console.ForegroundColor = ConsoleColor.Red);
                    }
                    string[] item = rdr.GetString(8).Split(",");
                    string juodut = "Juodut pullot: ";
                    spaces = new string(' ', juodut.Length);
                    Console.WriteLine(juodut, Console.ForegroundColor = ConsoleColor.Yellow);
                    for (int i = 0; i < item.Length; i++)
                    {
                        Console.WriteLine(spaces + item[i].Replace(";", " - "), Console.ForegroundColor = ConsoleColor.Green);
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                rdr.Close();
            }
            connection.Close();

        }

        public void ClearDatabase()
        {
            var connection = new SqliteConnection($"Data Source ={this.db}");
            connection.Open();
            string sql = "DELETE FROM HighScores";
            var cmd = new SqliteCommand(sql, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void PrintPlayerDataByName(String name)
        {
            string sql = "SELECT * FROM HighScores WHERE @Nimi = Nimi";
            this.PrintData(sql, name);

        }

        public void PrintAllData()
        {
            string sql = "SELECT * FROM HighScores ORDER BY Pisteet DESC";
            this.PrintData(sql, null);
        }

        public void PrintPlayerStatsWihtID(int id)
        {
            PrintPlayerStats(id);
        }
    }
}