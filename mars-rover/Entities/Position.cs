using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover.Entities
{
    public record Position(int X, int Y, CompassDirection Facing)
    {
        public Position((int, int) coordinates, CompassDirection facing) : this(coordinates.Item1, coordinates.Item2, facing)
        {

        }

        public Position((int, int) coordinates) : this(coordinates.Item1, coordinates.Item2, CompassDirection.N)
        {

        }

        public (int,int) Next()
        {
            return Facing switch
            {
                CompassDirection.N => (X, Y + 1),
                CompassDirection.S => (X, Y - 1),
                CompassDirection.E => (X + 1, Y),
                CompassDirection.W => (X - 1, Y),
                _ => throw new NotSupportedException("Unknown direction: " + Facing),
            };
        }

        public override string ToString()
        {
            return $"({X}, {Y}) {Facing}";
        }
    }
    
}
