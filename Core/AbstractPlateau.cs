using System;
using System.Collections.Generic;
using System.Text;

namespace Zip.MarsRover.Core
{
    public abstract class AbstractPlateau : IPlateauValidations
    {
        public AbstractPlateau(IEnumerable<Coord> coords)
        {
            Coords = coords;
        }

        public IEnumerable<Coord> Coords { get; }

        public abstract bool IsInside(Coord coord);
    }
}
