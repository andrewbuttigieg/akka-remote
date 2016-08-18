using Akka.Actor;
using Akka.Configuration;
using akka_remote_actors;
using akka_remote_messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akka_remote_chatroom_client
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
akka {
    actor {
        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
    }
    remote {
        helios.tcp {
            port = 0
            hostname = localhost
        }
    }
}
");

            using (var system = ActorSystem.Create("MyClient", config))
            {
                var consoleWriterLocal = system.ActorOf<ConsoleWriterActor>("consoleWriterLocal" + DateTime.Now.Ticks);

                //get a reference to the remote actor
                var chatWriter = system
                    .ActorSelection("akka.tcp://MyServer@localhost:8080/user/chatWriter");
                
                var chatHistory = system
                    .ActorSelection("akka.tcp://MyServer@localhost:8080/user/chatHistory");

                var task = chatHistory.Ask<List<ConsoleWriterMessage>>(new GetHistoryMessage());
                task.Wait();

                foreach(var message in task.Result)
                {
                    consoleWriterLocal.Tell(message);
                }

                //send a message to the remote actor
                chatWriter.Tell(new ChatSubMessage(consoleWriterLocal));
                string input;
                do
                {
                    input = Console.ReadLine();
                    chatWriter.Tell(new ConsoleWriterMessage(input, ConsoleColor.Cyan));
                } while (!input.Equals("exit", StringComparison.CurrentCultureIgnoreCase));


                Console.ReadLine();
            }
        }
    }
}
