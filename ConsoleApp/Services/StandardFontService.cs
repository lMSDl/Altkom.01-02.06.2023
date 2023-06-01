using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class StandardFontService : IFontService
    {
        public StandardFontService()
        {
            Console.WriteLine(GetType().Name);
        }

        public string Render(string input)
        {
            return Figgle.FiggleFonts.Standard.Render(input);
        }
    }
}
