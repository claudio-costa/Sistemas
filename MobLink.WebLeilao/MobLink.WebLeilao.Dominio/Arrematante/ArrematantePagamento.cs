using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class ArrematantePagamento
    {
        public string id_boleto { get; set; }
        public string data_documento { get; set; }
        public string boleto_vencimento { get; set; }
        public string data_credito_boleto { get; set; }
        public string lote_leilao { get; set; }
        public string valor_avaliacao { get; set; }
        public string valor_arrematacao { get; set; }
        public string taxa_administrativa { get; set; }
        public string outras_taxas { get; set; }
        public string comissao { get; set; }
        public string taxa_comissao_leiloeiro { get; set; }
        public string valor_pago { get; set; }
        public string sacado_nome { get; set; }
        public string sacado_cpfcnpj { get; set; }
        public string taxa_bancaria { get; set; }
    }
}
