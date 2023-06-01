using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class ConsoleOutput : IOutputService
    {
        private readonly IFontService _fontService;

        public ConsoleOutput(IFontService fontService, ILogger<ConsoleOutput> logger)
        {
            _fontService = fontService;
            logger.LogInformation(GetType().Name);
        }

        public void WriteText(string text)
        {
            Console.WriteLine(_fontService.Render(text));
        }
    }
}
