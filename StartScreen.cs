﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace School_Project
{
    public class StartScreen
    {
        private string command;
        private GameController gc = GameController.Instance;
        private string IntroText = "       _                                 _             _             \r\n      | |                               | |           (_)            \r\n      | |_   _  ___  _ __   ___  _ __   | |_ __ _ _ __ _ _ __   __ _ \r\n  _   | | | | |/ _ \\| '_ \\ / _ \\| '_ \\  | __/ _` | '__| | '_ \\ / _` |\r\n | |__| | |_| | (_) | |_) | (_) | | | | | || (_| | |  | | | | | (_| |\r\n  \\____/ \\__,_|\\___/| .__/ \\___/|_| |_|  \\__\\__,_|_|  |_|_| |_|\\__,_|\r\n                    | |                                              \r\n                    |_|      ";

        public string PlayerName { get; set; }

        public StartScreen()
        {
            this.command = "";
        }

        public void Run()
        {
            PrintInfo();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
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
                    
                }
                if (this.command == "3")
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("- Liikuta pelaajaa nuoli- tai numpad näppäimillä.");
                    Console.WriteLine(" ");
                    Console.WriteLine("Merkit:");      
                    Console.WriteLine(" # - Seinä               + - Ovi");
                    Console.WriteLine(" ! - Itemi               Kirjaimet - Vastustajia");
                    Console.WriteLine(" < - Rappuset ylös       > - Rappuset alas");
                    Console.WriteLine(" ");
                    Console.WriteLine(" - Paina Välilyöntiä tutkiaksesi maailmaa.");
                    Console.WriteLine(" ");
                    Console.WriteLine(" - Paina Esc lopettaaksesi pelin");
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

        public void PrintInfo()
        {
            Console.WriteLine(IntroText);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1 - Uusi peli");
            Console.WriteLine("2 - Piste tilastot");
            Console.WriteLine("3 - Info");
            Console.WriteLine("0 - Lopeta");
        }

        public void NewGame()
        {
            ConsoleColor date = ConsoleColor.Green;
            ConsoleColor text = ConsoleColor.Yellow;
            ConsoleColor drink = ConsoleColor.Red;
            string lines = new string('-', Console.LargestWindowWidth);
            String spaces = new String(' ', 12);
            Console.WriteLine();
            Console.Write("Anna sankarillesi nimi: ", Console.ForegroundColor = ConsoleColor.Yellow);
            this.PlayerName = Console.ReadLine();
            gc.PlayerName = this.PlayerName;
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(spaces + "Luet nyt päiväkirjaa minkä omistaja on " + PlayerName + " ja jos en oo kuollu ja luet\n" + spaces + "ilman lupaa ni etin sut ja vedän lättyy runkku!");
            Console.WriteLine(lines);
            Console.Write("01.02.2020: ", Console.ForegroundColor = date);
            Console.WriteLine("Elämä hymyilee, bisnekset rullaa. On vaimoo, on isoo taloo, on autoo, on venettä yms!\n" + spaces + "Janoo menee vissyä ja safkan kans ehkä tilkka viiniä. Elämä hymyilee", Console.ForegroundColor = text);
            Console.WriteLine(lines);
            Console.Write("04.02.2020: ", Console.ForegroundColor = date);
            Console.WriteLine("Iski joku saatanan rokko keniasta ja väki paniikissa pistää kuljetukset poikki ja ulkonaliikkumis kieltoja.\n" + spaces + "Kaikki bisnekset kusee huolella!", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "Ny pakko vetää pari kaljaa ressii!", Console.ForegroundColor = drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("04.07.2020: ", Console.ForegroundColor = date);
            Console.WriteLine("Firma meni konkkaan ja ulosottaja myi sen pilkka hintaa naapurin liimatukka petterille.", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "(siinä vasta kunnon mulkku! Hinkkaa bemariaa pihassa ilman paitaa sikspakkiä esitelle ja kehuu kuin on bisnesmies vaikka isin taaloilla\n" + spaces + "tehny vaa tappioo ja sikspäkkiki on silikoonia.)");
            Console.WriteLine(spaces + "Pistää vihaks sen verta et taidan hakee laatikon nelosta!", Console.ForegroundColor = drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("01.08.2020: ", Console.ForegroundColor = date);
            Console.WriteLine("Appiukko onneks sponssas et pääsis uutee alkuu mut sit toi prkl läskiperse muija sai selville että oon vuosia kusettanu olevani ylitöissä\n" + spaces + "vaikka tuli temmottua savuja meitsin sihteeristä sirpasta. Paska homma, sinne meni seki perse!", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "Tilanne huutaa paria napanderia!!", Console.ForegroundColor = drink);
            Console.WriteLine();
            Console.WriteLine("Paina entteriä jatkaaksesi", Console.ForegroundColor = ConsoleColor.Blue);
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine();
            Console.Write("02.10.2020: ", Console.ForegroundColor = date);
            Console.WriteLine("Muija ny sit tietty otti kakarat ja lähti (Ne saatanan kiittänättömät paskat saaki mennä!), mut appiukon rahoja tuli ikävä\n            vaikka joutuki nuolee senki ikälopun haisevan mulkun persettä", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "Ny kyllä vedetää kunnon kännit!", Console.ForegroundColor = drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("24.12.2020: ", Console.ForegroundColor = date);
            Console.WriteLine("No erohan siitä tuli ja se saatanan ammatti runkkari palkkas tyttärellee kunnon hyeenan imee pölykki taskusta! Hyvää joulua\n            vaa teillekki prkl runkkarit!", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "Ny kyllä si vedetää viikko viinaa!", Console.ForegroundColor = drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("12.01.2021: ", Console.ForegroundColor = date);
            Console.WriteLine("Meni vähä pitkäks ja ihan törkee darra mut ny tarvii himmaa ja ihan vaan tasotella muutama päivä..", Console.ForegroundColor = text);
            Console.WriteLine(lines);
            Console.Write("28.01.2021: ", Console.ForegroundColor = date);
            Console.WriteLine("Ny on taisteltu asiosta kelan kanssa sain viihtyisän 14 neliön kompaktin yksiön yhteisillä wc tiloilla ihan entisen talon vierestä\n            et näkee suoraa vanhaa omaa olkkarii ku ämmä kattoo salkkareitaa mun 70 tuumasesta LED TV:stä.", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "Kyllä mies lääkkeen tietää, viinalla tänki paskan sietää!", Console.ForegroundColor = drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("22.03.2021: ", Console.ForegroundColor = date);
            Console.WriteLine("Vähä on tullu ryypiskeltyä mut hei kai ny maistuu tässä paskassa! Ny ne laitto työkkäristäki kuntouttavaan työtoimintaan ja viä vittu\n            mun omaa entiseen firmaa kattelee sitä liimatukka petteriä!", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "Sinne kuunteleen sen hitusen omakehun löyhkäsiä juttuja vaikka äijä ihan teline!", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "Pistää sen verta vihaks et pakko ryyppää varastossa", Console.ForegroundColor = drink);
            Console.WriteLine();
            Console.WriteLine("Paina etteriä jatkaaksesi", Console.ForegroundColor = ConsoleColor.Blue);
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine();
            Console.Write("12.04.2021: ", Console.ForegroundColor = date);
            Console.WriteLine("Jotai viinan huurusia muistikuvia puistosta ja kämppä täynnä juoppoja...", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "pakko antaa mennä vaa ei tätä sevinpäin kestä", Console.ForegroundColor = drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("30.05.2021: ", Console.ForegroundColor = date);
            Console.WriteLine("Rallatrallati rai! Ny juhlitaa!", Console.ForegroundColor = drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("01.06.2021: ", Console.ForegroundColor = date);
            Console.WriteLine("Kattelin ikkunasta vanhaa kämppää ni siähän se saatanan tuhkamuna homo petteri kairas sitä läskiperse ex-muijaa!", Console.ForegroundColor = text);
            Console.WriteLine(spaces + "Kävi kiahuttaa mut onneks kävin alkossa jo päivällä tankkaa varstot...", Console.ForegroundColor = drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("20.06.2021: ", Console.ForegroundColor = date);
            Console.WriteLine("RapPPIppioolla oONO huvAä oollaa ei hhHuuolet pinAAaaaa eIIii RAAasaituu pOllaaaaa", Console.ForegroundColor = drink);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("17.06.2021: ", Console.ForegroundColor = date);
            Console.WriteLine("Olin pari viikkoo rokulilla ja se petteri kävi jotai mulisee ni kiskasin kunnolla tukkaa ja lähin tepon kans puistoo dokaa!", Console.ForegroundColor = text);
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.Write("01.07.2021: ", Console.ForegroundColor = date);
            Console.WriteLine("Nyy on kämoppä ryysypätty myt kessällöä pärjrrää perkeele ilmmanjki!", Console.ForegroundColor = drink);
            Console.WriteLine(spaces + "vaiahan tän vituin läpppärrinki ny kossyyy ja annan mennnää! Sytököö paskakaa pettttrit ja muauutki runkkkart!");
            Console.WriteLine(spaces + "NY VEESDETÄÄÄ JA TTAPPELLLAAA SI TAAPPPII ASTI!!! TULKAAA SAATAANA KOITTAA!");
            Console.WriteLine(lines, Console.ForegroundColor = text);
            Console.WriteLine("Kuten sankarimme taustasta voimme päätellä että sitä ollaan jo pikkasen ehkä mukiin menevää sorttia ja tappelukin irtoo herkästi.");
            Console.WriteLine("Sinun tehtäväsi on auttaa sankariamme hiomaan rappionsa huippuunsa ja vetää kaikkia ketkä vittuilee turpaan kunnes kohtalo suo kovemman konkarin");
            Console.WriteLine("kehän toiseen kulmaan joka paukuttaa lättyy niin että saadaan tarina päätökseen ja sankarimme pääsee ansaitulle levolle matohotelliin!");
            Console.WriteLine();
            Console.WriteLine("paina enteriä jatkaaksesi", Console.ForegroundColor = ConsoleColor.Blue);
            Console.ReadLine();
        }
    }
}