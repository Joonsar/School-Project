namespace School_Project
{
    //tän koko systeemin laittasin player classin sisään. ja poistasin moveup movedown jne, tekisin yhen metodin Move(Direction dir) ja directionista tekisin
    //enum up,down,left,right,upright jnejne.. esimerkiksi up = (0,-1) down = (0,1) jnejne.. ja map on tuolla gamecontroller classissa. esim tässäkin käytettäs gc.map.IsPositionValid()
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
