using System;
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

        public override async void Use()
        {
            UseAction.Invoke();
            SoundManager.PlayItemPickupSoundAsync().Wait(1);
            await Task.Delay(TimeSpan.FromSeconds(1));
            SoundManager.PlayMainMusic();
        }
    }
}