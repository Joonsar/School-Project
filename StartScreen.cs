using System;
using System.Diagnostics;

namespace School_Project
{
    public class StartScreen
    {
#pragma warning disable CS4014
        private string command;
        private GameController gc = GameController.Instance;
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable CA1416 // Validate platform compatibility
        private static System.Media.SoundPlayer player = new();
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning restore IDE0044 // Add readonly modifier
        private readonly string IntroText = "       _                                 _             _             \r\n      | |                               | |           (_)            \r\n      | |_   _  ___  _ __   ___  _ __   | |_ __ _ _ __ _ _ __   __ _ \r\n  _   | | | | |/ _ \\| '_ \\ / _ \\| '_ \\  | __/ _` | '__| | '_ \\ / _` |\r\n | |__| | |_| | (_) | |_) | (_) | | | | | || (_| | |  | | | | | (_| |\r\n  \\____/ \\__,_|\\___/| .__/ \\___/|_| |_|  \\__\\__,_|_|  |_|_| |_|\\__,_|\r\n                    | |                                              \r\n                    |_|      ";
        private readonly ConsoleColor[] Colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Cyan };
        private readonly int maxPlayerNameLength = 15;
        private readonly string viewer = " __      ___                              _ _                   _   _               _                 _       _              _ \r\n \\ \\    / (_)                            | (_)                 | | (_)             (_)               | |     (_)            | |\r\n  \\ \\  / / _  _____      _____ _ __    __| |_ ___  ___ _ __ ___| |_ _  ___  _ __    _ ___    __ _  __| |_   ___ ___  ___  __| |\r\n   \\ \\/ / | |/ _ \\ \\ /\\ / / _ \\ '__|  / _` | / __|/ __| '__/ _ \\ __| |/ _ \\| '_ \\  | / __|  / _` |/ _` \\ \\ / / / __|/ _ \\/ _` |\r\n    \\  /  | |  __/\\ V  V /  __/ |    | (_| | \\__ \\ (__| | |  __/ |_| | (_) | | | | | \\__ \\ | (_| | (_| |\\ V /| \\__ \\  __/ (_| |\r\n     \\/   |_|\\___| \\_/\\_/ \\___|_|     \\__,_|_|___/\\___|_|  \\___|\\__|_|\\___/|_| |_| |_|___/  \\__,_|\\__,_| \\_/ |_|___/\\___|\\__,_|\r\n                                                                                                                               \r\n                                                                                                                               ";
        public string PlayerName { get; set; }

        public StartScreen()
        {
            this.command = "";
        }

        public void Run()
        {
            Console.Clear();
            Console.Write(viewer);
            SoundManager.Play(SoundType.Viewer);
            System.Threading.Thread.Sleep(5000);
            Console.Clear();

            SoundManager.Play(SoundType.GameStart);
            PrintInfo();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Anna Komento: ");
                this.command = Console.ReadLine();
                if (this.command == "1")
                {
                    this.NewGame();
                    gc.running = true;
                    break;
                }
                if (this.command == "2")
                {
                    this.DatabaseUsage();
                }

                if (this.command == "3")
                {
                    SoundManager.Play(SoundType.Score);
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = "http://juopontarina.servebeer.com/",
                        UseShellExecute = true
                    });
                }

                if (this.command == "4")
                {
                    Console.Clear();
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(" ");
                    Console.WriteLine("Merkit:");
                    Console.WriteLine(" # - Seinä               + - Ovi");
                    Console.WriteLine(" ! - Itemi               Kirjaimet - Vastustajia");
                    Console.Write(" < ", Console.ForegroundColor = ConsoleColor.Green);
                    Console.Write("- Rappuset ylös       ", Console.ForegroundColor = ConsoleColor.Yellow);
                    Console.Write(">", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write(" - Rappuset alas\n", Console.ForegroundColor = ConsoleColor.Yellow);
                    Console.Write(" " + Map.qMarket.Mark + " -", Console.ForegroundColor = Map.qMarket.Color);
                    Console.WriteLine(" " + Map.qMarket.Description, Console.ForegroundColor = ConsoleColor.Yellow);
                    Console.WriteLine(" ");
                    Console.WriteLine("- Paina Välilyöntiä tutkiaksesi maailmaa.");
                    Console.WriteLine(" ");
                    Console.WriteLine("- Liikuta pelaajaa nuoli- tai numpad näppäimillä.");
                    Console.WriteLine(" ");
                    Console.WriteLine("- Paina Esc lopettaaksesi pelin");
                    System.Threading.Thread.Sleep(3000);
                    this.PrintInfo();
                }
                if (this.command == "0")
                {
                    gc.running = false;
                    System.Environment.Exit(0);
                }
            }
        }

        private void DatabaseUsage()
        {
            Console.Clear();
            gc.localdb.PrintAllPlayerData();
            SoundManager.Play(SoundType.Score);
            while (true)
            {
                Console.WriteLine("1 - Hae pelaajan sijoitukset nimellä");
                Console.WriteLine("2 - Hae tarkemmat tiedot id:llä");
                Console.WriteLine("3 - Tulosta sijoitus lista");
                Console.WriteLine("4 - Paluu valikkoon");
                Console.WriteLine("9 - Tyhjennä tilastot");
                Console.Write("Anna komento: ");
                string c = Console.ReadLine();
                if (c == "1")
                {
                    Console.Write("Anna pelaajan nimi: ");
                    string name = Console.ReadLine();
                    gc.localdb.PrintPlayerDataByName(name);
                }
                if (c == "2")
                {
                    Console.Write("Anna id: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    gc.localdb.PrintPlayerStatsWhitId(id);
                }
                if (c == "3")
                {
                    gc.localdb.PrintAllPlayerData();
                    SoundManager.Play(SoundType.Score);
                }
                if (c == "4")
                {
                    this.PrintInfo();
                    break;
                }
                if (c == "9")
                {
                    gc.localdb.ClearDatabase();
                    this.PrintInfo();
                    break;
                }
            }
        }

        private void PrintInfo()
        {
            Random random = new();
            Console.ForegroundColor = Colors[random.Next(Colors.Length)];
            Console.WriteLine(IntroText);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1 - Uusi peli");
            Console.WriteLine("2 - Paikallinen piste tilasto");
            Console.WriteLine("3 - Maailmanlaajuinen ranking tilasto");
            Console.WriteLine("4 - Info");
            Console.WriteLine("0 - Lopeta");
        }

        public void NewGame()
        {
            ConsoleColor date = ConsoleColor.Green;
            ConsoleColor text = ConsoleColor.Yellow;
            ConsoleColor drink = ConsoleColor.Red;
            string lines = new('-', Console.LargestWindowWidth);
            String spaces = new(' ', 12);
            Console.WriteLine();

            bool isNameValid = false;
            while (!isNameValid)
            {
                Console.Write("Anna sankarillesi nimi: ", Console.ForegroundColor = ConsoleColor.Yellow);
                this.PlayerName = Console.ReadLine();
                if (this.PlayerName.Length > 0 && this.PlayerName.Length < maxPlayerNameLength)
                {
                    isNameValid = true;
                }
                else if (this.PlayerName.Length < 1)
                {
                    Console.WriteLine("Pelaajan nimi ei voi olla tyhjä, yritäppä uudestaan");
                }
                else if (this.PlayerName.Length >= maxPlayerNameLength)
                {
                    Console.WriteLine($"Pelaajan nimi ei voi olla yli {maxPlayerNameLength} merkkiä pitkä, yritäppä uudestaan");
                }
            }
            gc.PlayerName = this.PlayerName;
            Console.Clear();

            // SoundManager.PlayMainMusic();

            Console.WriteLine(lines, Console.ForegroundColor = text);
            PrintText(spaces + "Luet nyt päiväkirjaa minkä omistaja on " + this.PlayerName + " ja jos en oo kuollu ja luet ilman lupaa ni etin sut ja vedän lättyy runkku!", text);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("01.02.2020", date);
            PrintText("Elämä hymyilee, bisnekset rullaa. On vaimoo, on isoo taloo, on autoo, on venettä yms! Janoo menee vissyä ja safkan kans ehkä tilkka viiniä. Elämä hymyilee", text);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("04.02.2020", date);
            PrintText("Iski joku saatanan rokko keniasta ja väki paniikissa pistää kuljetukset poikki ja ulkonaliikkumis kieltoja. Kaikki bisnekset kusee huolella!", text);
            PrintText(spaces + "Ny pakko vetää pari kaljaa ressii!", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("04.07.2020", date);
            PrintText("Firma meni konkkaan ja ulosottaja myi sen pilkka hintaa naapurin liimatukka petterille. (siinä vasta kunnon mulkku! Hinkkaa bemariaa pihassa ilman paitaa sikspakkiä esitelle ja kehuu kuin on bisnesmies vaikka isin taaloilla tehny vaa tappioo ja sikspäkkiki on silikoonia.)", text);
            PrintText(spaces + "Pistää vihaks sen verta et taidan hakee laatikon nelosta!", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("01.08.2020", date);
            PrintText("Appiukko onneks sponssas et pääsis uutee alkuu mut sit toi prkl läskiperse muija sai selville että oon vuosia kusettanu olevani ylitöissä vaikka tuli temmottua savuja meitsin sihteeristä sirpasta. Paska homma, sinne meni seki perse!", text);
            PrintText(spaces + "Tilanne huutaa paria napanderia!!", drink);

            Console.WriteLine();
            Console.WriteLine("Paina jotain näppäintä jatkaaksesi", Console.ForegroundColor = ConsoleColor.Blue);
            Console.ReadKey(false);
            Console.Clear();
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("02.10.2020", date);
            PrintText("Muija ny sit tietty otti kakarat ja lähti (Ne saatanan kiittänättömät paskiaiset saaki mennä!) mut appiukon rahoja tuli ikävä vaikka joutuki nuolee se haisevan mulkun persettä", text);
            PrintText(spaces + "Ny kyllä vedetää kunnon kännit!", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("24.12.2020", date);
            PrintText("No erohan siitä tuli ja se saatanan ammatti runkkari palkkas tyttärellee kunnon hyeenan imee pölykki taskusta! Hyvää joulua vaa teillekki prkl runkkarit!", text);
            PrintText(spaces + "Ny kyllä si vedetää viikko viinaa!", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("12.01.2021", date);
            PrintText("Meni vähä pitkäks ja ihan törkee darra mut ny tarvii himmaa ja ihan vaan tasotella muutama päivä..", text);
            PrintText(spaces + "Sikspäkki viinilaatikoita hoitanee homman kotio", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("28.01.2021", date);
            PrintText("Ny on taisteltu asiosta kelan kanssa sain viihtyisän 14 neliön kompaktin yksiön yhteisillä wc tiloilla ihan entisen talon vierestä et näkee suoraa vanhaa omaa olkkarii ku ämmä kattoo salkkareitaa mun 70 tuumasesta LED TV:stä.", text);
            PrintText(spaces + "Kyllä mies lääkkeen tietää, viinalla tänki paskan sietää!", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            Console.WriteLine();
            Console.WriteLine("Paina jotain nappia jatkaaksesi", Console.ForegroundColor = ConsoleColor.Blue);
            Console.ReadKey(false);
            Console.Clear();
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("22.03.2021", date);
            PrintText("Vähä on tullu ryypiskeltyä mut hei kai ny maistuu tässä paskassa! Ny ne laitto työkkäristäki kuntouttavaan työtoimintaan ja viä vittu mun omaa entiseen firmaa kattelee sitä liimatukka petteriä! Sinne kuunteleen sen hitusen omakehun löyhkäsiä juttuja vaikka äijä ihan teline!", text);
            PrintText(spaces + "Pistää sen verta vihaks et pakko ryyppää varastossa", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("12.04.2021", date);
            PrintText("Jotai viinan huurusia muistikuvia puistosta ja kämppä täynnä juoppoja...", text);
            PrintText(spaces + "pakko antaa mennä vaa ei tätä sevinpäin kestä", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("30.05.2021", date);
            PrintText("Rallatrallati rai! Ny juhlitaa!", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("01.06.2021", date);
            PrintText("Kattelin ikkunasta vanhaa kämppää ni siähän se saatanan tuhkamuna homo petteri kairas sitä läskiperse ex-muijaa!", text);
            PrintText(spaces + "Kävi kiahuttaa mut onneks kävin alkossa jo päivällä tankkaa varstot...", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            Console.WriteLine();
            Console.WriteLine("Paina jotain nappia jatkaaksesi", Console.ForegroundColor = ConsoleColor.Blue);
            Console.ReadKey(false);
            Console.Clear();
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("20.06.2021", date);
            PrintText("RapPPIppioolla oONO huvAä oollaa ei hhHuuolet pinAAaaaa eIIii RAAasaituu pOllaaaaa", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("17.06.2021", date);
            PrintText("Olin pari viikkoo rokulilla ja se petteri kävi jotai mulisee ni kiskasin kunnolla tukkaa ja lähin tepon kans puistoo dokaa!", text);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintDate("01.07.2021", date);
            PrintText("Nyy on kämoppä ryysypätty myt kessällöä pärjrrää perkeele ilmmanjki!", drink);
            PrintText(spaces + "vaiahan tän vituin läpppärrinki ny kossyyy ja annan mennnää! Sytököö paskakaa pettttrit ja muauutki runkkkart!", drink);
            PrintText(spaces + "NY VEESDETÄÄÄ JA TTAPPELLLAAA SI TAAPPPII ASTI!!! TULKAAA SAATAANA KOITTAA!", drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);

            PrintText(spaces + "Kuten sankarimme taustasta voimme päätellä että sitä ollaan jo pikkasen ehkä mukiin menevää sorttia ja tappelukin irtoo herkästi.", text);
            Console.WriteLine();
            PrintText(spaces + "Sinun tehtäväsi on auttaa sankariamme hiomaan rappionsa huippuunsa ja vetää kaikkia ketkä vittuilee turpaan kunnes kohtalo suo kovemman konkarin kehän toiseen kulmaan joka paukuttaa lättyy niin että saadaan tarina päätökseen ja sankarimme pääsee ansaitulle levolle matohotelliin!", text);
            Console.WriteLine();
            Console.WriteLine("paina jotain näppäintä jatkaaksesi", Console.ForegroundColor = ConsoleColor.Blue);
            Console.ReadKey(false);
        }

        private void PrintText(string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            String spaces = new(' ', 12);
            string[] words = line.Split(' ');
            String l = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (l.Length < Console.LargestWindowWidth / 2)
                {
                    l += words[i] + " ";
                }
                else
                {
                    Console.WriteLine(l);
                    l = spaces + words[i] + " ";
                }
            }
            if (l != spaces)
            {
                Console.WriteLine(l);
            }
        }

        private void PrintDate(String date, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(date + ": ");
        }
    }
}