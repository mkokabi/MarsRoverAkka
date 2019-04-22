using FluentAssertions;
using System;
using Xunit;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.UnitTests
{
    public class RoverTests
    {
        [Fact]
        public void InvalidArgumentExceptions()
        {
            Action action = () => new Rover<int>(null, null);
            action.Should().Throw<ArgumentNullException>();

            action = () => new Rover<int>(new Coord<int>(1, 1), null);
            action.Should().Throw<ArgumentNullException>();

            action = () => new Rover<int>(null, new RectPlateau<int>(new Coord<int>(2, 2)));
            action.Should().Throw<ArgumentNullException>();

            action = () => new Rover<int>(new Coord<int>(3, 3), new RectPlateau<int>(new Coord<int>(2, 2)));
            action.Should().Throw<ArgumentOutOfRangeException>();

            action = () => new Rover<int>(new Coord<int>(1, 1), new RectPlateau<int>(new Coord<int>(2, 2)));
            action.Should().NotThrow<ArgumentNullException>();
        }
    }
}