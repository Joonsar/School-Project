﻿using System;

namespace School_Project
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //  GameController gc = GameController.Instance;

            //  gc.Init();

            Screen screen = new Screen(30, 25);
            screen.DrawScreen();
            Console.ReadLine();
        }
    }
}
