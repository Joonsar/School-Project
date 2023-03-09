namespace School_Project
{
    //itseasiassa miksei me käytettäs vector2 positionissa? Tosin tääkin toimii, mutta jos meillä olis esim vector2 position, niin siihen vois lisätä position += new vector2(1,0) jne
    public class Player
    {
        //nää toimii, mutta yleinen nimikäytäntö on että nää alkaa isolla kirjaimella (turhaa höpötystä, mutta näin ne yleensä tehdään) :)
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
