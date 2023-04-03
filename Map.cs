using System;
using System.Collections.Generic;

namespace School_Project
{
    public class Map
    {
        private GameController gc = GameController.Instance;
        public char[,] Mapping { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Position StairDown { get; set; }
        public Position StairUp { get; set; }

        public List<Entity> entities;

        public Map(int width, int height, char emptySpaceChar = ' ')
        {
            this.Width = width;
            this.Height = height;
            entities = new List<Entity>();
            // Create game board array
            Mapping = new char[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        // Place "#" at edges of the board
                        Mapping[x, y] = '#';
                    }
                    else
                    {
                        // Fill the rest of the board with empty space character
                        Mapping[x, y] = emptySpaceChar;
                    }
                }
            }
            // Generate random walls
            GenerateRandomWalls();
        }

        private void GenerateRandomWalls()
        {
            Random random = new Random();

            // Determine the number of walls to create based on the size of the map
            int wallCount = Width * Height / 10;

            // Generate the specified number of walls in random positions
            for (int i = 0; i < wallCount; i++)
            {
                int x = random.Next(1, Width - 1);
                int y = random.Next(1, Height - 1);

                if (Mapping[x, y] == ' ')
                {
                    Mapping[x, y] = '#';
                }
            }
        }

        // Check if a given position is within the bounds of the game board and if there is wall at position

        public bool IsPositionValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height && Mapping[x, y] != '#';
        }

        public bool IsPositionValid(Position movePos)
        {
            return movePos.X >= 0 && movePos.X < Width && movePos.Y >= 0 && movePos.Y < Height && Mapping[movePos.X, movePos.Y] != '#';
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
            List<Enemy> enemyList = enemies.GetEnemyListByLevel(level, enemyCount);
            foreach(Enemy enemy in enemyList)
            {
                entities.Add(enemy);
            }
        }

        // Update the game board with a new 2D char array
        public void UpdateMap(char[,] updatedBoard)
        {
            Mapping = updatedBoard;
        }

        public void GenerateStairs()
        {
            Random random = new Random();
            int x = random.Next(1, Width - 1);
            int y = random.Next(1, Height - 1);

            Mapping[x, y] = '<';
            StairDown = new Position(x, y);

            int newX = random.Next(1, Width - 1);
            int newY = random.Next(1, Height - 1);

            // Make sure the new position is not the same as the first one
            while (newX == x && newY == y)
            {
                newX = random.Next(1, Width - 1);
                newY = random.Next(1, Height - 1);
            }

            Mapping[newX, newY] = '>';
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
                        newMap.Mapping[x, y] = '#';
                    }
                    else
                    {
                        newMap.Mapping[x, y] = emptySpaceChar;
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