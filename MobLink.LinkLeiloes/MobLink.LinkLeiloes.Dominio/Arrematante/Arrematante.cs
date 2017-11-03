namespace MobLink.LinkLeiloes.Dominio
{
    //public class arrematante_recebimento
    //{
    //    public string id_boleto { get; set; }
    //    public string boleto_vencimento { get; set; }
    //    public string valor_cobrado { get; set; }
    //    public string boleto_banco { get; set; }
    //    public string data_documento { get; set; }
    //    public string data_credito_boleto { get; set; }
    //    public string leilao { get; set; }
    //    public string lote_leilao { get; set; }
    //    public string placa { get; set; }
    //    public string chassi { get; set; }
    //    public string marca_modelo { get; set; }
    //    public string cor { get; set; }
    //    public string tipo_veiculo { get; set; }
    //    public string id_status_lote { get; set; }
    //    public string valor_avaliacao { get; set; }
    //    public string valor_arrematacao { get; set; }
    //    public string taxa_administrativa { get; set; }
    //    public string outras_taxas { get; set; }
    //    public string comissao { get; set; }
    //    public string taxa_comissao_leiloeiro { get; set; }
    //    public string taxa_bancaria { get; set; }
    //    public string valor_pago { get; set; }
    //    public string sacado_cpfCnpj { get; set; }
    //    public string sacado_nome { get; set; }
    //}



    public class Arrematante
    {
        public int id { get; set; }

        public int? id_grv { get; set; }
        public string avaliacao { get; set; }
        public string arrematacao { get; set; }
        public string descontos { get; set; }
        public string taxa_administrativa { get; set; }
        public string outras_taxas { get; set; }
        public string tarifa_bancaria { get; set; }
        public string valor_total { get; set; }
        public string valor_pago { get; set; }
        public string iss { get; set; }
        public string comissao { get; set; }
        public string lote { get; set; }
        public string leilao { get; set; }
        public string placa { get; set; }
        public string chassi { get; set; }
        public string nome_arrematante { get; set; }
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public string fone_1 { get; set; }
        public string fone_2 { get; set; }
        public string email { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public string numero_boleto { get; set; }
        public string linha_digitavel { get; set; }
        public string identificacao_banco { get; set; }
        public string identificacao_agencia { get; set; }
        public string identificacao_conta_corrente { get; set; }
        public string data_emissao_boleto { get; set; }
        public string data_vencimento_boleto { get; set; }
        public string data_pagamento_boleto { get; set; }
        public string status_boleto { get; set; }
        public string numero_processo { get; set; }
        public string descricao_status { get; set; }
        public string cartela { get; set; }

        public string status_cadastro_cliente_sap { get; set; }
        public string status_cadastro_fb70_sap { get; set; }
        public string id_documento_cliente_sap { get; set; }
        public string id_documento_fb70_sap { get; set; }
    }
}
