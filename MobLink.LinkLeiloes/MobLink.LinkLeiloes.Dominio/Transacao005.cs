using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class Transacao005
    {
        public int Id { get; set; }

        public int Id_lote { get; set; }

        public string Retorno { get; set; }
        
        public string Chassi { get; set; }
        public string ChassiRemarcado { get; set; }

        public string Placa { get; set; }
        public string PlacaAnterior { get; set; }
        public string PlacaNova { get; set; }

        public string AnoFabricacao { get; set; }
        public string AnoModelo { get; set; }

        public string Renavam { get; set; }

        public string TipoDocumento { get; set; }
        public string NumeroCpfCnpj { get; set; }

        public string DescricaoCor { get; set; }
        public string DescricaoMarca { get; set; }
        public string DescricaoTipo { get; set; }
        public string DescricaoCarroceria { get; set; }
        public string DescricaoCategoria { get; set; }
        public string DescricaoCombustivel { get; set; }
        public string DescricaoEspecie { get; set; }
        public string DescricaoMunicipioEndereco { get; set; }
        public string DescricaoMunicipioEmplacamento { get; set; }

        public string CapacidadePassageiros { get; set; }
        public string CapacidadeCarga { get; set; }
        public string DataLimiteRestricao { get; set; }
        
        public string PesoBrutoTotal { get; set; }
        public string DescricaoSerie { get; set; }
        public string NumeroMotor { get; set; }
        public string DiaJuliano { get; set; }
        public string Sequencial { get; set; }
        public string Transacao { get; set; }

        public string IndicacaoMultasRenainf { get; set; }
        public string IndicacaoDividaAtiva { get; set; }
        public string IndicacaoVeiculoBaixado { get; set; }
        public string IndicacaoRouboFurto { get; set; }

        public string Observacoes { get; set; }

        public string CepEnderecoProprietario { get; set; }
        public string NomeProprietario { get; set; }
        public string EnderecoProprietario { get; set; }
        public string NumeroEnderecoProprietario { get; set; }
        public string ComplementoEnderecoProprietario { get; set; }
        public string NomeProprietarioAnterior { get; set; }

        public string TipoDocumentoComunicadoVenda { get; set; }
        public string NomeComunicacaoVenda { get; set; }
        public string EnderecoComunicadoVenda { get; set; }
        public string NumeroComunicadoVenda { get; set; }
        public string ComplementoComunicadoVenda { get; set; }
        public string BairroComunicadoVenda { get; set; }
        public string MunicipioComunicadoVenda { get; set; }
        public string UfComunicadoVenda { get; set; }
        public string CpfCnpjComunicadoVenda { get; set; }
        public string CepComunicadoVenda { get; set; }
        public string DataVendaComunicadoVenda { get; set; }

        public string TipoDocumentoFinanciamentoEfet { get; set; }
        public string CpfCnpjFinanciamentoEfet { get; set; }
        public string CepFinanciamentoEfet { get; set; }
        public string DataFinanciamentoEfet { get; set; }
        public string HoraFinanciamentoEfet { get; set; }
        public string NomeFinanciamentoEfet { get; set; }
        public string EnderecoFinanciamentoEfet { get; set; }
        public string NumeroFinanciamentoEfet { get; set; }
        public string ComplementoFinanciamentoEfet { get; set; }
        public string MunicipioFinanciamentoEfet { get; set; }

        public string TipoDocumentoFinanciadoSng { get; set; }
        public string CpfCnpjFinanciadoSng { get; set; }
        public string DataFinanciadoSng { get; set; }
        public string HoraFinanciadoSng { get; set; }
        public string NomeFinanciadoSng { get; set; }

        public string TipoDocumentoAgenteFinanceiro { get; set; }
        public string CpfCnpjAgeteFinanceiro { get; set; }
        public string NomeAgenteFinanceiro { get; set; }

        public string IndicacaoFinanciamento { get; set; }

        public string BairroProprietario { get; set; }
        public string UfProprietario { get; set; }

        public string BairroFinanciamentoEfet { get; set; }
        public string UfFinanciamentoEfet { get; set; }
        

        public string[] DescricaoRestricoes { get; set; }
        public string[] ObservacaoRestricoes { get; set; }
        public string[] DescricaoSubRestricoes { get; set; }

        public string flag_notificar_proprietario { get; set; }
        public string flag_notificar_financeira { get; set; }
        public string flag_notificar_comunicado { get; set; }
    }
}
