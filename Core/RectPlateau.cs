using System;
using System.Linq;

namespace Zip.MarsRover.Core
{
    public class RectPlateau<Type> : AbstractPlateau<Type> where Type : IComparable<Type>
    {
        public RectPlateau(Coord<Type> upperRight) :
            base(new[] { new Coord<Type>(default(Type), default(Type)), upperRight })
        {
        }

        public RectPlateau(Coord<Type> lowerLeft, Coord<Type> upperRight) :
            base(new[] { lowerLeft, upperRight })
        {
        }

        public override bool IsInside(Coord<Type> coord)
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
