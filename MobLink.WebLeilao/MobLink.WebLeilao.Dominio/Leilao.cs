using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class Leilao
    {
        public int id { get; set; }

        [Required]
        [Display(Name ="Descrição")]
        public string descricao { get; set; }

        [Display(Name = "Local")]
        public string nome_local { get; set; }

        [Required]
        [Display(Name = "Comitente")]
        public int id_comitente { get; set; }

        [Required]
        [Display(Name = "Expositor")]
        public int id_expositor { get; set; }

        [Required]
        [Display(Name = "Leiloeiro")]
        public int id_leiloeiro { get; set; }

        [Display(Name = "Status")]
        public int id_status { get; set; }

        [Display(Name = "Usuário de cadastro")]
        public int id_usuario_cadastro { get; set; }

        [Display(Name = "CEP")]
        public string cep { get; set; }

        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        [Display(Name = "Nº")]
        public string end_numero { get; set; }

        [Display(Name = "Complemento")]
        public string end_complemento { get; set; }

        [Display(Name = "Bairro")]
        public string bairro { get; set; }

        [Display(Name = "Município")]
        public string municipio { get; set; }

        [Display(Name = "UF")]
        public string uf { get; set; }

        [Required]
        [Display(Name = "Nº D.O.")]
        public string numero_diario_oficial { get; set; }

        [Required]
        [Display(Name = "Data Pub. D.O.")]
        public string data_hora_publicacao_diario_oficial { get; set; }

        [Required]
        [Display(Name = "Data do leilão")]
        public string data_leilao { get; set; }

        [Display(Name = "Hora do leilão")]
        public string hora_leilao { get; set; }

        [Required]
        [Display(Name = "O.I. Matriz")]
        public string ordem_interna_matriz { get; set; }

        [Required]
        [Display(Name = "O.I. Leilão")]
        public string ordem_interna_leilao { get; set; }

        [Required]
        [Display(Name = "Empresa")]
        public int id_empresa { get; set; }

        [Display(Name = "Ident.Leilão Orgão")]
        public string identificacao_leilao_orgao { get; set; }

        [Required]
        [Display(Name = "Regra Prest.Contas")]
        public int id_regra_prestacao_contas { get; set; }

        public string data_agendamento { get; set; }

        public string data_inicio_retirada { get; set; }

        public string data_fim_retirada { get; set; }

        public virtual Comitente comitente { get; set; }

        public virtual List<Lote> lotes { get; set; }

        public virtual List<Despesa> despesas { get; set; }

        public virtual StatusLeilao status { get; set; }

        public virtual string nome_comitente { get; set; }

        public virtual int quantidade_lotes { get; set; }

        public virtual int quantidade_lotes_validos { get; set; }

        public virtual string usuario_cadastro { get; set; }

        public virtual List<Deposito> depositos { get; set; }

        public string situacao_lote { get; set; }

        public int qtd_arrematantes { get; set; }

        public int qtd_despesas { get; set; }

        public int qtd_notificacoes { get; set; }

        public string ativo { get; set; }




        public Leilao()
        {
            comitente = new Comitente();
            lotes = new List<Lote>();
            despesas = new List<Despesa>();
            depositos = new List<Deposito>();
            status = new StatusLeilao();
        }
    }
}
