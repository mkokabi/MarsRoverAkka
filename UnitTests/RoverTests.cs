using Akka.Actor;
using Akka.TestKit.Xunit2;
using FluentAssertions;
using Xunit;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.UnitTests
{
    public class RoverTests : TestKit
    {
        [Fact]
        public void DefiningPlateau()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("5 5");
            var result = ExpectMsg<PlateauSetOperationResult>();
            result.Result.Should().Be(RoverMessages.PlateauSet);
            result.Plateau.Should().Be(new RectPlateau(new Coord(5, 5)));
        }

        [Fact]
        public void RedefinePlateau_should_fail()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("5 5");
            rover.Tell("5 6");
            var firstResult = ExpectMsg<SuccessOperationResult>();
            var SecondResult = ExpectMsg<FailOperationResult>();
            SecondResult.Error.Should().Be(RoverErrors.PlateauDoubleSetError);
        }

        [Fact]
        public void Setting_rover_initials_before_defining_Plateau_should_fail()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("1 2 N");
            var result = ExpectMsg<FailOperationResult>();
            result.Error.Should().Be(RoverErrors.SettingRoverLocationBeforeDefiningPlateau);
        }

        [Fact]
        public void Moving_rover_before_defining_Plateau_should_fail()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("LM");
            var result = ExpectMsg<FailOperationResult>();
            result.Error.Should().Be(RoverErrors.MovingRoverBeforeDefiningPlateau);
        }

        [Fact]
        public void Moving_rover_before_setting_position_should_fail()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("5 5");
            rover.Tell("LM");
            var firstResult = ExpectMsg<SuccessOperationResult>();
            var secondResult = ExpectMsg<FailOperationResult>();
            secondResult.Error.Should().Be(RoverErrors.MovingRoverBeforeSettingInitialPosition);
        }

        [Fact]
        public void Set_rover_plateu_set_position()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("5 5");
            rover.Tell("1 2 N");
            var firtsResult = ExpectMsg<SuccessOperationResult>();
            var secondResult = ExpectMsg<InitialPositionSetOperationResult>();
            secondResult.Position.Should().Be(new Position(1, 2, Direction.N));
        }

        [Theory]
        [InlineData("5 5", "1 2 N", "M", 1, 3, Direction.N)]
        [InlineData("5 5", "1 2 N", "ML", 1, 3, Direction.W)]
        [InlineData("5 5", "1 2 N", "MR", 1, 3, Direction.E)]
        [InlineData("5 5", "1 2 N", "MRM", 2, 3, Direction.E)]
        [InlineData("5 5", "1 2 N", "LMLMLMLMM", 1, 3, Direction.N)]
        [InlineData("5 5", "3 3 E", "MMRMMRMRRM", 5, 1, Direction.E)]
        public void Set_rover_plateu_set_position_and_move(string plateau, string initialPos, string move, 
            int newX, int newY, Direction newDirection)
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell(plateau);
            rover.Tell(initialPos);
            rover.Tell(move);
            var firtsResult = ExpectMsg<SuccessOperationResult>();
            var secondResult = ExpectMsg<SuccessOperationResult>();
            var thirdResult = ExpectMsg<MovedOperationResult>();
            thirdResult.Position.Should().Be(new Position(newX, newY, newDirection));
        }

        [Fact]
        public void Rover_moving_out_of_plateau_should_fail_and_revert_the_move()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("2 2");
            rover.Tell("1 1 N");
            rover.Tell("MM");
            var firtsResult = ExpectMsg<SuccessOperationResult>();
            var secondResult = ExpectMsg<SuccessOperationResult>();
            var thirdResult = ExpectMsg<FailOperationResult>();
            thirdResult.Error.Should().Be(RoverErrors.MovingRoverOutOfPlateauError);
            rover.Tell("L");
            var fourthResult = ExpectMsg<SuccessOperationResult>();
        }
    }
}
