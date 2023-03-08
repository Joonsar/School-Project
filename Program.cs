using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School_Project
{
    public class Program
    {
        private static void Main(string[] args)
        {
          //  GameController gc = GameController.Instance;

            //  gc.Init();

            Screen screen = new Screen(40, 20);
            screen.DrawScreen();
            Console.ReadLine();
        }
    }
}
