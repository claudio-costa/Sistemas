using MobLink.LinkLeiloes.Dominio;
using System;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using MobLink.Framework;
using MobLink.Framework.Database;
using System.Diagnostics;
using System.Collections.Generic;

namespace MobLink.LinkLeiloes.Repositorio
{
    public class TransacoesRepositorio : DbSqlServer
    {
        public TransacoesRepositorio(): base(Util.LerConfiguracao("CONEXAO"))
        {

        }

        private string ws_ConsultarVeiculosParaLeilao(string Operador, string Placa = null, string Chassi = null)
        {
            var ws = new Framework.WebServices.WSPatioxDetran(Util.DetectarAmbiente());

            var res = ws.ConsultaVeiculoParaLeilao(Operador, Placa, Chassi);

            return res;
        }

        private string ws_ConsultarVeiculo(string Operador, string Placa = null)
        {
            var ws = new Framework.WebServices.WSPatioxDetran(Util.DetectarAmbiente());

            var res = ws.ConsultaVeiculo(Placa, Operador);

            if (res == "Erro")
            {
                return null;
            }

            return res;
        }

        private string ws_InclusaoVeiculoPatio(string Operador, string Placa = null)
        {
            var ws = new Framework.WebServices.WSPatioxDetran(Util.DetectarAmbiente());

            var res = ws.InclusaoVeiculoPatio(Operador, Placa);

            if (res == "Erro")
            {
                return null;
            }

            return res;
        }

        public int InserirTransacao(Transacao Transacao)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO dbo.tb_leilao_lotes_transacoes  ");
            sql.AppendLine("  (id_lote                                  ");
            sql.AppendLine(" , transacao                                ");
            sql.AppendLine(" , retorno)                                 ");

            sql.AppendLine("VALUES                                      ");

            sql.AppendLine(string.Format("('{0}'  ", Transacao.id_lote));
            sql.AppendLine(string.Format(",'{0}'  ", Transacao.transacao));
            sql.AppendLine(string.Format(",'{0}') ", Transacao.retorno));

            try
            {
                return ExecutaSQL_ScopeIdentity(sql.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Normalizacao()
        {
            //NORMALIZAR ENDERECO FINANCEIRA
            var mainQuery = @"
            SELECT p.id ID,
                   substring(convert(VARCHAR(100),(CONVERT(BIGINT,p.cpf_cnpj_financiamento_efet))),0,7) CNPJ_CORRIGIDO, f.nome NOME_FINANCEIRA, 
                   CASE 
       		            WHEN len(f.cep) = 7 THEN '0' + f.cep 
       		            WHEN len(f.cep) = 8 THEN f.cep 
       		            ELSE 'INDEFINIDO' 
                   END CEP_CORRIGIDO, 
                   f.endereco ENDERECO, f.complemento
              FROM tb_leilao_lotes_proprietarios p
              JOIN tb_financeiras f on f.cnpj like (substring(convert(VARCHAR(100),(CONVERT(BIGINT,p.cpf_cnpj_financiamento_efet))),0,7)+'%')
             WHERE CONVERT(BIGINT,p.cpf_cnpj_financiamento_efet) <> '0'
          ORDER BY p.id";
            
            var dtMain = ConsultaSQL(mainQuery).AsEnumerable();


            foreach (var item in dtMain)
            {
                var id_proprietario = (Int32)item.ItemArray[0];
                var cep_financeira = item.ItemArray[3].ToString();
                var complemento_financeira = item.ItemArray[5].ToString();
                var nome_financeira = item.ItemArray[2].ToString();

                var retornoCEPFinanceira = Framework.WebServices.WebserviceCorreios.ConsultaCEP(cep_financeira);

                var proprietario = RepositorioGlobal.Proprietario.SelecionarPorId(id_proprietario);
               
                proprietario.BairroFinanciamentoEfet = retornoCEPFinanceira.bairro;
                proprietario.CepFinanciamentoEfet = retornoCEPFinanceira.cep;
                proprietario.ComplementoFinanciamentoEfet = complemento_financeira;
                //proprietario.cpfcnpjfinanciamentoefet = 
                proprietario.EnderecoFinanciamentoEfet = retornoCEPFinanceira.end;
                proprietario.MunicipioFinanciamentoEfet = retornoCEPFinanceira.cidade;
                proprietario.NomeFinanciamentoEfet = nome_financeira;
                //proprietario.numerofinanciamentoefet 
                proprietario.TipoDocumentoFinanciamentoEfet = "2";
                proprietario.UfFinanciamentoEfet = retornoCEPFinanceira.uf;
                proprietario.flag_notificar_financeira = "S";

                RepositorioGlobal.Proprietario.Alterar(proprietario);
            }


            //CONFIRMAR CEP DOS ENVOLVIDOS
            var sql_proprietario = @"
                SELECT * 
                  FROM tb_leilao_lotes_proprietarios
                 WHERE flag_normalizado = 'N'
                   AND (cep_endereco_proprietario <> '' OR cep_comunicado_venda <> '' OR cep_financiamento_efet <> '')
            ";




#if (DEBUG)
            //sql_proprietario += " AND tb_leilao_lotes_proprietarios.id_lote IN(SELECT id FROM tb_leilao_lotes WHERE id_leilao = 79)";
#endif
            var retorno_proprietario = ConsultaSQL(sql_proprietario).ConverterParaLista<Proprietario>();

            foreach (var item in retorno_proprietario)
            {
                Framework.WebServices.WebserviceCorreios.Endereco endereco = new Framework.WebServices.WebserviceCorreios.Endereco();

                //PROPRIETARIO
                if (item.cep_endereco_proprietario != null)
                {
                    try
                    {
                        endereco = Framework.WebServices.WebserviceCorreios.ConsultaCEP(item.cep_endereco_proprietario.FormatarParaCEP());
                        item.bairro_proprietario = endereco.bairro;
                        item.descricao_municipio_endereco = endereco.cidade;
                        item.endereco_proprietario = endereco.end;
                        item.uf_proprietario = endereco.uf;
                        Console.Write("PROPRIETARIO => " + item.nome_proprietario);
                    }
                    catch (Exception e)
                    {
                        //CEP NAO ENCONTRADO
                    }
                }

                //COMUNICADO VENDA
                if (item.cep_comunicado_venda != null)
                {
                    try
                    {
                        endereco = Framework.WebServices.WebserviceCorreios.ConsultaCEP(item.cep_comunicado_venda.FormatarParaCEP());
                        item.bairro_comunicado_venda = endereco.bairro;
                        item.municipio_comunicado_venda = endereco.cidade;
                        item.endereco_comunicado_venda = endereco.end;
                        item.uf_comunicado_venda = endereco.uf;
                        Console.Write("\tCOMUNICADO VENDA => " + item.nome_comunicado_venda);
                    }
                    catch (Exception e)
                    {
                        //CEP NAO ENCONTRAD
                    }
                }

                

                //FINANCEIRA
                if (item.cep_financiamento_efet != null)
                {
                    try
                    {
                        endereco = Framework.WebServices.WebserviceCorreios.ConsultaCEP(item.cep_financiamento_efet.FormatarParaCEP());
                        item.bairro_financiamento_efet = endereco.bairro;
                        item.municipio_financiamento_efet = endereco.cidade;
                        item.endereco_financiamento_efet = endereco.end;
                        item.uf_financiamento_efet = endereco.uf;
                        Console.Write("\tFINANCEIRA => " + item.nome_financiamento_efet);
                    }
                    catch (Exception e)
                    {
                        //CEP NAO ENCONTRADO
                    }
                }

                item.flag_normalizado = "S";

                RepositorioGlobal.Proprietario.Alterar(item);

                Console.WriteLine();
            }
        }

        public void ProcessarAlteracaoStatusLeilao()
        {
            var leiloes = RepositorioGlobal.Leilao.SelecionarTudoDETRO()
                .Where(p => p.id_status == 1)
                .ToList();

            if (leiloes.Count > 0)
            {
                foreach (var leilao in leiloes)
                {
                    //VERIFICAR SE EXISTEM LOTES NÃO PROCESSADOS
                    //var lotes = LoteRepositorio.SelecionarTudo(leilao.Id)
                    //    .Where(p => p.Status_Processamento_DETRO == "N")
                    //    .ToList();

                    //if (lotes.Count <= 0)
                    //{
                    //    LeilaoRepositorio.AlterarStatus(leilao.Id, 2);
                    //}
                }
            }
        }

        private string GetProximaTransacao(int id)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT flag                                            ");
            sql.AppendLine("  FROM tb_leilao_lotes_tipos_transacao                 ");
            sql.AppendLine(" WHERE ordem = (SELECT ordem + 1                       ");
            sql.AppendLine("                  FROM tb_leilao_lotes_tipos_transacao ");
            sql.AppendLine("                 WHERE flag = (SELECT flag_transacao   ");
            sql.AppendLine("                                 FROM tb_leilao_lotes  ");
            sql.AppendFormat("                              WHERE id = {0}))       ", id);

            var ret = ConsultaSQL(sql.ToString());

            return ret.Rows[0][0].ToString();
        }

        /// <summary>
        /// Consulta 001
        /// </summary>
        public void Consulta()
        {
            var consulta =
            @"
                SELECT lo.*
                  FROM dbo.tb_leilao_lotes lo
                  JOIN tb_leilao le ON lo.id_leilao = le.id
                 WHERE lo.flag_agendado = 'S'
                   AND lo.flag_transacao = 'C'
                   AND le.id_status = (SELECT id FROM tb_leilao_status WHERE sequencia = 1)
             ";

            var lotes = ConsultaSQL(consulta).ConverterParaLista<Lote>();


#if (DEBUG)
            //lotes = lotes.Where(l => l.id_leilao == 86).ToList();




            //lotes = lotes.Where(l => l.id_leilao == 79).ToList();




#endif

            foreach (var lote in lotes)
            {
                string JsonResponse = string.Empty;

                if (!string.IsNullOrEmpty(lote.placa))
                {
                    JsonResponse = ws_ConsultarVeiculo("ROOT", lote.placa.Trim());
                }
                else if (!string.IsNullOrEmpty(lote.chassi))
                {
                    JsonResponse = ws_ConsultarVeiculo("ROOT", lote.chassi.Trim());
                }
                else //NÃO POSSUI PLACA OU CHASSI
                {
                    var id = InserirTransacao(new Transacao()
                    {
                        id_lote = lote.id,
                        retorno = "PLACA E CHASSI AUSENTES",
                        transacao = "001"
                    });

                    lote.Flag_Agendado = "N";
                    lote.Flag_Transacao = "X";
                    lote.obs_transacao = "PLACA E CHASSI AUSENTES";
                    RepositorioGlobal.Lote.Alterar(lote);

                    Debug.WriteLine(string.Format("TRANSAÇÃO {0} GERADA", id));

                    continue;
                }

                if (JsonResponse == null)
                {
                    //lote.Flag_Agendado = "N";
                    //lote.Flag_Transacao = "";
                    //lote.obs_transacao = "SEM RETORNO NA CONSULTA";
                    //RepositorioGlobal.Lote.Alterar(lote);

                    //var id = InserirTransacao(new Transacao()
                    //{
                    //    id_lote = lote.id,
                    //    retorno = "SEM RETORNO NA CONSULTA",
                    //    transacao = "001"
                    //});

                    continue;
                }

                var Transacao001 = JsonConvert.DeserializeObject<Transacao001>(JsonResponse);

                if (Transacao001.Retorno.Contains("VEICULO NAO CADASTRADO"))
                {
                    lote.Flag_Agendado = "N";
                    lote.Flag_Transacao = "X";
                    lote.obs_transacao = Transacao001.Retorno;
                    RepositorioGlobal.Lote.Alterar(lote);

                    var id = InserirTransacao(new Transacao()
                    {
                        id_lote = lote.id,
                        retorno = Transacao001.Retorno,
                        transacao = "001"
                    });

                    continue;
                }

                if (Transacao001.Retorno == "OK")
                {
                    Console.WriteLine(lote.numero_formulario_grv);

                    foreach (var item in Transacao001.RestricoesJuridicas)
                    {
                        lote.id_status_lote = 26;

                        RepositorioGlobal.Restricao.Inserir(new Restricao()
                        {
                            codigo = item.codigo,
                            id_lote = lote.id,
                            origem = Transacao001.Transacao,
                            restricao = item.restricao
                        });
                    }

                    foreach (var item in Transacao001.RestricoesAdministrativas)
                    {
                        lote.id_status_lote = 26;

                        RepositorioGlobal.Restricao.Inserir(new Restricao()
                        {
                            codigo = item.codigo,
                            id_lote = lote.id,
                            origem = Transacao001.Transacao,
                            restricao = item.restricao
                        });
                    }

                    if (Transacao001.InformacaoRoubo != "")
                    {
                        lote.id_status_lote = 26;
                        lote.informacao_roubo = Transacao001.InformacaoRoubo;
                    }

                    if (Transacao001.RestricaoEstelionato != "")
                    {
                        lote.id_status_lote = 26;
                        lote.restricao_estelionato = Transacao001.RestricaoEstelionato;
                    }

                    //COMPLEMENTAÇÃO DAS INFORMAÇÕES DE LOTE
                    lote.placa = Transacao001.Placa;
                    lote.chassi = Transacao001.Chassi;
                    lote.renavan = Transacao001.NumeroRenavam;
                    lote.ano_fabricacao = Transacao001.AnoFabricacao;
                    lote.ano_modelo = Transacao001.AnoModelo;
                    lote.uf = Transacao001.Uf;
                    lote.cor = Transacao001.DescricaoCor;
                    lote.marca_modelo = Transacao001.DescricaoMarca;
                    lote.tipo_veiculo = Transacao001.DescricaoTipo;
                    
                    lote.Flag_Transacao = GetProximaTransacao(lote.id);
                    lote.Flag_Agendado = "S";

                    //var id_grv =
                    //    RepositorioGlobal
                    //    .Util
                    //    .ConsultaGenerica(Util.LerConfiguracao("CONEXAO_DP"), 
                    //    string.Format("SELECT id_grv FROM tb_dep_grv WHERE numero_formulario_grv = '{0}'", lote.numero_formulario_grv)).DadoUnico().ToInt32();

                    //lote.id_grv = id_grv;

                    RepositorioGlobal.Lote.Alterar(lote);

                    var id = InserirTransacao(new Transacao() { id_lote = lote.id, retorno = Transacao001.Retorno, transacao = Transacao001.Transacao });

                    Debug.WriteLine(string.Format("TRANSAÇÃO {0} GERADA", id));
                    //Console.WriteLine(lote.numero_formulario_grv);
                    continue;
                }
                else
                {
                    lote.Flag_Agendado = "N";
                    lote.Flag_Transacao = "X";
                    lote.obs_transacao = Transacao001.Retorno;
                    RepositorioGlobal.Lote.Alterar(lote);

                    var id = InserirTransacao(new Transacao() { id_lote = lote.id, retorno = Transacao001.Retorno, transacao = Transacao001.Transacao });

                    continue;
                }
            }
        }

        /// <summary>
        /// TRANSAÇÃO 002 e 006
        /// </summary>
        public void Acautelamento()
        {
            var consulta = @"

              SELECT le.id_status, lo.*
                FROM dbo.tb_leilao_lotes lo
                JOIN tb_leilao le ON  le.id = lo.id_leilao
                JOIN tb_comitentes co ON co.id = le.id_comitente
                JOIN tb_leilao_status st ON le.id_status = st.id
               WHERE lo.flag_transacao = 'R'
                 --AND co.tipo_importacao = 1  --DETRO
                 AND le.id_status = (SELECT id FROM tb_leilao_status WHERE sequencia = 1)    
            ";

            var lotes = ConsultaSQL(consulta).ConverterParaLista<Lote>();

#if (DEBUG)
            //lotes = lotes.Where(l => l.id_leilao == 86).ToList();

            //lotes = lotes.Where(l => l.id_leilao == 79).ToList();
#endif

            foreach (var lote in lotes)
            {
                StringBuilder SQL = new StringBuilder();

                SQL.AppendLine("SELECT id_log, data_cadastro, resposta");

                SQL.AppendLine("  FROM db_link_patios_ws_prod.dbo.tb_log_ws_envioveiculo");

                SQL.AppendLine(string.Format(" WHERE ((placa = '{0}' AND numerogrve = '{1}')   ", lote.placa, lote.numero_formulario_grv));

                SQL.AppendLine(string.Format("   OR (chassi = '{0}' AND numerogrve = '{1}')) ", lote.chassi, lote.numero_formulario_grv));

                SQL.AppendLine("   AND resposta IS NOT NULL");

                SQL.AppendLine("   ORDER BY data_cadastro DESC");

                var dtRetorno = ConsultaSQL(SQL.ToString());

                if (dtRetorno.Rows.Count > 0)   //ACHOU CORRESPONDENCIA
                {
                    var IdLog = dtRetorno.Rows[0][0].ToString();

                    var DataRecolhimento = Convert.ToDateTime(dtRetorno.Rows[0][1].ToString());

                    var Recolhimento = JsonConvert.DeserializeObject<DadosRecolhimento>(dtRetorno.Rows[0][2].ToString());

                    //if (DateTime.Now >= DataRecolhimento.AddDays(3))
                    //{
                    //    lote.Flag_Transacao = GetProximaTransacao(lote.id);
                    //}

                    lote.Flag_Transacao = GetProximaTransacao(lote.id);

                    lote.Log_Recolhimento = Convert.ToInt32(IdLog);

                    lote.Flag_Agendado = "S";

                    RepositorioGlobal.Lote.Alterar(lote);

                    InserirTransacao(new Transacao()
                    {
                        transacao = "002",
                        id_lote = lote.id,
                        retorno = Recolhimento.Recolhimento.Retorno
                    });

                    InserirTransacao(new Transacao()
                    {
                        transacao = "006",
                        id_lote = lote.id,
                        retorno = Recolhimento.Guarda.Retorno
                    });
                }
                else
                {
                    //if (DetectarAmbiente() == Global.Ambiente.TiposAmbiente.Producao)
                    //{
                    //    ws_InclusaoVeiculoPatio("ROOT", lote.placa);
                    //    continue;
                    //}

                    lote.Flag_Agendado = "N";
                    lote.Flag_Transacao = "X";       //SERA MARCADO COMO IMPEDIDO
                    lote.obs_transacao = "DESENVOLVIMENTO - SEM DADOS NA BASE DE RECOLHIMENTO";

                    RepositorioGlobal.Lote.Alterar(lote);

                    //InserirTransacao(new Transacao()
                    //{
                    //    transacao = "002",
                    //    id_lote = lote.id,
                    //    retorno = "DESENVOLVIMENTO - SEM DADOS NA BASE DE RECOLHIMENTO"
                    //});

                    //InserirTransacao(new Transacao()
                    //{
                    //    transacao = "006",
                    //    id_lote = lote.id,
                    //    retorno = "DESENVOLVIMENTO - SEM DADOS NA BASE DE RECOLHIMENTO"
                    //});
                }

                Debug.WriteLine(string.Format("PROCESSO {0} EXECUTADO", lote.numero_formulario_grv));
                Console.WriteLine(string.Format("PROCESSO {0} EXECUTADO", lote.numero_formulario_grv));
            }
        }
        
        /// <summary>
        /// TRANSAÇÃO 005
        /// </summary>
        public void Proprietarios()
        {
            var consulta = @"
                  SELECT lo.*
                    FROM dbo.tb_leilao_lotes lo
                    JOIN tb_leilao le ON  le.id = lo.id_leilao
                    JOIN tb_comitentes co ON co.id = le.id_comitente
                   WHERE lo.flag_transacao = 'L'
                     AND co.tipo_importacao = 1 
                     AND le.id_status = (SELECT id FROM tb_leilao_status WHERE sequencia = 1)        
            ";

            var lotes = ConsultaSQL(consulta).ConverterParaLista<Lote>();

#if (DEBUG)
            //lotes = lotes
            //    .Where(p => p.id_leilao == 79).ToList();
#endif

            foreach (var lote in lotes)
            {
                string retorno = string.Empty;

                if (!string.IsNullOrEmpty(lote.placa))
                {
                    retorno = ws_ConsultarVeiculosParaLeilao("ROOT", lote.placa);
                }
                else if (!string.IsNullOrEmpty(lote.chassi))
                {
                    retorno = ws_ConsultarVeiculosParaLeilao("ROOT", lote.chassi);
                }

                if (retorno == "")
                {
                    continue;
                }
                        

                if (!retorno.ToUpper().Contains("ERRO"))
                {
                    var Transacao005 = JsonConvert.DeserializeObject<Transacao005>(retorno);

                    Transacao005.Id_lote = lote.id;

                    if (Transacao005.Retorno.ToUpper() == "OK")
                    {
                        InserirTransacao(new Transacao()
                        {
                            id_lote = lote.id,
                            retorno = Transacao005.Retorno,
                            transacao = Transacao005.Transacao
                        });

                        Transacao005.Id = RepositorioGlobal.Proprietario.Inserir(Transacao005);

                        lote.Flag_Transacao = GetProximaTransacao(lote.id);
                        lote.Flag_Agendado = "N";


                        //PEGAR RESTRIÇÕES
                        var Restricoes = Transacao005.DescricaoRestricoes.ToList<string>();
                        var SubRestricoes = Transacao005.DescricaoSubRestricoes.ToList<string>();
                        var ObsRestricoes = Transacao005.ObservacaoRestricoes.ToList<string>();

                        for (int i = 0; i < Restricoes.Where(p => p != "").ToList<string>().Count; i++)
                        {
                            lote.id_status_lote = 26;

                            Restricao r = new Restricao()
                            {
                                id_lote = lote.id,
                                restricao = Restricoes[i],
                                sub_restricao = SubRestricoes[i],
                                observacoes = ObsRestricoes[i],
                                origem = Transacao005.Transacao
                            };

                            RepositorioGlobal.Restricao.Inserir(r);
                        }

                        RepositorioGlobal.Lote.Alterar(lote);

                        Console.WriteLine("PROPRIETÁRIO " + lote.marca_modelo + " PROCESSO " + lote.numero_formulario_grv);

                        continue;
                    }

                    InserirTransacao(new Transacao()
                    {
                        id_lote = lote.id,
                        retorno = Transacao005.Retorno,
                        transacao = Transacao005.Transacao
                    });
                    
                    //ALTERAR LOTES DE ACORDO COM A RESTRIÇÃO
                    lote.Flag_Agendado = "N";
                    lote.Flag_Transacao = "X";

                    RepositorioGlobal.Lote.Alterar(lote);
                }
                else
                {

                }
            }
        }

        /// <summary>
        /// TRANSAÇÃO 008 (R/I)
        /// </summary>
        public void CadastrarVistoria()
        {

        }

        /// <summary>
        /// TRANSAÇÃO 009
        /// </summary>
        public void CadastrarExtratoLeilao()
        {

        }

        public List<Transacao> Selecionar_Transacoes_Lote(int id_lote)
        {
            string sql = string.Format("select * from tb_leilao_lotes_transacoes where id_lote = {0}", id_lote);

            return ConsultaSQL(sql).ConverterParaLista<Transacao>();
        }
    }
}
