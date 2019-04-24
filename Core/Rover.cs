using Akka.Actor;
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
    }
    public class RoverMessages
    {
        public const string PlateauSet = "Rover's plateau set.";
        public const string PoistionSet = "Rover's position set.";
        public const string Moved = "Rover moved.";
    }

    public class Rover<Type> : ReceiveActor
        where Type : IComparable<Type>
    {
        public Position<Type> InitialPosition { get => initialPosition; private set => initialPosition = value; }

        public Position<Type> Position { get; private set; }

        public AbstractPlateau<Type> Plateau { get; private set; }

        private Position<Type> initialPosition;

        public Rover()
        {
            Receive<string>(msg => DefinePlateau(msg), msg => Coord<int>.IsCoord(msg));
            Receive<string>(msg => SetInitialPoistion(msg), msg => Position<Type>.IsPosition(msg));
            Receive<string>(msg => Move(msg));
        }

        /// <summary>
        /// Plateau can be set only once
        /// </summary>
        /// <param name="msg"></param>
        private void DefinePlateau(string msg)
        {
            if (Plateau == null)
            {
                Coord<Type>.TryParse<Type>(msg, out Coord<Type> coord);
                Plateau = new RectPlateau<Type>(coord);
                Sender.Tell(new SuccessOperationResult(RoverMessages.PlateauSet));
            }
            else
            {
                Sender.Tell(new FailOperationResult(RoverErrors.PlateauDoubleSetError));
            }
        }

        private void SetInitialPoistion(string msg)
        {
            if (Plateau == null)
            {
                Sender.Tell(new FailOperationResult(RoverErrors.SettingRoverLocationBeforeDefiningPlateau));
            }
            else
            {
                Position<Type>.TryParse<Type>(msg, out initialPosition);
                Sender.Tell(new SuccessOperationResult(RoverMessages.PoistionSet));
            }
        }

        /// <summary>
        /// Move should be after defining the plateau and shouldn't take the rover out of the plateau
        /// </summary>
        /// <param name="message"></param>
        private void Move(string message)
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
            //var newCoord = initialPosition.Move( Coord.Transfer
            //if (!Plateau.IsInside(initialCoord))
            //{
            //    throw new ArgumentOutOfRangeException("The initial coordinate should be in the plateau");
            //}
            else
            {
                Sender.Tell(new SuccessOperationResult(RoverMessages.Moved));
            }
        }
    }
}
