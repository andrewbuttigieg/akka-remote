﻿using Akka.Actor;
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
            port = 8090
            hostname = localhost
        }
    }
}
");

            using (var system = ActorSystem.Create("MyClient", config))
            {
                var consoleWriterLocal = system.ActorOf<ConsoleWriterActor>("consoleWriterLocal");

                //get a reference to the remote actor
                var chatWriter = system
                    .ActorSelection("akka.tcp://MyServer@localhost:8080/user/chatWriter");

                var consoleWriter = system
                    .ActorSelection("akka.tcp://MyServer@localhost:8080/user/consoleWriter");

                var chatHistory = system
                    .ActorSelection("akka.tcp://MyServer@localhost:8080/user/chatHistory");

                var task = chatHistory.Ask<List<ConsoleWriterMessage>>(new GetHistoryMessage());
                task.Wait();

                foreach(var message in task.Result)
                {
                    consoleWriterLocal.Tell(message);
                }

                //send a message to the remote actor
                chatWriter.Tell(new ConsoleWriterMessage("Hello chat!", ConsoleColor.Cyan));

                Console.ReadLine();
            }
        }
    }
}
