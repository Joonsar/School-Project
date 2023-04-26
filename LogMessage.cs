using System;

namespace School_Project
{
    public class LogMessage
    {
        public ConsoleColor Color { get; set; }
        public string Message { get; set; }

        public LogMessage(string message, ConsoleColor color)
        {
            Color = color;
            Message = message;
        }
    }
}