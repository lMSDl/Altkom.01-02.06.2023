using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Config.Models
{
    public class Greetings
    {
        public string Value { get; set; }
        public Targets Targets { get; set; }
        public int Repeat { get; set; }
    }
}
