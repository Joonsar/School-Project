using System;
using System.Runtime.InteropServices;

namespace School_Project
{
    public class Screen
    {
        public int Width { get; set; }
        public int Height { get; set; }

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
            Console.WindowHeight = Height;
            Console.WindowWidth = Width;
            Console.SetBufferSize(width, height);
        }

        public void PrintPlayer()
        {
            // tää ei tuu toimiin, ennenkun pelaajalla on position value, public Position pos {get; set;} ja asetettu jossain (todnäk player constructorissa)
            
            
            //tää on rikki viä, ennenkun gamecontrollerissa määritellään pelaaja
            //Console.SetCursorPosition(gc.Player.Pos.x, gc.Player.Pos.y);
            Write("@");
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

        public void DrawScreen()
        {
            //tähän tulee vielä kaikki mapin piirtämiset, entityt, pelaaja jne. kunhan ne ny on eka valmiina.

            // Create a new instance of the Map class
            //esim tää pois
            Map gameMap = new Map(35, 25);

            // Set the player position on the map
            //tää pois
            gameMap.SetPlayerPosition(5, 5);

            // Create a new instance of the PlayerMovement class
            
            //eli kaikki liikkumiset jne pois tästä classista. Pelkästään tulostaminen.
            //tää pois
            PlayerMovement playerMovement = new PlayerMovement(gameMap, 5, 5);

            // Draw the game board
            // tähän public void DrawMap(Map map) { joku tyhmä esimerkki if(map.mapArray[10,10] == TileType.Wall { console.write("#"); }
            
            gameMap.Draw();

            // Print the player on the screen
            PrintPlayer();

            // Keep moving the player until the user presses the Esc key
            while (true)
            {
                // Get the key pressed by the user
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Move the player in the corresponding direction
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        playerMovement.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        playerMovement.MoveDown();
                        break;
                    case ConsoleKey.LeftArrow:
                        playerMovement.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        playerMovement.MoveRight();
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }

            // Update the player position
            gameMap.ClearPlayerPosition(5, 5);
            gameMap.SetPlayerPosition(6, 6);

            // Draw the updated game board
            gameMap.Draw();

            // Print the player on the screen again
            PrintPlayer();

            // Keep the console window open
            Console.ReadLine();
        }
    }
}