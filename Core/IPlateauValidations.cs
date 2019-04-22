using System;

namespace Zip.MarsRover.Core
{
    public interface IPlateauValidations<Type> where Type : IComparable<Type>
    {
        bool IsInside(Coord<Type> coord);
    }
}
