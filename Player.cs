using System;
using System.Collections.Generic;

namespace School_Project
{
    //itseasiassa miksei me käytettäs vector2 positionissa? Tosin tääkin toimii, mutta jos meillä olis esim vector2 position, niin siihen vois lisätä position += new vector2(1,0) jne
    public class Player
    {
#pragma warning disable CS4014

        //nää toimii, mutta yleinen nimikäytäntö on että nää alkaa isolla kirjaimella (turhaa höpötystä, mutta näin ne yleensä tehdään) :)
        //eli kun kutsutaan sit vaikka gc.Player.Name; gc.Player.Health;
        public string Name { get; private set; }

        public int HealthValue { get; private set; }
        public int HitPoints { get; set; }
        public int ExpPoints { get; private set; }

        public int MaxHp { get; set; }

        public int BaseDamage { get; set; }
        public int BaseHitChance { get; set; }

        public int Level { get; private set; }

        public int Bottles { get; set; }

        public List<Entity> Inventory { get; set; }

        public Map map;
        public ConsoleColor Color { get; private set; }

        public char Mark { get; set; }

        private GameController gc = GameController.Instance;

        //private Tuple<Position, char> LastPosition;

        private MapObject LastPosition;

        public Position Pos { get; set; }

        public float Money { get; set; }

        private Random rand = new();

        public Player(string name, int healthValue, int hitPoints)
        {
            Inventory = new List<Entity>();
            Level = 1;
            Name = name;
            HealthValue = healthValue;
            HitPoints = hitPoints;
            MaxHp = hitPoints;
            ExpPoints = 0;
            Pos = gc.Map.StairUp;
            BaseDamage = 50;
            BaseHitChance = 30;
            Money = 0;
            Bottles = 0;

            //LastPosition = new Tuple<Position, char>(new Position(10, 10), gc.Map.Mapping[10, 10]);

            SetPlayerLastPosition();

            Color = ConsoleColor.Green;
            Mark = '@';
        }

        public string GetStats()
        {
            return $"{Name} - Hp: {HitPoints}/{MaxHp} Exp: {ExpPoints} Lvl: {Level} $:{Money} L: {gc.Level} Pullot {Bottles} Hit: {100 - BaseHitChance}% Dam: {BaseDamage}             ";
        }

        public void MovePlayerToPosition(Position pos)
        {
            Pos = pos;
        }

        public void Addmoney(float amount)
        {
            Money += amount;
        }

        public void SetPlayerLastPosition()
        {
            //LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.Map.Mapping[Pos.X, Pos.Y].Mark);
            LastPosition = gc.Map.Mapping[Pos.X, Pos.Y];
        }

        public void MovePlayer(int x, int y)
        {
            var oldPos = new Position(Pos.X, Pos.Y);
            if (gc.Map.Mapping[Pos.X + x, Pos.Y + y] == Map.door)
            {
                gc.Map.Mapping[Pos.X + x, Pos.Y + y] = Map.openDoor;
                gc.MessageLog.AddMessage(new LogMessage("Avasit oven!", ConsoleColor.DarkYellow));
                gc.screen.WriteAtPosition(new Position(Pos.X + x, Pos.Y + y), Map.openDoor.Mark);
                return;
            }
            if (gc.Map.Mapping[Pos.X + x, Pos.Y + y] == Map.qMarket)
            {
                gc.MessageLog.AddMessage(new LogMessage("Menit Q-Markettiin!", ConsoleColor.DarkYellow));

                SoundManager.ChangeMusic(SoundType.Market);
                gc.Qmarket.Shop();
            }
            if (gc.Map.IsEnemyAtPosition(Pos.X + x, Pos.Y + y) != null)
            {
                Attack(gc.Map.IsEnemyAtPosition(Pos.X + x, Pos.Y + y));
            }

            if (gc.Map.IsItemAtPosition(Pos.X + x, Pos.Y + y) != null)
            {
                var item = gc.Map.IsItemAtPosition(Pos.X + x, Pos.Y + y);
                //gc.MessageLog.AddMessage(new LogMessage($"Poimit maasta {item.Name} {item.Description}", ConsoleColor.Blue));
                item.Use();
                Inventory.Add(item);
                Bottles++;
                gc.GameStats.ItemsCollected.Add(item);
                gc.Map.entities.Remove(item);
            }
            if (gc.Map.IsPositionValid(Pos.X + x, Pos.Y + y) && gc.Map.IsEnemyAtPosition(Pos.X + x, Pos.Y + y) == null)
            {
                Pos.X += x;
                Pos.Y += y;
                gc.screen.WriteAtPosition(oldPos, LastPosition.Mark, LastPosition.Color);
                gc.screen.PrintPlayer();
                //gc.MessageLog.AddMessage($"{Name} moves to {Pos.X}.{Pos.Y}");
                LastPosition = gc.Map.Mapping[Pos.X, Pos.Y];

                if (gc.Map.Mapping[Pos.X, Pos.Y] == Map.stairsDown)
                {
                    gc.ChangeLevel(1);
                }
                else if (gc.Map.Mapping[Pos.X, Pos.Y] == Map.stairsUp)
                {
                    gc.ChangeLevel(-1);
                }
            }
        }

        private void Attack(Entity e)
        {
            var hitChance = rand.Next(1, 100);
            if (hitChance > BaseHitChance)
            {
                e.TakeDamage(BaseDamage);

                //gc.MessageLog.AddMessage($"{Name} Hits {e.Name} for {damage}.");
            }
            else
            {
                gc.MessageLog.AddMessage(new LogMessage($"Epäonnistut nolosti ja kaadut turvallesi.", ConsoleColor.Red));
                SoundManager.Play(SoundType.Fail);
            }
        }

        public bool CheckIfPlayerCollidesWithStairs()
        {
            {
                if (gc.Map.Mapping[Pos.X, Pos.Y] == Map.stairsDown || gc.Map.Mapping[Pos.X, Pos.Y] == Map.stairsUp)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void AddExperience(int amount)
        {
            ExpPoints += amount;
            CheckLevelUp();
        }

        public void CheckLevelUp()
        {
            if (ExpPoints > Level * 100)
            {
                Level++;
                BaseDamage += 10;
                MaxHp += 25;

                SoundManager.Play(SoundType.LevelUp);
                //Task.Delay(TimeSpan.FromSeconds(1));
                //SoundManager.PlayMainMusic();

                gc.MessageLog.AddMessage(new LogMessage($"{Name} on nyt tason {Level} pultsari.", ConsoleColor.DarkYellow));
                gc.screen.PrintPlayerStats();
            }
        }

        public void TakeDamage(int amount)
        {
            gc.GameStats.DamageTaken += amount;
            gc.MessageLog.AddMessage(new LogMessage($"{Name} otaa {amount} vahinkoa", ConsoleColor.Red));
            HitPoints -= amount;
            gc.screen.PrintPlayerStats();
            CheckDeath();
        }

        public void CheckDeath()
        {
            if (HitPoints <= 0)
            {
                SoundManager.Play(SoundType.Die);
                gc.MessageLog.AddMessage(new LogMessage($"Kaadut maahan, silmissä pimenee. Seikkailusi on ohi!", ConsoleColor.Red));
                gc.UpdateDatabases();
                gc.screen.EndScreen();
                gc.Init();
            }
        }

        public void Update()
        {
            HitPoints += 1;
            if (HitPoints > MaxHp)
            {
                HitPoints = MaxHp;
            }
        }
    }
}