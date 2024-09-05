using FluentAssertions;
using mars_rover.Entities;
using mars_rover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mars_rover_tests.Entities
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void Next_Should_Return_Correct_Coordinates_When_Facing_North()
        {
            var position = new Position(3, 3, CompassDirection.N);

            var nextPosition = position.Next();

            nextPosition.Should().Be((3, 4));
        }

        [Test]
        public void Next_Should_Return_Correct_Coordinates_When_Facing_South()
        {
            var position = new Position(3, 3, CompassDirection.S);

            var nextPosition = position.Next();

            nextPosition.Should().Be((3, 2));
        }

        [Test]
        public void Next_Should_Return_Correct_Coordinates_When_Facing_East()
        {
            var position = new Position(3, 3, CompassDirection.E);

            var nextPosition = position.Next();

            nextPosition.Should().Be((4, 3));
        }

        [Test]
        public void Next_Should_Return_Correct_Coordinates_When_Facing_West()
        {
            var position = new Position(3, 3, CompassDirection.W);

            var nextPosition = position.Next();

            nextPosition.Should().Be((2, 3));
        }

        [Test]
        public void Next_Should_Throw_NotSupportedException_When_Facing_Unknown_Direction()
        {
            var position = new Position(3, 3, (CompassDirection)999);

            Action action = () => position.Next();

            action.Should().Throw<NotSupportedException>()
                .WithMessage("Unknown direction: 999");
        }
    }

}
