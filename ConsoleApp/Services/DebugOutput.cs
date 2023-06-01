using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class DebugOutput : IOutputService
    {
        public void WriteText(string text)
        {
            Debug.WriteLine(text);
        }
    }
}
