

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
        private GameController gc = GameController.Instance;

        Tuple<Position, char> LastPosition;

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }

        public Enemy(string name, string description, Position pos, char mark, ConsoleColor color) : base(name, description, pos, mark, color)
        {
            LastPosition = new Tuple<Position, char>(pos, gc.Map.Mapping[Pos.X, Pos.Y]);
        }

        public override void MoveEntity(Position movePos)
        {
            
            //Console.Write(this.Name + " " + moveX + moveY);

            if(gc.Map.IsPositionValid(Pos.X + movePos.X, Pos.Y + movePos.Y)) {
                Pos.X += movePos.X;
                Pos.Y += movePos.Y;
                gc.screen.WriteAtPosition(LastPosition.Item1, LastPosition.Item2);
                LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.Map.Mapping[Pos.X, Pos.Y]);
            }
        }
    }
}
