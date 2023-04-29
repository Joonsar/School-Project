using System;

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
            SoundManager.Play(SoundType);
            //await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}