using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{   
    public class Rel_PC_Lote
    {
        public Rel_PC_Lote()
        {
            DespesasLeilao = new List<Despesa_Leilao>();
            DespesasLote = new List<Despesa_Lote>();
        }

        public int? id_leilao { get; set; }
        public int? id_comitente { get; set; }
        public string descricao { get; set; }
        public string data_leilao { get; set; }
        public string nome { get; set; }
        public string documento { get; set; }
        public string placa { get; set; }
        public string chassi { get; set; }
        public string renavan { get; set; }
        public string marca_modelo { get; set; }
        public string ano_fabricacao { get; set; }
        public string ano_modelo { get; set; }
        public int? id_grv { get; set; }
        public string numero_dias_patio { get; set; }
        public string numero_lote { get; set; }
        public string tipo_veiculo { get; set; }
        public double? avaliacao { get; set; }
        public double? arrematacao { get; set; }
        public double? taxa_comitente { get; set; }

        public List<Despesa_Leilao> DespesasLeilao { get; set; }
        public List<Despesa_Lote> DespesasLote { get; set; }
    }
}
