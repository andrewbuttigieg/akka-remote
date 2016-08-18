using Akka.Actor;
using akka_remote_messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akka_remote_actors
{
    public class ConsoleWriterActor : ReceiveActor
    {
        public ConsoleWriterActor()
        {
            Receive<ConsoleWriterMessage>(message =>
            {
                Console.ForegroundColor = message.ConsoleColor;
                Console.WriteLine(message.Message);
                Console.ResetColor();
            });
        }
    }
}
