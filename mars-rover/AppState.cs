using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover
{
    public enum AppState
    {
        FIRST_LOAD,
        MENU_PROMPT,
        PROMPT_FOR_ROVER,
        PROMPT_FOR_PLATEAU,
        PROMPT_FOR_INSTRUCTIONS,
        DISPLAY_PLATEAU
    };
}
