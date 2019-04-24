using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.UnitTests
{
    public class PositionTests
    {
        [Theory]
        [InlineData("5 10 E", true, 5, 10, Direction.E)]
        [InlineData("-5 10 N", true, -5, 10, Direction.N)]
        [InlineData("4.75 10.25 S", true, 4.75, 10.25, Direction.S)]
        [InlineData("  5  10  E  ", true, 5, 10, Direction.E)]
        public void Parse(string input, bool result, double x, double y, Direction direction)
        {
            Position<double>.TryParse<double>(input, out Position<double> position).Should().Be(result);
            position.Coord.X.Should().Be(x);
            position.Coord.Y.Should().Be(y);
            position.Direction.Should().Be(direction);
        }
    }
}
