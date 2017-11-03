using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportarExcel
{
    public class NAO_CADASTRADOS_DETRAN_RJ
    {
        public int ID { get; set; }
        public string PLACA { get; set; }
        public string CHASSI { get; set; }
        public string UF { get; set; }
        public string CD_RENAVAM { get; set; }
        public string NU_MOTOR { get; set; }
        public string SITUACAO_VEICULO { get; set; }
        public string CD_TIPO { get; set; }
        public string DESC_TIPO { get; set; }
        public string COD_MARCA_MODELO { get; set; }
        public string DESC_MARCA_MODELO { get; set; }
        public string COD_COR { get; set; }
        public string DESC_COR { get; set; }
        public string ANO_MODELO { get; set; }
        public string ANO_FABRICACAO { get; set; }
        public string CMT_VEICULO { get; set; }
        public string PBT_VEICULO { get; set; }
        public string CAPAC_CARGA { get; set; }
        public string NOME_PROCED_VEICULO { get; set; }
        public string MENSAGEM { get; set; }
        public string NOME_AGENTE { get; set; }
        public string CGC_AGENTE { get; set; }
        public string NOME_FINANCIADO { get; set; }
        public string CPF_CGC_FINANCIADO { get; set; }
        public string UF_DESTINO_FATURAMENTO { get; set; }
    }
}
