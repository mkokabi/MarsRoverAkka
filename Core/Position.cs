using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Zip.MarsRover.Core
{
    public enum Direction
    {
        N, E, S, W
    }

    public class Position<Type> where Type : IComparable<Type>
    {
        public Coord<Type> Coord { get; private set; }
        public Direction Direction { get; set; }

        public Position(Type x, Type y, Direction direction)
        {
            Coord = new Coord<Type>(x, y);
            Direction = direction;
        }

        private const string regex = @"^\s*[0-9,.,-]*\s*[0-9,.,-]*\s*[N,E,S,W]\s*$";

        public static bool IsPosition(string st) => Regex.Match(st, regex).Success;

        public static bool TryParse<PType>(string st, out Position<Type> position)
            where PType : IComparable<Type>
        {
            if (!IsPosition(st))
            {
                position = null;
                return false;
            }
            var parts = st.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            Type x = (Type)Convert.ChangeType(parts[0], typeof(Type));
            Type y = (Type)Convert.ChangeType(parts[1], typeof(Type));
            Direction d = (Direction)Enum.Parse(typeof(Direction), parts[2], true);
            position = new Position<Type>(x, y, d);

            return true;
        }
    }
}
