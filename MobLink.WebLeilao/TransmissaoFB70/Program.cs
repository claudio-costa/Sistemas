using MobLink.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransmissaoFB70
{
    class Program
    {
        static WSSapLinkPatios.WSSapLinkPatios ws;

        static void CarregarArrematantes()
        {
            System.Data.DataTable tabela = new System.Data.DataTable();

            using (var stream = File.Open(@"C:\temp\processos.xlsx", FileMode.Open, FileAccess.Read))
            {

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(stream))
                {

                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    //do
                    //{
                    //    while (reader.Read())
                    //    {
                    //        // reader.GetDouble(0);
                    //    }
                    //} while (reader.NextResult());

                    // 2. Use the AsDataSet extension method


                    reader.IsFirstRowAsColumnNames = true;
                    tabela = reader.AsDataSet().Tables[0];
                    
                    
                    // The result of each spreadsheet is in result.Tables
                }
            }

            var arrematantes_brbid = tabela.ConverterParaLista<MobLink.WebLeilao.Dominio.Arrematante>();

            foreach (var a in arrematantes_brbid)
            {
                var str = String.Join("", System.Text.RegularExpressions.Regex.Split(a.cpf, @"[^\d]"));

                if (str.Length > 11)
                {
                    a.cnpj = a.cpf;
                    a.cpf = string.Empty;
                }

                try
                {
                   a.status_cadastro_cliente_sap = "P"; //SOLICITO ENVIO

                    MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.Inserir(a);

                    Console.WriteLine("INSERIU " + a.nome_arrematante);
                }
                catch (Exception e)
                {
                    throw;
                }
            }



            //CARREGO ARREMATANTES DA VIP

            var Arrematantes = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.FinanceiroDetalhamento("TRGD02.17")
                //.Where(x => x.data_pagamento_boleto == "04/09/2017")
                .ToList();


            var TESTE = Arrematantes.Where(x => x.lote == "524").ToList();

            //var teste = Arrematantes.Where(x => x.leilao == "TRGD02.17").ToList();

            //CARREGO LISTA DE PAGAMENTOS DO LEILÃO

            var ListaPagamentos = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.ObterListaPagamentos(65);

            //PARA CADA, VERIFICO SE HÁ PAGAMENTO REGISTRADO

            foreach (var a in Arrematantes)
            {
                var pgto = ListaPagamentos.Where(boleto => boleto.id_boleto == a.numero_boleto).FirstOrDefault();

                if (pgto.IsNotNull())
                {
                    //TRATAR O TIPO DE DOCUMENTO
                    var str = String.Join("", System.Text.RegularExpressions.Regex.Split(a.cpf, @"[^\d]"));

                    if (str.Length > 11)
                    {
                        a.cnpj = a.cpf;
                        a.cpf = string.Empty;
                    }
                    
                    try
                    {
                        //a.data_pagamento_boleto = pgto.boleto_data_arrecadado;
                        a.data_emissao_boleto = pgto.data_documento;
                        a.data_vencimento_boleto = pgto.boleto_vencimento;
                        //a.linha_digitavel = pgto.linha_digitavel;

                        //a.descontos = pgto.desconto;
                        a.outras_taxas = pgto.outras_taxas;
                        a.valor_pago = pgto.valor_pago;

                        a.status_cadastro_cliente_sap = "P"; //SOLICITO ENVIO

                        MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.Inserir(a);

                        Console.WriteLine("INSERIU " + a.nome_arrematante);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
                else
                {
                    //inserir o cadastro sem boleto porem setar algum campo para que possa ser identificado e tratado depois
                }
            }
        }

        private static void AtualizarStatusDepositoPublico()
        {
            var Arrematantes = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.SelecionarTudo(65)
                .ToList();
            
            foreach (var item in Arrematantes)
            {
                Int32 id_grv = 0;

                Business.Sistema.GlobalDataBase.ConnectDataBase(MobLink.Framework.Util.LerConfiguracao("CONEXAO_DP"));

                try
                {
                    string sql = string.Format("select id_grv from dbo.tb_dep_grv where numero_formulario_grv = '{0}'", item.numero_processo.TrimStart('0'));

                    var teste = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Util.ConsultaGenerica(MobLink.Framework.Util.LerConfiguracao("CONEXAO_DP"), sql);

                    id_grv = (Int32)teste.Rows[0]["id_grv"];

                    
                    var ret = Business.GRV.GRV.AtualizarStatusOperacao(id_grv, "3", 1);
                }
                catch(Exception e)
                {
                    Console.WriteLine("ERRO PROCESSO: " + item.numero_processo + " --- \t" + e.Message);
                }
                finally
                {
                    item.id_grv = id_grv;
                    MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.Alterar(item);

                    Console.WriteLine(item.numero_processo + "\t" + item.placa + "\t" +item.chassi + "\t" + item.lote);
                }
            }

            Business.Sistema.GlobalDataBase.DisconnectDataBase();
            Console.ReadLine();
        }

        static void CriarClienteSAP()
        {
            var novos_cliente_sap = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.SelecionarTudo(65)
                //.Where(p => p.status_cadastro_cliente_sap == "P" && p.data_pagamento_boleto != "04/09/2017")
                .Where(p => p.status_cadastro_cliente_sap == "P")
                .ToList();


            WSSapLinkPatios.ClienteSap clienteSap;

            foreach (var arr in novos_cliente_sap)
            {
                clienteSap = new WSSapLinkPatios.ClienteSap()
                {
                    Codigo_Empresa = "1080",
                    Organizacao_Vendas = "1080",
                    Codigo_Organizacao_Parceiro = "1080",
                    Nome_Cliente = arr.nome_arrematante,
                    Endereco_Rua = arr.logradouro,
                    Endereco_Numero = arr.numero,
                    Endereco_Bairro = arr.bairro,
                    Endereco_CEP = arr.cep.FormatarParaCEP(),
                    Endereco_Cidade = arr.cidade,
                    Endereco_Regiao = arr.estado,
                    Telefone = arr.fone_1,
                    Email = arr.email,
                    CNPJ = arr.cnpj,
                    CPF = arr.cpf,
                    Forma_Pagamento = "D",
                    Condicao_Pagamento = "B001"
                };

                if (clienteSap.Endereco_CEP == "ERRO")
                    clienteSap.Endereco_CEP = "24000400".FormatarParaCEP();

                try
                {
                    var retorno_criacao_cliente = ws.CadastrarClienteSAP(clienteSap);

                    if (retorno_criacao_cliente.Resultado == true)
                    {
                        Console.WriteLine("CRIADO O CLIENTE " + arr.nome_arrematante);

                        //ATUALIZO O CAMPO status_cadastro_cliente_sap PARA FLAG S - ENVIADO
                        arr.status_cadastro_cliente_sap = "S";
                        MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.Alterar(arr);
                    }
                    else
                    {

                    }
                }
                catch (Exception e)
                {
                    throw;
                }

            }
        }

        static void AtualizarCodigoCliente()
        {
            //DEPOIS DE CRIAR O CLIENTE, PRECISO CRIAR UMA ROTINA PARA PEGAR OS CÓDIGOS SAP GERADOS E ATUALIZAR OS ARREMATANTES
            try
            {
                var lista_fb70_sap = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.SelecionarTudo(65)
                .Where(p => p.status_cadastro_cliente_sap == "S")   //S-ENVIADO
                .ToList();

                foreach (var a in lista_fb70_sap)
                {
                    if (a.cpf != "" || a.cnpj != "")
                    {
                        var codigo_cliente_sap = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Sap.RecuperaCodigoClienteSAP(a.cpf == "" ? a.cnpj : a.cpf);

                        if (codigo_cliente_sap == "")
                            continue;

                        //ATUALIZAR A COLUNA id_documento_cliente_sap DA TABELA DE ARREMATANTES

                        a.id_documento_cliente_sap = codigo_cliente_sap;

                        //ATUALIZAR A COLUNA status_cadastro_cliente_sap DA TABELA DE ARREMATANTES (F)INALIZADO

                        a.status_cadastro_cliente_sap = "F";

                        //ATUALIZAR A COLUNA status_cadastro_fb70_sap DA TABELA DE ARREMATANTES (P-ENVIAR)

                        a.status_cadastro_fb70_sap = "P";

                        //ATUALIZAR O ARREMATANTE
                        MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.Alterar(a);

                        Console.WriteLine("RECUPERADO CODIGO SAP CLI PARA " + a.nome_arrematante);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        static void TransmitirFB70()
        {
            var lista_fb70_sap = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.SelecionarTudo(65)
                .Where(p => p.status_cadastro_fb70_sap == "P")   //P-ENVIAR
                .ToList();

            WSSapLinkPatios.OrdemInternaFB70 ordemInternaFB70;

            foreach (var a in lista_fb70_sap)
            {
                var data_fatura = a.data_emissao_boleto.Replace("/", "");
                var data_pagamento = a.data_pagamento_boleto.Replace("/", "");

                ordemInternaFB70 = new WSSapLinkPatios.OrdemInternaFB70()
                {
                    codigo_banco = "BR04",
                    codigo_empresa = "1080",
                    condicao_pagamento = "B001",
                    forma_pagamento = "D",

                    numero_ordem_interna = "500314", //PRD 500314 QAD 500280 ??? 30082017

                    codigo_cliente = a.id_documento_cliente_sap,
                    data_fatura = data_fatura,
                    data_pagamento = data_pagamento,
                    identificacao_leilao_patio_lote = a.lote,
                    numero_boleto_pagamento = a.numero_boleto
                };

                int casas_decimais = 2;

                //VALORES
                ordemInternaFB70.valor_comissao = decimal.Round(decimal.Parse(a.comissao == "" ? "0" : a.comissao), casas_decimais);
                ordemInternaFB70.valor_desconto = decimal.Round(decimal.Parse(a.descontos == "" ? "0" : a.descontos), casas_decimais);
                //ordemInternaFB70.valor_tarifa_bancaria = decimal.Round(decimal.Parse(a.tarifa_bancaria == "" ? "0" : a.tarifa_bancaria), casas_decimais);
                ordemInternaFB70.valor_tarifa_bancaria = decimal.Parse("3,70");
                ordemInternaFB70.valor_taxa_administrativa = decimal.Round(decimal.Parse(a.taxa_administrativa == "" ? "0" : a.taxa_administrativa), casas_decimais);
                ordemInternaFB70.valor_lote = decimal.Round(decimal.Parse(a.arrematacao == "" ? "0" : a.arrematacao), casas_decimais);
                //ordemInternaFB70.valor_total_pago = decimal.Round(decimal.Parse(a.valor_total == "" ? "0" : a.valor_total), casas_decimais);
                ordemInternaFB70.valor_total_pago = decimal.Round(decimal.Parse(a.valor_pago == "" ? "0" : a.valor_pago), casas_decimais);

                var valor_do_boleto = decimal.Round(decimal.Parse(a.valor_total == "" ? "0" : a.valor_total), casas_decimais);



                /*
                <montante_bruto> = <valor_lote> + <valor_taxa_administrativa> + <valor_comissao> + <valor_tarifa_bancaria> - <valor_desconto>​
                Caso o valor pago seja diferente do valor registrado no boleto, a diferença deverá ser informada no campo <valor_pagamento_maior>.
                 */

                //ordemInternaFB70.valor_tarifa_bancaria = ordemInternaFB70.valor_total_pago - valor_do_boleto;

                ordemInternaFB70.montante_bruto = decimal.Round((
                    ordemInternaFB70.valor_lote +
                    ordemInternaFB70.valor_taxa_administrativa +
                    ordemInternaFB70.valor_comissao +
                    ordemInternaFB70.valor_tarifa_bancaria -
                    ordemInternaFB70.valor_desconto), casas_decimais);

                ordemInternaFB70.opcao_valor_desconto = ordemInternaFB70.valor_desconto > 0;
                ordemInternaFB70.opcao_valor_tarifa_bancaria = ordemInternaFB70.valor_tarifa_bancaria > 0;
                ordemInternaFB70.opcao_valor_pagamento_maior = (valor_do_boleto + ordemInternaFB70.valor_tarifa_bancaria) < ordemInternaFB70.montante_bruto;

                if (ordemInternaFB70.opcao_valor_pagamento_maior)
                    ordemInternaFB70.valor_pagamento_maior = decimal.Round((ordemInternaFB70.valor_total_pago - valor_do_boleto), casas_decimais);

                try
                {
                    var retorno_fb70 = ws.FB70(ordemInternaFB70);

                    if (retorno_fb70.Resultado)
                    {
                        Console.WriteLine("fb70 transmitida para o lote " + ordemInternaFB70.identificacao_leilao_patio_lote);

                        //ATUALIZO O CAMPO status_cadastro_cliente_sap PARA FLAG S - ENVIADO
                        a.status_cadastro_fb70_sap = "S";
                        MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.Alterar(a);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        static void AtualizarDocumentoFB70()
        {
            var lista_fb70_sap = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.SelecionarTudo(65)
                .Where(p => p.status_cadastro_fb70_sap == "S")   //S-ENVIADO
                .ToList();

            string lotes_nao_recuperados = string.Empty;

            foreach (var a in lista_fb70_sap)
            {
                if (a.cpf.IsNotNull())
                {
                    string codigo_ordem_sap = string.Empty;

                    try
                    {
                        codigo_ordem_sap = MobLink.WebLeilao.Repositorio.RepositorioGlobal.Sap.RecuperaOrdemFB70SAP(a.lote);
                    }
                    catch (Exception e)
                    {
                        lotes_nao_recuperados = string.Join(",", lotes_nao_recuperados + a.lote);
                    }

                    if (codigo_ordem_sap == "")
                        continue;

                    //ATUALIZAR A COLUNA id_documento_cliente_sap DA TABELA DE ARREMATANTES

                    a.id_documento_fb70_sap = codigo_ordem_sap;

                    //ATUALIZAR A COLUNA status_cadastro_fb70_sap DA TABELA DE ARREMATANTES (F-FINALIZADO)

                    a.status_cadastro_fb70_sap = "F";

                    //ATUALIZAR O ARREMATANTE
                    MobLink.WebLeilao.Repositorio.RepositorioGlobal.Arrematante.Alterar(a);

                    Console.WriteLine("CODIGO RECUPERADO SAP FB70 PARA " + a.nome_arrematante);
                }
            }
        }



        static void Main(string[] args)
        {
            //ws = new WSSapLinkPatios.WSSapLinkPatios();
            //ws.Url = "http://localhost:65518/WSSapLinkPatios.asmx";
            //ws.Timeout = 1000000000;

            CarregarArrematantes();

            //AtualizarStatusDepositoPublico();

            //CriarClienteSAP();

            //AtualizarCodigoCliente();

            //TransmitirFB70();

            //AtualizarDocumentoFB70();
        }
    }
}
