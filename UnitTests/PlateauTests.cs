using FluentAssertions;
using System.Linq;
using Xunit;
using Zip.MarsRover.Core;

namespace UnitTests
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

        [Fact]
        public void IsInside()
        {
            var rectPlateau = new RectPlateau(new Coord(2, 2));
            rectPlateau.IsInside(new Coord(1, 1)).Should().BeTrue();
            rectPlateau.IsInside(new Coord(0, 0)).Should().BeTrue();
            rectPlateau.IsInside(new Coord(0, 2)).Should().BeTrue();
            rectPlateau.IsInside(new Coord(2, 0)).Should().BeTrue();
            rectPlateau.IsInside(new Coord(2, 2)).Should().BeTrue();

            rectPlateau.IsInside(new Coord(-1, -1)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(-1, 0)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(-1, 2)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(-1, 3)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(0, 3)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(2, 3)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(3, 3)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(3, 2)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(3, 0)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(3, -1)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(2, -1)).Should().BeFalse();
            rectPlateau.IsInside(new Coord(0, -1)).Should().BeFalse();
        }
    }
}
