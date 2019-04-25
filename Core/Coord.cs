using System;
using System.Text.RegularExpressions;

namespace Zip.MarsRover.Core
{
    public class Coord
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var coord = obj as Coord;
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
        
        public static bool TryParse(string st, out Coord coord)
        {
            if (!IsCoord(st))
            {
                coord = null;
                return false;
            }
            var parts = st.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            coord = new Coord(x, y);

            return true;
        }
    }
}
