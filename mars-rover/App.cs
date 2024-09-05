using mars_rover.Input;
using mars_rover.Mars_Rover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover
{
    // TODO1: actually display the error messages like "crashed into rover" or "hit end of plateau"
    // TODO2: This needs a big refactor and redesign! Lots of validation issues lurking

    public class App
    {
        private UI ui;
        private AppState state;
        private MissionControl missionControl;
        
        public App(UI ui)
        {
            this.ui = ui;
            state = AppState.FIRST_LOAD;
        }

        public void Run()
        {           
            while (true)
            {
                try
                {
                    ProcessCurrentState();
                }catch(Exception e)
                {
                    ui.DisplayException(e);
                }
                
            }
        }

        private void ProcessCurrentState()
        {
            switch (state)
            {
                case AppState.FIRST_LOAD:
                    ui.RenderWelcomeScreen();
                    ui.Clear();
                    state = AppState.PROMPT_FOR_PLATEAU; 
                    break;
                case AppState.PROMPT_FOR_PLATEAU:
                    if (PromptForPlateau())
                    {
                        state = AppState.MENU_PROMPT;
                    }
                    break;
                case AppState.PROMPT_FOR_ROVER:
                    ui.WriteTitle();
                    ui.RenderPlateau(missionControl);
                    if (PromptForRover())
                    {
                        state = AppState.MENU_PROMPT;
                    }
                    break;
                case AppState.PROMPT_FOR_INSTRUCTIONS:
                    ui.WriteTitle();
                    ui.RenderPlateau(missionControl);
                    if (PromptForInstructions())
                    {
                        state = AppState.MENU_PROMPT;
                    }
                    break;
                case AppState.MENU_PROMPT:
                    ui.WriteTitle();
                    ui.RenderPlateau(missionControl);
                    state = PromptMenuChoice();
                    break;
                default:
                    throw new NotSupportedException("State " + state + " not handled");
            }
        }

        private bool PromptForRover()
        {
            var landingPosition = ui.PromptString("Enter landing position e.g. \"1 2 N\" : ");
            var parsed = InputParser.ParseLandingPosition(landingPosition);
                        
            if(parsed.Outcome == ParseOutcome.SUCCESS)
            {
                var roverId = ui.PromptString("Enter new Rover ID: ");
                if (missionControl.Rovers.Any(r => r.Id == roverId)) return false;
                
                missionControl.LandRover(roverId, parsed.ParsedValue!);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PromptForInstructions()
        {
            var roverId = ui.PromptRoverChoice(missionControl.Rovers);

            if(roverId == null) return false;
                        
            var instructions = ui.PromptString("Enter instructions. e.g. \"LMLMLMMM\" : ");
            var parsed = InputParser.ParseInstructions(instructions);

            if (parsed.Outcome == ParseOutcome.SUCCESS)
            {
                missionControl.ProcessInstructions(roverId, parsed.ParsedValue!);
                return true; // TODO make this a proper UI
            }
            else
            {
                return false;
            }
        }

        private bool PromptForPlateau()
        {
            ui.WriteTitle();
            var plateauInput = ui.PromptString("What size is your plateau? e.g. \"5 5\" : ");
            var parsed = InputParser.ParsePlateauSize(plateauInput);

            if (parsed.Outcome == ParseOutcome.SUCCESS)
            {
                missionControl = new MissionControl(new Plateau(parsed.ParsedValue!));
                return true;
            }
            else
            {
                return false;
            }
        }

        private AppState PromptMenuChoice()
        {
            var chosenOption = ui.RenderMainMenu();

            return chosenOption switch
            {
                MainMenuChoice.LAND_NEW_ROVER => AppState.PROMPT_FOR_ROVER,
                MainMenuChoice.INSTRUCT_ROVER => AppState.PROMPT_FOR_INSTRUCTIONS,
                MainMenuChoice.INVALID => AppState.MENU_PROMPT,
                _ => throw new NotSupportedException("Missing menu choice: " + chosenOption),
            };
        }

        


    }
}
