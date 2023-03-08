namespace School_Project
{
    public class Player
    {
        private string name;
        private int health {get;set;}
        private int points {get;set;}
        //private int money {get;set;}
        private Position pos;
        public Player(string name, int xAxis, int yAxis)
        {
            this.name = name;
            //this.health = ?; Päätetään myöhemmin
            this.points = 0;
            this.pos = new Position(xAxis, yAxis);
        }
    }
}