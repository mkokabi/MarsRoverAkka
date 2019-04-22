using System;

namespace Zip.MarsRover.Core
{
    public class Coord<Type> where Type : IComparable<Type>
    {
        public Type X { get; private set; }
        public Type Y { get; private set; }

        public Coord(Type x, Type y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var coord = obj as Coord<Type>;
            return (coord != null) && coord.X.Equals(this.X) && coord.Y.Equals(this.Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
