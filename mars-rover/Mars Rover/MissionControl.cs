using mars_rover.Entities;
using mars_rover.Mars_Rover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover
{
    public class MissionControl
    {
        public IPlateau Plateau { get; private set; }

        public List<Rover> Rovers { get; private set; }

        public MissionControl(IPlateau plateau) { 
            this.Plateau = plateau;
            this.Rovers = [];
        }

        public LandingResult LandRover(string roverId, Position p)
        {
            if (!Plateau.IsWithinBounds(p)) return LandingResult.MISSED_PLATEAU;

            if (Rovers.Any(r => r.Position.X == p.X && r.Position.Y == p.Y)) return LandingResult.HIT_OTHER_ROVER;

            var newRover = new Rover(roverId, p);
            Rovers.Add(newRover);
            return LandingResult.SUCCESS;
        }

        public LandingResult LandRover(string roverId, (int, int) coordinates, CompassDirection initialFacing = CompassDirection.N)
        {
            return LandRover(roverId, new Position(coordinates, initialFacing));
        }

        public Position? GetRoverPosition(string roverId)
        {
            return Rovers.SingleOrDefault(r => r.Id == roverId)?.Position;
        }

       

        public ListInstructionResult ProcessInstructions(string roverId, List<Instruction> instructions)
        {
            for (int i = 0; i < instructions.Count; i++)
            {
                var result = ProcessInstruction(roverId, instructions[i]);
                if (!IsInstructionResultSuccessful(result))
                {
                    return new ListInstructionResult(instructions, i, result);
                }
            }

            return new ListInstructionResult(instructions, instructions.Count, InstructionResult.SUCCESS);
        }

        private bool IsInstructionResultSuccessful(InstructionResult result)
        {
            return result switch
            {
                InstructionResult.SUCCESS => true,
                InstructionResult.UNKNOWN_ROVER or InstructionResult.HIT_PLATEAU_EDGE or InstructionResult.HIT_OTHER_ROVER or InstructionResult.DEVELOPER_ERROR => false,
                _ => throw new NotSupportedException("Unknown instruction result: " + result),
            };
        }

        public InstructionResult ProcessInstruction(string roverId, Instruction instruction)
        {
            var rover = Rovers.SingleOrDefault(r => r.Id == roverId);
            if (rover == null) return InstructionResult.UNKNOWN_ROVER;

            switch (instruction)
            {
                case Instruction.L:
                case Instruction.R:
                    rover.Rotate((Rotation) instruction);
                    return InstructionResult.SUCCESS;                    
                case Instruction.M:
                    return TryMoveRover(roverId, Movement.M);                    
                default:
                    throw new NotSupportedException("Cannot process unknown instruction " + instruction + " for rover " + roverId);
            }

#pragma warning disable CS0162 // Unreachable code detected
            return InstructionResult.DEVELOPER_ERROR;
#pragma warning restore CS0162 // Unreachable code detected
        }
                
        public InstructionResult TryMoveRover(string roverId, Movement instruction)
        {
            var rover = Rovers.SingleOrDefault(r => r.Id == roverId);

            if (rover == null) return InstructionResult.UNKNOWN_ROVER;
                        
            switch (instruction)
            {
                case Movement.M:
                    var newPosition = rover.Position.Next();
                    if (!Plateau.IsWithinBounds(newPosition)) return InstructionResult.HIT_PLATEAU_EDGE;
                    if (Rovers.Any(r => r.Position.X == newPosition.Item1 && r.Position.Y == newPosition.Item2)) return InstructionResult.HIT_OTHER_ROVER;
                    rover.SetPosition(newPosition);
                    return InstructionResult.SUCCESS;                    
                default:
                    throw new NotSupportedException("Instruction " + instruction + " cannot move a rover.");
            }
        }

        

    }
}
