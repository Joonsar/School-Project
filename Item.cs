using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Item : Entity
    {
        public Action UseAction { get; set; }

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
        //    SoundManager.PlayItemPickupSound();

            // Wait for 1 second before playing the main music
        //    Task.Delay(TimeSpan.FromSeconds(1)).Wait();

        //    SoundManager.PlayMainMusic();
        }
    }
}