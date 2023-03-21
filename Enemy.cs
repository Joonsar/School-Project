using System;

namespace School_Project
{
    public class Enemy : Entity
    {
        private Random rand = new Random();
        private GameController gc;

        private Tuple<Position, char> LastPosition;

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }

        public int Level { get; set; }

        public Enemy(string name, string description, Position pos, char mark, ConsoleColor color, int maxHealth, int damage) : base(name, description, pos, mark, color)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;
            gc = GameController.Instance;
            SetEnemyLastPosition();
        }

        public Enemy(string name, string description, Position pos, char mark, ConsoleColor color, int maxHealth, int damage, int level) : base(name, description, pos, mark, color)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;
            gc = GameController.Instance;
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
                gc.MessageLog.AddMessage($"{Name} moves to {Pos.X}.{Pos.Y}");
                //tulostetaan vihollinen liikkumisen jälkeen.
                gc.screen.PrintEnemy(this);
                //kirjoitetaan ruutuun mistä liikuttiin, sen edellinen merkki.
                gc.screen.WriteAtPosition(LastPosition.Item1, LastPosition.Item2);

                SetEnemyLastPosition();
            }
        }

        public void SetEnemyLastPosition()
        {
            LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.Map.Mapping[Pos.X, Pos.Y]);
        }
    }
}