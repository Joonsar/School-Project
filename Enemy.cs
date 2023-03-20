

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Enemy : Entity
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }

        public Enemy(string name, string description, Position pos, char mark, ConsoleColor color, ) : base(name, description, pos, mark, color)
        {
            //lisätään default valuet
        }
    }
}
