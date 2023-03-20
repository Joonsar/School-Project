

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Enemy : Entity
    {

        private Random rand = new Random();
        private GameController gc;

        Tuple<Position, char> LastPosition;

        

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }

        public Enemy(string name, string description, Position pos, char mark, ConsoleColor color, int maxHealth, int damage) : base(name, description, pos, mark, color)
        {

            MaxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;
            gc = GameController.Instance;
            LastPosition = new Tuple<Position, char>(Pos, gc.Map.Mapping[Pos.X, Pos.Y]);

        }

        public override void MoveEntity(int x, int y)
        {

            var newPosX = Pos.X + x;
            var newPosY = Pos.Y + y;

            
            //tarkistetaan onko ruudussa johon yritetään liikkua seinä, toinen vihollinen tai pelaaja.. jos ei liikutetaan vihollista siihen ruutuun
            if (gc.Map.IsPositionValid(newPosX, newPosY) && gc.Map.IsEnemyAtPosition(newPosX, newPosY) == null && newPosX != gc.Player.Pos.X && newPosY != gc.Player.Pos.Y) {
                Pos.X = newPosX;
                Pos.Y = newPosY;
                //tulostetaan vihollinen liikkumisen jälkeen.
                gc.screen.PrintEnemy(this);
                //kirjoitetaan ruutuun mistä liikuttiin, sen edellinen merkki.
                gc.screen.WriteAtPosition(LastPosition.Item1, LastPosition.Item2);
                
                
                LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.Map.Mapping[Pos.X, Pos.Y]);
            }
        }
    }
}
