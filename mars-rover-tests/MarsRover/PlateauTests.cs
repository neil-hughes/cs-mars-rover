using mars_rover.Entities;
using mars_rover.Mars_Rover;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover_tests.MarsRover
{
    [TestFixture]
    public class PlateauTests
    {

        [Test]
        public void Constructor_Should_Set_Size()
        {
            
            var size = new PlateauSize(5, 5);

            var plateau = new Plateau(size);

            plateau.Size.Should().Be(size);
        }

        [Test]
        public void Width_Should_Return_Correct_Width()
        {
            
            var size = new PlateauSize(5, 5);
            var plateau = new Plateau(size);
                        
            var width = plateau.Width;
                        
            width.Should().Be(5);
        }

        [Test]
        public void Height_Should_Return_Correct_Height()
        {            
            var size = new PlateauSize(5, 5);
            var plateau = new Plateau(size);

            var height = plateau.Height;
                     
            height.Should().Be(5);
        }

        [Test]
        [TestCase(0, 0, true)]
        [TestCase(4, 4, true)]
        [TestCase(5, 5, false)]
        [TestCase(-1, 0, false)]
        [TestCase(0, -1, false)]
        public void IsWithinBounds_Should_Return_Correct_Value(int x, int y, bool expected)
        {            
            var size = new PlateauSize(5, 5);
            var plateau = new Plateau(size);
                        
            var result = plateau.IsWithinBounds((x, y));
                        
            result.Should().Be(expected);
        }
    }
}
