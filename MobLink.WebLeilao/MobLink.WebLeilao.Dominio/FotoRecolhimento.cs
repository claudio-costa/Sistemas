using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class FotoRecolhimento
    {
        public int id { get; set; }
        public int id_lote { get; set; }
        public byte[] foto { get; set; }
        public string nome_foto { get; set; }
        public string observacao { get; set; }
        public string linkfoto { get; set; }
    }
}
