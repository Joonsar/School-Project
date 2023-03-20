using System;
using System.Collections.Generic;
using System.Reflection.Emit;
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

            Console.WindowHeight = Height+1;
            Console.WindowWidth = Width;
            Console.SetBufferSize(width, height+1);
         
            Console.CursorVisible = false;
        }

        public void PrintPlayer()
        {
            Console.SetCursorPosition(gc.Player.Pos.X, gc.Player.Pos.Y);
            Write(gc.Player.Mark.ToString(), gc.Player.Color);
            PrintPlayerStats();
        }

        public void PrintPlayerStats()
        {
            Console.SetCursorPosition(0, Height);
            Write(gc.Player.GetStats());
        }

        public void PrintMap()
        {
           
            for(int y = 0; y < gc.Map.Height; y++)
            {
                for(int x = 0; x < gc.Map.Width; x++ )
                {
                    Console.SetCursorPosition(x, y);
                    Write(gc.Map.Mapping[x, y].ToString());
                }
            }
           
        }

        public void PrintEntities(List<Entity> entities)
        {
            // tulostetaan jokainen entity ruudulle.
            foreach(Entity e in entities)
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

        public void DrawScreen()
        {
            //tähän tulee vielä kaikki mapin piirtämiset, entityt, pelaaja jne. kunhan ne ny on eka valmiina.

            // Create a new instance of the Map class

            // Set the Player position on the map
            //tää pois

            // Create a new instance of the PlayerMovement class

            //eli kaikki liikkumiset jne pois tästä classista. Pelkästään tulostaminen.
            //tää pois

            // Draw the game board
            // tähän public void DrawMap(Map map) { joku tyhmä esimerkki if(map.mapArray[10,10] == TileType.Wall { console.write("#"); }

            // Print the Player on the screen
            PrintPlayer();

            // Keep moving the Player until the user presses the Esc key
          

          

            
            
        }
    }
}