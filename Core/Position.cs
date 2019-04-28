﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Zip.MarsRover.Core
{
    public class Direction
    {
        public const int N = 0;
        public const int E = 1;
        public const int S = 2;
        public const int W = 3;

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
        public int Direction { get; private set; }

        public Position(int x, int y, int direction)
        {
            Coord = new Coord(x, y);
            Direction = direction;
        }

        public override bool Equals(object obj)
        {
            return (obj is Position position)
                && position.Coord.Equals(this.Coord)
                && position.Direction.Equals(this.Direction);
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

        static Dictionary<string, int> directionNames = new Dictionary<string, int>()
        {
            { "N", Core.Direction.N },
            { "E", Core.Direction.E },
            { "S", Core.Direction.S },
            { "W", Core.Direction.W },
        };

        public static bool TryParse(string st, out Position position)
        {
            if (!st.IsPosition())
            {
                position = null;
                return false;
            }
            var parts = st.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            int d = directionNames[parts[2]];
            position = new Position(x, y, d);

            return true;
        }

        Dictionary<int, Func<Coord, Coord>> DirectionalMoves = 
            new Dictionary<int, Func<Coord, Coord>> {
                { Core.Direction.N, coord => new Coord(coord.X, coord.Y + 1)},
                { Core.Direction.E, coord => new Coord(coord.X + 1, coord.Y)},
                { Core.Direction.S, coord => new Coord(coord.X, coord.Y - 1)},
                { Core.Direction.W, coord => new Coord(coord.X - 1, coord.Y)},
        };

        public void Transfer(TransferType transferType)
        {
            switch (transferType)
            {
                case TransferType.M:
                    if (DirectionalMoves.TryGetValue(Direction, out Func<Coord, Coord> func))
                    {
                        Coord = func(Coord);
                    }
                    break;
                case TransferType.R:
                    Direction = (Direction < 3) ? Direction + 1 : Core.Direction.N;
                    break;
                case TransferType.L:
                    Direction = (Direction > 0) ? Direction - 1 : Core.Direction.W;
                    break;
            }
        }
    }

    public static class PositionExtensions
    {
        private const string positionRegex = @"^\s*[0-9,.,-]+\s+[0-9,.,-]+\s+[N,E,S,W]\s*$";

        public static bool IsPosition(this string st) => Regex.Match(st, positionRegex).Success;

        private const string transferRegex = @"^\s*[MLR]+\s*$";

        public static bool IsTranserType(this string st) => Regex.Match(st, transferRegex).Success;

    }
}
