namespace mars_rover
{

    public enum CompassDirection
    {
        N,
        S,
        E,
        W
    }

    public class Direction
    {
        public static CompassDirection Rotate(CompassDirection initial, Rotation rotation)
        {
            return rotation switch
            {
                Rotation.L => rotateLeft(initial),
                Rotation.R => rotateRight(initial),
                _ => throw new Exception("Unknown rotation: " + rotation),
            };
        }

        private static CompassDirection rotateLeft(CompassDirection initial)
        {
            return initial switch
            {
                CompassDirection.S => CompassDirection.E,
                CompassDirection.E => CompassDirection.N,
                CompassDirection.N => CompassDirection.W,
                CompassDirection.W => CompassDirection.S,
                _ => throw new Exception("Unknown direction: " + initial),
            };
        }

        private static CompassDirection rotateRight(CompassDirection initial)
        {
            return initial switch
            {
                CompassDirection.S => CompassDirection.W,
                CompassDirection.E => CompassDirection.S,
                CompassDirection.N => CompassDirection.E,
                CompassDirection.W => CompassDirection.N,
                _ => throw new Exception("Unknown direction: " + initial),
            };
        }
    }

}