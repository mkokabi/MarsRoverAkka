using Akka.Actor;
using System;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.ConsoleApp
{
    public class Console : UntypedActor
    {
        public const string ExitCommand = "exit";

        private readonly IActorRef rover;
        private readonly ConsoleColor consoleColor;

        public Console(IActorRef rover)
        {
            consoleColor = System.Console.ForegroundColor;
            this.rover = rover;
        }

        protected override void OnReceive(object message)
        {
            if (message is MovedOperationResult movedOperationResult)
            {
                ColoredWriteLine(movedOperationResult.Position.ToString(), ConsoleColor.Yellow);
            }
            else if (message is InitialPositionSetOperationResult positionSetOperationResult)
            {
                ColoredWriteLine(positionSetOperationResult.Position.ToString(), ConsoleColor.Cyan);
            }
            else if (message is PlateauSetOperationResult plateauSetOperationResult)
            {
                ColoredWriteLine(plateauSetOperationResult.Plateau.ToString(), ConsoleColor.Green);
            }
            else if (message is FailOperationResult failOperationResult)
            {
                ColoredWriteLine(failOperationResult.Error, ConsoleColor.Red);
            }
            else
            {
                System.Console.WriteLine(message);
            }
            var read = System.Console.ReadLine();
            if (!string.IsNullOrEmpty(read) && String.Equals(read, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                System.Console.ForegroundColor = consoleColor;
                // shut down the system (acquire handle to system via
                // this actors context)
                Context.System.Terminate();
                return;
            }

            // send input to the console writer to process and print
            rover.Tell(read);
        }

        private void ColoredWriteLine(string message, ConsoleColor color)
        {
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(message);
            System.Console.ForegroundColor = consoleColor;
        }
    }
}
