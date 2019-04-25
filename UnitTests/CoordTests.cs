using FluentAssertions;
using Xunit;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.UnitTests
{
    public class CoordTests
    {
        [Theory]
        [InlineData("5 10", true, 5, 10)]
        [InlineData("-5 10", true, -5, 10)]
        [InlineData("1 2 N", false, 0, 0)]
        [InlineData("  5  10  ", true, 5, 10)]
        public void Parse(string input, bool result, int x, int y)
        {
            Coord.TryParse(input, out Coord coord).Should().Be(result);
            if (result)
            {
                coord.X.Should().Be(x);
                coord.Y.Should().Be(y);
            }
        }
    }
}
