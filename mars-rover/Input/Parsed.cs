using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover.Input
{
    public class Parsed<T>
    {
        public string Input { get; set; }
        public T? ParsedValue { get; set; }
        public ParseOutcome Outcome { get; set; }
        public string? Message { get; set; }
    }
}
