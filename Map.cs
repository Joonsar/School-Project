
using System;
using System.Collections.Generic;

namespace School_Project
{
    public class Map
    {
        public char[,] Mapping { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public List<Entity> entities;

        public Map(int width, int height, char emptySpaceChar = '.')
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
                    if (x == 0 || x == width -1 || y == 0 || y == height - 1)
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

        public Entity IsEnemyAtPosition(int x, int y)
        {
            foreach(Entity e in entities)
            {
                if(e.Pos.X == x && e.Pos.Y == y && e.GetType() == typeof(Enemy))
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

        public void CreateEnemies()
        {
            for(int i = 0; i < 5; i ++)
            {
                var enemy = new Enemy("juoppo" + i, "melkonen juoppo", new Position(15, 15), 'J', ConsoleColor.Yellow);
                entities.Add(enemy);
            }
        }

        // Update the game board with a new 2D char array
        public void UpdateMap(char[,] updatedBoard)
        {
            Mapping = updatedBoard;
        }

        public void GenerateRandomRooms()
        {
            // Using <> as markers for stairs
            // Tee liikkuminen yksi ylös päin ja yksi alaspäin huoneita. Jos aikaa niin Generoi myös erikoikonen huone
        }
    }

}
