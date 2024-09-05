using mars_rover.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover.Mars_Rover
{
    public class Rover
    {
        public Position Position { get; private set; }

        public string Id { get; private set; }
        public int X { get { return this.Position.X; } }
        public int Y { get { return this.Position.Y; } }
        public CompassDirection Facing { get { return this.Position.Facing; } }

        public Rover(string id, Position initialPosition) {
            this.Id = id;
            this.Position = initialPosition;
        }

        public void SetPosition((int, int) newPosition)
        {
            this.Position = new Position(newPosition.Item1, newPosition.Item2, Facing);
        }

        public void Rotate(Rotation rotation)
        {
            this.Position = new Position(Position.X, Position.Y, Direction.Rotate(this.Facing, rotation));
        }

        public override string ToString()
        {
            return $"{Id} - {Position.ToString()}";
        }
    }
}
