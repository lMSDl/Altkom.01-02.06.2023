using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class ConsoleRandomFontOutput : ConsoleOutput
    {

        public ConsoleRandomFontOutput(IEnumerable<IFontService> fontServices, ILogger<ConsoleRandomFontOutput> logger) : base(fontServices.ToList()[new Random().Next(0, fontServices.Count())], logger)
        {
        }
    }
}
