using System.Collections.Generic;

namespace MobLink.WSSap.Dominio
{
    public class OrdemVendaSAP
    {
        #region Mapeamento dos campos
        /*
                Campos			Descrição							Obrigatoriedade		Tipo de dados	Tamanho
                ===============================================================================================
                IDTRANSACAO		ID da solicitação do DSIN			Sim					INTEIRO	
                AUART			Tipo de documento					Sim					STRING			4
                WERKS			Centro								Sim					STRING			4
                VBELN			Número Contrato						Não					STRING			10
                KUNNR			Código do Cliente					Sim					STRING			10
                BSTNK			Código do Pedido do cliente no DSIN	Sim					STRING			20
                ORDER_TXT		Texto de cabeçalho					Não					STRING			132

                ITEMS			Estrutura de Items	Sim	Estrutura	1...n
	                MATNR			Código do Material				Sim					STRING			18
	                MENGE			Quantidade						Sim					DECIMAL			12,3
	                KBETR			Valor Bruto						Sim					DECIMAL			8,3
	                AUFNR			Código da Ordem					Não					STRING			12

                DOC_PAGAM		Estrutura para Documentos de Pagamento	Não	Estrutura	1...n
	                MEIO_PAG		Meio de Pagamento ( I = Deposito ; D = Boleto )	Não		STRING		1
	                DOCUMENTO		Número do documento de pagamento				Não		STRING		100
        */
        #endregion

        public OrdemVendaSAP()
        {
            itensVenda = new List<ITEMS>();
            documentosPagamento = new List<DOC_PAGAM>();
        }

        public struct ITEMS
        {
            /// <summary>
            /// Código do Material ::: SAP=MATNR
            /// </summary>
            public string codigoMaterial { get; set; }
            /// <summary>
            /// Quantidade ::: SAP=MENGE
            /// </summary>
            public decimal quantidade { get; set; }
            /// <summary>
            /// Valor Bruto ::: SAP=KBETR
            /// </summary>
            public decimal valorBruto { get; set; }
            /// <summary>
            /// Valor com desconto ::: SAP=KBRTR_DESC
            /// </summary>
            public decimal valorComDesconto { get; set; }
            /// <summary>
            /// Código da Ordem ::: SAP=AUFNR
            /// </summary>
            public string codigoOrdem { get; set; }

            //a pedido do beto
            /// <summary>
            /// Tipo de documento ::: SAP=AUART
            /// </summary>
            public string tipoDocumento { get; set; }

            /// <summary>
            /// Para cada loop em itens, utilizar essa descricao por item
            /// </summary>
            public string textoCabecalho { get; set; }

            public int IdAtendimento { get; set; }

            public string tipoComposicao { get; set; }

            public List<int> ListaIDFaturamentoComposicao { get; set; }

            public int? id_grupo { get; set; }

            public string codigo_material_agrupamento { get; set; }
        }

        //public struct IDFaturamentoComposicao
        //{
        //    public int idFaturamentoComposicao { get; set; }
        //}

        public struct DOC_PAGAM
        {
            /// <summary>
            /// Meio de Pagamento ::: SAP=MEIO_PAG ::: ( I = Deposito ; D = Boleto )
            /// </summary>
            public string meioPagamento { get; set; }
            /// <summary>
            /// Número do documento de pagamento ::: SAP=DOCUMENTO
            /// </summary>
            public string numeroDocumento { get; set; }
        }

        /// <summary>
        /// ID da solicitação do DSIN ::: SAP=IDTRANSACAO
        /// </summary>
        public int IDTRANSACAO { get; set; }

        /// <summary>
        /// Centro ::: SAP=WERKS
        /// </summary>
        public string centro { get; set; }

        /// <summary>
        /// Número Contrato ::: SAP=VBELN
        /// </summary>
        public string numeroContrato { get; set; }

        /// <summary>
        /// Código do Cliente ::: SAP=KUNNR
        /// </summary>
        public string codigoCliente { get; set; }

        /// <summary>
        /// Código do Pedido do cliente no DSIN ::: SAP=BSTNK
        /// </summary>
        public string codigoPedido { get; set; }

        /// <summary>
        /// Texto de cabeçalho ::: SAP=ORDER_TXT
        /// </summary>
        public string textoCabecalho { get; set; }

        /// <summary>
        /// Estrutura de Items ::: SAP=ITEMS
        /// </summary>
        public List<ITEMS> itensVenda { get; set; }

        /// <summary>
        /// Estrutura para Documentos de Pagamento ::: SAP=DOC_PAGAM
        /// </summary>
        public List<DOC_PAGAM> documentosPagamento { get; set; }

        /// <summary>
        /// ZZPROC
        /// </summary>
        public string NumeroProcesso { get; set; }

        /// <summary>
        /// ZZPERI
        /// </summary>
        public string Periodo { get; set; }

        /// <summary>
        /// ZZLEIL
        /// </summary>
        public string NumeroLeilaoPatioLote { get; set; }

        /// <summary>
        /// ZTERM           Condição de Pagamento       Adicionado em 27/09/2017
        /// </summary>
        public string condicao_pagamento { get; set; }
    }
}
