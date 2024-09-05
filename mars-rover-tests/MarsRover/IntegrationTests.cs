using mars_rover.Entities;
using mars_rover.Mars_Rover;
using mars_rover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mars_rover.Input;
using FluentAssertions;

namespace mars_rover_tests.Integration
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void Should_Parse_And_Move_Rover_Correctly_Following_Brief()
        {
            var plateauInput = "5 5";
            var landingPositionInput = "1 2 N";
            var instructionInput = "LMLMLMLMM";

            var expectedFinalPosition = new Position(1, 3, CompassDirection.N);

            var plateauSize = InputParser.ParsePlateauSize(plateauInput);
            var landingPosition = InputParser.ParseLandingPosition(landingPositionInput);
            var instructions = InputParser.ParseInstructions(instructionInput);

            var missionControl = new MissionControl(new Plateau(plateauSize.ParsedValue!));

            missionControl.LandRover("onlyRover", landingPosition.ParsedValue!);

            var result = missionControl.ProcessInstructions("onlyRover", instructions.ParsedValue!);

            result.LastResult.Should().Be(InstructionResult.SUCCESS);
            var finalPosition = missionControl.GetRoverPosition("onlyRover");
            finalPosition.Should().BeEquivalentTo(expectedFinalPosition);
        }
    }
}
