using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akka_remote_messages
{
    public class ConsoleWriterMessage
    {
        public string Message { get; private set; }
        public ConsoleColor ConsoleColor { get; set; }

        public ConsoleWriterMessage(string message, ConsoleColor consoleColor)
        {
            Message = message;
            ConsoleColor = consoleColor;
        }
    }
}
