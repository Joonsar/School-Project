using System;
using System.Collections.Generic;

namespace School_Project
{
    public class MessageLog
    {
        private GameController gc = GameController.Instance;
        private int MaxMessages;
        private List<string> Messages = new List<string>();

        public MessageLog(int maxMessages)
        {
            MaxMessages = maxMessages;
        }

        //lisätään message listaan jos lista on täynnä poistetaan ensimmäinen elementti
        public void AddMessage(string message)
        {
            if (Messages.Count >= MaxMessages)
            {
                Messages.RemoveAt(0);
            }
            Messages.Add(message);
            gc.screen.PrintMessageLog();
        }

        //tulostetaan lista mapin oikealle puolelle käänteisessä järjestyksessä.
        public void PrintMessages()
        {
            for (int i = Messages.Count - 1; i >= 0; i--)
            {
                Console.SetCursorPosition(gc.Map.Width + 1, i);
                //Console.Write(Messages[i] + new string(' ', 60));
                //Console.Write(Messages[i]);
                Console.Write(Messages[i] + new string(' ', Console.LargestWindowWidth - Messages[i].Length - gc.Map.Width - 1));
            }
        }
    }
}