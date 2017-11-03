using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.LinkLeiloes.Dominio
{
    public class Transacao001
    {
        public string Retorno { get; set; }
        public string AnoFabricacao { get; set; }
        public string AnoModelo { get; set; }
        public string AnoUltimaLicenca { get; set; }
        public string CapacidadeCarga { get; set; }
        public string CapacidadePassageiros { get; set; }
        public string ChassiRemarcado { get; set; }
        public string NumeroRenavam { get; set; }
        public string Chassi { get; set; }
        public string Classificacao { get; set; }
        public string CodigoCategoria { get; set; }
        public string DescricaoCategoria { get; set; }
        public string DescricaoCor { get; set; }
        public string DescricaoMarca { get; set; }
        public string DescricaoTipo { get; set; }
        public string PesoBrutoTotal { get; set; }
        public string Placa { get; set; }
        public string InformacaoRoubo { get; set; }
        public string RestricaoEstelionato { get; set; }
        public string Uf { get; set; }
        public string DiaJuliano { get; set; }
        public string Sequencial { get; set; }
        public string Transacao { get; set; }
        public List<Restricao> RestricoesAdministrativas { get; set; }
        public List<Restricao> RestricoesJuridicas { get; set; }
    }
}
