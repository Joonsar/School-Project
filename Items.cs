using System;
using System.Collections.Generic;
using System.Linq;

namespace School_Project
{
    public class Items
    {
        private List<Item> ItemsList;
        private List<Item> ItemsList2;
        private GameController gc = GameController.Instance;
        private Random random = new();

        public Items()
        {
            ItemsList = new List<Item>();

            ItemsList.Add(new Item("Koskenkorvapullo", "(+Vahinko -Osumatarkkuus)", new Position(0, 0), '!', ConsoleColor.Blue, SoundType.ItemPickup, () =>
            {
                gc.Player.BaseDamage += 20;
                gc.Player.BaseHitChance += 10;
                gc.MessageLog.AddMessage(new LogMessage("Juot pullon, tunnet itsesi voimakkaammaksi, samalla myös osumatarkkuus heikentyy", ConsoleColor.Blue));
            }));
            ItemsList.Add(new Item("Välivesi", "(+Osumatarkkuus)", new Position(0, 0), '!', ConsoleColor.Cyan, SoundType.ItemPickup, () =>
            {
                gc.Player.BaseHitChance -= 15;
                gc.MessageLog.AddMessage(new LogMessage("Hörppäät väliveden ja maailma näyttää selkeämmältä. Osumatarkkuutesi paranee", ConsoleColor.Blue));
            }));
            ItemsList.Add(new Item("Vissy", "ilman kossua (+Hp +MaxHp)", new Position(0, 0), '!', ConsoleColor.DarkBlue, SoundType.ItemPickup, () =>
            {
                gc.Player.HitPoints += 100;
                gc.Player.MaxHp += 50;
                gc.MessageLog.AddMessage(new LogMessage("Kulautat vissyn naamariin, tunnet voivasi paremmin.", ConsoleColor.Blue));
            }));
            ItemsList.Add(new Item("Konjakki", "(+Kokemuspisteet)", new Position(0, 0), '!', ConsoleColor.DarkRed, SoundType.ItemPickup, () =>
            {
                gc.Player.AddExperience(100);
                gc.MessageLog.AddMessage(new LogMessage("Nautiskelet konjakin. Tunnet itsesi ammattilaiseksi", ConsoleColor.Blue));
            }));
            ItemsList.Add(new Item("Smurffilimu", "(+Hp)", new Position(0, 0), '!', ConsoleColor.DarkGreen, SoundType.ItemPickup, () =>
            {
                gc.Player.HitPoints += 100;
                gc.MessageLog.AddMessage(new LogMessage("Juot smurffilimun ja tunnet voivasi paremmin", ConsoleColor.Blue));
            }));
            ItemsList.Add(new Item("Maitotölkki", "(+raha)", new Position(0, 0), '!', ConsoleColor.White, SoundType.ItemPickup, () =>
            {
                gc.Player.Money += 5;
                gc.MessageLog.AddMessage(new LogMessage("Juot maitotölkin. Mitä ihmettä, sen pohjalta löytyi 5 euron kolikkoa!", ConsoleColor.Blue));
            }));
            ItemsList.Add(new Item("Roskis", "Roskis täynnä rojua", new Position(0, 0), '®', ConsoleColor.DarkGreen, SoundType.Bottles, () =>
            {
                var chance = random.Next(0, 100);
                if (chance > 50)
                {
                    var amount = random.Next(2, 7);

                    gc.MessageLog.AddMessage(new LogMessage($"Tongit roskista. Hirveän näköistä touhua. Saaliisi oli {amount} kpl tyhjiä pulloja.", ConsoleColor.Blue));
                    gc.Player.Bottles += amount;
                }
                else
                {
                    gc.MessageLog.AddMessage(new LogMessage($"Tongit roskista, mutta et löydä mitään.", ConsoleColor.Blue));
                }
            }));
        }

        public Item GetRandomItem()
        {
            var randomItem = ItemsList[random.Next(0, ItemsList.Count())];
            Item newItem = new(randomItem.Name, randomItem.Description, randomItem.Pos, randomItem.Mark, randomItem.Color, randomItem.SoundType, randomItem.UseAction);
            return newItem;
            //  return ItemsList[random.Next(0, ItemsList.Count())];
        }
    }
}