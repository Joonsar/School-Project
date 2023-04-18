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
            if (gc.Inspecting)
            {
                Console.CursorVisible = true;
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (!gc.Map.IsPositionInsideBounds(Console.CursorLeft + 1, Console.CursorTop))
                        {
                            break;
                        }
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        CheckCurrentPosition();

                        break;

                    case ConsoleKey.LeftArrow:
                        if (!gc.Map.IsPositionInsideBounds(Console.CursorLeft - 1, Console.CursorTop))
                        {
                            break;
                        }
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        CheckCurrentPosition();
                        break;

                    case ConsoleKey.UpArrow:
                        if (!gc.Map.IsPositionInsideBounds(Console.CursorLeft, Console.CursorTop - 1))
                        {
                            break;
                        }
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                        CheckCurrentPosition();
                        break;

                    case ConsoleKey.DownArrow:
                        if (!gc.Map.IsPositionInsideBounds(Console.CursorLeft, Console.CursorTop + 1))
                        {
                            break;
                        }
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
                        CheckCurrentPosition();
                        break;

                    case ConsoleKey.Spacebar:
                        Console.CursorVisible = false;
                        gc.Inspecting = false;
                        break;
                }
            }
            else
            {
                // Move the Player in the corresponding direction
                switch (key)
                {
                    case ConsoleKey.Spacebar:
                        gc.Inspecting = true;
                        Console.CursorVisible = true;
                        gc.screen.SetCursorPosition(gc.Player.Pos.X, gc.Player.Pos.Y);
                        break;

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

        private static void CheckCurrentPosition()
        {
            foreach (Entity e in gc.entities)
            {
                if (e.Pos.X == Console.CursorLeft && e.Pos.Y == Console.CursorTop)
                {
                    var oldPosX = Console.CursorLeft;
                    var oldPosY = Console.CursorTop;
                    gc.screen.PrintInspectingObject(e.Name + " " + e.Description + " " + new string(' ', gc.Width));
                    //gc.screen.PrintMessageLog();
                    gc.screen.SetCursorPosition(oldPosX, oldPosY);
                }
            }

            CheckMapItems();
        }

        private static void CheckMapItems()
        {
            if (gc.Map.Mapping[Console.CursorLeft, Console.CursorTop] != Map.empty)
            {
                SetPositionAndPrintToMessageLog(gc.Map.Mapping[Console.CursorLeft, Console.CursorTop]);
            }
        }

        private static void SetPositionAndPrintToMessageLog(MapObject c)
        {
            var oldPosX = Console.CursorLeft;
            var oldPosY = Console.CursorTop;
            /*   if (c == '#')
               {
                   gc.MessageLog.AddMessage("seinä");
               }
               else if (c == Map.stairsDown)
               {
                   gc.MessageLog.AddMessage("Portaat alas");
               }
               else if (c == Map.stairsUp)
               {
                   gc.MessageLog.AddMessage("Portaal ylös");
               }*/
            //gc.MessageLog.AddMessage(c.Description);
            //gc.screen.PrintMessageLog();
            gc.screen.PrintInspectingObject(c.Description + new string(' ', gc.Width));
            gc.screen.SetCursorPosition(oldPosX, oldPosY);
        }
    }
}