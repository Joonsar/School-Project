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
            int width = random.Next(7, 20); // Random width between
            int height = random.Next(7, 20); // Random height between
            int x = random.Next(1, gc.Width - width - 1); // Random x position within the game board
            int y = random.Next(1, gc.Height - height - 1); // Random y position within the game board
            Rect = new Rectangle(x, y, width, height);


        }
    }
}
