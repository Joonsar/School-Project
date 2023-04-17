using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Item : Entity
    {
        

        public Item(string name, string description, Position pos, char mark, ConsoleColor color) : base(name, description, pos, mark, color)
        {

        }
        
    }
}
