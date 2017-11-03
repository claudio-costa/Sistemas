using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Framework.Dominio.Sap
{
    public class Transacao
    {
        public int idTransacao { get; set; }
        public string retDocId { get; set; }
        public string retMensagens { get; set; }
        public string retnota { get; set; }
        public string retdtemissao { get; set; }
    }
}
