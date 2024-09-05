using mars_rover.Entities;
using mars_rover.Mars_Rover;
using mars_rover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace mars_rover_tests.MarsRover
{
    [TestFixture]
    public class RoverTests
    {
        [Test]
        public void Constructor_Should_Set_Id_And_InitialPosition()
        {
            var position = new Position(1, 2, CompassDirection.N);
            var id = "Rover1";

            var rover = new Rover(id, position);

            rover.Id.Should().Be(id);
            rover.Position.Should().Be(position);
        }

        [Test]
        public void X_Should_Return_Correct_X_Coordinate()
        {
            var position = new Position(3, 4, CompassDirection.E);
            var rover = new Rover("Rover1", position);

            var x = rover.X;

            x.Should().Be(3);
        }

        [Test]
        public void Y_Should_Return_Correct_Y_Coordinate()
        {
            // Arrange
            var position = new Position(3, 4, CompassDirection.W);
            var rover = new Rover("Rover1", position);

            // Act
            var y = rover.Y;

            // Assert
            y.Should().Be(4);
        }

        [Test]
        public void Facing_Should_Return_Correct_CompassDirection()
        {
            var position = new Position(3, 4, CompassDirection.S);
            var rover = new Rover("Rover1", position);

            var facing = rover.Facing;

            facing.Should().Be(CompassDirection.S);
        }

        [Test]
        public void SetPosition_Should_Update_Position()
        {
            var initialPosition = new Position(1, 2, CompassDirection.N);
            (int, int) newPosition = (3,2);
            var rover = new Rover("Rover1", initialPosition);

            rover.SetPosition(newPosition);

            rover.Position.Should().BeEquivalentTo(new Position(3,2, CompassDirection.N));
        }

        [Test]
        public void Rotate_Should_Update_Facing_Direction()
        {
            var initialPosition = new Position(1, 2, CompassDirection.N);
            var rover = new Rover("Rover1", initialPosition);

            rover.Rotate(Rotation.R);

            rover.Facing.Should().Be(CompassDirection.E);
        }

        [Test]
        public void ToString_Should_Return_Correct_Format()
        {
            var position = new Position(3, 4, CompassDirection.W);
            var rover = new Rover("Rover1", position);

            var result = rover.ToString();

            result.Should().Be("Rover1 - (3, 4) W");
        }
    }
}