using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Framework.Dominio.Sap
{
    public class OrdemVendaLeilao
    {
        public OrdemVendaLeilao()
        {
            //Cliente = new ClienteSAP();
        }

        /// <summary>
        /// <IDTRANSACAO>
        /// </summary>
        private int Transacao_SAP { get; set; }
        /// <summary>
        /// <BUKRS>
        /// </summary>
        public string CodigoEmpresaMatriz { get; set; }
        ///<KUNNR>
        public string CodigoClienteSAP { get; set; }
        ///<BLDAT>
        public string DataEmissaoBoleto { get; set; }
        ///<WRBTR>
        public decimal ValorTotalBoleto { get; set; }
        ///<BKTXT>
        public string OrdemInternaSSG { get; set; }
        ///<XREF1_HD>
        public string Lote { get; set; }
        ///<ZTERM>
        public string CondicaoPagamento { get; set; }
        ///<WRBTR_LOTE>
        public decimal ValorArrematacao { get; set; }
        ///<WRBTR_TAXA>
        public decimal ValorTaxaAdministrativa { get; set; }
        ///<WRBTR_COM>
        public decimal ValorComissao { get; set; }
        ///<WRBTR_TARIF>
        public decimal ValorTarifaBancaria { get; set; }
        ///<WRBTR_DESC>
        public decimal ValorDesconto { get; set; }
        ///<WRBTR_PAG_MAIOR>
        public decimal DiferencaPagaMaior { get; set; }
        ///<WRBTR_PAG>
        public decimal ValorPago { get; set; }
        ///<HBKID>
        public string Banco { get; set; }
        ///<ZLSCH>
        public string FormaPagamento { get; set; }
        ///<BLDAT_PAG>
        public string DataPagamento { get; set; }
        ///<XREF2_HD>
        public string NumeroBoleto { get; set; }

        //public ClienteSAP Cliente { get; set; }
    }
}
