
using System;

namespace School_Project
{
    public class Map
    {
        private char[,] mapping;
        private int width;
        private int height;

        public Map(int width, int height, char emptySpaceChar = '.')
        {
            this.width = width;
            this.height = height;

            // Create game board array
            mapping = new char[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        // Place "#" at edges of the board
                        mapping[y, x] = '#';
                    }
                    else
                    {
                        // Fill the rest of the board with empty space character
                        mapping[y, x] = emptySpaceChar;
                    }
                }
            }
        }


        // Update the map with player's current position
        public void SetPlayerPosition(int x, int y)
        {
            mapping[y, x] = '@';
        }
        // Clear player's previous position on the map
        public void ClearPlayerPosition(int x, int y)
        {
            mapping[y, x] = '.';
        }

        // Check if a given position is within the bounds of the game board
        public bool IsPositionValid(int x, int y)
        {
            
            return x >= 0 && x < width && y >= 0 && y < height && mapping[y, x] != '#';
        }

        // Draw the game board to the console
        public void Draw()
        {
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (mapping[y, x] == '@')
                    {
                        // Set player's character to green
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(mapping[y, x]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        // Draw the game board to the console, including edges of the board
        public void DrawMap()
        {
            Console.Clear();
            for (int y = 0; y < mapping.GetLength(0); y++)
            {
                for (int x = 0; x < mapping.GetLength(1); x++)
                {
                    if (mapping[y, x] == '@')
                    {
                        // Set player's character to green
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (mapping[y, x] == '#')
                    {
                        // Set edges of the board to gray
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(mapping[y, x]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        // Update the game board with a new 2D char array
        public void UpdateMap(char[,] updatedBoard)
        {
            mapping = updatedBoard;
        }
    }

}
