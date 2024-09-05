using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover
{
    public class WelcomeScreen
    {
        public static void Display()
        {            
            AnsiConsole.Write(
                new FigletText("Welcome to the")
                    .Centered()
                    .Color(Color.Orange1));

            AnsiConsole.Write(
                new FigletText("Mars Rover")
                    .Centered()
                    .Color(Color.Red));

            
            AnsiConsole.Write(new Panel(Align.Center(new Rows(
                [
                    new Markup("Ready to explore the red planet?"),
                   new Markup("[bold green]Press any key to begin...[/]")
                    ])))
                .Border(BoxBorder.Double)
                .Header("[bold yellow]Mars Mission[/]")
                .BorderStyle(new Style(Color.Yellow))
                .Padding(1, 1)
                );

            
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine();
                        
            
            Console.ReadKey(true);
        }
    }
}
