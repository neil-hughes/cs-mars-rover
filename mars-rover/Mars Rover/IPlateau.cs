using mars_rover.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover.Mars_Rover
{
    public interface IPlateau
    {
        public PlateauSize Size { get; }

        public bool IsWithinBounds((int, int) coords);

        public bool IsWithinBounds(Position p);
    }
}
