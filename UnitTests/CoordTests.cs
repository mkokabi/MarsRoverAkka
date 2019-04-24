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
        [InlineData("4.75 10.25", true, 4.75, 10.25)]
        [InlineData("1 2 N", false, 0, 0)]
        [InlineData("  5  10  ", true, 5, 10)]
        public void Parse(string input, bool result, double x, double y)
        {
            Coord<double>.TryParse<double>(input, out Coord<double> coord).Should().Be(result);
            if (result)
            {
                coord.X.Should().Be(x);
                coord.Y.Should().Be(y);
            }
        }
    }
}
