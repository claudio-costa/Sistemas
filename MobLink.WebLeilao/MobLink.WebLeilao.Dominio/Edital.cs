using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class Edital
    {
        public int id { get; set; }
        public int id_leilao { get; set; }
        public DateTime data_geracao { get; set; }
        public int id_usuario_geracao { get; set; }
    }
}
