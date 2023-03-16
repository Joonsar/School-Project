using System;
using System.Reflection.Emit;
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
            Console.SetCursorPosition(gc.player.Pos.X, gc.player.Pos.Y);
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

            // Set the player position on the map
            //tää pois

            // Create a new instance of the PlayerMovement class

            //eli kaikki liikkumiset jne pois tästä classista. Pelkästään tulostaminen.
            //tää pois

            // Draw the game board
            // tähän public void DrawMap(Map map) { joku tyhmä esimerkki if(map.mapArray[10,10] == TileType.Wall { console.write("#"); }

            // Print the player on the screen
            PrintPlayer();

            // Keep moving the player until the user presses the Esc key
            while (true)
            {
                // Get the key pressed by the user
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Move the player in the corresponding direction
            }

            // Update the player position

            // Print the player on the screen again

            // Keep the console window open
            Console.ReadLine();
        }
    }
}