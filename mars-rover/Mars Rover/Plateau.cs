using mars_rover.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover.Mars_Rover
{
    

    public class Plateau : IPlateau
    {
        public const int MAX_PLATEAU_SIZE = 20;

        public PlateauSize Size { get; private set; }

        public int Width { get { return Size.Width; } }
        public int Height { get { return Size.Height; } }

        public Plateau(PlateauSize size) {
            Size = size;
        }
                

        public bool IsWithinBounds((int, int) coords)
        {
            return coords.Item1 >= 0 && coords.Item1 < Width && coords.Item2 >= 0 && coords.Item2 < Height;
        }

        public bool IsWithinBounds(Position p)
        {
            return IsWithinBounds((p.X, p.Y));
        }
    }
}
