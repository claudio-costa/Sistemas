using MobLink.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MobLink.WSSap.Repositorio
{
    public class OrdemVendaRepositorio___ : SapRepositorio___
    {
        //ORDENS DE VENDA
        protected si_ordemvenda_requestService.si_ordemvenda_requestService SERVICO_SAP_ORDEM_VENDA;
        protected si_ordemvenda_requestService.dt_ordemvenda_request OBJETO_SAP_ORDEM_VENDA;
        protected List<si_ordemvenda_requestService.dt_ordemvenda_requestITEMS> OBJETO_SAP_ORDEM_VENDA_ITENS;
        protected List<si_ordemvenda_requestService.dt_ordemvenda_requestDOC_PAGAM> OBJETO_SAP_ORDEM_VENDA_PAGTO;

        public OrdemVendaRepositorio___()
        {
            CredentialCache Credenciais;

            SERVICO_SAP_ORDEM_VENDA = new si_ordemvenda_requestService.si_ordemvenda_requestService();

            Credenciais = new CredentialCache
            {
                {
                    new Uri(SERVICO_SAP_ORDEM_VENDA.Url), "Basic",
                    new NetworkCredential()
                    {
                        UserName = Framework.Setup.WebServices.WsSap(Ambiente).Usuario,
                        Password = Framework.Setup.WebServices.WsSap(Ambiente).Senha
                    }
                }
            };

            SERVICO_SAP_ORDEM_VENDA.Credentials = Credenciais;
            SERVICO_SAP_ORDEM_VENDA.PreAuthenticate = true;

            OBJETO_SAP_ORDEM_VENDA = new si_ordemvenda_requestService.dt_ordemvenda_request();
            OBJETO_SAP_ORDEM_VENDA_ITENS = new List<si_ordemvenda_requestService.dt_ordemvenda_requestITEMS>();
            OBJETO_SAP_ORDEM_VENDA_PAGTO = new List<si_ordemvenda_requestService.dt_ordemvenda_requestDOC_PAGAM>();
        }

        private void GravaFaturamentoComposicao(Dominio.OrdemVendaSAP OrdemVenda)
        {
            try
            {
                foreach (var item in OrdemVenda.itensVenda)
                {
                    StringBuilder SQL = new StringBuilder();

                    #region Insert statement
                    SQL.AppendLine("INSERT INTO dbo.tb_sap_faturamento_composicao");
                    SQL.AppendLine("      (id_transacao_sap");
                    SQL.AppendLine("      ,id_atendimento");
                    SQL.AppendLine("      ,centro");
                    SQL.AppendLine("      ,numero_contrato");
                    SQL.AppendLine("      ,codigo_cliente");
                    SQL.AppendLine("      ,codigo_pedido");
                    SQL.AppendLine("      ,texto_cabecalho");
                    SQL.AppendLine("      ,numero_processo");
                    SQL.AppendLine("      ,periodo");
                    SQL.AppendLine("      ,numero_leilao_patio_lote");
                    SQL.AppendLine("      ,codigo_material");
                    SQL.AppendLine("      ,quantidade");
                    SQL.AppendLine("      ,valor_bruto");
                    SQL.AppendLine("      ,valor_desconto");
                    SQL.AppendLine("      ,codigo_ordem");
                    SQL.AppendLine("      ,tipo_documento");
                    SQL.AppendLine("      ,meio_pagamento");
                    SQL.AppendLine("      ,numero_documento, tipo_composicao)");

                    SQL.AppendLine("VALUES");

                    SQL.AppendLine("      (" + OrdemVenda.IDTRANSACAO);
                    SQL.AppendLine("      ," + item.IdAtendimento);
                    SQL.AppendLine("      ,'" + OrdemVenda.centro + "'");
                    SQL.AppendLine("      ,'" + OrdemVenda.numeroContrato + "'");
                    SQL.AppendLine("      ,'" + OrdemVenda.codigoCliente + "'");
                    SQL.AppendLine("      ,'" + OrdemVenda.codigoPedido + "'");
                    SQL.AppendLine("      ,'" + item.textoCabecalho + "'");
                    SQL.AppendLine("      ,'" + OrdemVenda.NumeroProcesso + "'");
                    SQL.AppendLine("      ,'" + OrdemVenda.Periodo + "'");
                    SQL.AppendLine("      ,'" + OrdemVenda.NumeroLeilaoPatioLote + "'");
                    SQL.AppendLine("      ,'" + item.codigoMaterial + "'");

                    //System.Globalization.NumberFormatInfo info = new System.Globalization.NumberFormatInfo();
                    //info.NumberDecimalSeparator = ",";
                    //info.NumberGroupSeparator = ".";
                    //double d = Convert.ToDouble(item.quantidade, info);
                    //SQL.AppendLine("      ," + d.ToString().Replace(",", "."));

                    var qtde = Decimal.Round(item.quantidade, 4).ToString().Replace(",", ".");

                    SQL.AppendLine("      ," + qtde);
                    SQL.AppendLine("      ," + item.valorBruto.ToString().Replace(",", "."));
                    SQL.AppendLine("      ," + item.valorComDesconto.ToString().Replace(",", "."));
                    SQL.AppendLine("      ,'" + item.codigoOrdem + "'");
                    SQL.AppendLine("      ,'" + item.tipoDocumento + "'");
                    //DOC_PAGAM
                    SQL.AppendLine("      ,'" + OrdemVenda.documentosPagamento[0].meioPagamento + "'");
                    SQL.AppendLine("      ,'" + OrdemVenda.documentosPagamento[0].numeroDocumento + "'");
                    SQL.AppendLine("      ,'" + item.tipoComposicao + "')");
                    #endregion

                    string novoID;

                    //GERAR TB_SAP_FATURAMENTO_COMPOSICAO
                    novoID = ExecutaSQL_ScopeIdentity(SQL.ToString()).ToString();

                    //GERAR TB_SAP_FATURAMENTO_COMPOSICAO_ITENS
                    var SQL_INS = new StringBuilder();
                    SQL_INS.AppendLine("INSERT INTO tb_sap_faturamento_composicao_itens(id_sap_faturamento_composicao, id_faturamento_composicao)");
                    SQL_INS.AppendFormat("VALUES({0}, {1})", novoID, item.ListaIDFaturamentoComposicao[0]);
                    ExecutaSQL(SQL_INS.ToString());
                }
            }
            catch (Exception e)
            {
                Log(e, "GravaFaturamentoComposicao");
                throw e;
            }
            finally
            {

            }
        }

        private void ChamarTransacaoSAP(Dominio.OrdemVendaSAP OrdemVenda)
        {
            SERVICO_SAP_ORDEM_VENDA = new si_ordemvenda_requestService.si_ordemvenda_requestService();

            CookieContainer _cookie = new CookieContainer();
            SERVICO_SAP_ORDEM_VENDA.CookieContainer = _cookie;

            CredentialCache credentialCache = new CredentialCache();

            var p = Framework.Setup.WebServices.WsSap(this.Ambiente);
            credentialCache.Add(new Uri(SERVICO_SAP_ORDEM_VENDA.Url), "Basic", new NetworkCredential(p.Usuario, p.Senha));

            SERVICO_SAP_ORDEM_VENDA.Credentials = credentialCache;
            SERVICO_SAP_ORDEM_VENDA.PreAuthenticate = true;

            //ORDEM DE VENDA
            OBJETO_SAP_ORDEM_VENDA = new si_ordemvenda_requestService.dt_ordemvenda_request()
            {
                IDTRANSACAO = GetTransacaoSap(),
                BSTNK = OrdemVenda.codigoPedido,
                KUNNR = OrdemVenda.codigoCliente,
                VBELN = OrdemVenda.numeroContrato,
                WERKS = OrdemVenda.centro,
                ZZPROC = OrdemVenda.NumeroProcesso,
                ZZPERI = OrdemVenda.Periodo,
                ZZLEIL = OrdemVenda.NumeroLeilaoPatioLote,
                ORDER_TXT = OrdemVenda.itensVenda[0].textoCabecalho,
                AUART = OrdemVenda.itensVenda[0].tipoDocumento
            };

            OrdemVenda.IDTRANSACAO = int.Parse(OBJETO_SAP_ORDEM_VENDA.IDTRANSACAO);

            //ITENS DA ORDEM
            //List<OBJETO_SAP_ORDEM_VENDA_ITENS> MEUS_ITENS_SAP = new List<SAP_OrdemVenda.dt_ordemvenda_requestITEMS>();

            OBJETO_SAP_ORDEM_VENDA_ITENS.Clear();
            foreach (var item in OrdemVenda.itensVenda)
            {
                OBJETO_SAP_ORDEM_VENDA_ITENS.Add(new si_ordemvenda_requestService.dt_ordemvenda_requestITEMS()
                {
                    KBETR = item.valorBruto,
                    MATNR = item.codigoMaterial,
                    MENGE = Decimal.Round(item.quantidade, 4),
                    KBRTR_DESCSpecified = true,
                    KBRTR_DESC = item.valorComDesconto
                });
            }

            //FORMA DE PAGAMENTO DA ORDEM
            //List<> FORMA_PAGTO_SAP = new List<SAP_OrdemVenda.dt_ordemvenda_requestDOC_PAGAM>();

            //22/02/2017 - MANDAR APENAS UMA FORMA DE PAGAMENTO POR NOTA
            //foreach (var item in OrdemVendaLinkPatios.documentosPagamento)
            //{
            //    FORMA_PAGTO_SAP.Add(new SAP_OrdemVenda.dt_ordemvenda_requestDOC_PAGAM()
            //    {
            //        DOCUMENTO = item.numeroDocumento,
            //        MEIO_PAG = item.meioPagamento
            //    });
            //}

            OBJETO_SAP_ORDEM_VENDA_PAGTO.Clear();
            OBJETO_SAP_ORDEM_VENDA_PAGTO.Add(new si_ordemvenda_requestService.dt_ordemvenda_requestDOC_PAGAM()
            {
                DOCUMENTO = OrdemVenda.documentosPagamento[0].numeroDocumento,
                MEIO_PAG = OrdemVenda.documentosPagamento[0].meioPagamento
            });

            OBJETO_SAP_ORDEM_VENDA.ITEMS = OBJETO_SAP_ORDEM_VENDA_ITENS.ToArray();
            OBJETO_SAP_ORDEM_VENDA.DOC_PAGAM = OBJETO_SAP_ORDEM_VENDA_PAGTO.ToArray();

            try
            {
                this.InserirSolicitacao(OBJETO_SAP_ORDEM_VENDA.IDTRANSACAO, "ORDEM DE VENDA", id_atendimento: OrdemVenda.itensVenda.FirstOrDefault().IdAtendimento);
                GravaFaturamentoComposicao(OrdemVenda);
                SERVICO_SAP_ORDEM_VENDA.si_ordemvenda_request(OBJETO_SAP_ORDEM_VENDA);
            }
            catch (Exception e)
            {
                Log(e, "ChamarTransacaoSAP");
                throw e;
            }
            finally
            {
                //TODO: ATUALIZAR A TABELA tb_sap_solicitacao COM A TRANSAÇÃO SAP
                //InserirSolicitacao(OBJETO_SAP_ORDEM_VENDA.IDTRANSACAO, "ORDEM DE VENDA");
            }
        }

        public Dominio.Retorno IngressaOrdemVendaSAP(Dominio.OrdemVendaSAP OrdemVenda)
        {
            //CASO O ORDEM NÃO TENHA ITENS, NÃO PROSSEGUIR COM A GERAÇÃO DA NOTA PARA O SAP
            if (OrdemVenda.itensVenda.Count <= 0)
            {
                return new Dominio.Retorno() { Mensagem = "NENHUM ITEM NA ESTRUTURA DE ITENS", Resultado = false };
            }

            //REMOVER ITENS COM VALOR 0 OU QUE O VALOR TOTAL SEJA IGUAL AO DESCONTO
            foreach (var item in OrdemVenda.itensVenda)
            {
                if (item.valorBruto == 0 || Math.Round((item.valorBruto * item.quantidade)) == Math.Round(item.valorComDesconto))
                {
                    OrdemVenda.itensVenda.Remove(item);
                }
            }

            try
            {
                //ATRIBUIÇÃO DOS IDS DE FATURAMENTO COMPOSIÇÃO INDIVIDUAIS POR ITEM
                var Contador = OrdemVenda.itensVenda[0].ListaIDFaturamentoComposicao.Count;
                for (int i = 0; i <= Contador - 1; i++)
                {
                    var id = OrdemVenda.itensVenda[i].ListaIDFaturamentoComposicao[i];
                    OrdemVenda.itensVenda[i].ListaIDFaturamentoComposicao.Clear();
                    OrdemVenda.itensVenda[i].ListaIDFaturamentoComposicao.Add(id);
                }

                //NOVA REGRA DE AGRUPAMENTO
                bool cliente_possui_agrupamento = true; //TODO: SUBSTITUIR POR PARAMETRIZAÇÃO QDO HOUVERAM NOVOS CLIENTES

                if (cliente_possui_agrupamento)
                {

                    var lista_notas = new List<Dominio.OrdemVendaSAP.ITEMS>();

                    foreach (var item in OrdemVenda.itensVenda)
                    {
                        Dominio.OrdemVendaSAP.ITEMS iTEMS = new Dominio.OrdemVendaSAP.ITEMS();

                        iTEMS.codigoMaterial = item.codigoMaterial;
                        iTEMS.codigoOrdem = item.codigoOrdem;
                        iTEMS.IdAtendimento = item.IdAtendimento;
                        iTEMS.ListaIDFaturamentoComposicao = item.ListaIDFaturamentoComposicao;
                        iTEMS.quantidade = item.quantidade;
                        iTEMS.textoCabecalho = item.textoCabecalho;
                        iTEMS.tipoComposicao = item.tipoComposicao;
                        iTEMS.valorBruto = item.valorBruto;
                        iTEMS.valorComDesconto = item.valorComDesconto;

                        iTEMS.id_grupo = DPRepositorio.CapturaGrupo(item.codigoMaterial.ToString()).ToInt32();
                        iTEMS.codigo_material_agrupamento = DPRepositorio.CapturaMaterialAgrupamento(iTEMS.id_grupo.Value);
                        iTEMS.tipoDocumento = DPRepositorio.CapturaTipoDocumentoAgrupamento(iTEMS.id_grupo.Value);

                        lista_notas.Add(iTEMS);
                    };

                    //EXCLUIR MATERIAIS DA LISTA QUE NAO POSSUEM AGRUPAMENTO NEM GRUPO CADASTRADO
                    //var Itens_validos = lista_notas.Where(x => x.id_grupo > 0).ToList();

                    lista_notas = lista_notas.Where(x => x.id_grupo > 0).ToList();

                    if (lista_notas.Count > 0)
                    {
                        //SELECIONO OS GRUPOS DISPONIVEIS
                        var grupos = DPRepositorio.SelecionaGrupos();

                        foreach (var grupo in grupos)
                        {
                            var captura = lista_notas.Where(x => x.id_grupo == grupo.id_sap_tipo_composicao_grupos).ToList();

                            Dominio.OrdemVendaSAP.ITEMS nota = new Dominio.OrdemVendaSAP.ITEMS();

                            nota.tipoComposicao = captura.FirstOrDefault().tipoComposicao;
                            nota.tipoDocumento = captura.FirstOrDefault().tipoDocumento;
                            nota.textoCabecalho = captura.FirstOrDefault().textoCabecalho;


                            decimal total = 0;
                            decimal total_descontos = 0;

                            foreach (var item2 in captura)
                            {
                                total = total + (item2.valorBruto * item2.quantidade);
                                total_descontos = total_descontos + item2.valorComDesconto;
                            }

                            //MONTAGEM DA NOTA FINAL

                            nota.codigoMaterial = DPRepositorio.CapturaMaterialAgrupamento(grupo.id_sap_tipo_composicao_grupos);
                            nota.valorBruto = total;
                            nota.quantidade = 1;
                            nota.valorComDesconto = total_descontos;

                            OrdemVenda.itensVenda.Clear();
                            OrdemVenda.itensVenda.Add(nota);
                            ChamarTransacaoSAP(OrdemVenda);
                        }


                    }
                    else
                    {
                        return new Dominio.Retorno() { Resultado = false, Mensagem = "NENHUM AGRUPAMENTO CONFIGURADO" };
                    }

                }






                //PARA CADA:
                // ATRIBUIR O CÓDIGO DO MATERIAL DE AGRUPAMENTO
                // SOMAR VALORES
                // SOMAR QUANTIDADES




                //var codigos = OrdemVenda.itensVenda.Select(x => "'" + x.codigoMaterial + "'").ToList();

                //string novasCores = String.Join(",", codigos.ToArray());


                //AGRUPAR POR TIPO DE DOCUMENTO
                //var ItensYBO1 = OrdemVenda.itensVenda.Where(p => p.tipoDocumento == "YBO1").ToList();
                //var ItensYBO2 = OrdemVenda.itensVenda.Where(p => p.tipoDocumento == "YBO2").ToList();

                //if (ItensYBO1.Count > 0)
                //{
                //    var ItensYBO1_A = ItensYBO1.Where(x => new DPRepositorio().CapturaIndicadorAgrupamento(x.codigoMaterial) == "S").ToList();

                //    if (ItensYBO1_A.Count > 0)
                //    {
                //        OrdemVenda.itensVenda = ItensYBO1_A;
                //        ChamarTransacaoSAP(OrdemVenda);
                //    }

                //    var ItensYBO1_D = ItensYBO1.Where(x => new DPRepositorio().CapturaIndicadorAgrupamento(x.codigoMaterial) == "N").ToList();

                //    if (ItensYBO1_D.Count > 0)
                //    {
                //        OrdemVenda.itensVenda = ItensYBO1_D;
                //        ChamarTransacaoSAP(OrdemVenda);
                //    }
                //}

                //if (ItensYBO2.Count > 0)
                //{
                //    var ItensYBO2_A = ItensYBO2.Where(x => new DPRepositorio().CapturaIndicadorAgrupamento(x.codigoMaterial) == "S").ToList();

                //    if (ItensYBO2_A.Count > 0)
                //    {
                //        OrdemVenda.itensVenda = ItensYBO2_A;
                //        ChamarTransacaoSAP(OrdemVenda);
                //    }

                //    var ItensYBO2_D = ItensYBO2.Where(x => new DPRepositorio().CapturaIndicadorAgrupamento(x.codigoMaterial) == "N").ToList();

                //    if (ItensYBO2_D.Count > 0)
                //    {
                //        OrdemVenda.itensVenda = ItensYBO2_D;
                //        ChamarTransacaoSAP(OrdemVenda);
                //    }
                //}

                return new Dominio.Retorno() { Resultado = true, Mensagem = "OK" };
            }
            catch (Exception e)
            {
                Log(e, "IngressaOrdemVendaSAP");
                return new Dominio.Retorno() { Resultado = false, Mensagem = "ERRO AO INGRESSAR ORDEM DE VENDA" };
            }

        }

        public static Dominio.Retorno CadastrarOrdemVendaSAP(Dominio.OrdemVendaSAP OrdemVenda)
        {
            OrdemVendaRepositorio___ OrdemVendaRepositorio = new OrdemVendaRepositorio___();
            return OrdemVendaRepositorio.IngressaOrdemVendaSAP(OrdemVenda);
        }
    }
}
