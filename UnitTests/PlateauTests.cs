using FluentAssertions;
using System.Linq;
using Xunit;
using Zip.MarsRover.Core;

namespace Zip.MarsRover.UnitTests
{
    public class PlateauTests
    {
        [Fact]
        public void ConsttuctorWith2CoordsAndGetCoords()
        {
            var rectPlateau = new RectPlateau(new Coord(1, 1), new Coord(2, 2));
            rectPlateau.Coords.Should().HaveCount(2);
            var coords = rectPlateau.Coords.ToArray();
            coords[0].Should().Be(new Coord(1, 1));
            coords[1].Should().Be(new Coord(2, 2));
        }
        [Fact]
        public void ConsttuctorWith1CoordsAndGetCoords()
        {
            var rectPlateau = new RectPlateau(new Coord(2, 2));
            rectPlateau.Coords.Should().HaveCount(2);
            var coords = rectPlateau.Coords.ToArray();
            coords[0].Should().Be(new Coord(0, 0));
            coords[1].Should().Be(new Coord(2, 2));
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 0, true)]
        [InlineData(0, 2, true)]
        [InlineData(2, 0, true)]
        [InlineData(2, 2, true)]
        [InlineData(-1, -1, false)]
        [InlineData(-1, 0, false)]
        [InlineData(-1, 2, false)]
        [InlineData(-1, 3, false)]
        [InlineData( 0, 3, false)]
        [InlineData( 2, 3, false)]
        [InlineData( 3, 3, false)]
        [InlineData( 3, 2, false)]
        [InlineData( 3, 0, false)]
        [InlineData( 3, -1, false)]
        [InlineData( 2, -1, false)]
        [InlineData( 0, -1, false)]
        public void IsInside(int x, int y, bool result)
        {
            var rectPlateau = new RectPlateau(new Coord(2, 2));
            rectPlateau.IsInside(new Coord(x, y)).Should().Be(result);
        }
    }
}
