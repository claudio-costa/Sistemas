using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.LinkLeiloes.Dominio
{
    public class Despesa_Lote
    {
        public int Id { get; set; }
        public int Id_Lote { get; set; }
        public int Id_Despesa { get; set; }
        public double Valor { get; set; }

        public virtual string DescricaoDespesa { get; set; }
    }
}
