using System;
using System.Collections.Generic;

using System.Text.Json;

namespace School_Project
{
    public class GameController
    {
        public static GameController Instance { get; set; }
        public Player Player { get; set; }
        public StartScreen StartScreen { get; set; }

        public string PlayerName { get; set; }
        public Map Map { get; set; }

        public List<Map> Maps { get; set; }
        public int Level { get; set; }
        public int EnemiesCount { get; set; }

        //width / height max valuet. pitäisi fiksata ongelma, jos halutaan eri kokoisia mappeja. kun luodaan screen näillä arvoilla
        public static readonly int SCREEN_WIDTH = Console.LargestWindowWidth;

        public static readonly int SCREEN_HEIGHT = Console.LargestWindowHeight;

        //nää pitäs vaihtaa MapWidth, kun joku jaksaa. käytetään perkeleen monessa paikassa :D
        public int Width { get; set; }

        public int Height { get; set; }

        public MessageLog MessageLog { get; set; }

        public GameStats GameStats { get; set; }

        private Random rand;

        public List<Entity> entities { get; set; }

        public int Turn { get; set; }

        public Screen screen;
        public bool running { get; set; }

        public bool Inspecting { get; set; }

        public Qmarket Qmarket { get; set; }

        //    private Stack<Map> previousMaps = new Stack<Map>();

        public bool StairsGenerated = false;

        private DatabaseTest db;

        public DataBase localdb { get; set; }

        public GameController()
        {
        }

        public void Init()
        {
            Qmarket = new Qmarket();
            localdb = new DataBase();
            localdb.CreateDatabase();
            db = new DatabaseTest();
            //All here that needs to be initialized like map, Player, screen etc.
            // for example Player Player = new Player(blabla);
            // Screen screen = new screen(80,35) (or whatever it is)
            // Map map =new Map(mitä onkaan)
            Width = 50;
            Height = 24;
            Level = 0;
            EnemiesCount = 3;
            Turn = 1;
            rand = new Random();

            MessageLog = new MessageLog(Height);
            StartScreen = new StartScreen();

            screen.Clear();
            StartScreen.Run();
            entities = new List<Entity>();

            Maps = new List<Map>();
            this.Map = new Map(Width, Height);
            Maps.Add(Map);
            Map.CreateEnemies(this.Level, this.EnemiesCount);

            Player = new Player(PlayerName, 100, 100);
            GameStats = new GameStats();

            // kopioidaan tämänhetkisen mapin entityt entities listaan. Näin voidaan luoda uusia mappeja ja niiden viholliset jäävät niihin talteen.
            entities = Map.entities;

            screen.DrawScreen();
        }

        public void Run()
        {
            SoundManager.ChangeMusic(SoundType.MainMusic);
            while (running)
            {
                if (Inspecting)
                {
                    var input = Console.ReadKey(true);
                    Input.CheckInput(input.Key);
                }
                else
                {
                    //tulostetaan messagelogin sisältö
                    screen.PrintMessageLog();
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

                    Input.CheckInput(input.Key);
                    Player.Update();
                    screen.PrintEntities(entities);
                    Turn++;
                }
            }
        }

        //Nää 2 metodia tein ja sit niitä käytetää player luokassa kuollessa ja programmissa ku peli alkaa ja sit poistin tosta init metodista tualta lopusta noi localdb = new...
        // Ku se oli tua alussa jo ja sit siirsin ton db = new databasetest... kans tonne alkuu mut kokeilin ni ei siitä siirrosta ollu kii
        // Sit vaihoin tonne input luokkaa sille ku painaa Q ni
        // gc.Player.HitPoints = -1;
        // gc.Player.CheckDeath();
        // Korjasin myös noi database tulostelut ja siinä si tommonen hiano ratkasu tolle haulle pelaajan nimen mukaa et sai ne sijotukset kohillee ku muuten olis joko pitäny copy pastee koko roska uutee
        //metodii tai si koittaa jotenki änkee se sijotus sinne kantaa mitä en ny heti ainaka keksi et tekis järkevästi
        public void CreateScreen()
        {
            screen = new Screen(SCREEN_WIDTH, SCREEN_HEIGHT);
        }

        public void UpdateDatabases()
        {
            GameStats.Update();
            localdb.GetData(GameStats);
            localdb.SaveToDatabase();

            var gamestatsJson = JsonSerializer.Serialize(GameStats);

            db.UploadToServer(gamestatsJson);
        }

        private void MoveEntities()
        {
            foreach (Entity e in entities)
            {
                //katsotaan jos entityn tyyppi on Enemy ja liikutetaan jos on
                if (e.GetType() == typeof(Enemy))
                {
                    e.Update();
                }
            }
        }

        public void ChangeLevel(int direction)
        {
            //jos liikutaan alaspäin
            if (direction == 1)
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
                    MessageLog.AddMessage(new LogMessage($"{Player.Name} saapuu lähiön tasolle {Level + 1}", ConsoleColor.Yellow));
                }

                //jos ei löydy jo listasta
                else
                {
                    //tehdään uusi mappi
                    var newMap = new Map(Width, Height);
                    //tehdään viholliset

                    //tehdään portaat

                    //newMap.GenerateStairs();
                    //lisätään uusi mappi listaan
                    Maps.Add(newMap);
                    //vaihdetaan gamecontrollerin mapiksi uusi mappi
                    Map = newMap;
                    //haetaan levelin entityt mapista
                    entities = Map.entities;
                    Level++;
                    Player.Pos = Map.StairUp;
                    newMap.CreateEnemies(this.Level, this.EnemiesCount);

                    screen.DrawScreen();
                    Player.SetPlayerLastPosition();
                    MessageLog.AddMessage(new LogMessage($"{Player.Name} saapuu lähiön tasolle {Level + 1}", ConsoleColor.Yellow));
                }
            }

            if (direction == -1)
            {
                if (Level > 0)
                {
                    Map = Maps[Level - 1];
                    entities = Map.entities;
                    Level--;
                    Player.Pos = Map.StairDown;
                    screen.DrawScreen();
                    Player.SetPlayerLastPosition();
                    MessageLog.AddMessage(new LogMessage($"{Player.Name} saapuu lähiön tasolle {Level + 1}", ConsoleColor.Yellow));
                }
                else
                {
                    return;
                }
            }
        }
    }
}