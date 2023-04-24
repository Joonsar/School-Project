using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace School_Project
{
    class Room
    {
        private GameController gc = GameController.Instance;
        public Rectangle Rect { get; set; }

        public Room()
        {
            GenerateRoom();
        }

        public void GenerateRoom()
        {
            Random random = new Random();
            int width = random.Next(5, 14); // Random width between
            int height = random.Next(5, 14); // Random height between
            int corridorWidth = 1; // Width of the corridor
            int x = random.Next(1, gc.Width - roomWidth - corridorWidth - 1); // Random x position within the game board
            int y = random.Next(1, gc.Height - roomHeight - corridorWidth - 1); // Random y position within the game board

            // Add an empty row and column to the room dimensions
            int width = roomWidth + corridorWidth;
            int height = roomHeight + corridorWidth;

            // Shift the room position to account for the added row and column
            x += corridorWidth;
            y += corridorWidth;

            Rect = new Rectangle(x, y, width, height);


        }
    }
}
