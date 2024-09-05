using NUnit.Framework;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using mars_rover.Entities;
using mars_rover.Mars_Rover;
using mars_rover;

namespace mars_rover_tests
{
    [TestFixture]
    public class MissionControlTests
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
        public void Constructor_Should_Initialize_Rovers_List()
        {
            var missionControl = new MissionControl(_mockPlateau.Object);

            missionControl.Rovers.Should().NotBeNull();
            missionControl.Rovers.Should().BeEmpty();
        }

        [Test]
        public void LandRover_Should_Return_MISSED_PLATEAU_If_Coordinates_Are_Out_Of_Bounds()
        {
            var coordinates = (5, 5);
            _mockPlateau.Setup(p => p.IsWithinBounds(coordinates)).Returns(false);

            var result = _missionControl.LandRover("Rover1", coordinates);

            result.Should().Be(LandingResult.MISSED_PLATEAU);
            _missionControl.Rovers.Should().BeEmpty();
        }

        [Test]
        public void LandRover_Should_Return_HIT_OTHER_ROVER_If_Coordinates_Are_Occupied()
        {
            var coordinates = (2, 2);
            var position = new Position(2, 2, CompassDirection.N);
            // _mockPlateau.Setup(p => p.IsWithinBounds(coordinates)).Returns(true);
            _mockPlateau.Setup(p => p.IsWithinBounds(position)).Returns(true);
            _missionControl.Rovers.Add(new Rover("Rover1", position));

            var result = _missionControl.LandRover("Rover2", coordinates);

            result.Should().Be(LandingResult.HIT_OTHER_ROVER);
            _missionControl.Rovers.Count.Should().Be(1);
        }

        [Test]
        public void LandRover_Should_Return_SUCCESS_If_Coordinates_Are_Valid()
        {
            var position = new Position(3, 3, CompassDirection.N);
            _mockPlateau.Setup(p => p.IsWithinBounds(position)).Returns(true);

            var result = _missionControl.LandRover("Rover1", position);

            result.Should().Be(LandingResult.SUCCESS);
            _missionControl.Rovers.Count.Should().Be(1);
            _missionControl.Rovers.First().Id.Should().Be("Rover1");
            _missionControl.Rovers.First().Position.Should().BeEquivalentTo(new Position(3, 3, CompassDirection.N)); 
        }

        [Test]
        public void TryMoveRover_Should_Return_UNKNOWN_ROVER_If_Rover_Does_Not_Exist()
        {
            var result = _missionControl.TryMoveRover("NonExistentRover", Movement.M);

            result.Should().Be(InstructionResult.UNKNOWN_ROVER);
        }

        [Test]
        public void TryMoveRover_Should_Return_HIT_PLATEAU_EDGE_If_Move_Would_Go_Out_Of_Bounds()
        {
            var rover = new Rover("Rover1", new Position(5, 5, CompassDirection.N));
            _missionControl.Rovers.Add(rover);
            _mockPlateau.Setup(p => p.IsWithinBounds(It.IsAny<(int, int)>())).Returns(false);

            var result = _missionControl.TryMoveRover("Rover1", Movement.M);

            result.Should().Be(InstructionResult.HIT_PLATEAU_EDGE);
        }

        [Test]
        public void TryMoveRover_Should_Return_HIT_OTHER_ROVER_If_Move_Would_Collide_With_Another_Rover()
        {
            var rover1 = new Rover("Rover1", new Position(1, 1, CompassDirection.N));
            var rover2 = new Rover("Rover2", new Position(1, 2, CompassDirection.N));
            _missionControl.Rovers.Add(rover1);
            _missionControl.Rovers.Add(rover2);
            _mockPlateau.Setup(p => p.IsWithinBounds(It.IsAny<(int, int)>())).Returns(true);

            var result = _missionControl.TryMoveRover("Rover1", Movement.M);

            result.Should().Be(InstructionResult.HIT_OTHER_ROVER);
        }
        [Test]
        public void TryMoveRover_Should_Return_SUCCESS_And_Update_Position_If_Move_Is_Valid()
        {            
            var rover = new Rover("Rover1", new Position(1, 1, CompassDirection.N));
            _missionControl.Rovers.Add(rover);
            var newPosition = (1, 2);
            _mockPlateau.Setup(p => p.IsWithinBounds(newPosition)).Returns(true);
                     
            var result = _missionControl.TryMoveRover("Rover1", Movement.M);
                        
            result.Should().Be(InstructionResult.SUCCESS);
            rover.Position.Should().BeEquivalentTo(new Position(1, 2, CompassDirection.N));
        }

        [Test]
        public void TryMoveRover_Should_Throw_NotSupportedException_For_Invalid_Instruction()
        {            
            var rover = new Rover("Rover1", new Position(1, 1, CompassDirection.N));
            _missionControl.Rovers.Add(rover);
                        
            Action action = () => _missionControl.TryMoveRover("Rover1", (Movement)999);
                        
            action.Should().Throw<NotSupportedException>()
                .WithMessage("Instruction 999 cannot move a rover.");
        }

        [Test]
        public void ProcessInstruction_Should_Return_UNKNOWN_ROVER_If_Rover_Does_Not_Exist()
        {            
            var result = _missionControl.ProcessInstruction("NonExistentRover", Instruction.M);
                     
            result.Should().Be(InstructionResult.UNKNOWN_ROVER);
        }

        [Test]
        public void ProcessInstruction_Should_Return_SUCCESS_And_Rotate_Left()
        {            
            var rover = new Rover("Rover1", new Position(1, 1, CompassDirection.N));
            _missionControl.Rovers.Add(rover);
                        
            var result = _missionControl.ProcessInstruction("Rover1", Instruction.L);
                        
            result.Should().Be(InstructionResult.SUCCESS);
            rover.Facing.Should().Be(CompassDirection.W);
        }

        [Test]
        public void ProcessInstruction_Should_Return_SUCCESS_And_Rotate_Right()
        {            
            var rover = new Rover("Rover1", new Position(1, 1, CompassDirection.N));
            _missionControl.Rovers.Add(rover);
                        
            var result = _missionControl.ProcessInstruction("Rover1", Instruction.R);
                        
            result.Should().Be(InstructionResult.SUCCESS);
            rover.Facing.Should().Be(CompassDirection.E);
        }

        [Test]
        public void ProcessInstruction_Should_Return_SUCCESS_And_Move_Forward()
        {            
            var rover = new Rover("Rover1", new Position(1, 1, CompassDirection.N));
            _missionControl.Rovers.Add(rover);
            var newPosition = (1, 2);

            _mockPlateau.Setup(p => p.IsWithinBounds(newPosition)).Returns(true);
                        
            var result = _missionControl.ProcessInstruction("Rover1", Instruction.M);
                        
            result.Should().Be(InstructionResult.SUCCESS);
            rover.Position.Should().BeEquivalentTo(new Position(1, 2, CompassDirection.N));
        }

        [Test]
        public void ProcessInstruction_Should_Throw_NotSupportedException_For_Unknown_Instruction()
        {            
            var rover = new Rover("Rover1", new Position(1, 1, CompassDirection.N));
            _missionControl.Rovers.Add(rover);
                        
            Action action = () => _missionControl.ProcessInstruction("Rover1", (Instruction)999);

            action.Should().Throw<NotSupportedException>()
                .WithMessage("Cannot process unknown instruction 999 for rover Rover1");
        }

        [Test]
        public void GetRoverPosition_Should_Return_Position_If_Rover_Exists()
        {            
            var expectedPosition = new Position(3, 4, CompassDirection.N);
            var rover = new Rover("Rover1", expectedPosition);
            _missionControl.Rovers.Add(rover);
                     
            var position = _missionControl.GetRoverPosition("Rover1");
                        
            position.Should().BeEquivalentTo(expectedPosition);
        }

        [Test]
        public void GetRoverPosition_Should_Return_Null_If_Rover_Does_Not_Exist()
        {            
            var position = _missionControl.GetRoverPosition("NonExistentRover");
                     
            position.Should().BeNull();
        }

        [Test]
        public void ProcessInstructions_Should_Return_Success_If_All_Instructions_Are_Successful()
        {            
            var rover = new Rover("Rover1", new Position(1, 1, CompassDirection.N));
            _missionControl.Rovers.Add(rover);
            var instructions = new List<Instruction> { Instruction.M, Instruction.L, Instruction.R };

            _mockPlateau.Setup(p => p.IsWithinBounds(It.IsAny<(int, int)>())).Returns(true);
                        
            var result = _missionControl.ProcessInstructions("Rover1", instructions);
                        
            result.Completed.Should().Be(instructions.Count);
            result.LastResult.Should().Be(InstructionResult.SUCCESS);
        }

        [Test]
        public void ProcessInstructions_Should_Return_Partial_Result_If_An_Instruction_Fails()
        {            
            var rover = new Rover("Rover1", new Position(0, 1, CompassDirection.S));
            _missionControl.Rovers.Add(rover);
            var instructions = new List<Instruction> { Instruction.M, Instruction.M };

            _mockPlateau.SetupSequence(p => p.IsWithinBounds(It.IsAny<(int, int)>())).Returns(true).Returns(false);
                        
            var result = _missionControl.ProcessInstructions("Rover1", instructions);

            result.Completed.Should().Be(1); // Only first instruction was successful
            result.LastResult.Should().Be(InstructionResult.HIT_PLATEAU_EDGE);            
        }

        [Test]
        public void ProcessInstructions_Should_Return_UNKNOWN_ROVER_If_Rover_Does_Not_Exist()
        {            
            var instructions = new List<Instruction> { Instruction.M, Instruction.L, Instruction.R };
                        
            var result = _missionControl.ProcessInstructions("NonExistentRover", instructions);
                        
            result.Completed.Should().Be(0);
            result.LastResult.Should().Be(InstructionResult.UNKNOWN_ROVER);            
        }              
               

    }
}