using mars_rover.Entities;
using mars_rover.Mars_Rover;
using mars_rover;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace mars_rover_tests.MarsRover
{
    [TestFixture]
    public class LogicTests
    {
        
            private Mock<IPlateau> _mockPlateau;
            private MissionControl _missionControl;

            [SetUp]
            public void Setup()
            {
                _mockPlateau = new Mock<IPlateau>();
                _missionControl = new MissionControl(_mockPlateau.Object);
            }

            [Test]
            public void Should_Move_Rover_Correctly_After_Sequence_Of_Instructions_From_Brief()
            {             
                var initialPosition = new Position(1, 2, CompassDirection.N);            
                var roverId = "Rover1";
                var instructions = new List<Instruction>
                {
                   Instruction.L,
                    Instruction.M,
                    Instruction.L,
                    Instruction.M,
                    Instruction.L,
                    Instruction.M,
                    Instruction.L,
                    Instruction.M,
                    Instruction.M
                };
            var expectedFinalPosition = new Position(1, 3, CompassDirection.N);    

            // have the mock plateau be effectively infinite for this test
            _mockPlateau.Setup(p => p.IsWithinBounds(It.IsAny<(int, int)>())).Returns(true);
            _mockPlateau.Setup(p => p.IsWithinBounds(It.IsAny<Position>())).Returns(true);

            _missionControl.LandRover(roverId, initialPosition);
                                        
                var result = _missionControl.ProcessInstructions(roverId, instructions);
                            
                result.LastResult.Should().Be(InstructionResult.SUCCESS);
                var finalPosition = _missionControl.GetRoverPosition(roverId);
                finalPosition.Should().BeEquivalentTo(expectedFinalPosition);
            }

        [Test]
        public void Should_Move_Rover_Correctly_After_Other_Sequence_Of_Instructions_From_Brief()
        {
            var initialPosition = new Position(3, 3, CompassDirection.E);
            var roverId = "Rover2";
            var instructions = new List<Instruction>
                {
                    Instruction.M,
                    Instruction.M,
                    Instruction.R,
                    Instruction.M,
                    Instruction.M,
                    Instruction.R,
                    Instruction.M,
                    Instruction.R,
                    Instruction.R,
                    Instruction.M
                };
            var expectedFinalPosition = new Position(5, 1, CompassDirection.E);

            // have the mock plateau be effectively infinite for this test
            _mockPlateau.Setup(p => p.IsWithinBounds(It.IsAny<(int, int)>())).Returns(true);
            _mockPlateau.Setup(p => p.IsWithinBounds(It.IsAny<Position>())).Returns(true);

            _missionControl.LandRover(roverId, initialPosition);

            var result = _missionControl.ProcessInstructions(roverId, instructions);

            result.LastResult.Should().Be(InstructionResult.SUCCESS);
            var finalPosition = _missionControl.GetRoverPosition(roverId);
            finalPosition.Should().BeEquivalentTo(expectedFinalPosition);
        }
    }
    
}
