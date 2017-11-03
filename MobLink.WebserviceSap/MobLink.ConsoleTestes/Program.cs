using MobLink.Framework;
using MobLink.WSSap.Dominio;
using System;

namespace MobLink.ConsoleTestes
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.Exit(0);

            MobLink.WSSap.WebService.WSSapLinkPatios ws = new WSSap.WebService.WSSapLinkPatios();

            OrdemInternaFB70 ordemInternaFB70 = new OrdemInternaFB70()
            {
                codigo_banco = "BR04",
                codigo_cliente = "0000139583",
                codigo_empresa = "1080",
                condicao_pagamento = "B001",
                data_fatura = "30082017",
                data_pagamento = "30082017",
                forma_pagamento = "D",
                identificacao_leilao_patio_lote = "1",
                 

                /*
                 * <WRBTR> = <WRBTR_LOTE> + <WRBTR_TAXA> + <WRBTR_COM> + <WRBTR_TARIF> - <WRBTR_DESC>​
                   Caso o valor pago seja diferente do valor registrado no boleto, a diferença deverá ser informada no campo <WRBTR_PAG_MAIOR>.
                 */

                /*
                 * <montante_bruto> = <valor_lote> + <valor_taxa_administrativa> + <valor_comissao> + <valor_tarifa_bancaria> - <valor_desconto>​
                   Caso o valor pago seja diferente do valor registrado no boleto, a diferença deverá ser informada no campo <WRBTR_PAG_MAIOR>.
                 */

                //WRBTR
                montante_bruto = Convert.ToDecimal(110),
                //WRBTR_LOTE
                valor_lote = Convert.ToDecimal(50),
                //WRBTR_TAXA
                valor_taxa_administrativa = Convert.ToDecimal(25),
                //WRBTR_COM
                valor_comissao = Convert.ToDecimal(25),
                //WRBTR_TARIF
                valor_tarifa_bancaria = Convert.ToDecimal(10),
                //WRBTR_DESC
                valor_desconto = Convert.ToDecimal(0.000),

                numero_boleto_pagamento = "1",
                numero_ordem_interna= "500280",
                transacao="9371",                
                valor_pagamento_maior = Convert.ToDecimal(0),
                valor_total_pago= Convert.ToDecimal(110),
                opcao_valor_desconto=false,
                opcao_valor_pagamento_maior=false,
                opcao_valor_tarifa_bancaria=true
            };

            var ret = ws.FB70(ordemInternaFB70);

            OrdemVendaSAP ov = new OrdemVendaSAP();

            ov.itensVenda.Add(new OrdemVendaSAP.ITEMS()
            {
                codigoMaterial = "9000000016",
                quantidade = Convert.ToDecimal("1,0"),
                valorBruto = Convert.ToDecimal("138,5"),
                valorComDesconto = 0,
                tipoDocumento = "YBO1"
            });

            ov.documentosPagamento.Add(new OrdemVendaSAP.DOC_PAGAM()
            {
                meioPagamento = "B",
                numeroDocumento = "100998"
            });

            ov.centro = "";

            ov.codigoCliente = "0000139552";

            ov.codigoPedido = "2600015160005001";

            ov.IDTRANSACAO = -1;

            ov.numeroContrato = "";

            ov.NumeroLeilaoPatioLote = "";

            ov.textoCabecalho = "PROCESSO TESTE - PARAMETRO CENTRO";

            var retorno = ws.CadastrarOrdemVendaSAP(ov);


            //WSUtilitariosLinkPatios.WebCEP WebCEP = new WSUtilitariosLinkPatios.WebCEP() { cep = "24241000" };

            //WSUtilitariosLinkPatios.WSUtilitariosLinkPatios ws = new WSUtilitariosLinkPatios.WSUtilitariosLinkPatios();

            //CredentialCache CredentialCache = new CredentialCache();

            //CredentialCache.Add(new Uri(ws.Url), "Basic", new NetworkCredential("user_ws_sap", "Buracica12#"));

            //ws.Credentials = CredentialCache;

            //var retorno = ws.GetCepFromCorreios(WebCEP);
        }
    }
}
