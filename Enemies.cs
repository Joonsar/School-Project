using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Enemies
    {
        List<Enemy> enemies;
        Random rand =  new Random();
        string charString = "ABCDEFGHIJKLMNOPQRST!!#¤%&/()♀-N`↨-↨00Kdkoewjatfiheioahteaotih}cA";
       
        public Enemies()
        {
            this.enemies = new List<Enemy>();
            char randomChar1 = charString[rand.Next(0, charString.Length)];
            char randomChar2 = charString[rand.Next(0, charString.Length)];
            char randomChar3 = charString[rand.Next(0, charString.Length)];
            // Lisää vihollinen liestaan (Nimi, kuvaus, positio, merkki, väri, max healt, damage, level)
            //Lvl 0
            this.enemies.Add(new Enemy("Tissuttelija Tauno", "Pari sillon tällön", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar1, ConsoleColor.Yellow, 100, 10, 0));
            this.enemies.Add(new Enemy("Matti", "Saunakaljat mukana", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar2, ConsoleColor.Cyan, 100, 5, 0));
            this.enemies.Add(new Enemy("Seppo Sivistyneesti", "Muutamat aina maistuu", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar3, ConsoleColor.DarkYellow, 100, 3, 0));
            //Lvl 1
            this.enemies.Add(new Enemy("Junnu Jannu", "Koko vkl putkeen!", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar1, ConsoleColor.Yellow, 200, 10, 1));
            this.enemies.Add(new Enemy("Repa Duunari", "Sixpäkki duunin jälkee heti pöytää!", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar2, ConsoleColor.Cyan, 200, 5, 1));
            this.enemies.Add(new Enemy("Patu", "Eläkkeellä voi pari konjakkia ottaa!", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar3, ConsoleColor.DarkYellow, 200, 3, 1));
            //Lvl 2
            this.enemies.Add(new Enemy("juoppo", "melkonen juoppo", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar1, ConsoleColor.Yellow, 300, 10, 2));
            this.enemies.Add(new Enemy("piilojuoppo", "Juo salaa.. hyi!", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar2, ConsoleColor.Cyan, 300, 5, 2));
            this.enemies.Add(new Enemy("rapajuoppo", "Ei mitään toivoa", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar3, ConsoleColor.DarkYellow, 300, 3, 2));
            //Lvl 3
            this.enemies.Add(new Enemy("Semi pro", "Välillä huiliiki", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar1, ConsoleColor.Yellow, 400, 10, 3));
            this.enemies.Add(new Enemy("Roku", "Katkolta alkon kautta", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar2, ConsoleColor.Cyan, 400, 5, 3));
            this.enemies.Add(new Enemy("Seppo", "Välillä heilahtaa viikko kaks..", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar3, ConsoleColor.DarkYellow, 400, 3, 3));
            //Lvl 4
            this.enemies.Add(new Enemy("Pro", "Kaikki tauluu mitä löytyy", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar1, ConsoleColor.Yellow, 500, 10, 4));
            this.enemies.Add(new Enemy("Puiston jaska", "Kusen tuoksu hiipii nenää jo kaukaa", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar2, ConsoleColor.Cyan, 500, 5, 4));
            this.enemies.Add(new Enemy("Ammattilainen", "Täyspäivänen duuni pysyy tönössä..", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar3, ConsoleColor.DarkYellow, 500, 3, 4));

        }

        public List<Enemy> GetEnemyListByLevel(int lvl, int howMany)
        {
            int count = 0;
            var randomizedList = this.enemies.OrderBy(item => rand.Next());
            List<Enemy> enemies = new List<Enemy>();
            while (count < howMany)
            {
                if (lvl <= 4)
                {
                    foreach (Enemy enemy in randomizedList)
                    {
                        if (enemy.Level == lvl && count < howMany)
                        {
                            enemies.Add(enemy);
                            count++;
                        }
                    }
                }
                else
                {
                    foreach (Enemy enemy in randomizedList)
                    {
                        if (count < howMany)
                        {
                            int levelDifference = lvl - enemy.Level;
                            enemy.Level += levelDifference;
                            enemy.MaxHealth *= levelDifference;
                            enemy.Damage *= levelDifference;
                            enemy.Health *= levelDifference;
                            enemy.Description = "Romahti täydellisesti rappiolle!";
                            enemies.Add((enemy));
                            count++;
                        }
                    }
                }
            }
            return enemies;
        }


        public Enemy GetEnemyByLevel(int lvl)
        {
            var randomizedList = this.enemies.OrderBy(item => rand.Next());
            Enemy enemy1 = null;
            if (lvl <= 4)
            {
                foreach (Enemy enemy in randomizedList)
                {
                    if (enemy.Level == lvl)
                    {
                        enemy1 = enemy;
                        break;
                    }

                }
            }
            else
            {
                foreach (Enemy enemy in randomizedList)
                {
                    
                    int levelDifference = lvl - enemy.Level;
                    enemy.Level += levelDifference;
                    enemy.MaxHealth *= levelDifference;
                    enemy.Damage *= levelDifference;
                    enemy.Health *= levelDifference;
                    enemy.Description = "Romahti täydellisesti rappiolle!";
                    enemy1 = enemy;
                    break;
                }
            }
        return enemy1;

        }

    }

}
