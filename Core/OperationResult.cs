namespace Zip.MarsRover.Core
{
    public class OperationResult
    {
        public bool Successful { get; protected set; } = true;

        public bool Failed { get; protected set; } = false;

        public string Error { get; protected set; }

        public string Result { get; protected set; }
    }

    public class SuccessOperationResult : OperationResult
    {
        public SuccessOperationResult(string result)
        {
            Failed = false;
            Successful = true;
            Result = result;
        }
    }

    public sealed class PlateauSetOperationResult : SuccessOperationResult
    {
        public PlateauSetOperationResult(IPlateau plateau)
            : base (RoverMessages.PlateauSet)
        {
            Plateau = plateau;
        }

        public IPlateau Plateau { get; }
    }

    public sealed class InitialPositionSetOperationResult : SuccessOperationResult
    {
        public InitialPositionSetOperationResult(Position position)
            : base (RoverMessages.PoistionSet)
        {
            Position = position;
        }

        public Position Position { get; }
    }

    public sealed class MovedOperationResult : SuccessOperationResult
    {
        public MovedOperationResult(Position position) 
            : base(RoverMessages.Moved)
        {
            Position = position;
        }

        public Position Position { get; }
    }

    public sealed class FailOperationResult : OperationResult
    {
        public FailOperationResult(string error)
        {
            Failed = true;
            Successful = false;
            Error = error;
        }
    }
}
