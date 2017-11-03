using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.LinkLeiloes.Dominio
{
    public class Restricao
    {
        public int id { get; set; }
        public int id_lote { get; set; }
        public string codigo { get; set; }
        public string restricao { get; set; }
        public string sub_restricao { get; set; }
        public string observacoes { get; set; }
        public string origem { get; set; }
    }
}
