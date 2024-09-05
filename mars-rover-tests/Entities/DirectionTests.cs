using mars_rover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover_tests.Entities
{
    [TestFixture]
    public class DirectionTests
    {
        [TestCase(CompassDirection.N, Rotation.L, CompassDirection.W)]
        [TestCase(CompassDirection.N, Rotation.R, CompassDirection.E)]
        [TestCase(CompassDirection.E, Rotation.L, CompassDirection.N)]
        [TestCase(CompassDirection.E, Rotation.R, CompassDirection.S)]
        [TestCase(CompassDirection.S, Rotation.L, CompassDirection.E)]
        [TestCase(CompassDirection.S, Rotation.R, CompassDirection.W)]
        [TestCase(CompassDirection.W, Rotation.L, CompassDirection.S)]
        [TestCase(CompassDirection.W, Rotation.R, CompassDirection.N)]
        public void Rotate_ReturnsCorrectDirection(CompassDirection initial, Rotation rotation, CompassDirection expected)
        {            
            var result = Direction.Rotate(initial, rotation);
                        
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
