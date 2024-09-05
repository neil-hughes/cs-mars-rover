using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover.Mars_Rover
{
    public enum InstructionResult
    {
        SUCCESS,
        UNKNOWN_ROVER,
        HIT_PLATEAU_EDGE,
        HIT_OTHER_ROVER,
        DEVELOPER_ERROR
    }

}