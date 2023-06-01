using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class KeyboardSmallFontService : IFontService
    {
        public KeyboardSmallFontService()
        {
            Console.WriteLine(GetType().Name);
        }

        public string Render(string input)
        {
            return Figgle.FiggleFonts.KeyboardSmall.Render(input);
        }
    }
}
