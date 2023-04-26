using System;

namespace School_Project
{
    public class MapObject
    {
        public char Mark { get; set; }
        public string Description { get; set; }

        public bool CanWalkThrough { get; set; }
        public ConsoleColor Color;

        public MapObject(char mark, string description, bool canWalkThrhough)
        {
            Mark = mark;
            Description = description;
            CanWalkThrough = canWalkThrhough;
            Color = ConsoleColor.White;
        }

        public MapObject(char mark, string description, bool canWalkThrhough, ConsoleColor color)
        {
            Mark = mark;
            Description = description;
            CanWalkThrough = canWalkThrhough;
            Color = color;
        }
    }
}