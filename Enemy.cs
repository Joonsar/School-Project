using System;
using System.Text.Json.Serialization;

using System.Threading.Tasks;

namespace School_Project
{
    public class Enemy : Entity
    {
        private Random rand = new Random();

        private GameController gc = GameController.Instance;

        private Tuple<Position, Char> LastPosition;

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }

        public int Level { get; set; }

        public Enemy(string name, string description, Position pos, char mark, ConsoleColor color, int maxHealth, int damage) : base(name, description, pos, mark, color)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;

            SetEnemyLastPosition();
        }

        [JsonConstructor]
        public Enemy(string name, string description, Position pos, char mark, ConsoleColor color, int maxHealth, int damage, int level) : base(name, description, pos, mark, color)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;

            SetEnemyLastPosition();
            Level = level;
        }

        public override void MoveEntity(int x, int y)
        {
            var newPosX = Pos.X + x;
            var newPosY = Pos.Y + y;

            //tarkistetaan onko ruudussa johon yritetään liikkua seinä, toinen vihollinen tai pelaaja.. jos ei liikutetaan vihollista siihen ruutuun
            if (gc.Map.IsPositionValid(newPosX, newPosY) && gc.Map.IsEnemyAtPosition(newPosX, newPosY) == null && (newPosX != gc.Player.Pos.X || newPosY != gc.Player.Pos.Y))
            {
                Pos.X = newPosX;
                Pos.Y = newPosY;
                //gc.MessageLog.AddMessage($"{Name} moves to {Pos.X}.{Pos.Y}");
                //tulostetaan vihollinen liikkumisen jälkeen.
                gc.screen.PrintEnemy(this);
                //kirjoitetaan ruutuun mistä liikuttiin, sen edellinen merkki.
                gc.screen.WriteAtPosition(LastPosition.Item1, LastPosition.Item2);
                // gc.MessageLog.AddMessage($"{Name} moves to {Pos.X}.{Pos.Y}");
                // gc.MessageLog.AddMessage($"{Name} on {Description}");

                SetEnemyLastPosition();
            }
            else if (newPosX == gc.Player.Pos.X && newPosY == gc.Player.Pos.Y)
            {
                var hitChance = rand.Next(0, 100);
                if (hitChance > 50)
                {
                    gc.MessageLog.AddMessage(new LogMessage($"{Name} lyö {gc.Player.Name}", ConsoleColor.DarkYellow));
                    gc.Player.TakeDamage(Damage);
                }
                else
                {
                    gc.MessageLog.AddMessage(new LogMessage($"{Name} yrittää huitaista, mutta {gc.Player.Name} huojunta osuu kohilleen ja isku viilettää ohi!", ConsoleColor.DarkYellow));
                }
            }
        }

        public void SetEnemyLastPosition()
        {
            LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.Map.Mapping[Pos.X, Pos.Y].Mark);
        }

        public override void TakeDamage(int basedamage)
        {
            int damage = basedamage;
            Random rand = new Random();
            int move = rand.Next(0, 120);
            switch (move)
            {
                case int n when n > 0 && n < 5:
                    damage =Health + 1;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Lataat kaikki voimasi uskomattomaan pubi heijariin ja säkällä horjahdat sopivasti niin että isku osuu keskelle naamaa!", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"{this.Name} tippuu ku hanskat duunarilta ja ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;
                case int n when n > 5 && n < 20:
                    damage *= 2;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Pistät painiks ja möyritte maassa 20min ähisten jonka jälkeen pidätte juomatauon.", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"Tauolla lyöt takaapäin ja juokset karkuun. {this.Name} kärsii {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;
                case int n when n > 20 && n < 30:
                    damage *= 2;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Näytät persettä ja {this.Name} heittää laatat sekä kärsii {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;
                case int n when n > 30 && n < 50:
                    damage *= 1;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Annat pikku läpsyn naamalle. {this.Name} ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;
                case int n when n > 50 && n < 70:
                    damage *= 1;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"{this.Name} kompastuu kesken matsin naama edellä sokoksen lasiin ja ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;
                case int n when n > 70 && n < 80:
                    damage *= 3;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Potku kulkusille osoittautuu tehokkaaks (always). {this.Name} ottaa {damage} vahinkoa. ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;
                case int n when n > 80 && n < 90:
                    damage *= 3;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Uskomaton humalainen saksipotku lässähtää keskelle ohimoo. {this.Name} ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;
                case int n when n > 90 && n < 110:
                    damage *= 2;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Päätät ottaa henkisen yliotteen ja ottaa paidan pois. {this.Name} nauraa pää polvissa koska riisuit housut!", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"Käytät tilanteen hyväksesi ja tempaset puskista leukaa antaen {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;
                default:
                    damage *= 1;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Tökkäät silmään. {this.Name} ottaa {damage} vahikoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;
            }
            gc.GameStats.DamageDealt += damage;
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (Health <= 0)
            {
                SoundManager.PlayVictorySound();
                Task.Delay(TimeSpan.FromSeconds(1)).Wait(1);
                SoundManager.PlayMainMusic();

                gc.MessageLog.AddMessage(new LogMessage($"{Name} kaatuu maahan. Sinut valtaa voittajafiilis.", ConsoleColor.Green));
                gc.GameStats.EnemiesKilled.Add(this);
                gc.Player.AddExperience((Level + 1) * 50);
                gc.Map.entities.Remove(this);
            }
        }

        public override void Update()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int speed = rand.Next(1, 10);
            int moveX = 0;
            int moveY = 0;
            int xDiff = gc.Player.Pos.X - Pos.X;
            int yDiff = gc.Player.Pos.Y - Pos.Y;
            double distance = Math.Sqrt(yDiff * yDiff + xDiff * xDiff);

            if (distance > 6)
            {
                moveX = rand.Next(-1, 2);
                moveY = rand.Next(-1, 2);
            }
            else if (speed > 3)
            {
                if (Math.Abs(xDiff) > Math.Abs(yDiff))
                {
                    moveX = xDiff > 0 ? 1 : -1;
                }
                else
                {
                    moveY = yDiff > 0 ? 1 : -1;
                }
            }
            MoveEntity(moveX, moveY);
        }
    }
}