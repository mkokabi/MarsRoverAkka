using System;
using System.Text.RegularExpressions;

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
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + (X != null ? X.GetHashCode() : 0);
                hash = hash * 23 + (Y != null ? Y.GetHashCode() : 0);
                return hash;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        private const string regex = @"^\s*[0-9,.,-]*\s*[0-9,.,-]*\s*$";

        public static bool IsCoord(string st) => Regex.Match(st, regex).Success;
        
        public static bool TryParse<CType>(string st, out Coord<Type> coord)
            where CType: IComparable<Type>
        {
            if (!IsCoord(st))
            {
                coord = null;
                return false;
            }
            var parts = st.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            Type x = (Type)Convert.ChangeType(parts[0], typeof(Type));
            Type y = (Type)Convert.ChangeType(parts[1], typeof(Type));
            coord = new Coord<Type>(x, y);

            return true;
        }
    }
}
