using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Framework.Setup
{
    public class Email
    {
        public struct ServidorSMTP
        {
            public const string Host = "smtp.gmail.com";
            public const int Porta = 587;
            public const bool RequerAutenticacao = true;
            public const bool RequerSSL = true;
        }

        public struct ServidorIMAP
        {
            public const string Host = "imap.gmail.com";
            public const int Porta = 993;
        }
    }
}
