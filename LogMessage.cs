using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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