using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.LinkLeiloes.Dominio
{
    public class Lote
    {
        public int id { get; set; }
        public int id_leilao { get; set; }
        public int numero_lote { get; set; }
        public string placa { get; set; }
        public string chassi { get; set; }
        public string nome_arquivo_importacao { get; set; }
        public string numero_formulario_grv { get; set; }
        public int id_status_lote { get; set; }
        public string marca_modelo { get; set; }
        public string cor { get; set; }
        public string cor_ostentada { get; set; }
        public string tipo_veiculo { get; set; }
        public string municipio { get; set; }

        [MaxLength(50, ErrorMessage ="Campo deve conter no máximo 50 caracteres")]
        public string localizacao { get; set; }

        public string responsavel_remocao { get; set; }
        public string patio { get; set; }
        public string observacoes { get; set; }
        public string reboque { get; set; }
        public DateTime? data_hora_apreensao { get; set; }
        public string situacao_lote { get; set; }
        public string situacao_placa { get; set; }
        public string situacao_chassi { get; set; }
        public string situacao_GNV { get; set; }
        public string situacao_veiculo { get; set; }

        public string tipo_combustivel { get; set; }
        public string procedencia_veiculo { get; set; }
        public string chave { get; set; }

        public string numero_motor { get; set; }
        public string cambio { get; set; }
        public string ar_condicionado { get; set; }
        public string direcao { get; set; }
        public string vidro_eletrico { get; set; }
        public string trava_eletrica { get; set; }

        public string flag_normalizado { get; set; }
        public string status_pericia { get; set; }
        public string renavan { get; set; }
        public string ano_fabricacao { get; set; }
        public string ano_modelo { get; set; }

        public string valor_avaliacao { get; set; }
        public string lance_minimo { get; set; }

        public int? quilometragem { get; set; }

        public int? situacao_veiculo_pericia { get; set; }

        public string uf { get; set; }

        public string Conferido_Patio { get; set; }

        public string Flag_Transacao { get; set; }

        public string Flag_Agendado { get; set; }

        public string obs_transacao { get; set; }

        public int? Log_Recolhimento { get; set; }

        public string informacao_roubo { get; set; }

        public string restricao_estelionato { get; set; }

        public virtual Leilao Leilao { get; set; }
        
        public virtual string nome_leilao { get; set; }
        
        public virtual DateTime? data_leilao { get; set; }
        
        public virtual string _placa { get; set; }
        
        public virtual string _chassi { get; set; }
        
        public virtual string _marca_modelo { get; set; }
        
        public virtual string _cor { get; set; }
        
        public virtual string _tipo_veiculo { get; set; }
        
        public virtual string descricao_status { get; set; }

        public virtual List<FotoRecolhimento> ListaFotosRecolhimento { get; set; }

        public virtual List<Restricao> Restricoes { get; set; }
        public virtual List<Transacao> Transacoes { get; set; }

        public virtual Transacao005 Proprietario { get; set; }

        public int qtd_fotos { get; set; }

        public int qtd_restricoes { get; set; }

        public int qtd_despesas { get; set; }

        public int? id_grv { get; set; }

        public Lote()
        {
            Leilao = new Leilao();
            ListaFotosRecolhimento = new List<FotoRecolhimento>();
            Restricoes = new List<Restricao>();
            Proprietario = new Transacao005();
            Transacoes = new List<Transacao>();
        }
    }
}
