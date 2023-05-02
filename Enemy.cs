using System;
using System.Text.Json.Serialization;

namespace School_Project
{
    public class Enemy : Entity
    {
        private Random rand = new();

        private GameController gc = GameController.Instance;

        private MapObject LastPosition;

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
            var oldPos = new Position(Pos.X, Pos.Y);
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
                gc.screen.WriteAtPosition(oldPos, LastPosition.Mark, LastPosition.Color);
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
                    gc.MessageLog.AddMessage(new LogMessage($"{Name} yrittää huitaista, mutta juoppo {gc.Player.Name} huojuu sopivasti ja isku viilettää ohi!", ConsoleColor.DarkYellow));
                }
            }
        }

        public void SetEnemyLastPosition()
        {
            LastPosition = gc.Map.Mapping[Pos.X, Pos.Y];
        }

        public override void TakeDamage(int basedamage)
        {
            SoundManager.Play(SoundType.HitEnemy);
            int damage = basedamage;
            Random rand = new();
            int move = rand.Next(0, 200);
            switch (move)
            {
                case int n when n > 0 && n < 5:
                    damage = Health + 1;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Lataat kaikki voimasi uskomattomaan pubi heijariin ja säkällä horjahdat sopivasti niin että isku osuu keskelle naamaa!", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"{this.Name} tippuu ku hanskat duunarilta ja ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 5 && n < 15:
                    damage *= 2;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Pistät painiks ja möyritte maassa 20min ähisten jonka jälkeen pidätte juomatauon.", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"Tauolla lyöt takaapäin ja juokset karkuun. {this.Name} kärsii {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 15 && n < 30:
                    damage *= 1;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Kaivat kiivaasti taskusta jotain asetta ja löydät napin. Heität napin ja {this.Name} luuli et se on massia!", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"samalla kun {this.Name} ettii sitä potkaset selkää aiheuttaen {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 30 && n < 50:
                    damage *= 1;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Annat pikku läpsyn naamalle. {this.Name} ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 50 && n < 60:
                    damage *= 2;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"{this.Name} kompastuu kesken matsin naama edellä sokoksen lasiin ja ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 60 && n < 70:
                    damage *= 2;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"katot että {this.Name} on ottanu vaa parit joten päätät heittää kivellä mutta kivi osuuki", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"Väiski frendii!! Hirveen väännön jälkee Väiski kuiteski uskoo et se oli {this.Name} ja hakkaatte sen kimpassa!", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"{this.Name} ottaa damagee ({Health}/{MaxHealth})", ConsoleColor.Green));
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

                case int n when n > 110 && n < 120:
                    damage *= 2;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Kompastut ottaessasi juoksulähtöä karkuun ja nyrkkisi heilahtaa suoraan vastustajan leukaan!", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"{this.Name} ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 120 && n < 130:
                    damage *= 3;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Kaivat taskustasi rautaputken ja vastustajasi frendit pakenee paikalta.", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"{this.Name} saa putkesta päähänsä ja ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 130 && n < 140:
                    damage *= 2;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"{this.Name} kaatuu yrittäessää lyödä sua joten päätät painaa täpöö päälle", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"ja potkit kylkeen kunnes kunto pettäää.(siis kerran) {this.Name} ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 140 && n < 150:
                    damage *= 1;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Väität kovasti ettet halua tapella ja kun {this.Name} kääntyy ympäri ja lähtee käveleen", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"pois päin potkaset selkään! {this.Name} ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 150 && n < 160:
                    damage *= 4;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Päätät käyttää käyttökelpoisinta taitoa: puremista. Puret vastustajasi kättä kunnes hän huutaa", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"'löysin bissen' ja päästät irt. Huomattuasi tulleeksi huijatuksi sinut valtaa raivo ja", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"paukutat menee ku merimies bordellissa! {this.Name} ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));

                    break;

                case int n when n > 160 && n < 170:
                    damage *= 1;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Otat käyttöösi matadorin liikkeet ja juokset ympäriinsä kuten härän kanssa. Vastustajasi  kompastuu ja kaatuu", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"maahan joten potkaiset häntä tuttuun tyylii kulkusille! {this.Name} ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
                    break;

                case int n when n > 170 && n < 180:
                    damage *= 2;
                    Health -= damage;
                    gc.MessageLog.AddMessage(new LogMessage($"Laitat kätesi vastustajasi naamalle ja painat lujaa. Hän yrittää huutaa 'päästä irti', mutta painat niin", ConsoleColor.Green));
                    gc.MessageLog.AddMessage(new LogMessage($"kauan että taju lähtee ja potkaset kulkusille. {this.Name} ottaa {damage} vahinkoa ({Health}/{MaxHealth})", ConsoleColor.Green));
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
                SoundManager.Play(SoundType.Victory);

                gc.MessageLog.AddMessage(new LogMessage($"{Name} kaatuu maahan. Sinut valtaa voittajafiilis.", ConsoleColor.Green));
                gc.GameStats.EnemiesKilled.Add(this);
                gc.Player.AddExperience((Level + 1) * 50);
                gc.Map.entities.Remove(this);
            }
        }

        public override void Update()
        {
            Random rand = new(Guid.NewGuid().GetHashCode());
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