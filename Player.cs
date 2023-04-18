using System;
using System.Collections.Generic;

namespace School_Project
{
    //itseasiassa miksei me käytettäs vector2 positionissa? Tosin tääkin toimii, mutta jos meillä olis esim vector2 position, niin siihen vois lisätä position += new vector2(1,0) jne
    public class Player
    {
        //nää toimii, mutta yleinen nimikäytäntö on että nää alkaa isolla kirjaimella (turhaa höpötystä, mutta näin ne yleensä tehdään) :)
        //eli kun kutsutaan sit vaikka gc.Player.Name; gc.Player.Health;
        public string Name { get; private set; }

        public int HealthValue { get; private set; }
        public int HitPoints { get; private set; }
        public int ExpPoints { get; private set; }

        public int MaxHp { get; set; }

        public int Level { get; private set; }

        public List<Entity> Inventory { get; set; }

        public Map map;
        public ConsoleColor Color { get; private set; }

        public char Mark { get; set; }

        private GameController gc = GameController.Instance;

        //private Tuple<Position, char> LastPosition;

        private MapObject LastPosition;

        public Position Pos { get; set; }

        private Random rand = new Random();

        public Player(string name, int healthValue, int hitPoints)
        {
            Inventory = new List<Entity>();
            Level = 1;
            Name = name;
            HealthValue = healthValue;
            HitPoints = hitPoints;
            MaxHp = hitPoints;
            ExpPoints = 0;
            Pos = new Position(10, 10);

            //LastPosition = new Tuple<Position, char>(new Position(10, 10), gc.Map.Mapping[10, 10]);

            SetPlayerLastPosition();

            Color = ConsoleColor.Green;
            Mark = '@';
        }

        public string GetStats()
        {
            return $"{Name} - Hp: {HitPoints}/{MaxHp} Exp: {ExpPoints} Lvl: {Level} T: {gc.Turn} L: {gc.Level}";
        }

        public void MovePlayerToPosition(Position pos)
        {
            Pos = pos;
        }

        public void SetPlayerLastPosition()
        {
            //LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.Map.Mapping[Pos.X, Pos.Y].Mark);
            LastPosition = gc.Map.Mapping[Pos.X, Pos.Y];
        }

        public void MovePlayer(int x, int y)
        {
            var oldPos = new Position(Pos.X, Pos.Y);
            if (gc.Map.IsEnemyAtPosition(Pos.X + x, Pos.Y + y) != null)
            {
                Attack(gc.Map.IsEnemyAtPosition(Pos.X + x, Pos.Y + y));
            }

            if (gc.Map.IsItemAtPosition(Pos.X + x, Pos.Y + y) != null)
            {
                var item = gc.Map.IsItemAtPosition(Pos.X + x, Pos.Y + y);
                gc.MessageLog.AddMessage($"Poimit maasta {item.Name} {item.Description}");
                Inventory.Add(item);
                gc.Map.entities.Remove(item);
            }
            if (gc.Map.IsPositionValid(Pos.X + x, Pos.Y + y) && gc.Map.IsEnemyAtPosition(Pos.X + x, Pos.Y + y) == null)
            {
                Pos.X += x;
                Pos.Y += y;
                gc.screen.WriteAtPosition(oldPos, LastPosition.Mark, LastPosition.Color);
                gc.screen.PrintPlayer();
                //gc.MessageLog.AddMessage($"{Name} moves to {Pos.X}.{Pos.Y}");
                LastPosition = gc.Map.Mapping[Pos.X, Pos.Y];

                if (gc.Map.Mapping[Pos.X, Pos.Y] == Map.stairsDown)
                {
                    gc.ChangeLevel(1);
                }
                else if (gc.Map.Mapping[Pos.X, Pos.Y] == Map.stairsUp)
                {
                    gc.ChangeLevel(-1);
                }
            }
        }

        private void Attack(Entity e)
        {
            var hitChance = rand.Next(1, 100);
            if (hitChance > 50)
            {
                var damage = (50 + rand.Next(1, 50) * Level);
                e.TakeDamage(damage);
                gc.GameStats.DamageDealt += damage;
                //gc.MessageLog.AddMessage($"{Name} Hits {e.Name} for {damage}.");
            }
            else
            {
                gc.MessageLog.AddMessage($"{Name} misses {e.Name}");
            }
        }

        public bool CheckIfPlayerCollidesWithStairs()
        {
            {
                if (gc.Map.Mapping[Pos.X, Pos.Y] == Map.stairsDown || gc.Map.Mapping[Pos.X, Pos.Y] == Map.stairsUp)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void AddExperience(int amount)
        {
            ExpPoints += amount;
            CheckLevelUp();
        }

        public void CheckLevelUp()
        {
            if (ExpPoints > Level * 100)
            {
                Level++;
                gc.MessageLog.AddMessage($"{Name} is now level {Level}");
                gc.screen.PrintPlayerStats();
            }
        }

        public void TakeDamage(int amount)
        {
            gc.GameStats.DamageTaken += amount;
            gc.MessageLog.AddMessage($"{Name} takes {amount} damage");
            HitPoints -= amount;
            gc.screen.PrintPlayerStats();
            CheckDeath();
        }

        public void CheckDeath()
        {
            if (HitPoints <= 0)
            {
                gc.MessageLog.AddMessage($"You die!");
                foreach (Entity e in gc.GameStats.EnemiesKilled)
                {
                    gc.running = false;
                    gc.MessageLog.AddMessage(e.Name);
                }
            }
        }
    }
}