namespace School_Project
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        //tarkistetaan onko Positionit samat
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Position other = (Position)obj;
            return (X == other.X && Y == other.Y);
        }

        public override int GetHashCode()
        {
            return (X * 31) ^ Y;
        }
    }
}