using System;

namespace Zip.MarsRover.Core
{
    public class Rover<Type> where Type : IComparable<Type>
    {
        private readonly AbstractPlateau<Type> plateau;

        public Rover(Coord<Type> initialCoord, AbstractPlateau<Type> plateau)
        {
            InitialCoord = initialCoord ?? throw new ArgumentNullException(nameof(initialCoord));
            this.plateau = plateau ?? throw new ArgumentNullException(nameof(plateau));
            if (!this.plateau.IsInside(initialCoord))
            {
                throw new ArgumentOutOfRangeException("The initial coordinate should be in the plateau");
            }
        }

        public Coord<Type> InitialCoord { get; }
    }
}
