using Akka.Actor;
using Akka.Persistence;
using System;

namespace Zip.MarsRover.Core
{
    public class RoverErrors
    {
        public const string PlateauDoubleSetError = "Plateau can be only set in first command.";
        public const string SettingRoverLocationBeforeDefiningPlateau = "First plateau should be defined.";
        public const string MovingRoverBeforeDefiningPlateau = "Before moving rover, first plateau should be defined.";
        public const string MovingRoverBeforeSettingInitialPosition = "Before moving rover, first set initial position.";
        public const string FirstPoistionOutOfPlateauError = "Rover can not start out of plateau.";
        public const string MovingRoverOutOfPlateauError = "Rover can not go out of plateau.";
    }

    public class RoverMessages
    {
        public const string PlateauSet = "Rover's plateau set.";
        public const string PoistionSet = "Rover's position set.";
        public const string Moved = "Rover moved.";
    }

    public class Rover : ReceivePersistentActor
    {
        public Position InitialPosition { get => initialPosition; private set => initialPosition = value; }

        public Position Position { get; private set; }

        public Plateau Plateau { get; private set; }

        public override string PersistenceId => $"Rover Persistence ID";

        private Position initialPosition;

        public Rover()
        {
            Recover<string>(msg => DefinePlateau(msg), msg => Coord.IsCoord(msg));
            Recover<string>(msg => SetInitialPoistion(msg), msg => Position.IsPosition(msg));
            Recover<string>(msg => Move(msg));

            Command<string>(msg => Persist(msg, m => DefinePlateau(m)), msg => Coord.IsCoord(msg));
            Command<string>(msg => Persist(msg, m => SetInitialPoistion(m)), msg => Position.IsPosition(msg));
            Command<string>(msg => Persist(msg, m => Move(m)));
        }

        /// <summary>
        /// Plateau can be set only once
        /// </summary>
        /// <param name="msg"></param>
        private void DefinePlateau(string msg)
        {
            if (Plateau == null)
            {
                Coord.TryParse(msg, out Coord coord);
                Plateau = new RectPlateau(coord);
                Sender.Tell(new PlateauSetOperationResult(Plateau));
            }
            else
            {
                Sender.Tell(new FailOperationResult(RoverErrors.PlateauDoubleSetError));
            }
        }

        /// <summary>
        /// Setting initial position should be after defining plateau
        /// </summary>
        /// <param name="msg"></param>
        private void SetInitialPoistion(string msg)
        {
            if (Plateau == null)
            {
                Sender.Tell(new FailOperationResult(RoverErrors.SettingRoverLocationBeforeDefiningPlateau));
            }
            else
            {
                Position.TryParse(msg, out initialPosition);
                Position = initialPosition;
                Sender.Tell(new InitialPositionSetOperationResult(initialPosition));
            }
        }

        /// <summary>
        /// Move should be after defining the plateau and shouldn't take the rover out of the plateau
        /// </summary>
        /// <param name="message"></param>
        private void Move(string msg)
        {
            if (Plateau == null)
            {
                Sender.Tell(new FailOperationResult(RoverErrors.MovingRoverBeforeDefiningPlateau));
                return;
            }
            if (initialPosition == null)
            {
                Sender.Tell(new FailOperationResult(RoverErrors.MovingRoverBeforeSettingInitialPosition));
                return;
            }
            var prevPosition = Position;
            foreach (var move in msg)
            {
                Position.Transfer((TransferType)Enum.Parse(typeof(TransferType), move.ToString(), ignoreCase: true));
            }

            if (!Plateau.IsInside(Position.Coord))
            {
                Position = prevPosition;
                Sender.Tell(new FailOperationResult(RoverErrors.MovingRoverOutOfPlateauError));
            }
            else
            {
                Sender.Tell(new MovedOperationResult(Position));
            }
        }
    }
}
