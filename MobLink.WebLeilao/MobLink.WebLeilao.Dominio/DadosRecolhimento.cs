using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class DadosRecolhimento
    {
        public DadosRecolhimento()
        {
            Recolhimento = new Transacao002();
            Guarda = new Transacao006();
        }

        public Transacao002 Recolhimento { get; set; }
        public Transacao006 Guarda { get; set; }
    }
}
