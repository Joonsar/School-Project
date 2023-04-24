using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;

namespace School_Project
{
    public class Map
    {
        private GameController gc = GameController.Instance;
        public MapObject[,] Mapping { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Items items;

        public int ItemCount { get; set; }

        public Position StairDown { get; set; }
        public Position StairUp { get; set; }

        //tässä määritellään seinät jne. toi true false arvo on voiko sen läpi kävellä vai ei
        public static readonly MapObject wall = new MapObject('#', "seinä", false, ConsoleColor.DarkYellow);

        public static readonly MapObject empty = new MapObject(' ', "lattia", true);
        public static readonly MapObject stairsDown = new MapObject('<', "portaat alas", true, ConsoleColor.Green);
        public static readonly MapObject stairsUp = new MapObject('>', "portaal ylös", true, ConsoleColor.Red);
        public static readonly MapObject door = new MapObject('+', "Ovi", false, ConsoleColor.White);
        public static readonly MapObject openDoor = new MapObject('-', "Ovi", true, ConsoleColor.White);

        public List<Entity> entities;
        private List<Position> playerPath;

        public Map(int width, int height, char emptySpaceChar = ' ', int numRooms = 0)
        {
            items = new Items();
            ItemCount = 6;
            this.Width = width;
            this.Height = height;
            entities = new List<Entity>();
            // Create game board array
            Mapping = new MapObject[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        // Place "#" at edges of the board
                        Mapping[x, y] = wall;
                    }
                    else
                    {
                        // Fill the rest of the board with empty space character
                        Mapping[x, y] = empty;
                    }
                }
            }
            int numberOfRooms = numRooms > 0 ? numRooms : new Random().Next(2, 5);
            GenerateRoom(numberOfRooms);
            GenerateStairs();
            GenerateItems();

            // Generate random walls
            //  GenerateRandomWalls();
        }

        private void GenerateRoom(int numRooms)
        {
            List<Room> rooms = new List<Room>();
            int tries = 0;

            while (rooms.Count < numRooms && tries < 1000)
            {
                Room room = new Room();

                bool overlaps = false;
                foreach (Room otherRoom in rooms)
                {
                    int xr = room.Rect.X - 1;
                    int yr = room.Rect.Y + 1;
                    int rwidht = room.Rect.Width + 2;
                    int rheight = room.Rect.Height + 2;

                    int xo = otherRoom.Rect.X - 1;
                    int yo = otherRoom.Rect.Y + 1;
                    int owidht = otherRoom.Rect.Width + 2;
                    int oheight = otherRoom.Rect.Height + 2;

                    Rectangle r = new Rectangle(xr, yr, rwidht, rheight);
                    Rectangle ro = new Rectangle(xo, yo, owidht, oheight);
                    if (room.Rect.IntersectsWith(otherRoom.Rect) || room.Rect.IntersectsWith(ro) || r.IntersectsWith(ro))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    rooms.Add(room);

                    for (int x = room.Rect.Left; x < room.Rect.Right; x++)
                    {
                        for (int y = room.Rect.Top; y < room.Rect.Bottom; y++)
                        {
                            if (x == room.Rect.Left && y == room.Rect.Top + room.Rect.Height / 2)
                            {
                                Mapping[x, y] = door;
                            }
                            else if (x == room.Rect.Right - 1 && y == room.Rect.Top + room.Rect.Height / 2)
                            {
                                Mapping[x, y] = door;
                            }
                            else if (x == room.Rect.Left || x == room.Rect.Right - 1 || y == room.Rect.Top || y == room.Rect.Bottom - 1)
                            {
                                Mapping[x, y] = wall;
                            }
                            else
                            {
                                Mapping[x, y] = empty;
                            }
                        }
                    }
                }
                tries++;
            }
        }

        public void GenerateItems()
        {
            var count = 0;
            Random rand = new Random();
            while (ItemCount > count)
            {
                bool isvalid = false;
                var oldPos = new Position(-10, -10);
                while (isvalid == false)
                {
                    Position randomPos = new Position(rand.Next(1, Width - 1), rand.Next(1, Height - 1));
                    var item = items.GetRandomItem();
                    if (IsPositionValid(randomPos) && IsEnemyAtPosition(randomPos) == null && !IsStairsAtPosition(randomPos) && !randomPos.Equals(oldPos))
                    {
                        item.Pos = randomPos;
                        oldPos = randomPos;
                        entities.Add(item);
                        isvalid = true;
                    }
                }
                count++;
            }
        }

        public bool IsStairsAtPosition(Position pos)
        {
            if ((pos.X == StairDown.X && pos.Y == StairDown.Y) || (pos.X == StairUp.X && pos.Y == StairUp.Y))
            {
                return true;
            }
            return false;
        }

        private void GenerateRandomWalls()
        {
            Random random = new Random();

            // Determine the number of walls to create based on the size of the map
            int wallCount = Width * Height / 80;

            // Generate the specified number of walls in random positions
            for (int i = 0; i < wallCount; i++)
            {
                int x = random.Next(1, Width - 1);
                int y = random.Next(1, Height - 1);

                if (Mapping[x, y] == empty)
                {
                    Mapping[x, y] = wall;
                }
            }
        }

        // Check if a given position is within the bounds of the game board and if there is wall at position

        public bool IsPositionInsideBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool IsPositionValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height && Mapping[x, y].CanWalkThrough;
        }

        public bool IsPositionValid(Position movePos)
        {
            return movePos.X >= 0 && movePos.X < Width && movePos.Y >= 0 && movePos.Y < Height && Mapping[movePos.X, movePos.Y] != wall;
        }

        // tarkastetaan onko ruudustta entity. jos on palautetaan se. muuten palautetaan null
        public Entity IsEnemyAtPosition(int x, int y)
        {
            foreach (Entity e in entities)
            {
                if (e.Pos.X == x && e.Pos.Y == y && e.GetType() == typeof(Enemy))
                {
                    return e;
                }
            }
            return null;
        }

        public Entity IsItemAtPosition(int x, int y)
        {
            foreach (Entity i in entities)
            {
                if (i.Pos.X == x && i.Pos.Y == y && i.GetType() == typeof(Item))
                {
                    return i;
                }
            }
            return null;
        }

        public Entity IsEnemyAtPosition(Position movePos)
        {
            foreach (Entity e in entities)
            {
                if (e.Pos == movePos && e.Pos == movePos && e.GetType() == typeof(Enemy))
                {
                    return e;
                }
            }
            return null;
        }

        // luodaan vihollisia karttaan. ja lisätään ne entities listaan.
        // tähän vois kehitellä jonkun systeemin, että noi viholliset kaivetaan jostain sen mukaan kuinka syvällä ollaan jne.
        public void CreateEnemies(int level, int enemyCount)
        {
            Enemies enemies = new Enemies();
            var nrEnemies = enemyCount + gc.Level;
            if (nrEnemies > 20)
            {
                nrEnemies = 20;
            }
            List<Enemy> enemyList = enemies.GetEnemyListByLevel(level, nrEnemies);
            foreach (Enemy enemy in enemyList)
            {
                entities.Add(enemy);
            }
        }

        // Update the game board with a new 2D char array
        public void UpdateMap(MapObject[,] updatedBoard)
        {
            Mapping = updatedBoard;
        }

        public void GenerateStairs()
        {
            Random random = new Random();

            int newX = random.Next(1, Width - 1);
            int newY = random.Next(1, Height - 1);
            while (!IsPositionValid(newX, newY))
            {
                newX = random.Next(1, Width - 1);
                newY = random.Next(1, Height - 1);
            }
            Mapping[newX, newY] = stairsDown;
            StairDown = new Position(newX, newY);
            newX = random.Next(1, Width - 1);
            newY = random.Next(1, Height - 1);
            // Make sure the new position is not the same as the first one
            while (new Position(newX, newY).Equals(stairsDown) && !IsPositionValid(newX, newY))
            {
                newX = random.Next(1, Width - 1);
                newY = random.Next(1, Height - 1);
            }

            Mapping[newX, newY] = stairsUp;
            StairUp = new Position(newX, newY);
        }

        public void GenerateNewMap()
        {
            int newWidth = Width;
            int newHeight = Height;
            char emptySpaceChar = '.';

            Map newMap = new Map(newWidth, newHeight, emptySpaceChar);

            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    if (x == 0 || x == newWidth - 1 || y == 0 || y == newHeight - 1)
                    {
                        newMap.Mapping[x, y] = wall;
                    }
                    else
                    {
                        newMap.Mapping[x, y] = empty;
                    }
                }
            }

            // Update the game controller's map and redraw it
            gc.Map = newMap;
            gc.screen.Clear();
            newMap.GenerateStairs();
            UpdateMap(gc.Map.Mapping);
        }
    }
}