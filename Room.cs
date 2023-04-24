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
            int x = random.Next(2, gc.Width - width - 1); // Random x position within the game board
            int y = random.Next(2, gc.Height - height - 1); // 


            Rect = new Rectangle(x, y, width, height);


        }
    }
}
