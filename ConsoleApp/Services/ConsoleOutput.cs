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

        public ConsoleOutput(IFontService fontService)
        {
            Console.WriteLine(GetType().Name);
            _fontService = fontService;
        }

        public void WriteText(string text)
        {
            Console.WriteLine(_fontService.Render(text));
        }
    }
}
