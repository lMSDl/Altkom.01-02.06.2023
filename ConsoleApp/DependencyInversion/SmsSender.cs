using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DependencyInversion
{
    internal class SmsSender : IMessage
    {
        public string Number { get; set; }
        public string Content { get; set; }

        public void Send()
        {
            SendSms();
        }

        public void SendSms()
        {
            Console.WriteLine("Sending SMS...");
        }
    }
}
