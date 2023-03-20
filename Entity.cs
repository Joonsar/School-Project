using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Position Pos { get; private set; }
        public Char Mark { get; private set; }
        public ConsoleColor Color {get; private set;}


        public Entity(string name, String description, Position pos, Char mark, ConsoleColor color)
        {
            this.Name = name;
            this.Description = description;
            this.Pos = pos;
            this.Mark = mark;
            this.Color = color;
        }


    }
}
