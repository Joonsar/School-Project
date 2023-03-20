using System;
using System.Collections.Generic;

namespace School_Project
{
    public class GameController
    {
        public static GameController Instance { get; set; }
        public Player Player { get; set; }
        public Map Map { get; set; }

        public List<Map> Maps { get; set; }
        public int Level { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private Random rand;

        public List<Entity> entities { get; set; }

        public int Turn { get; set; }

        public Screen screen;
        private bool running = false;

    //    private Stack<Map> previousMaps = new Stack<Map>();

        public bool StairsGenerated = false;
        public GameController()
        {
        }

        public void Init()
        {

            Level = 0;
            Maps = new List<Map>();
            Width = 50;
            Height = 20;
            this.Map = new Map(Width, Height);
            Map.CreateEnemies();
            Maps.Add(Map);
            rand = new Random();
            entities = new List<Entity>();
            Turn = 1;
           
            //All here that needs to be initialized like map, Player, screen etc.
            // for example Player Player = new Player(blabla);
            // Screen screen = new screen(80,35) (or whatever it is)
            // Map map =new Map(mitä onkaan)
            screen = new Screen(Width, Height);
            
            // kopioidaan tämänhetkisen mapin entityt entities listaan. Näin voidaan luoda uusia mappeja ja niiden viholliset jäävät niihin talteen.
            entities = Map.entities;
           
            Player = new Player("Pelaaja", 100, 100);

            //   screen.PrintMap();
            screen.DrawScreen();

            screen.PrintMap();
            //printing entities to screen
            screen.PrintEntities(entities);
            screen.PrintPlayer();

            running = true;
            
        }

        public void Run()
        {
            while (running)
            {
                


                

                //liikutetaan entityjä
                
                MoveEntities();
                //tulostetaan entityt
                //screen.PrintEntities(entities);

                //tulostetaan entityt ruudulle.


                //tähän game looppi.
                //Mikä ikinä onkaan syötteen luku.. esim InputParser() -> täällä voi sit olla, että jos vaikka rightarrow, niin Player.move(0,1) ja
                //ja jos käytetään tätä gamecontroller instancee. esim niinkun tossa program.cs on toi kommentoituna pois. niin joka classissa voi sit käyttää sitä
                //kun luo vaan classissa GameController gc = Gamecontroller.Instance. sit pystyy kutsuun gc.Player.blabla, gc.screen.blabla, gc.mikälie.blabla.
                //eli voidaan poistaa tosta program.cs noi Screen = screen blabla jne
                //esimerkkinä vaikka jos pitää piirtää pelaaja siihen pisteeseen missä se on. (screen class todennäkösesti). niin voidaan vaan laitta
                //mikälie meidän tulostus funktio onkaan.
                // ja tässä game loopissa voidaan kutsua sit screen.printPlayer(); tai jos halutaan yksinkertastaa niin Player luokassa voi olla vaikka draw funktio.
                //niin sit voidaan vaan kutsua Player.Draw(); ja se sit viitaa screen luokkaan jne.
                var input = Console.ReadKey(true);
                CheckInput(input);
                Turn++;
            }
        }

        private void MoveEntities()
        {
            foreach (Entity e in entities)
            {
                int moveX = rand.Next(-1, 2);
                int moveY = rand.Next(-1, 2);
                //katsotaan jos entityn tyyppi on Enemy ja liikutetaan jos on
                if (e.GetType() == typeof(Enemy))
                {
                    e.MoveEntity(moveX, moveY);
                }

            }
        }

        public void ChangeLevel(int direction)
        {
            //jos liikutaan alaspäin
            if(direction == 1)
            {
                //jos listasta löytyy jo seuraavan levelin kartta.
                if (Maps.Count > Level + 1)
                {
                    //otetaan mappi listasta ja vaihdetaan se gamecontrollerin mapiksi
                    Map = Maps[Level + 1];
                    //otetaan entityt mapista
                    entities = Map.entities;
                    //lisätään leveliin 1
                    Level++;
                    Player.Pos = Map.StairUp;
                    //piirrettään ruutu uudestaan
                    screen.DrawScreen();
                    Player.SetPlayerLastPosition();
                }

                //jos ei löydy jo listasta
                else
                {
                    //tehdään uusi mappi
                    var newMap = new Map(Width, Height);
                    //tehdään viholliset
                    newMap.CreateEnemies();
                    //tehdään portaat
                    
                    newMap.GenerateStairs();
                    //lisätään uusi mappi listaan
                    Maps.Add(newMap);
                    //vaihdetaan gamecontrollerin mapiksi uusi mappi
                    Map = newMap;
                    //haetaan levelin entityt mapista
                    entities = Map.entities;
                    Level++;
                    Player.Pos = Map.StairUp;
                    
                    screen.DrawScreen();
                    Player.SetPlayerLastPosition();
                }
            }

            if(direction == -1)
            {
                if(Level > 0)
                {
                    Map = Maps[Level - 1];
                    entities = Map.entities;
                    Level--;
                    Player.Pos = Map.StairDown;
                    screen.DrawScreen();
                    Player.SetPlayerLastPosition();
                }

                else
                {
                    return;
                }
            }
        }

        private void CheckInput(ConsoleKeyInfo input)
        {
            // Move the Player in the corresponding direction
            switch (input.Key)
            {
                case ConsoleKey.Q:
                    running = false;
                    break;
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
                case ConsoleKey.NumPad8:
                    Player.MovePlayer(0, -1);
                    break;

                case ConsoleKey.NumPad1:
                    Player.MovePlayer(-1, 1);
                    break;
                case ConsoleKey.NumPad3:
                    Player.MovePlayer(1, 1);
                    break;
                case ConsoleKey.NumPad9:
                    Player.MovePlayer(1, -1);
                    break;
                case ConsoleKey.NumPad7:
                    Player.MovePlayer(-1, -1);
                    break;
                case ConsoleKey.NumPad2:
                    Player.MovePlayer(0, 1);
                    break;

                case ConsoleKey.NumPad4:
                    Player.MovePlayer(-1, 0);
                    break;

                case ConsoleKey.NumPad6:
                    Player.MovePlayer(1, 0);
                    break;
                case ConsoleKey.Escape:
                    System.Environment.Exit(0);
                    break;
                case ConsoleKey.O:
                    ChangeLevel(1);
                    break;
                case ConsoleKey.I:
                    ChangeLevel(-1);
                    break;

                //case ConsoleKey.OemComma: // 
                //    if (Player.CollidesWith('<'))
                //    {
                //        previousMaps.Push(Map);
                //        Map = new Map(Width, Height);
                //    }
                //    break;

                //case ConsoleKey.OemPeriod: 
                //    if (previousMaps.Count > 0 && Player.CollidesWith('>'))
                //    {
                        
                //        Map = previousMaps.Pop();
                //    }
                //    break;
            }
        }

        
    }
}