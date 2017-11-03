using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.LinkLeiloes.Dominio
{
    public class Proprietario
    {
        public int id { get; set; }
        public int id_lote { get; set; }
        public string cep_endereco_proprietario { get; set; }
        public string cep_comunicado_venda { get; set; }
        public string cep_financiamento_efet { get; set; }
        public string bairro_comunicado_venda { get; set; }
        public string bairro_proprietario { get; set; }
        public string bairro_financiamento_efet { get; set; }
        public string uf_financiamento_efet { get; set; }
        public string uf_proprietario { get; set; }
        public string uf_comunicado_venda { get; set; }
        public string endereco_comunicado_venda { get; set; }
        public string endereco_proprietario { get; set; }
        public string endereco_financiamento_efet { get; set; }
        public string municipio_comunicado_venda { get; set; }
        public string municipio_financiamento_efet { get; set; }
        public string descricao_municipio_endereco { get; set; }
        public string retorno { get; set; }
        public string chassi { get; set; }
        public string chassi_remarcado { get; set; }
        public string placa { get; set; }
        public string placa_anterior { get; set; }
        public string placa_nova { get; set; }
        public string ano_fabricacao { get; set; }
        public string ano_modelo { get; set; }
        public string renavam { get; set; }
        public string descricao_cor { get; set; }
        public string descricao_marca { get; set; }
        public string descricao_tipo { get; set; }
        public string descricao_categoria { get; set; }
        public string descricao_combustivel { get; set; }
        public string descricao_especie { get; set; }
        public string capacidade_passageiros { get; set; }
        public string capacidade_carga { get; set; }
        public string data_limite_restricao { get; set; }
        public string peso_bruto_total { get; set; }
        public string descricao_serie { get; set; }
        public string numero_motor { get; set; }
        public string dia_juliano { get; set; }
        public string sequencial { get; set; }
        public string transacao { get; set; }
        public string indicacao_multas_renainf { get; set; }
        public string indicacao_divida_ativa { get; set; }
        public string indicacao_veiculo_baixado { get; set; }
        public string indicacao_roubo_furto { get; set; }
        public string indicacao_financiamento { get; set; }
        public string tipo_documento { get; set; }
        public string numero_cpf_cnpj { get; set; }
        public string cpf_cnpj_comunicado_venda { get; set; }
        public string descricao_municipio_emplacamento { get; set; }
        public string nome_proprietario { get; set; }
        public string nome_proprietario_anterior { get; set; }
        public string nome_comunicado_venda { get; set; }
        public string nome_financiamento_efet { get; set; }
        public string nome_financiado_sng { get; set; }
        public string numero_endereco_proprietario { get; set; }
        public string complemento_endereco_proprietario { get; set; }
        public string tipo_documento_comunicado_venda { get; set; }
        public string numero_comunicado_venda { get; set; }
        public string complemento_comunicado_venda { get; set; }
        public string data_venda_comunicado_venda { get; set; }
        public string data_financiado_sng { get; set; }
        public string hora_financiado_sng { get; set; }
        public string data_financiamento_efet { get; set; }
        public string hora_financiamento_efet { get; set; }
        public string tipo_documento_financiamento_efet { get; set; }
        public string cpf_cnpj_financiamento_efet { get; set; }
        public string cpf_cnpj_financiado_sng { get; set; }
        public string cpf_cnpj_agente_financeiro { get; set; }
        public string numero_financiamento_efet { get; set; }
        public string complemento_financiamento_efet { get; set; }
        public string tipo_documento_financiado_sng { get; set; }
        public string tipo_documento_agente_financeiro { get; set; }
        public string nome_agente_financeiro { get; set; }
        public string observacoes { get; set; }
        public string flag_notificar_proprietario { get; set; }
        public string flag_notificar_financeira { get; set; }
        public string flag_notificar_comunicado { get; set; }
        public string flag_normalizado { get; set; }
    }
}
