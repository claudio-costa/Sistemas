namespace MobLink.WSSap.Dominio
{
    public class OrdemInternaFB70 //fb70
    {
        public string numero_ordem_interna { get; set; }
        public string data_fatura { get; set; }
        public string data_pagamento { get; set; }
        public string codigo_empresa { get; set; }
        public string codigo_banco { get; set; }
        public string transacao { get; set; }
        public string codigo_cliente { get; set; }
        public decimal montante_bruto { get; set; }
        public decimal valor_comissao { get; set; }
        public decimal valor_desconto { get; set; }
        public bool opcao_valor_desconto { get; set; }
        public decimal valor_lote { get; set; }
        public decimal valor_total_pago { get; set; }
        public decimal valor_pagamento_maior { get; set; }
        public bool opcao_valor_pagamento_maior { get; set; }
        public decimal valor_tarifa_bancaria { get; set; }
        public bool opcao_valor_tarifa_bancaria { get; set; }
        public decimal valor_taxa_administrativa { get; set; }
        public string identificacao_leilao_patio_lote { get; set; }
        public string numero_boleto_pagamento { get; set; }
        public string forma_pagamento { get; set; }
        public string condicao_pagamento { get; set; }
    }
}
