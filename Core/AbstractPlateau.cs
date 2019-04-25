using System.Collections.Generic;

namespace Zip.MarsRover.Core
{
    public abstract class Plateau : IPlateauValidations, IPlateau
    {
        public Plateau(IEnumerable<Coord> coords)
        {
            Coords = coords;
        }

        public IEnumerable<Coord> Coords { get; }

        public abstract bool IsInside(Coord coord);
    }
}
