using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Items
    {
        private List<Item> ItemsList;
        private GameController gc = GameController.Instance;
        private Random random = new Random();

        public Items()
        {
            ItemsList = new List<Item>();
            ItemsList.Add(new Item("Koskenkorvapullo", "(Tyhjä)", new Position(0, 0), '!', ConsoleColor.Blue, () =>
            {
                gc.Player.BaseDamage += 10;
                gc.Player.BaseHitChance += 10;
                gc.MessageLog.AddMessage("Juot helmen pullon pohjalta tunnet itsesi voimakkaammaksi, samalla myös osumatarkkuus heikentyy");
            }));
            ItemsList.Add(new Item("Välivesi", "helpottaa humalatilaan", new Position(0, 0), '!', ConsoleColor.Cyan, () =>
            {
                gc.Player.BaseHitChance -= 15;
                gc.MessageLog.AddMessage("Hörppäät väliveden ja maailma näyttää selkeämmältä. Osumatarkkuutesi paranee");
            }));
            ItemsList.Add(new Item("Vissy", "ilman kossua", new Position(0, 0), '!', ConsoleColor.DarkBlue, () =>
            {
                gc.Player.HitPoints += 100;
                gc.Player.MaxHp += 20;
                gc.MessageLog.AddMessage("Kulautat vissyn naamariin, tunnet itsesti terveellisemmäksi.");
            }));
        }

        public Item GetRandomItem()
        {
            return ItemsList[random.Next(0, ItemsList.Count())];
        }
    }
}