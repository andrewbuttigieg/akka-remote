using Akka.Actor;
using Akka.Configuration;
using akka_remote_actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akka_remote
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
            port = 8089
            hostname = localhost
        }
    }
}
");

            using (ActorSystem system = ActorSystem.Create("MyServer", config))
            {
                var chatHistory = system.ActorOf<ChatHistoryActor>("chatHistory");
                system.ActorOf(Props.Create(() =>  new ChatWriterActor(chatHistory)), "chatWriter");
                
                Console.ReadKey();
            }
        }
    }
}
