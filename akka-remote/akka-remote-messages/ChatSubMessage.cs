using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akka_remote_messages
{
    public class ChatSubMessage
    {
        public IActorRef ConsoleWriter { get; private set; }

        public ChatSubMessage(IActorRef consoleWriter)
        {
            ConsoleWriter = consoleWriter;
        }
    }
}
