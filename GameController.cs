using System;

namespace School_Project
{
    public class GameController
    {
        public static GameController Instance { get; set; }
        public Player Player { get; set; }
        public Map Map { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int Turn { get; set; }

        public Screen screen;
        private bool running = false;

        public GameController()
        {
        }

        public void Init()
        {
            Turn = 1;
            Width = 80;
            Height = 20;
            //All here that needs to be initialized like map, Player, screen etc.
            // for example Player Player = new Player(blabla);
            // Screen screen = new screen(80,35) (or whatever it is)
            // Map map =new Map(mitä onkaan)
            screen = new Screen(Width, Height);
            Map = new Map(Width, Height);
            Player = new Player("Pelaaja", 100, 100);
            screen.PrintMap();
            running = true;
        }

        public void Run()
        {
            while (running)
            {
                screen.PrintPlayer();
                

                var input = Console.ReadKey(true);
                CheckInput(input);

                //tähän game looppi.
                //Mikä ikinä onkaan syötteen luku.. esim InputParser() -> täällä voi sit olla, että jos vaikka rightarrow, niin Player.move(0,1) ja
                //ja jos käytetään tätä gamecontroller instancee. esim niinkun tossa program.cs on toi kommentoituna pois. niin joka classissa voi sit käyttää sitä
                //kun luo vaan classissa GameController gc = Gamecontroller.Instance. sit pystyy kutsuun gc.Player.blabla, gc.screen.blabla, gc.mikälie.blabla.
                //eli voidaan poistaa tosta program.cs noi Screen = screen blabla jne
                //esimerkkinä vaikka jos pitää piirtää pelaaja siihen pisteeseen missä se on. (screen class todennäkösesti). niin voidaan vaan laitta
                //mikälie meidän tulostus funktio onkaan.
                // ja tässä game loopissa voidaan kutsua sit screen.printPlayer(); tai jos halutaan yksinkertastaa niin Player luokassa voi olla vaikka draw funktio.
                //niin sit voidaan vaan kutsua Player.Draw(); ja se sit viitaa screen luokkaan jne.
                Turn++;
            }
        }

        private void CheckInput(ConsoleKeyInfo input)
        {
            // Move the Player in the corresponding direction
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    Player.MovePlayer(0, -1);
                    break;

                case ConsoleKey.DownArrow:
                    Player.MovePlayer(0, 1);
                    break;

                case ConsoleKey.LeftArrow:
                    Player.MovePlayer(-1, 0);
                    break;

                case ConsoleKey.RightArrow:
                    Player.MovePlayer(1, 0);
                    break;
            }
        }
    }
}