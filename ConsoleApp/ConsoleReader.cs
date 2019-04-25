using Akka.Actor;
using System;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.ConsoleApp
{
    public class Console : UntypedActor
    {
        public const string ExitCommand = "exit";

        private readonly IActorRef rover;

        public Console(IActorRef rover)
        {
            this.rover = rover;
        }

        protected override void OnReceive(object message)
        {
            if (message is MovedOperationResult movedOperationResult)
            {
                System.Console.WriteLine(movedOperationResult.Position);
            }
            else if (message is InitialPositionSetOperationResult positionSetOperationResult)
            {
                System.Console.WriteLine(positionSetOperationResult.Position);
            }
            else if (message is PlateauSetOperationResult plateauSetOperationResult)
            {
                System.Console.WriteLine(plateauSetOperationResult.Plateau);
            }
            else
            {
                System.Console.WriteLine(message);
            }
            var read = System.Console.ReadLine();
            if (!string.IsNullOrEmpty(read) && String.Equals(read, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                // shut down the system (acquire handle to system via
                // this actors context)
                Context.System.Terminate();
                return;
            }

            // send input to the console writer to process and print
            rover.Tell(read);
        }
    }
}
