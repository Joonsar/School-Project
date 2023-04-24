using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace School_Project
{
    public class Enemy : Entity
    {
        private Random rand = new Random();

        [XmlIgnore]
        private GameController gc = GameController.Instance;

        private Tuple<Position, Char> LastPosition;

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }

        public int Level { get; set; }

        public Enemy(string name, string description, Position pos, char mark, ConsoleColor color, int maxHealth, int damage) : base(name, description, pos, mark, color)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;

            SetEnemyLastPosition();
        }

        [JsonConstructor]
        public Enemy(string name, string description, Position pos, char mark, ConsoleColor color, int maxHealth, int damage, int level) : base(name, description, pos, mark, color)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;

            SetEnemyLastPosition();
            Level = level;
        }

        public override void MoveEntity(int x, int y)
        {
            var newPosX = Pos.X + x;
            var newPosY = Pos.Y + y;

            //tarkistetaan onko ruudussa johon yritetään liikkua seinä, toinen vihollinen tai pelaaja.. jos ei liikutetaan vihollista siihen ruutuun
            if (gc.Map.IsPositionValid(newPosX, newPosY) && gc.Map.IsEnemyAtPosition(newPosX, newPosY) == null && (newPosX != gc.Player.Pos.X || newPosY != gc.Player.Pos.Y))
            {
                Pos.X = newPosX;
                Pos.Y = newPosY;
                //gc.MessageLog.AddMessage($"{Name} moves to {Pos.X}.{Pos.Y}");
                //tulostetaan vihollinen liikkumisen jälkeen.
                gc.screen.PrintEnemy(this);
                //kirjoitetaan ruutuun mistä liikuttiin, sen edellinen merkki.
                gc.screen.WriteAtPosition(LastPosition.Item1, LastPosition.Item2);
                // gc.MessageLog.AddMessage($"{Name} moves to {Pos.X}.{Pos.Y}");
                // gc.MessageLog.AddMessage($"{Name} on {Description}");

                SetEnemyLastPosition();
            }
            else if (newPosX == gc.Player.Pos.X && newPosY == gc.Player.Pos.Y)
            {
                var hitChance = rand.Next(0, 100);
                if (hitChance > 50)
                {
                    gc.MessageLog.AddMessage($"{Name} hits {gc.Player.Name}");
                    gc.Player.TakeDamage(Damage);
                }
                else
                {
                    gc.MessageLog.AddMessage($"{Name} misses {gc.Player.Name}");
                }
            }
        }

        public void SetEnemyLastPosition()
        {
            LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.Map.Mapping[Pos.X, Pos.Y].Mark);
        }

        public override void TakeDamage(int v)
        {
            Health -= v;
            switch (v)
            {
                case int n when n < 70:
                    gc.MessageLog.AddMessage($"Annat pikku läpsyn naamalle. {this.Name} ottaa {v} vahinkoa ({Health}/{MaxHealth})");
                    break;

                case int n when n > 70 && n < 90:
                    gc.MessageLog.AddMessage($"Potku kulkusille osoittautuu tehokkaaks. {this.Name} {v} vahinkoa. ({Health}/{MaxHealth})");
                    break;

                case int n when n > 90 && n < 120:
                    gc.MessageLog.AddMessage($"Uskomaton humalainen saksipotku. {this.Name} ottaa {v} vahikoa ({Health}/{MaxHealth})");
                    break;

                default:
                    gc.MessageLog.AddMessage($"Tökkäät silmään. {this.Name} ottaa {v} vahikoa ({Health}/{MaxHealth})");
                    break;
            }

            CheckDeath();
        }

        private void CheckDeath()
        {
            if (Health <= 0)
            {
                gc.MessageLog.AddMessage($"{Name} kaatuu maahan. Sinut valtaa voittajafiilis.");
                gc.GameStats.EnemiesKilled.Add(this);
                gc.Player.AddExperience((Level + 1) * 50);
                gc.Map.entities.Remove(this);
            }
        }

        public override void Update()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int speed = rand.Next(1, 10);
            int moveX = 0;
            int moveY = 0;
            int xDiff = gc.Player.Pos.X - Pos.X;
            int yDiff = gc.Player.Pos.Y - Pos.Y;
            double distance = Math.Sqrt(yDiff * yDiff + xDiff * xDiff);

            if (distance > 6)
            {
                moveX = rand.Next(-1, 2);
                moveY = rand.Next(-1, 2);
            }
            else if (speed > 3)
            {
                if (Math.Abs(xDiff) > Math.Abs(yDiff))
                {
                    moveX = xDiff > 0 ? 1 : -1;
                }
                else
                {
                    moveY = yDiff > 0 ? 1 : -1;
                }
            }
            MoveEntity(moveX, moveY);
        }
    }
}