using Akka.Actor;
using akka_remote_messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akka_remote_actors
{
    public class ChatWriterActor : ReceiveActor
    {
        IActorRef consoleWriter;
        IActorRef chatHistory;
        public ChatWriterActor(IActorRef consoleWriter, IActorRef chatHistory)
        {
            this.consoleWriter = consoleWriter;
            this.chatHistory = chatHistory;

            Receive<ConsoleWriterMessage>(message =>
            {
                consoleWriter.Tell(message);
                chatHistory.Tell(message);
            });
        }
    }
}
