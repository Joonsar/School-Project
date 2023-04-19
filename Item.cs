using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Item : Entity
    {
        private Action UseAction;

        public Item(string name, string description, Position pos, char mark, ConsoleColor color) : base(name, description, pos, mark, color)
        {
        }

        public Item(string name, string description, Position pos, char mark, ConsoleColor color, Action useAction) : base(name, description, pos, mark, color)
        {
            UseAction = useAction;
        }

        public override void Use()
        {
            UseAction.Invoke();
        }
    }
}