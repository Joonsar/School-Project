using System;
using System.Threading.Tasks;

namespace School_Project
{
    public class Item : Entity
    {
        public Action UseAction { get; set; }
        public SoundType SoundType { get; set; }

        public Item(string name, string description, Position pos, char mark, ConsoleColor color) : base(name, description, pos, mark, color)
        {
        }

        public Item(string name, string description, Position pos, char mark, ConsoleColor color, SoundType soundType, Action useAction) : base(name, description, pos, mark, color)
        {
            SoundType = soundType;
            UseAction = useAction;
        }

        public override void Use()
        {
            UseAction.Invoke();
            SoundManager.PlayAsync(SoundType).Wait(1);
            //await Task.Delay(TimeSpan.FromSeconds(1));
            SoundManager.PlayMainMusic();
        }
    }
}