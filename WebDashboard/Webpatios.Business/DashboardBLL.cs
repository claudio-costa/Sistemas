using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPatios.Business
{
    public class DashboardBLL : BaseBLL
    {
        public DashboardBLL()
        {

        }

        public DashboardBLL(Model.Deposito filtroDeposito)
        {
            base._idCliente = filtroDeposito.clienteDeposito.idCliente;
            base._idDeposito = filtroDeposito.idDeposito;
        }
        
        public IList<Model.Dashboard.Infracao> getInfracoes()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"
                        SELECT TOP 10
                            COUNT(tb_dep_condutor.id_grv) AS qtd,
 	                        tb_dep_enquadramento_infracoes.codigo_infracao as cod, tb_dep_enquadramento_infracoes.descricao AS descr
                        FROM tb_dep_condutor 
                    INNER JOIN tb_dep_enquadramento_infracoes 
                            ON tb_dep_condutor.id_enquadramento_infracao = tb_dep_enquadramento_infracoes.id_enquadramento_infracao
                    INNER JOIN tb_dep_grv 
                            ON tb_dep_condutor.id_grv = tb_dep_grv.id_grv
                        WHERE tb_dep_grv.id_cliente = {0} AND tb_dep_grv.id_deposito = {1}
                    GROUP BY tb_dep_enquadramento_infracoes.descricao,
                            tb_dep_enquadramento_infracoes.codigo_infracao
                    ORDER BY COUNT(tb_dep_condutor.id_grv) DESC", _idCliente, _idDeposito);
            #endregion

            var infracoes = new List<Model.Dashboard.Infracao>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var itemInfracao = new Model.Dashboard.Infracao();
                        itemInfracao.infracaoDescricao = linha["descr"].ToString();
                        itemInfracao.infracaoCodigo = linha["cod"].ToString();
                        itemInfracao.quantidade = int.Parse(linha["qtd"].ToString());
                        infracoes.Add(itemInfracao);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return infracoes;
        }

        public Model.Dashboard.ResumoPeriodico getResumoPeriodico()
        {
            #region CONSULTA

            var semana = CCFW.Funcoes.DataUtils.PrimeiroEUltimoDiaDaSemana();
            var primeiroDiaSemana = semana[0].ToShortDateString();
            var ultimoDiaSemana = semana[1].ToShortDateString();

            string SQL;
            SQL = string.Format(@"

            SELECT (SELECT COUNT(grv.id_grv) AS qtd
                      FROM tb_dep_grv grv 
                     WHERE id_cliente = {0} AND id_deposito = {1}) APREENSOES, 
	   
	               (SELECT COUNT(grv.id_grv) AS qtd
	                  FROM tb_dep_grv grv 
		             WHERE id_cliente = {0} AND id_deposito = {1}
		               AND id_status_operacao = 'E') SAIDAS,
       
                   (SELECT COUNT(grv.id_grv) AS qtd
                      FROM tb_dep_grv grv 
                     WHERE id_cliente = {0} AND id_deposito = {1}
                       AND id_status_operacao IN ('1','2','B','G','L','M','P','T' ))ESTOQUE,
       
                   --APRENSOES
                   (SELECT COUNT(grv.id_grv)
                      FROM tb_dep_grv grv 
                     WHERE id_cliente = {0} AND id_deposito = {1}
                       AND convert(date, grv.data_hora_remocao) = convert(date,GETDATE())) APREENSAO_HOJE,
       
                   (SELECT COUNT(grv.id_grv) AS qtd
                      FROM tb_dep_grv grv 
                     WHERE id_cliente = {0} AND id_deposito = {1}
                       AND convert(date, grv.data_hora_remocao) 
                   BETWEEN convert(date,'{2}')
                       AND convert(date, '{3}'))                                          APREENSAO_SEMANA,
        
                    (SELECT COUNT(grv.id_grv)
                       FROM tb_dep_grv grv 
                      WHERE id_cliente = {0} AND id_deposito = {1}
                        AND MONTH(grv.data_hora_remocao) = MONTH(GETDATE())
                        AND YEAR(grv.data_hora_remocao) = YEAR(GETDATE())) APREENSAO_MES,
            
            
                    --SAÍDAS
                    
					(  SELECT COUNT(*) from vw_dep_relatorio_liberacao
					where id_cliente = {0} and id_deposito = {1}
					AND CONVERT(DATE, data_liberacao) = CONVERT(DATE,GETDATE()) ) SAIDAS_HOJE,          


					(  SELECT COUNT(*) from vw_dep_relatorio_liberacao
					where id_cliente = {0} and id_deposito = {1}
					AND CONVERT(DATE, data_liberacao) 
					BETWEEN CONVERT(DATE, '{2}') AND CONVERT(DATE, '{3}') )               SAIDAS_SEMANA,

					(  SELECT COUNT(*) from vw_dep_relatorio_liberacao
					where id_cliente = {0} and id_deposito = {1}
					AND MONTH(data_liberacao) = MONTH(GETDATE()) 
                    AND YEAR(data_liberacao) = YEAR(GETDATE()) ) SAIDAS_MES,
               
                    --ESTOQUE
                   (SELECT COUNT(grv.id_grv)
                      FROM tb_dep_grv grv 
                     WHERE id_cliente = {0} AND id_deposito = {1}
                       AND id_status_operacao IN ('1','2','B','G','L','M','P','T' )
                       AND convert(date, grv.data_hora_remocao) = convert(date,GETDATE())) ESTOQUE_HOJE,
       
                   (SELECT COUNT(grv.id_grv) AS qtd
                      FROM tb_dep_grv grv 
                     WHERE id_cliente = {0} AND id_deposito = {1}
                       AND id_status_operacao IN ('1','2','B','G','L','M','P','T' )
                       AND convert(date, grv.data_hora_remocao) 
                   BETWEEN convert(date, '{2}')
                       AND convert(date, '{3}'))                                          ESTOQUE_SEMANA,
        
                    (SELECT COUNT(grv.id_grv)
                       FROM tb_dep_grv grv 
                      WHERE id_cliente = {0} AND id_deposito = {1}
                        AND id_status_operacao IN ('1','2','B','G','L','M','P','T' )
                        AND MONTH(grv.data_hora_remocao) = MONTH(GETDATE())
                        AND YEAR(grv.data_hora_remocao) = YEAR(GETDATE())) ESTOQUE_MES,
            
            
                    ( SELECT ISNULL(SUM(valor_pagamento), 0) FROM VW_DEP_RELATORIO_LIBERACAO
                       WHERE valor_pagamento > 0
                         AND ID_CLIENTE = {0} AND ID_DEPOSITO = {1}
                         AND CONVERT(DATE, DATA_LIBERACAO) = CONVERT(DATE,GETDATE()) ) FAT_HOJE,

		            ( SELECT ISNULL(SUM(valor_pagamento), 0) FROM VW_DEP_RELATORIO_LIBERACAO
		               WHERE valor_pagamento > 0
		                 AND ID_CLIENTE = {0} AND ID_DEPOSITO = {1}
		                 AND CONVERT(DATE, DATA_LIBERACAO) 
	                 BETWEEN CONVERT(DATE, '{2}') AND CONVERT(DATE, '{3}') ) FAT_SEMANA,

		            ( SELECT ISNULL(SUM(valor_pagamento), 0) FROM VW_DEP_RELATORIO_LIBERACAO
		               WHERE valor_pagamento > 0
		                 AND ID_CLIENTE = {0} AND ID_DEPOSITO = {1}
		                 AND MONTH(DATA_LIBERACAO) = MONTH(GETDATE())
                        AND YEAR(DATA_LIBERACAO) = YEAR(GETDATE())) FAT_MES
              FROM DUAL

            ", _idCliente, _idDeposito, primeiroDiaSemana, ultimoDiaSemana);
            #endregion

            var resumo = new Model.Dashboard.ResumoPeriodico();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    resumo.apreensoesTotal = int.Parse(tbRes.Rows[0]["APREENSOES"].ToString());
                    resumo.saidasTotal = int.Parse(tbRes.Rows[0]["SAIDAS"].ToString());
                    resumo.estoqueTotal = int.Parse(tbRes.Rows[0]["ESTOQUE"].ToString());

                    resumo.apreensoesHoje = int.Parse(tbRes.Rows[0]["APREENSAO_HOJE"].ToString());
                    resumo.apreensoesSemana = int.Parse(tbRes.Rows[0]["APREENSAO_SEMANA"].ToString());
                    resumo.apreensoesMes = int.Parse(tbRes.Rows[0]["APREENSAO_MES"].ToString());

                    resumo.saidasHoje = int.Parse(tbRes.Rows[0]["SAIDAS_HOJE"].ToString());
                    resumo.saidasSemana = int.Parse(tbRes.Rows[0]["SAIDAS_SEMANA"].ToString());
                    resumo.saidasMes = int.Parse(tbRes.Rows[0]["SAIDAS_MES"].ToString());

                    resumo.estoqueHoje = int.Parse(tbRes.Rows[0]["ESTOQUE_HOJE"].ToString());
                    resumo.estoqueSemana = int.Parse(tbRes.Rows[0]["ESTOQUE_SEMANA"].ToString());
                    resumo.estoqueMes = int.Parse(tbRes.Rows[0]["ESTOQUE_MES"].ToString());

                    resumo.FaturamentoHoje = string.Format("{0:C}", Double.Parse(tbRes.Rows[0]["FAT_HOJE"].ToString()));
                    resumo.FaturamentoSemana = string.Format("{0:C}", Double.Parse(tbRes.Rows[0]["FAT_SEMANA"].ToString()));
                    resumo.FaturamentoMes = string.Format("{0:C}", Double.Parse(tbRes.Rows[0]["FAT_MES"].ToString()));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return resumo;
        }

        public Model.Dashboard.TicketMedio getTicketMedio()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"
                SELECT ISNULL(FATURAMENTO / APREENSOES, 0) AS TICKET_MEDIO
                  FROM (	SELECT SUM(valor_pagamento) AS FATURAMENTO, COUNT(*) AS APREENSOES 
                              FROM vw_dep_relatorio_liberacao
						     WHERE id_cliente = {0} AND id_deposito = {1}
							   AND MONTH(data_liberacao) = MONTH(GETDATE())
                               AND YEAR(data_liberacao) = YEAR(GETDATE())) AS TB

            ", _idCliente, _idDeposito);
            #endregion

            var tm = new Model.Dashboard.TicketMedio() { ticketMedio = string.Format("{0:C}", 0) };

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    tm.ticketMedio = string.Format("{0:C}", Double.Parse(tbRes.Rows[0]["TICKET_MEDIO"].ToString()));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tm;
        }

        public Model.Dashboard.TaxaLiberacao getTaxaLiberacao()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"
               			SELECT (SELECT ISNULL(COUNT(*), 0)
			                      FROM VW_DEP_RELATORIO_LIBERACAO	       
			                     WHERE ID_CLIENTE = {0} 
                                   AND ID_DEPOSITO = {1}
			                       AND MONTH(DATA_LIBERACAO) = MONTH(GETDATE()) 
                                   AND year(DATA_LIBERACAO) = year(GETDATE())) AS ENT,

			                   (SELECT COUNT(GRV.ID_GRV)
			                      FROM TB_DEP_GRV GRV 
			                     WHERE ID_CLIENTE = {0}
                                   AND ID_DEPOSITO = {1}
			                       AND MONTH(GRV.data_hora_remocao) = MONTH(GETDATE())
			                       AND YEAR(GRV.data_hora_remocao) = YEAR(GETDATE())) AS APR 
			              FROM DUAL", _idCliente, _idDeposito);
                         // FROM DUAL", _idCliente, _idDeposito);
            #endregion

            var taxa = new Model.Dashboard.TaxaLiberacao();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    double entregas = 0;
                    double apreensoes = 0;
                    double tx_entregas = 0;
                    double tx_apreensoes = 0;

                    entregas = Double.Parse(tbRes.Rows[0]["ENT"].ToString());
                    apreensoes = Double.Parse(tbRes.Rows[0]["APR"].ToString());
                    

                    tx_entregas = entregas / apreensoes;
                    tx_apreensoes = 1 - tx_entregas;

                    taxa.taxaLiberacaoNumeral = Math.Round(tx_entregas * 100);
                    taxa.taxaLiberacao = string.Format("{0:P2}", tx_entregas);
                    taxa.taxaApreensao = string.Format("{0:P2}", tx_apreensoes);

                    if (taxa.taxaApreensao == "NaN")
                    {
                        taxa.taxaApreensao = string.Format("{0:P2}", 0);
                    }

                    if (taxa.taxaLiberacao == "NaN")
                    {
                        taxa.taxaLiberacao = string.Format("{0:P2}", 0);
                    }

                    


                }
            }
            catch (Exception)
            {
                throw;
            }

            return taxa;
        }

        public IList<Model.Deposito> getDadosClientedeposito()
        {
            #region CONSULTA
            string SQL = string.Empty;
                        SQL = string.Format(@"
                        select dp.id_deposito, dp.descricao, 
                               cl.id_cliente, cl.nome
                          from tb_dep_depositos dp
                    inner join tb_dep_clientes_depositos cd on dp.id_deposito = cd.id_deposito
                    inner join tb_dep_clientes cl on cd.id_cliente = cl.id_cliente
                         where dp.flag_ativo = 'S' and cl.flag_ativo = 'S'
                      order by dp.id_deposito
            ");
            #endregion

            var depositos = new List<Model.Deposito>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow item in tbRes.Rows)
                    {
                        var dep = new Model.Deposito();

                        dep.clienteDeposito.idCliente = (int)item["id_cliente"];
                        dep.clienteDeposito.nomeCliente = item["nome"].ToString();

                        dep.descricaoClienteDeposito = string.Empty;
                        dep.descricaoDeposito = item["descricao"].ToString();
                        dep.idDeposito = (int)item["id_deposito"];

                        depositos.Add(dep);
                    }
                    
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }

            return depositos;
            
        }
    }
}
