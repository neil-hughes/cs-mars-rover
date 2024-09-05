using mars_rover.Entities;
using mars_rover.Mars_Rover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover.Input
{
    public class InputParser
    {
        public static Parsed<PlateauSize> ParsePlateauSize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new Parsed<PlateauSize> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
            }
            string[] parts = input.Split(' ');

            if (parts.Length != 2) {
                return new Parsed<PlateauSize> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
            }

            try
            {
                int width = int.Parse(parts[0]);
                int height = int.Parse(parts[1]);

                if(width > Plateau.MAX_PLATEAU_SIZE || height > Plateau.MAX_PLATEAU_SIZE)
                {
                    return new Parsed<PlateauSize> { Input = input, Outcome = ParseOutcome.INVALID_INPUT, Message = "Cannot exceed plateau size of " + Plateau.MAX_PLATEAU_SIZE };
                }

                return new Parsed<PlateauSize> { Input = input, Outcome = ParseOutcome.SUCCESS, ParsedValue = new PlateauSize(width, height) };
            }
            catch
            {
                return new Parsed<PlateauSize> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
            }                        
        }

        public static Parsed<List<Instruction>> ParseInstructions(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new Parsed<List<Instruction>> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
            }

            string[] parts = input.Split(' ');

            if (parts.Length != 1)
            {
                return new Parsed<List<Instruction>> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
            }

            try
            {
                var list = parts[0].Select(ParseInstruction).ToList();
                return new Parsed<List<Instruction>> { Input = input, Outcome = ParseOutcome.SUCCESS, ParsedValue = list };
            }
            catch
            {
                return new Parsed<List<Instruction>> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
            }

        }

        public static Parsed<Position> ParseLandingPosition(string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                return new Parsed<Position> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
            }

            string[] parts = input.Split(' ');

            if (parts.Length != 3)
            {
                return new Parsed<Position> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
            }

            try
            {
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int x) &&
                    int.TryParse(parts[1], out int y) &&
                    Enum.TryParse(parts[2], true, out CompassDirection facing))
                {
                    return new Parsed<Position> { Input = input, Outcome = ParseOutcome.SUCCESS, ParsedValue = new Position(x, y, facing) };
                }
                else
                {
                    return new Parsed<Position> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
                }
            }
            catch (Exception)
            {
                return new Parsed<Position> { Input = input, Outcome = ParseOutcome.INVALID_INPUT };
            }

        }

        private static Instruction ParseInstruction(char input)
        {       
            switch (input)
            {
                case 'L':
                    return Instruction.L;
                case 'M':
                    return Instruction.M;
                case 'R':
                    return Instruction.R;
                default:
                    throw new Exception("Unknown instruction: " + input);
            }
        }
    }
}
