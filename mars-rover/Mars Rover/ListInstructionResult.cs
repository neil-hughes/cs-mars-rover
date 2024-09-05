using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover.Mars_Rover
{
    public class ListInstructionResult(List<Instruction> instructions, int completed, InstructionResult lastResult)
    {
        public List<Instruction> Instructions { get; set; } = instructions;
        public int Completed { get; set; } = completed;
        public InstructionResult LastResult { get; set; } = lastResult;
    }
}
