using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace School_Project
{
    public class Screen
    {
        private readonly int Width;
        private readonly int Height;

        private GameController gc = GameController.Instance;

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public Screen(int width, int height)
        {
            Width = width;
            Height = height;
            IntPtr consoleWindow = GetConsoleWindow();
            if (consoleWindow != IntPtr.Zero)
            {
                //tää maximoi konsolin (en tiä toimiiko visual studio code konsolissa). suosittelen vahvasti visual studioo, tai sit ku buildaa, ni menee siihen kansioon ja käyttää exe filee
                ShowWindow(consoleWindow, 3);
            }

            /*   Console.WindowHeight = Height;
               Console.WindowWidth = Width;
               Console.SetBufferSize(width, height);*/
#pragma warning disable CA1416 // Validate platform compatibility
            Console.SetWindowSize(width, height);

            Console.SetBufferSize(width, height);
#pragma warning restore CA1416 // Validate platform compatibility

            Console.CursorVisible = false;
        }

        public void PrintPlayer()
        {
            Console.SetCursorPosition(gc.Player.Pos.X, gc.Player.Pos.Y);
            Write(gc.Player.Mark.ToString(), gc.Player.Color);
            PrintPlayerStats();
        }

        public void PrintEnemy(Enemy enemy)
        {
            WriteAtPosition(enemy.Pos, enemy.Mark, enemy.Color);
        }

        public void PrintPlayerStats()
        {
            Console.SetCursorPosition(0, gc.Height);
            Write(gc.Player.GetStats());
        }

        public void PrintInspectingObject(string message)
        {
            Console.SetCursorPosition(0, gc.Height + 1);
            Write(message);
        }

        public void PrintMessageLog()
        {
            gc.MessageLog.PrintMessages();
        }

        public void PrintMap()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            for (int y = 0; y < gc.Map.Height; y++)
            {
                for (int x = 0; x < gc.Map.Width; x++)
                {
                    Console.SetCursorPosition(x, y);

                    Write(gc.Map.Mapping[x, y].Mark.ToString(), gc.Map.Mapping[x, y].Color);
                    /* if (!gc.StairsGenerated)
                     {
                         gc.Map.GenerateStairs();
                         gc.StairsGenerated = true;
                     }*/
                }
            }
        }

        public void PrintEntities(List<Entity> entities)
        {
            // tulostetaan jokainen entity ruudulle.
            foreach (Entity e in entities)
            {
                WriteAtPosition(e.Pos, e.Mark, e.Color);
            }
        }

        public void Write(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.Write(text);
            Console.ResetColor();
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteAtPosition(Position pos, char mark)
        {
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(mark);
        }

        public void WriteAtPosition(Position pos, char mark, ConsoleColor color)
        {
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = color;
            Console.Write(mark);
            Console.ResetColor();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void DrawScreen()
        {
            Clear();
            //tähän tulee vielä kaikki mapin piirtämiset, entityt, pelaaja jne. kunhan ne ny on eka valmiina.
            // Generate stairs if they haven't been generated already

            PrintMap();

            // tähän public void DrawMap(Map map) { joku tyhmä esimerkki if(map.mapArray[10,10] == TileType.Wall { console.write("#"); }

            // Print the Player on the screen
            PrintPlayer();

            PrintEntities(gc.entities);

            // Keep moving the Player until the user presses the Esc key
        }

        public void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public void EndScreen()
        {
            Clear();
            //tämä  ny tässä testimielessä, joku oma metodi loppuscreenille ja sit lyö tietoo vähän tietokantaan ny hyvä tulee.
            Console.WriteLine("Tapoit seuraavat viholliset", Console.ForegroundColor = ConsoleColor.Green);
            foreach (Entity e in gc.GameStats.EnemiesKilled)
            {
                Console.WriteLine($"{e.Name} {e.Description}", Console.ForegroundColor = ConsoleColor.Yellow);
            }

            Console.WriteLine($"Teit yhteensä {gc.GameStats.DamageDealt} vahinkoa", Console.ForegroundColor = ConsoleColor.Green);
            Console.WriteLine($"Otit yhteensä {gc.GameStats.DamageTaken} vahinkoa", Console.ForegroundColor = ConsoleColor.Red);

            Console.WriteLine($"Keräsit myös {gc.GameStats.ItemsCollected.Count} pulloa näistä olisi saanut palautus rahoja {gc.GameStats.ItemsCollected.Count * 0.10} euroa. Harmi!", Console.ForegroundColor = ConsoleColor.Blue);
            Console.WriteLine($"Arvioitu promillemääräsi oli {gc.GameStats.ItemsCollected.Count * 0.15} promilleä. Melkoinen saavutus");
            Console.WriteLine($"Lopulta kuolit lähiön tasolla {gc.Level + 1}. olit itse {gc.Player.Level} tasolla", Console.ForegroundColor = ConsoleColor.Green);

            Console.WriteLine("\nPaina ESC jatkaaksesi!\n", Console.ForegroundColor = ConsoleColor.Yellow);

            while (true)
            {
                var input2 = Console.ReadKey(true);
                if (input2.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Silmäsi aukeavat ja havahdut olevasi sairaalassa letkuissa!");
            Console.WriteLine("hetken katseltasi ympärillesi huomaat väiskin istunvan penkillä huoneen kulmassa");
            Console.WriteLine("eikä si mitenkä kovin ilosen näkösenä. Hetken kaavittuasi kuolaa huuliltasi kysyt väiskiltä");
            Console.WriteLine("et mitäs mää täällä tee ja mitä nää letkut on? Sainks mää nii pahasti tauluu vai mitä kävi?");
            Console.WriteLine("Väiski si tokas et eks sää saatana retku muista viimestä parii viikkoo?");
            Console.WriteLine("Koitat selittää väiskille huikeista uroteoistasi kylillä ja kuin oot vedelly kaikkia pataa");
            Console.WriteLine("ja pikkusen sivussa ottanu nestettä...");
            Console.WriteLine("Väiski si totee et joo tosiaa oot pyöriny meiän talon pihassa ihan naamaat huudellen rivouksia,");
            Console.WriteLine("tapellu pyykkitelinee ja puitten kans sekä koittanu vonkaa piirakkaa naapurin irma mummelilta! ");
            Console.WriteLine("Vähä väliä kävit pöllyyttää pihan roskista ja toit kassi tolkulla jotai paskaa mun ovelle et");
            Console.WriteLine("tuut ny markettii ostoksille! Sit sää perkele näpytteliit hetken mun pasianssi pakkaa ja huusit jackpot!");
            Console.WriteLine("ja tulit tuomaa jotai roskaa pöytää et ny on ruplaa millä osetella! Oli hirvee homma aina saada");
            Console.WriteLine("sut houkuteltua painuu pellolle viinapullolla mut sit lähit onnessas takas pihaa ja huusi vaihtavas");
            Console.WriteLine("maisemat vähä laadukkaampii alueisii mut kuiteski kiipesit vaa pihan hiekkalaatikolle ja sama ralli jatku!");
            Console.WriteLine("perkele pari viikkoo sää siinä kerkesit sekoilee kunnes pihan kakarat paino sut sileeks ja soitettii");
            Console.WriteLine("lanssi hakee sut ja tohtorit totes et oot kiskassu kunnon viina psykoosit päälle");
            Console.WriteLine("Tähän si sankarimme totee et no jopas oli reissu, ihmettelinki et miks kaikki näyttää kirjaimilta ja maisemat");
            Console.WriteLine("pixeli mössöö. No ei siinä tänää paikkaillaa ja huomenna uusiks!");
            Console.WriteLine("");
            Console.WriteLine("\nPaina ESC jatkaaksesi!", Console.ForegroundColor = ConsoleColor.Yellow);
            while (true)
            {
                var input2 = Console.ReadKey(true);
                if (input2.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

        }
    }
}