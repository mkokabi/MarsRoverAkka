using Akka.Actor;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            ActorSystem MyActorSystem = ActorSystem.Create("MyActorSystem");
            var rover = MyActorSystem.ActorOf<Rover>();
            var console = MyActorSystem.ActorOf(Props.Create(() => new Console(rover)));

            // tell console reader to begin
            console.Tell("start");

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }
    }
}
