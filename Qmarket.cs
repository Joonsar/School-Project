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

            Console.WriteLine("e - Poistu, s - Palauta tyhjät pullot, p - Pelaa Ruplapottia ");
            Console.WriteLine($"Rahat: {gc.Player.Money}e");
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case (ConsoleKey.E):
                    gc.screen.DrawScreen();
                    break;

                case (ConsoleKey.S):
                    if (gc.Player.Bottles <= 0)
                    {
                        Console.WriteLine("Sinulla ei ole yhtään pulloa palautettavana");
                    }
                    else if (gc.Player.Bottles > 0)
                    {
                        Console.WriteLine($"Palautit {gc.Player.Bottles} pulloa ja sait niistä {gc.Player.Bottles} euroa");
                        gc.Player.Addmoney(gc.Player.Bottles);
                        gc.Player.Bottles = 0;
                    }
                    Console.WriteLine("Paina jotain nappia jatkaaksesi");
                    Console.ReadKey();
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

        private void RunMachine()
        {
            gc.screen.Clear();
            Console.WriteLine();
            PrintLogo(ruplaPottiLogo);
            Console.WriteLine("e - Poistu, p - Pelaa");
            Console.SetCursorPosition(20, 14);
            Console.WriteLine($"Rahat: {gc.Player.Money}e");
            var input = Console.ReadKey(true);
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
            }
        }

        private void Slots()
        {
            bool playing = true;
            int winnings = 0;
            var spaces = new string(' ', 20);
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
                Console.SetCursorPosition(20, 14);
                Console.WriteLine($"Rahat: {gc.Player.Money}");
                switch (input.Key)
                {
                    case ConsoleKey.P:

                        if (gc.Player.Money >= 1)
                        {
                            gc.Player.Money -= 1;
                            for (int i = 0; i < row.Length; i++)
                            {
                                row[i] = chars[rand.Next(0, chars.Count)];
                                Console.SetCursorPosition(20 + i, 20);
                                Console.Write("#");
                                System.Threading.Thread.Sleep(500);
                                Console.SetCursorPosition(20 + i, 20);
                                Console.Write(row[i]);
                            }
                            //  Console.WriteLine($"\n{row[0]} {row[1]} {row[2]}");

                            Console.WriteLine();

                            if (row[0] == row[1] && row[1] == row[2])
                            {
                                if (row[0] == '$')
                                {
                                    winnings = 50;
                                    Console.WriteLine("Humalaisen tuuria voitit JACKPOTIN 50 euroa!");
                                }
                                else if (row[0] == '%')
                                {
                                    winnings = 20;
                                    Console.WriteLine("Voitit 10 euroa." + spaces);
                                }
                                else if (row[0] == '£')
                                {
                                    winnings = 30;
                                    Console.WriteLine("Voitit 20 euroa." + spaces);
                                }
                                else if (row[0] == '¤')
                                {
                                    winnings = 40;
                                    Console.WriteLine("Voitit 30 euroa." + spaces);
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
                            }
                            else
                            {
                                //Console.WriteLine();
                                Console.WriteLine("Sinne meni, et voittanut mitään!" + spaces);
                            }
                            gc.Player.Money += winnings;
                            winnings = 0;
                            // Console.WriteLine($"Rahat {gc.Player.Money}\n");
                        }
                        else
                        {
                            Console.SetCursorPosition(20, 14);
                            Console.WriteLine("Sinulla ei ole tarpeeksi rahaa pelata.");
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
    }
}