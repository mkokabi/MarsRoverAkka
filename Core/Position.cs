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

    public enum TransferType
    {
        M, // move at current direction
        L, // turn left
        R, // turn right
    }

    public class Position
    {
        public Coord Coord { get; private set; }
        public Direction Direction { get; private set; }

        public Position(int x, int y, Direction direction)
        {
            Coord = new Coord(x, y);
            Direction = direction;
        }

        private const string regex = @"^\s*[0-9,.,-]*\s*[0-9,.,-]*\s*[N,E,S,W]\s*$";

        public override bool Equals(object obj)
        {
            return (obj is Position position)
                && position.Coord.Equals(this.Coord)
                && position.Direction == this.Direction;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + Coord.GetHashCode();
                hash = hash * 23 + Direction.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{Coord} {Direction}";
        }

        public static bool IsPosition(string st) => Regex.Match(st, regex).Success;

        public static bool TryParse(string st, out Position position)
        {
            if (!IsPosition(st))
            {
                position = null;
                return false;
            }
            var parts = st.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            Direction d = (Direction)Enum.Parse(typeof(Direction), parts[2], true);
            position = new Position(x, y, d);

            return true;
        }

        public void Transfer(TransferType transferType)
        {
            switch (transferType)
            {
                case TransferType.M:
                    switch (Direction)
                    {
                        case Direction.N:
                            Coord = new Coord(Coord.X, Coord.Y + 1);
                            break;
                        case Direction.E:
                            Coord = new Coord(Coord.X + 1, Coord.Y);
                            break;
                        case Direction.S:
                            Coord = new Coord(Coord.X, Coord.Y - 1);
                            break;
                        case Direction.W:
                            Coord = new Coord(Coord.X - 1, Coord.Y);
                            break;
                        default:
                            break;
                    }
                    break;
                case TransferType.R:
                    Direction = ((int)Direction < 3) ? Direction + 1 : Direction.N;
                    break;
                case TransferType.L:
                    Direction = (Direction > 0) ? Direction - 1 : Direction.W;
                    break;
            }
        }
    }
}
