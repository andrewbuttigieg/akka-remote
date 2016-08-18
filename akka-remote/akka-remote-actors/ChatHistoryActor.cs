using Akka.Actor;
using akka_remote_messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akka_remote_actors
{
    public class ChatHistoryActor: ReceiveActor
    {
        List<ConsoleWriterMessage> messages = new List<ConsoleWriterMessage>();

        public ChatHistoryActor()
        {
            Receive<ConsoleWriterMessage>(message =>
            {
                messages.Add(message);
            });

            Receive<GetHistoryMessage>(message =>
            {
                Sender.Tell(messages);
            });
        }
    }
}
