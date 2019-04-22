using System;
using System.Collections.Generic;
using System.Text;

namespace Zip.MarsRover.Core
{
    public abstract class AbstractPlateau<Type> : IPlateauValidations<Type>
        where Type: IComparable<Type>
    {
        public AbstractPlateau(IEnumerable<Coord<Type>> coords)
        {
            Coords = coords;
        }

        public IEnumerable<Coord<Type>> Coords { get; }

        public abstract bool IsInside(Coord<Type> coord);
    }
}
