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
            this.enemies = new List<Enemy>();
            // Lisää vihollinen liestaan (Nimi, kuvaus, positio, merkki, väri, max healt, damage, level)
            this.enemies.Add(new Enemy("juoppo", "melkonen juoppo", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar1, ConsoleColor.Yellow, 100, 10, 1));
            this.enemies.Add(new Enemy("piilojuoppo", "Juo salaa.. hyi!", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar2, ConsoleColor.Cyan, 100, 5, 1));
            this.enemies.Add(new Enemy("rapajuoppo", "Ei mitään toivoa", new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar3, ConsoleColor.DarkYellow, 100, 3, 1));

        }

        public List<Enemy> GetEnemyListByLevel(int lvl, int howMany)
        {
            int count = 0;
            var randomizedList = this.enemies.OrderBy(item => rand.Next());
            List<Enemy> enemies = new List<Enemy>();
            foreach(Enemy enemy in randomizedList)
            {
                if(enemy.Level == lvl && count < howMany)
                {
                    enemies.Add(enemy);
                    count++;
                }
            }
            return enemies;
        }


        public Enemy GetEnemyByLevel(int lvl)
        {
            var randomizedList = this.enemies.OrderBy(item => rand.Next());
            Enemy enemy1 = null;
            foreach(Enemy enemy in randomizedList)
            {
                if(enemy.Level == lvl)
                {
                    enemy1 = enemy;
                    break;
                }
                
            }
            return enemy1;

        }


    }

}
