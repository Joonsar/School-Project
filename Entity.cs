using System;

namespace School_Project
{
    public class Entity
    {
        public string Name { get; private set; }
        public string Description { get;  set; }
        public Position Pos { get; set; }
        public Char Mark { get; private set; }
        public ConsoleColor Color { get; private set; }

        public Entity(string name, String description, Position pos, Char mark, ConsoleColor color)
        {
            this.Name = name;
            this.Description = description;
            this.Pos = pos;
            this.Mark = mark;
            this.Color = color;
        }

        public virtual void MoveEntity(int x, int y)
        {
        }

        public virtual void Update()
        {
        }

        public virtual void TakeDamage(int v)
        {
        }
    }
}