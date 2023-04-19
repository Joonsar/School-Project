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
        private List<Item> ItemsList2;
        private GameController gc = GameController.Instance;
        private Random random = new Random();

        public Items()
        {
            ItemsList = new List<Item>();
            
            ItemsList.Add(new Item("Koskenkorvapullo", "(+Vahinko -Osumatarkkuus)", new Position(0, 0), '!', ConsoleColor.Blue, () =>
            {
                gc.Player.BaseDamage += 10;
                gc.Player.BaseHitChance += 10;
                gc.MessageLog.AddMessage("Juot pullon, tunnet itsesi voimakkaammaksi, samalla myös osumatarkkuus heikentyy");
            }));
            ItemsList.Add(new Item("Välivesi", "(+Osumatarkkuus)", new Position(0, 0), '!', ConsoleColor.Cyan, () =>
            {
                gc.Player.BaseHitChance -= 15;
                gc.MessageLog.AddMessage("Hörppäät väliveden ja maailma näyttää selkeämmältä. Osumatarkkuutesi paranee");
            }));
            ItemsList.Add(new Item("Vissy", "ilman kossua (+Hp +MaxHp)", new Position(0, 0), '!', ConsoleColor.DarkBlue, () =>
            {
                gc.Player.HitPoints += 100;
                gc.Player.MaxHp += 20;
                gc.MessageLog.AddMessage("Kulautat vissyn naamariin, tunnet voivasi paremmin.");
            }));
            ItemsList.Add(new Item("Konjakki", "(+Kokemuspisteet)", new Position(0, 0), '!', ConsoleColor.DarkRed, () =>
            {
                gc.Player.AddExperience(100);
                gc.MessageLog.AddMessage("Nautiskelet konjakin. Tunnet itsesi ammattilaiseksi");
            }));
            ItemsList.Add(new Item("Smurffilimu", "(+Hp)", new Position(0, 0), '!', ConsoleColor.DarkGreen, () =>
            {
                gc.Player.HitPoints += 20;
                gc.MessageLog.AddMessage("Juot smurffilimun ja tunnet voivasi paremmin");
            }));
        }

        public Item GetRandomItem()
        {
            var randomItem = ItemsList[random.Next(0, ItemsList.Count())];
            Item newItem = new Item(randomItem.Name, randomItem.Description, randomItem.Pos, randomItem.Mark, randomItem.Color, randomItem.UseAction);
            return newItem;
            //  return ItemsList[random.Next(0, ItemsList.Count())];
        }
    }
}