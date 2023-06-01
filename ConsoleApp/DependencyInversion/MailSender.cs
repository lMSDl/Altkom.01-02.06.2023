using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DependencyInversion
{
    internal class MailSender : IMessage
    {
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public void Send()
        {
            SendMail();
        }

        public void SendMail()
        {
            Console.WriteLine("Sending mail...");
        }
    }
}
