using System;

namespace School_Project
{
    public class PlayerMovement
    {
        private Map map;
        private int x;
        private int y;

        public PlayerMovement(Map map, int startX, int startY)
        {
            this.map = map;
            this.x = startX;
            this.y = startY;
            this.map.SetPlayerPosition(x, y);
        }

        public void MoveUp()
        {
            int newX = x;
            int newY = y - 1;

            if (map.IsPositionValid(newX, newY))
            {
                map.ClearPlayerPosition(x, y);
                x = newX;
                y = newY;
                map.SetPlayerPosition(x, y);
                map.Draw();
            }
        }

        public void MoveDown()
        {
            int newX = x;
            int newY = y + 1;

            if (map.IsPositionValid(newX, newY))
            {
                map.ClearPlayerPosition(x, y);
                x = newX;
                y = newY;
                map.SetPlayerPosition(x, y);
                map.Draw();
            }
        }

        public void MoveLeft()
        {
            int newX = x - 1;
            int newY = y;

            if (map.IsPositionValid(newX, newY))
            {
                map.ClearPlayerPosition(x, y);
                x = newX;
                y = newY;
                map.SetPlayerPosition(x, y);
                map.Draw();
            }
        }

        public void MoveRight()
        {
            int newX = x + 1;
            int newY = y;

            if (map.IsPositionValid(newX, newY))
            {
                map.ClearPlayerPosition(x, y);
                x = newX;
                y = newY;
                map.SetPlayerPosition(x, y);
                map.Draw();
            }
        }
    }
}
