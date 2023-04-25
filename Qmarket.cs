using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Qmarket
    {
        private GameController gc = GameController.Instance;

        private string qMarketLogo = " .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------. \r\n| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |\r\n| |    ___       | || |              | || | ____    ____ | || |      __      | || |  _______     | || |  ___  ____   | || |  _________   | || |  _________   | |\r\n| |  .'   '.     | || |              | || ||_   \\  /   _|| || |     /  \\     | || | |_   __ \\    | || | |_  ||_  _|  | || | |_   ___  |  | || | |  _   _  |  | |\r\n| | /  .-.  \\    | || |    ______    | || |  |   \\/   |  | || |    / /\\ \\    | || |   | |__) |   | || |   | |_/ /    | || |   | |_  \\_|  | || | |_/ | | \\_|  | |\r\n| | | |   | |    | || |   |______|   | || |  | |\\  /| |  | || |   / ____ \\   | || |   |  __ /    | || |   |  __'.    | || |   |  _|  _   | || |     | |      | |\r\n| | \\  `-'  \\_   | || |              | || | _| |_\\/_| |_ | || | _/ /    \\ \\_ | || |  _| |  \\ \\_  | || |  _| |  \\ \\_  | || |  _| |___/ |  | || |    _| |_     | |\r\n| |  `.___.\\__|  | || |              | || ||_____||_____|| || ||____|  |____|| || | |____| |___| | || | |____||____| | || | |_________|  | || |   |_____|    | |\r\n| |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | |\r\n| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |\r\n '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' ";
        private string ruplaPottiLogo = " /$$$$$$$                      /$$                                 /$$     /$$     /$$\r\n| $$__  $$                    | $$                                | $$    | $$    |__/\r\n| $$  \\ $$ /$$   /$$  /$$$$$$ | $$  /$$$$$$   /$$$$$$   /$$$$$$  /$$$$$$ /$$$$$$   /$$\r\n| $$$$$$$/| $$  | $$ /$$__  $$| $$ |____  $$ /$$__  $$ /$$__  $$|_  $$_/|_  $$_/  | $$\r\n| $$__  $$| $$  | $$| $$  \\ $$| $$  /$$$$$$$| $$  \\ $$| $$  \\ $$  | $$    | $$    | $$\r\n| $$  \\ $$| $$  | $$| $$  | $$| $$ /$$__  $$| $$  | $$| $$  | $$  | $$ /$$| $$ /$$| $$\r\n| $$  | $$|  $$$$$$/| $$$$$$$/| $$|  $$$$$$$| $$$$$$$/|  $$$$$$/  |  $$$$/|  $$$$/| $$\r\n|__/  |__/ \\______/ | $$____/ |__/ \\_______/| $$____/  \\______/    \\___/   \\___/  |__/\r\n                    | $$                    | $$                                      \r\n                    | $$                    | $$                                      \r\n                    |__/                    |__/                                      ";

        public Qmarket()
        {
        }

        public void PrintLogo(string logo)
        {
            gc.screen.Write(logo);
        }

        public void Shop()
        {
            gc.screen.Clear();
            PrintLogo(qMarketLogo);
            Console.WriteLine();
            Console.WriteLine("e - Poistu, s - Palauta tyhjät pullot, p - Pelaa Ruplapottia, k - Osta Kossu (20 damage) 10e, v - Osta Vissy (20 maxhealth) 10e");
            Console.WriteLine($"Rahat: {gc.Player.Money}e");
            SoundManager.PlayMarketSoundAsync();
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case (ConsoleKey.E):
                    gc.screen.DrawScreen();
                    SoundManager.StopMarketSound();
                    SoundManager.PlayMainMusicAsync();
                    break;

                case ConsoleKey.K:
                    if (gc.Player.Money >= 10)
                    {
                        gc.Player.BaseDamage += 20;
                        gc.Player.Money -= 10;
                        Console.WriteLine("Ostit kossun ja kulautit sen naamaan. Tunnet itsesi voimakkaammakksi");
                        gc.Player.Bottles++;
                        PressKeyToContinue();
                        Shop();
                    }
                    else
                    {
                        Console.WriteLine("Sinulla ei ole tarpeeksi rahaa!");
                        Console.WriteLine("Paina jotain nappia jatkaaksesi.");
                        Console.ReadKey(true);
                        Shop();
                    }
                    break;

                case ConsoleKey.V:
                    if (gc.Player.Money >= 10)
                    {
                        gc.Player.MaxHp += 20;
                        gc.Player.Money -= 10;
                        Console.WriteLine("Ostit vissyn ja kulautit sen naamaan. Tunnet voivasi paremmin");
                        gc.Player.Bottles++;
                        Console.WriteLine("Paina jotain nappia jatkaaksesi.");
                        Console.ReadKey(true);
                        Shop();
                    }
                    else
                    {
                        Console.WriteLine("Sinulla ei ole tarpeeksi rahaa!");
                        Console.WriteLine("Paina jotain nappia jatkaaksesi.");
                        Console.ReadKey(true);
                        Shop();
                    }
                    break;

                case (ConsoleKey.S):
                    if (gc.Player.Bottles <= 0)
                    {
                        Console.WriteLine("Sinulla ei ole yhtään pulloa palautettavana");
                    }
                    else if (gc.Player.Bottles > 0)
                    {
                        SoundManager.PlayBottlesSoundAsync();
                        Console.WriteLine($"Palautit {gc.Player.Bottles} pulloa ja sait niistä {gc.Player.Bottles} euroa");
                        gc.Player.Addmoney(gc.Player.Bottles);
                        gc.Player.Bottles = 0;
                    }
                    PressKeyToContinue();
                    Shop();
                    break;

                case (ConsoleKey.P):
                    RunMachine();
                    break;

                default:
                    Shop();
                    break;
            }
        }

        private static void PressKeyToContinue()
        {
            Console.WriteLine("Paina jotain nappia jatkaaksesi.");
            Console.ReadKey(true);
        }

        private void RunMachine()
        {
            gc.screen.Clear();
            Console.WriteLine();
            PrintLogo(ruplaPottiLogo);
            SoundManager.PlaySlotsMusicAsync();
            Console.WriteLine("e - Poistu, p - Pelaa");
            PrintMoney();
            Slots();
            /*     var input = Console.ReadKey(true);
                 switch (input.Key)
                 {
                     case ConsoleKey.E:
                         Shop();
                         break;

                     case ConsoleKey.P:
                         Slots();
                         break;

                     default:
                         RunMachine();
                         break;
                 } */
        }

        private void PrintMoney()
        {
            Console.SetCursorPosition(20, 14);
            Console.WriteLine($"Rahat: {gc.Player.Money}e            ");
        }

        private void Slots()
        {
            bool playing = true;
            int winnings = 0;
            var spaces = new string(' ', 40);
            Random rand = new Random();
            char[] row = new char[3];
            List<char> chars = new List<char>();
            chars.Add('$');
            chars.Add('%');
            chars.Add('£');
            chars.Add('¤');

            while (playing)
            {
                var input = Console.ReadKey(true);
                if (gc.Player.Money >= 1)
                {
                    PrintMoney();
                }
                else if (gc.Player.Money < 0)
                {
                    PrintNotEnoughMoney();
                }
                switch (input.Key)
                {
                    case ConsoleKey.P:

                        if (gc.Player.Money >= 1)
                        {
                            gc.Player.Money -= 1;
                            PrintMoney();
                            Console.SetCursorPosition(20, 20);
                            Console.Write("       ");
                            for (int i = 0; i < row.Length; i++)
                            {
                                row[i] = chars[rand.Next(0, chars.Count)];
                                Console.SetCursorPosition(20 + i, 20);
                                //Console.Write(".");
                                System.Threading.Thread.Sleep(500);
                                Console.SetCursorPosition(20 + i, 20);
                                if (i == 0)
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                if (i == 1)
                                    Console.ForegroundColor = ConsoleColor.Green;
                                if (i == 2)
                                    Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(row[i]);
                            }
                            //  Console.WriteLine($"\n{row[0]} {row[1]} {row[2]}");
                            Console.ResetColor();
                            Console.WriteLine();

                            if (row[0] == row[1] && row[1] == row[2])
                            {
                                if (row[0] == '$')
                                {
                                    winnings = 50;
                                    Console.WriteLine("Humalaisen tuuria voitit JACKPOTIN 50 euroa!" + spaces);
                                }
                                else if (row[0] == '%')
                                {
                                    winnings = 10;

                                    Console.WriteLine("Voitit 10 euroa." + spaces);
                                }
                                else if (row[0] == '£')
                                {
                                    winnings = 20;
                                    Console.WriteLine("Voitit 20 euroa." + spaces);
                                }
                                else if (row[0] == '¤')
                                {
                                    winnings = 40;
                                    Console.WriteLine("ISO voitto, meinaat lentää perseellesi. Voitit 40 euroa." + spaces);
                                }
                            }
                            else if (row[0] == row[1])
                            {
                                if (row[0] == '$')
                                {
                                    winnings = 5;
                                    Console.WriteLine("Voitit 5 euroa." + spaces);
                                }
                                else if (row[0] == '%')
                                {
                                    winnings = 4;
                                    Console.WriteLine("Voitit 4 euroa." + spaces);
                                }
                                else if (row[0] == '£')
                                {
                                    winnings = 3;
                                    Console.WriteLine("Voitit 3 euroa." + spaces);
                                }
                                else if (row[0] == '¤')
                                {
                                    winnings = 2;
                                    Console.WriteLine("Voitit 2 euroa." + spaces);
                                }
                                // Console.Write($"Voitit {winnings} euroa." + spaces);
                            }
                            else
                            {
                                //Console.WriteLine();
                                Console.WriteLine("Sinne meni, et voittanut mitään!" + spaces);
                            }
                            gc.Player.Money += winnings;
                            PrintMoney();
                            winnings = 0;
                            // Console.WriteLine($"Rahat {gc.Player.Money}\n");
                        }
                        else
                        {
                            PrintNotEnoughMoney();
                        }
                        break;

                    case ConsoleKey.E:
                        playing = false;
                        Shop();
                        break;

                    default:
                        break;
                }
            }
        }

        private static void PrintNotEnoughMoney()
        {
            Console.SetCursorPosition(20, 14);
            Console.Write("Sinulla ei ole tarpeeksi rahaa pelata.");
        }
    }
}