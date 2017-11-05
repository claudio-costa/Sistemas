using Excel;
using MobLink.Framework;
using MobLink.LinkLeiloes.Repositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ImportarExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            MobLink.Framework.WebServices.WSPatioxDetran ws = new MobLink.Framework.WebServices.WSPatioxDetran(Ambientes.Producao);
            var ret = ws.ConsultaVeiculo("IGG2033", "ROOT");


            FileStream stream = File.Open(@"C:\Temp\Garanhuns\Garanhuns.xlsx", FileMode.Open, FileAccess.Read);

            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            excelReader.IsFirstRowAsColumnNames = true;

            DataSet result = excelReader.AsDataSet();

            var Arrematantes = result.Tables[0].ConverterParaLista<MobLink.LinkLeiloes.Dominio.Arrematante>();
            

            foreach (var arrematante in Arrematantes)
            {
                //var del = string.Format(@"delete tb_leilao_lotes_arrematantes where id_leilao = 91 and lote = '{0}'", arrematante.lote);
                //RepositorioGlobal.Util.ExecutaSqlGenerico(Util.LerConfiguracao("CONEXAO"), del);

                Console.WriteLine(arrematante.placa);

                //if (arrematante.numero_processo.Contains("_"))
                //{
                //    arrematante.numero_processo = arrematante.numero_processo.Replace("_", "");
                //}

                //if (arrematante.numero_processo.Substring(0, 1) == "0")
                //{
                //    arrematante.numero_processo = arrematante.numero_processo.Substring(1, arrematante.numero_processo.Length - 1);
                //}

                if (!string.IsNullOrEmpty(arrematante.placa) || !string.IsNullOrEmpty(arrematante.chassi))
                {
                    if (arrematante.placa.Length > 6)
                    {
                        arrematante.placa = arrematante.placa.Substring(0, 7);
                    }

                    var sql = string.Format(@"SELECT * 
                                            FROM tb_leilao_lotes ll
                                           INNER JOIN tb_leilao l ON l.id = ll.id_leilao
                                           WHERE l.descricao = '{0}'
                                             AND ll.placa = '{1}' OR ll.chassi = '{2}'", arrematante.leilao, arrematante.placa, arrematante.chassi);

                    var dtLote = RepositorioGlobal.Util.ConsultaGenerica(Util.DetectarConexao(), sql).ConverterParaLista<MobLink.LinkLeiloes.Dominio.Lote>();

                    arrematante.numero_processo = dtLote.Count > 0 ? dtLote[0].numero_formulario_grv : string.Empty;

                    sql = string.Format(@"SELECT * FROM tb_dep_grv WHERE numero_formulario_grv = '{0}'", arrematante.numero_processo);

                    MobLink.LinkLeiloes.Dominio.GRV grv = new MobLink.LinkLeiloes.Dominio.GRV();

                    try
                    {
                        grv = RepositorioGlobal.Util.ConsultaGenerica(Util.LerConfiguracao("CONEXAO_DP"), sql).ConverterParaEntidade<MobLink.LinkLeiloes.Dominio.GRV>();
                    }
                    catch (Exception e)
                    {
                        grv.id_grv = 0;
                    }

                    arrematante.id_grv = grv.id_grv;

                    try
                    {
                        //var lote = RepositorioGlobal.Lote.SelecionarPorId(grv.id_grv.Value);
                        //lote.id_status_lote = 22; //LOTE ARREMATADO
                        //RepositorioGlobal.Lote.Alterar(lote);

                        var upd = string.Format(@"UPDATE dbo.tb_dep_grv SET id_status_operacao = '3' WHERE id_grv = {0}", grv.id_grv);
                        RepositorioGlobal.Util.ExecutaSqlGenerico(Util.LerConfiguracao("CONEXAO_DP"), upd);
                    }
                    catch (Exception e)
                    {

                    }

                }
                else
                {
                    arrematante.numero_processo = "**************";
                }

                RepositorioGlobal.Arrematante.Inserir(arrematante);
            }

            Environment.Exit(0);












            // I M P O R T A Ç Ã O   T E M P O R A R I A   D E   L O T E S   L E I L Ã O   P R F P B 0 1 . 1 7 
            //FileStream stream = File.Open(@"C:\Temp\PRFPB\PRF.xlsx", FileMode.Open, FileAccess.Read);

            //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            //excelReader.IsFirstRowAsColumnNames = true;

            //var tabela = excelReader.AsDataSet().Tables[0].ConverterParaLista<PRFPB.LotePRFPB>();

            //foreach (var item in tabela)
            //{
            //    string sql = string.Format(@"
            //    INSERT INTO dbo.PRFPB (patio, municipio, uf, drv, recolhimento, placa, marca_modelo, chassi, grv)
            //    VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}','{8}')",
            //    item.patio, item.municipio, item.uf, item.drv, item.recolhimento, item.placa, item.marca_modelo, item.chassi, item.grv);

            //    MobLink.LinkLeiloes.Repositorio.RepositorioGlobal.Util.ExecutaSqlGenerico(MobLink.Framework.Util.LerConfiguracao("CONEXAO"), sql);

            //    Console.WriteLine(item.grv);
            //}

            //var grvs = MobLink.LinkLeiloes.Repositorio.RepositorioGlobal.Util.ConsultaGenerica(MobLink.Framework.Util.LerConfiguracao("CONEXAO"), 
            //                                                                                 "select grv from dbo.PRFPB");


            //foreach (System.Data.DataRow item in grvs.Rows)
            //{
            //    var grv = item.ItemArray[0];


            //    MobLink.LinkLeiloes.Dominio.GRV res = new MobLink.LinkLeiloes.Dominio.GRV();

            //    try
            //    {
            //        res = RepositorioGlobal.GRV.SelecionarTudoFormularioGRV(grvs: grv.ToString())[0];
            //    }
            //    catch
            //    {
            //        Console.WriteLine(grv);
            //        continue;
            //    }
                

            //    RepositorioGlobal.GRV.Inserir(res, 74);//insere lote

            //    RepositorioGlobal.GRV.AlterarStatusGRV(res, "1");
            //}


            

            //Environment.Exit(0);
            // F I M  I M P O R T A Ç Ã O   T E M P O R A R I A   D E   L O T E S   L E I L Ã O   P R F P B 0 1 . 1 7 




            //VipLeiloes.VipRepositorio.Complementar_tb_boleto_lotes("TRGD02.17");
            //Environment.Exit(0);


            //TRGD01.17     OK
            //TRGD02.16     


            //var res = RepositorioGlobal.Arrematante.ObterInfoRodandoLegal("PETR01.16");

            //FileStream stream = File.Open(@"C:\Users\Claudio Costa\Downloads\TRGD01-16-X.xlsx", FileMode.Open, FileAccess.Read);
            //FileStream stream = File.Open(@"C:\Users\Claudio Costa\Downloads\TRGD02-16-X.xlsx", FileMode.Open, FileAccess.Read);
            //FileStream stream = File.Open(@"C:\Users\Claudio Costa\Downloads\TRGD01-17-X.xlsx", FileMode.Open, FileAccess.Read);

            //*.xls
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

            //*.xlsx
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);



            //new TransguardRepositorio().Importar_PROPRIETARIOS();

            //var dtNaoCadastrados = result.Tables["1 - NAO_CADASTRADOS_DETRAN_RJ"].ConverterParaLista<NAO_CADASTRADOS_DETRAN_RJ>();
            //var dtCadastrados = result.Tables["2 - CADASTRADOS"].ConverterParaLista<CADASTRADOS>();
            //var dtRestricoes = result.Tables["3 - RESTRICOES"].ConverterParaLista<RESTRICOES>();
            //var dtTributos = result.Tables["4 - TRIBUTOS"].ConverterParaLista<TRIBUTOS>();
            //var dtMultas = result.Tables["5 - MULTAS"].ConverterParaLista<MULTAS>();
            //var dtSituacaoRoubo = result.Tables["6 - SITUACAO_ROUBOFURTO_BIN"].ConverterParaLista<SITUACAO_ROUBOFURTO_BIN>();
            //var dtDetalheRenainf = result.Tables["7 - DETALHE_RENAINF"].ConverterParaLista<DETALHE_RENAINF>();

            //foreach (var item in dtNaoCadastrados)
            //{
            //    Console.WriteLine(string.Concat(item.DESC_MARCA_MODELO, " ", item.PLACA));
            //    new TransguardRepositorio().Inserir_NAO_CADASTRADOS_DETRAN_RJ(item);
            //}

            //foreach (var item in dtRestricoes)
            //{
            //    Console.WriteLine(string.Concat(item.PLACA));
            //    new TransguardRepositorio().Inserir_RESTRICOES(item);
            //}

            //foreach (var item in dtSituacaoRoubo)
            //{
            //    Console.WriteLine(string.Concat(item.PLACA));
            //    new TransguardRepositorio().Inserir_SITUACAO_ROUBOFURTO_BIN(item);
            //}

            //foreach (var item in dtTributos)
            //{
            //    Console.WriteLine(string.Concat(item.PLACA, "-", item.RENAVAM));
            //    new TransguardRepositorio().Inserir_TRIBUTOS(item);
            //}

            //foreach (var item in dtMultas)
            //{
            //    Console.WriteLine(string.Concat(item.AGENTE_AUTUADOR, " ", item.DESC_INFRACAO));
            //    new TransguardRepositorio().Inserir_MULTAS(item);
            //}

            //foreach (var item in dtDetalheRenainf)
            //{
            //    Console.WriteLine(string.Concat(item.AUTO, " ", item.DESCRICAO_MUNICIPIO));
            //    new TransguardRepositorio().Inserir_DETALHE_RENAINF(item);
            //}

            //foreach (var item in dtCadastrados)
            //{
            //    Console.WriteLine(string.Concat(item.PLACA, " ", item.CHASSI, " ", item.DESC_MARCA, " ", item.PROPRIETARIO));
            //    new TransguardRepositorio().Inserir_CADASTRADOS(item);
            //}

            //var dt = result.Tables[0].ConverterParaLista<Financeira>();

            //foreach (var item in dt)
            //{
            //    new Repositorio().InserirFinanceira(item);
            //}

            ////for (int i = 0; i < 10; i++)
            ////{
            ////    dt.Rows[i].Delete();
            ////}

            ////dt.AcceptChanges();

            ////...
            ////4. DataSet - Create column names from first row
            ////excelReader.IsFirstRowAsColumnNames = true;
            ////result = excelReader.AsDataSet();

            ////5. Data Reader methods
            //while (excelReader.Read())
            //{
            //    //var teste = excelReader.GetString(0);
            //}

            ////6. Free resources (IExcelDataReader is IDisposable)
            //excelReader.Close();
        }
    }
}
