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
        IActorRef chatHistory;
        List<IActorRef> subs = new List<IActorRef>();

        public ChatWriterActor(IActorRef chatHistory)
        {
            this.chatHistory = chatHistory;

            Receive<ConsoleWriterMessage>(message =>
            {
                chatHistory.Tell(message);
                foreach (var sub in subs)
                    sub.Tell(message);
            });

            Receive<ChatSubMessage>(message =>
            {
                subs.Add(message.ConsoleWriter);
            });
        }
    }
}
