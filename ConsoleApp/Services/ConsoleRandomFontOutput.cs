using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class ConsoleRandomFontOutput : ConsoleOutput
    {

        public ConsoleRandomFontOutput(IEnumerable<IFontService> fontServices) : base(fontServices.ToList()[new Random().Next(0, fontServices.Count())])
        {
            Console.WriteLine(GetType().Name);
        }
    }
}
