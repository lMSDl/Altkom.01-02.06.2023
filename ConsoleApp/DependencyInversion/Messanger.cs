using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DependencyInversion
{
    internal class Messanger
    {
        //wstrzykiwanie przez włąściwość
        public IEnumerable<IMessage> Messages { get; set; }

        //wstrzykiwanie przez konstruktor
        public Messanger(IEnumerable<IMessage> messages)
        {
            Messages = messages;
        }

        //wstrzykiwanie przez metodę
        public void Send(IEnumerable<IMessage> messages)
        {
            Messages = messages;
            Send();
        }

        public void Send()
        {
            foreach (var message in Messages)
            {
                message.Send();
            }
        }


    }
}
