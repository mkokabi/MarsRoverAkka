using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.ConsoleApp
{
    class Program
    {
        public static ActorSystem ActorSystem;

        static void Main(string[] args)
        {
            ShowInstruction();
            if (args.Length > 0 && args[0].Equals("persist", StringComparison.OrdinalIgnoreCase))
            {
                System.Console.WriteLine("In persistence mode.");
                var config = ConfigurationFactory.ParseString(File.ReadAllText("Sql.conf"));
                ActorSystem = ActorSystem.Create("MyActorSystem", config);
            }
            else
            {
                System.Console.WriteLine("In-memory persistence. (For database persistence start the app with 'persist' argument.");
                ActorSystem = ActorSystem.Create("MyActorSystem");
            }

            var rover = ActorSystem.ActorOf<Rover>();
            var console = ActorSystem.ActorOf(Props.Create(() => new Console(rover)));

            // tell console reader to begin
            console.Tell("start");

            // blocks the main thread from exiting until the actor system is shut down
            ActorSystem.WhenTerminated.Wait();
        }

        private static void ShowInstruction()
        {
            System.Console.WriteLine("First input should be the plateau size by introducing the upper right X Y (space delimated)");
            System.Console.WriteLine("Then the current position of Rover should be specified as X Y Direction (N, E, S or W)");
            System.Console.WriteLine("After that a series of one or many moves (L: Turn 90deg left, R: Turn 90deg right, M: Move)");
            System.Console.WriteLine("Type exit to terminate");
        }
    }
}
