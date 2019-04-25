using System.Collections.Generic;

namespace Zip.MarsRover.Core
{
    public interface IPlateau
    {
        IEnumerable<Coord> Coords { get; }

        bool IsInside(Coord coord);
    }
}