using System;
using System.Numerics;

namespace School_Project
{
    //itseasiassa miksei me käytettäs vector2 positionissa? Tosin tääkin toimii, mutta jos meillä olis esim vector2 position, niin siihen vois lisätä position += new vector2(1,0) jne
    public class Player
    {
        //nää toimii, mutta yleinen nimikäytäntö on että nää alkaa isolla kirjaimella (turhaa höpötystä, mutta näin ne yleensä tehdään) :)
        //eli kun kutsutaan sit vaikka gc.player.Name; gc.player.Health;
        public string Name { get; private set; }

        public int HealthValue { get; private set; }
        public int HitPoints { get; private set; }
        public int ExpPoints { get; private set; }

        private GameController gc = GameController.Instance;

        Tuple<Position, char> LastPosition;

        public Position Pos { get; private set; }
        /*public int X { get; set; }
        public int Y { get; set; }*/

        // private Position pos;

        //public Player(string name, int xAxis, int yAxis)
        // {
        //     this.name = name;
        //     //this.health = ?; Päätetään myöhemmin
        //     this.points = 0;
        //     this.pos = new Position(xAxis, yAxis);
        // }

        //ei välttämättä tarvi constructorissa expPoints(jos oletetaan, että se on alussa nolla. voidaan vaan laitta ExpPoints = 0)
        public Player(string name, int healthValue, int hitPoints)
        {
            Name = name;
            HealthValue = healthValue;
            HitPoints = hitPoints;
            ExpPoints = 0;
            Pos = new Position(10, 10);
            LastPosition = new Tuple<Position, char>(new Position(10, 10), gc.map.Mapping[10,10] );
        }

        public string GetStats()
        {
            return $"{Name} - Health: {HealthValue}, Hit Points: {HitPoints}, Exp Points: {ExpPoints}";
        }

        public void MovePlayer(int x, int y)
        {
            if (gc.map.IsPositionValid(Pos.X + x, Pos.Y + y))
            {
                Pos.X += x;
                Pos.Y += y;
                gc.screen.WriteAtPosition(LastPosition.Item1, LastPosition.Item2);
                LastPosition = new Tuple<Position, char>(new Position(Pos.X, Pos.Y), gc.map.Mapping[Pos.X, Pos.Y]);
            }
        }
    }
}