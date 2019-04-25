using System;
using System.Linq;

namespace Zip.MarsRover.Core
{
    public class RectPlateau : Plateau 
    {
        public RectPlateau(Coord upperRight) :
            base(new[] { new Coord(default(int), default(int)), upperRight })
        {
        }

        public RectPlateau(Coord lowerLeft, Coord upperRight) :
            base(new[] { lowerLeft, upperRight })
        {
        }

        public override bool Equals(object obj)
        {
            return (obj is RectPlateau rectPlateau)
                && rectPlateau.Coords.First().Equals(this.Coords.First())
                && rectPlateau.Coords.Last().Equals(this.Coords.Last());
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + Coords.First().GetHashCode();
                hash = hash * 23 + Coords.Last().GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{Coords.First()} {Coords.Last()}";
        }

        public override bool IsInside(Coord coord)
        {
            var lowerLeft = this.Coords.First();
            var upperRight = this.Coords.Last();
            return !(lowerLeft.X.CompareTo(coord.X) > 0) &&
                !(upperRight.X.CompareTo(coord.X) < 0) &&
                !(lowerLeft.Y.CompareTo(coord.Y) > 0) &&
                !(upperRight.Y.CompareTo(coord.Y) < 0);
        }
    }
}
