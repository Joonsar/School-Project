
using System;

namespace School_Project
{
    public class Map
    {
        public char[,] Mapping { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Map(int width, int height, char emptySpaceChar = '.')
        {
            this.Width = width;
            this.Height = height;

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
        }


        // Update the map with player's current position
        public void SetPlayerPosition(int x, int y)
        {
            Mapping[y, x] = '@';
        }
        // Clear player's previous position on the map
        public void ClearPlayerPosition(int x, int y)
        {
            Mapping[y, x] = '.';
        }

        // Check if a given position is within the bounds of the game board
        public bool IsPositionValid(int x, int y)
        {
            
            return x >= 0 && x < Width && y >= 0 && y < Height && Mapping[x, y] != '#';
        }

        // Draw the game board to the console
        public void Draw()
        {
            Console.Clear();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Mapping[y, x] == '@')
                    {
                        // Set player's character to green
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(Mapping[y, x]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        // Draw the game board to the console, including edges of the board
        public void DrawMap()
        {
            Console.Clear();
            for (int y = 0; y < Mapping.GetLength(0); y++)
            {
                for (int x = 0; x < Mapping.GetLength(1); x++)
                {
                    if (Mapping[y, x] == '@')
                    {
                        // Set player's character to green
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (Mapping[y, x] == '#')
                    {
                        // Set edges of the board to gray
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(Mapping[y, x]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        // Update the game board with a new 2D char array
        public void UpdateMap(char[,] updatedBoard)
        {
            Mapping = updatedBoard;
        }
    }

}
