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
            var result = ExpectMsg<OperationResult>();
            result.Successful.Should().BeTrue();
            result.Result.Should().Be(RoverMessages.PlateauSet);
        }

        [Fact]
        public void RedefinePlateau_should_fail()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("5 5");
            rover.Tell("5 6");
            var firstResult = ExpectMsg<OperationResult>();
            firstResult.Successful.Should().BeTrue();
            var SecondResult = ExpectMsg<OperationResult>();
            SecondResult.Failed.Should().BeTrue();
            SecondResult.Error.Should().Be(RoverErrors.PlateauDoubleSetError);
        }

        [Fact]
        public void Setting_rover_initials_before_defining_Plateau_should_fail()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("1 2 N");
            var result = ExpectMsg<OperationResult>();
            result.Failed.Should().BeTrue();
            result.Error.Should().Be(RoverErrors.SettingRoverLocationBeforeDefiningPlateau);
        }

        [Fact]
        public void Moving_rover_before_defining_Plateau_should_fail()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("LM");
            var result = ExpectMsg<OperationResult>();
            result.Failed.Should().BeTrue();
            result.Error.Should().Be(RoverErrors.MovingRoverBeforeDefiningPlateau);
        }

        [Fact]
        public void Moving_rover_before_setting_position_should_fail()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("5 5");
            rover.Tell("LM");
            var firstResult = ExpectMsg<OperationResult>();
            var secondResult = ExpectMsg<OperationResult>();
            secondResult.Failed.Should().BeTrue();
            secondResult.Error.Should().Be(RoverErrors.MovingRoverBeforeSettingInitialPosition);
        }

        [Fact]
        public void Set_rover_plateu_set_position()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("5 5");
            rover.Tell("1 2 N");
            var firtsResult = ExpectMsg<OperationResult>();
            firtsResult.Successful.Should().BeTrue();
            var secondResult = ExpectMsg<OperationResult>();
            secondResult.Successful.Should().BeTrue();
        }

        [Fact]
        public void Set_rover_plateu_set_position_and_move()
        {
            var rover = Sys.ActorOf(Props.Create(() => new Rover()));
            rover.Tell("5 5");
            rover.Tell("1 2 N");
            rover.Tell("LM");
            var firtsResult = ExpectMsg<OperationResult>();
            firtsResult.Successful.Should().BeTrue();
            var secondResult = ExpectMsg<OperationResult>();
            secondResult.Successful.Should().BeTrue();
            var thirdResult = ExpectMsg<OperationResult>();
            thirdResult.Successful.Should().BeTrue();
        }
    }
}
