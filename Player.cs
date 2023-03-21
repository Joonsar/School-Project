using System;

namespace School_Project
{
    //itseasiassa miksei me käytettäs vector2 positionissa? Tosin tääkin toimii, mutta jos meillä olis esim vector2 position, niin siihen vois lisätä position += new vector2(1,0) jne
    public class Player
    {
        //nää toimii, mutta yleinen nimikäytäntö on että nää alkaa isolla kirjaimella (turhaa höpötystä, mutta näin ne yleensä tehdään) :)
        //eli kun kutsutaan sit vaikka gc.Player.Name; gc.Player.Health;
        public string Name { get; private set; }

        public int HealthValue { get; private set; }
        public int HitPoints { get; private set; }
        public int ExpPoints { get; private set; }
        
        public int MaxHp { get; set; }

        public int Level { get; private set; }

        public Map map;
        public ConsoleColor Color { get; private set; }

        public char Mark { get; set; }

        private GameController gc = GameController.Instance;

        private Tuple<Position, char> LastPosition;

        public Position Pos { get; set; }

        public Player(string name, int healthValue, int hitPoints)
        {
            Level = 1;
            Name = name;
            HealthValue = healthValue;
            HitPoints = hitPoints;
            MaxHp = hitPoints;
            ExpPoints = 0;
            Pos = new Position(10, 10);

            //LastPosition = new Tuple<Position, char>(new Position(10, 10), gc.Map.Mapping[10, 10]);

            SetPlayerLastPosition();

            Color = ConsoleColor.Green;
            Mark = '@';
        }

        public string GetStats()
        {
            return $"{Name} - Hp: {HealthValue}/{MaxHp} Exp: {ExpPoints} Lvl: {Level} T: {gc.Turn} L: {gc.Level}";
        }

        public void MovePlayerToPosition(Position pos)
        {
            Pos = pos;
        }

        public void SetPlayerLastPosition()
        {
            LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.Map.Mapping[Pos.X, Pos.Y]);
        }

        public void MovePlayer(int x, int y)
        {
            if (gc.Map.IsPositionValid(Pos.X + x, Pos.Y + y) && gc.Map.IsEnemyAtPosition(Pos.X + x, Pos.Y + y) == null)
            {
                Pos.X += x;
                Pos.Y += y;
                gc.screen.WriteAtPosition(LastPosition.Item1, LastPosition.Item2);
                gc.screen.PrintPlayer();
                gc.MessageLog.AddMessage($"{Name} moves to {Pos.X}.{Pos.Y}");
                LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.Map.Mapping[Pos.X, Pos.Y]);

                if (gc.Map.Mapping[Pos.X, Pos.Y] == '<')
                {
                    gc.ChangeLevel(1);
                    
                }
                else if (gc.Map.Mapping[Pos.X, Pos.Y] == '>')
                {
                    gc.ChangeLevel(-1);
                    
                }
            }


        }

        public bool CheckIfPlayerCollidesWithStairs()
        {
            {
                if (gc.Map.Mapping[Pos.X, Pos.Y] == '<' || gc.Map.Mapping[Pos.X, Pos.Y] == '>')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}