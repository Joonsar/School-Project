using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public static class Input
    {

        private static GameController gc = GameController.Instance;
        
        public static void CheckInput(ConsoleKey key)
        {
           
                // Move the Player in the corresponding direction
                switch (key)
                {
                    case ConsoleKey.Q:
                        gc.running = false;
                        break;

                    case ConsoleKey.UpArrow:
                        gc.Player.MovePlayer(0, -1);
                        break;

                    case ConsoleKey.DownArrow:
                        gc.Player.MovePlayer(0, 1);
                        break;

                    case ConsoleKey.LeftArrow:
                        gc.Player.MovePlayer(-1, 0);
                        break;

                    case ConsoleKey.RightArrow:
                        gc.Player.MovePlayer(1, 0);
                        break;

                    case ConsoleKey.NumPad8:
                        gc.Player.MovePlayer(0, -1);
                        break;

                    case ConsoleKey.NumPad1:
                        gc.Player.MovePlayer(-1, 1);
                        break;

                    case ConsoleKey.NumPad3:
                        gc.Player.MovePlayer(1, 1);
                        break;

                    case ConsoleKey.NumPad9:
                        gc.Player.MovePlayer(1, -1);
                        break;

                    case ConsoleKey.NumPad7:
                        gc.Player.MovePlayer(-1, -1);
                        break;

                    case ConsoleKey.NumPad2:
                        gc.Player.MovePlayer(0, 1);
                        break;

                    case ConsoleKey.NumPad4:
                        gc.Player.MovePlayer(-1, 0);
                        break;

                    case ConsoleKey.NumPad6:
                        gc.Player.MovePlayer(1, 0);
                        break;

                    case ConsoleKey.Escape:
                        System.Environment.Exit(0);
                        break;

                    case ConsoleKey.O:
                        gc.ChangeLevel(1);
                        break;

                    case ConsoleKey.I:
                        gc.ChangeLevel(-1);
                        break;


                }
            }
    }
}
