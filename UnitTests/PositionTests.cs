using FluentAssertions;
using Xunit;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.UnitTests
{
    public class PositionTests
    {
        [Theory]
        [InlineData("5 10 E", true, 5, 10, Direction.E)]
        [InlineData("-5 10 N", true, -5, 10, Direction.N)]
        [InlineData("-5 10", false, -5, 10, Direction.N)]
        [InlineData("-5 10 NN", false, -5, 10, Direction.N)]
        [InlineData("  5  10  E  ", true, 5, 10, Direction.E)]
        public void Parse(string input, bool result, int x, int y, int direction)
        {
            Position.TryParse(input, out Position position).Should().Be(result);
            if (result)
            {
                position.Coord.X.Should().Be(x);
                position.Coord.Y.Should().Be(y);
                position.Direction.Should().Be(direction);
            }
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("M", true)]
        [InlineData(" MLR ", true)]
        [InlineData(" 4 ", false)]
        public void IsTransfer(string input, bool result)
        {
            input.IsTranserType().Should().Be(result);
        }

        [Theory]
        [InlineData(1, 1, Direction.N, TransferType.M, 1, 2, Direction.N)]
        [InlineData(1, 1, Direction.E, TransferType.M, 2, 1, Direction.E)]
        [InlineData(1, 1, Direction.S, TransferType.M, 1, 0, Direction.S)]
        [InlineData(1, 1, Direction.W, TransferType.M, 0, 1, Direction.W)]
        [InlineData(1, 1, Direction.N, TransferType.L, 1, 1, Direction.W)]
        [InlineData(1, 1, Direction.N, TransferType.R, 1, 1, Direction.E)]
        [InlineData(1, 1, Direction.W, TransferType.L, 1, 1, Direction.S)]
        [InlineData(1, 1, Direction.W, TransferType.R, 1, 1, Direction.N)]
        public void Transfer(int x, int y, int direction, TransferType transfer, int newX, int newY, int newDirection)
        {
            var position = new Position(x, y, direction);
            position.Transfer(transfer);
            position.Coord.X.Should().Be(newX);
            position.Coord.Y.Should().Be(newY);
            position.Direction.Should().Be(newDirection);
        }
    }
}
