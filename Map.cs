
using System;

namespace School_Project
{
    public class Map
    {
        private GameController gc = GameController.Instance;
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

            int newX = random.Next(1, Width - 1);
            int newY = random.Next(1, Height - 1);

            // Make sure the new position is not the same as the first one
            while (newX == x && newY == y)
            {
                newX = random.Next(1, Width - 1);
                newY = random.Next(1, Height - 1);
            }

            Mapping[newX, newY] = '>';

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
