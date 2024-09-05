using mars_rover.Mars_Rover;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover
{    

    public class UI
    {  
      
        public void RenderPlateau(MissionControl missionControl)
        {
            var plateau = missionControl.Plateau;
            var rovers = missionControl.Rovers;
                     
            
            var grid = new Grid().Alignment(Justify.Center);
            grid.AddColumns(plateau.Size.Width);
            
            grid.Expand = true;           

            
            for (int y = plateau.Size.Height - 1; y >= 0; y--)
            {
                
                var row = new List<string>();

                for (int x = 0; x < plateau.Size.Width; x++)
                {
                
                    bool isRoverHere = false;
                    foreach (var rover in rovers)
                    {
                        if (rover.Position.X == x && rover.Position.Y == y)
                        {
                            isRoverHere = true;
                            break;
                        }
                    }
                                        
                    if (isRoverHere)
                    {
                        var facing = GetRoverFacing(rovers.Single(r => r.X == x && r.Y == y));
                        row.Add($"[bold gray]{facing}[/]");
                    }
                    else
                    {
                        row.Add("[red].[/]");
                    }
                }
                                
                grid.AddRow(row.ToArray());
            }

            var panel = new Panel(Align.Center(grid)).DoubleBorder().BorderColor(Color.Red1).Padding(10, 1);
                        
            AnsiConsole.Write(panel);
        }

        private string GetRoverFacing(Rover rover)
        {
            return rover.Facing switch
            {
                CompassDirection.N => "^",
                CompassDirection.E => ">",
                CompassDirection.S => "V",
                CompassDirection.W => "<",
                _ => "*",
            };
        }

        public void RenderWelcomeScreen()
        {
            WelcomeScreen.Display();
        }

        public void Clear()
        {
            AnsiConsole.Clear();
        }

        public MainMenuChoice RenderMainMenu()
        {
                  
            AnsiConsole.Write(new Panel( new Rows([
                Align.Center(new Markup("1. Land New Rover")),
                Align.Center(new Markup("2. Instruct Rover")),
               ])).Border(new RoundedBoxBorder()).Padding(10, 1));

            var result = AnsiConsole.Prompt<int>(
                    new TextPrompt<int>("[bold green]Enter your choice:[/]")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red] That's not a valid number.[/]")
                    );

            return result switch
            {
                1 => MainMenuChoice.LAND_NEW_ROVER,
                2 => MainMenuChoice.INSTRUCT_ROVER,
                _ => MainMenuChoice.INVALID,
            };
        }

        public string PromptString(string text)
        {
            return AnsiConsole.Prompt<string>(new TextPrompt<string>(text)
                .PromptStyle("bold green"));
        }

        public string? PromptRoverChoice(List<Rover> rovers)
        {            

            for(int i = 0; i < rovers.Count; i++)
            {
                AnsiConsole.MarkupLine($"{i + 1}. {rovers[i].Id}");
            }

             var choice = AnsiConsole.Prompt<int>(new TextPrompt<int>("Select your rover: "));

            if(choice < 1 || choice > rovers.Count)
            {
                return null;
            }
            return rovers[choice - 1].Id;
        }

        public void WriteTitle()
        {
            AnsiConsole.Clear();
            var panel = new Panel(Align.Center(
                new Markup("[red]THE MARS ROVER[/]")        
                )).Border(new RoundedBoxBorder()).Padding(10, 1);
            AnsiConsole.Write(panel); 
        }

        public void SayHello()
        {            
            AnsiConsole.Markup("[underline red]Hello[/] World!");
        }

        public void DisplayException(Exception e)
        {
            AnsiConsole.WriteException(e);
        }

        public void DisplayError(string message)
        {
            AnsiConsole.Markup($"[underline red]{message}[/]");
        }


    }
}
