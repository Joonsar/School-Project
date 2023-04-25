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
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case (ConsoleKey.E):
                    gc.screen.DrawScreen();
                    break;

                case (ConsoleKey.S):
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
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.E:
                    Shop();
                    break;

                case ConsoleKey.P:
                    Shop();
                    break;

                default:
                    RunMachine();
                    break;
            }
        }
    }
}