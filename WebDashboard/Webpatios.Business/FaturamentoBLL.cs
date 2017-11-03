using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WebPatios.Model;

namespace WebPatios.Business
{
    public class FaturamentoBLL : BaseBLL
    {
        public class Coordenadas
        {
            public string ROTULO_X { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
        }

        public class GraficoModel
        {
            public Deposito Deposito { get; set; }
            public List<Coordenadas> CoordenadasGrafico { get; set; }

            public GraficoModel()
            {
                Deposito = new Deposito();
                CoordenadasGrafico = new List<Coordenadas>();
            }
        }

        public FaturamentoBLL()
        {

        }

        public FaturamentoBLL(Model.Deposito filtroDeposito)
        {
            base._idCliente = filtroDeposito.clienteDeposito.idCliente;
            base._idDeposito = filtroDeposito.idDeposito;
        }



        public List<GraficoModel> GetDadosGrafico()
        {
            string SQL;
            SQL = string.Format(@"

            select dp.id_deposito, dp.descricao nome_deposito,
                   cl.id_cliente, cl.nome nome_cliente
              from tb_dep_depositos dp
        inner join tb_dep_clientes_depositos cd on dp.id_deposito = cd.id_deposito
        inner join tb_dep_clientes cl on cd.id_cliente = cl.id_cliente
             where dp.flag_ativo = 'S' and cl.flag_ativo = 'S'
               AND cl.id_cliente = {0}
          order by dp.id_deposito", _idCliente);

            var depositos = new List<GraficoModel>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());


                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow item in tbRes.Rows)
                    {
                        var dep = new GraficoModel();
                        dep.Deposito.clienteDeposito.idCliente = (int)item["id_cliente"];
                        dep.Deposito.clienteDeposito.nomeCliente = item["nome_cliente"].ToString();
                        dep.Deposito.descricaoClienteDeposito = string.Empty;
                        dep.Deposito.descricaoDeposito = item["nome_deposito"].ToString();
                        dep.Deposito.idDeposito = (int)item["id_deposito"];
                        //depositos.Add(dep);

                        #region CONSULTA O FATURAMENTO DESTE DEPOSITO
                        //SQL = string.Format(@" SELECT tb_dep_grv.id_cliente
                        //                            , tb_dep_grv.id_deposito
                        //                            , YEAR(tb_dep_liberacao.data_cadastro) AS ano
                        //                            , MONTH(tb_dep_liberacao.data_cadastro) AS mes            
                        //                            , SUM(tb_dep_faturamento.valor_pagamento) AS faturamento

                        //                       FROM tb_dep_atendimento 
                        //                 INNER JOIN tb_dep_faturamento ON tb_dep_atendimento.id_atendimento = tb_dep_faturamento.id_atendimento 
                        //                 INNER JOIN tb_dep_faturamento_composicao ON tb_dep_faturamento.id_faturamento = tb_dep_faturamento_composicao.id_faturamento 
                        //                 INNER JOIN tb_dep_grv ON tb_dep_atendimento.id_grv = tb_dep_grv.id_grv 
                        //                 INNER JOIN tb_dep_liberacao ON tb_dep_grv.id_liberacao = tb_dep_liberacao.id_liberacao

                        //                       WHERE tb_dep_grv.id_cliente = {0}
                        //                         AND tb_dep_grv.id_deposito = {1}
                        //                   GROUP BY tb_dep_grv.id_cliente
                        //                            , tb_dep_grv.id_deposito
                        //                            , MONTH(tb_dep_liberacao.data_cadastro)
                        //                            , YEAR(tb_dep_liberacao.data_cadastro) 
                        //                   ORDER BY YEAR(tb_dep_liberacao.data_cadastro), MONTH(tb_dep_liberacao.data_cadastro)", dep.Deposito.clienteDeposito.idCliente, dep.Deposito.idDeposito);

                        SQL = string.Format(@" 	    SELECT id_cliente
	                                                     , id_deposito
		                                                 , YEAR(vw_dep_relatorio_liberacao.data_liberacao) AS ano
	    	                                             , MONTH(vw_dep_relatorio_liberacao.data_liberacao) AS mes            
    		                                             , SUM(vw_dep_relatorio_liberacao.valor_pagamento) AS faturamento
                                                  FROM vw_dep_relatorio_liberacao
                                                 WHERE vw_dep_relatorio_liberacao.id_cliente = {0}
                                                   AND vw_dep_relatorio_liberacao.id_deposito = {1}
                                              GROUP BY vw_dep_relatorio_liberacao.id_cliente
                                                       , vw_dep_relatorio_liberacao.id_deposito
                                                       , YEAR(vw_dep_relatorio_liberacao.data_liberacao)
                                                       , MONTH(vw_dep_relatorio_liberacao.data_liberacao)
                                             ORDER BY    YEAR(vw_dep_relatorio_liberacao.data_liberacao) 
                                                       , MONTH(vw_dep_relatorio_liberacao.data_liberacao) ", dep.Deposito.clienteDeposito.idCliente, dep.Deposito.idDeposito);

                        #endregion

                        var tbDep = ConsultaSQL(SQL.ToString());

                        if (tbDep.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow fat in tbDep.Rows)
                            {
                                string mesExtenso = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(int.Parse(fat["mes"].ToString())).ToUpper().Substring(0, 3);

                                Coordenadas Coordenada = new Coordenadas();
                                Coordenada.ROTULO_X = mesExtenso + " - " + fat["ano"].ToString();
                                Coordenada.Y = double.Parse(fat["faturamento"].ToString());

                                dep.CoordenadasGrafico.Add(Coordenada);

                            }
                        }

                        depositos.Add(dep);
                    }
                }
            }
            catch
            {
                throw;
            }



            return depositos;


        }

        public List<GraficoModel> GraficoFaturamento()
        {
            var SQL = string.Format(@" 	    
                SELECT    vw_dep_relatorio_liberacao.id_cliente
	                    , vw_dep_relatorio_liberacao.id_deposito
	                    , tb_dep_clientes.nome nome_cliente
	                    , tb_dep_depositos.descricao nome_deposito
		                , YEAR(vw_dep_relatorio_liberacao.data_liberacao) AS ano
	    	            , MONTH(vw_dep_relatorio_liberacao.data_liberacao) AS mes            
    		            , SUM(vw_dep_relatorio_liberacao.valor_pagamento) AS faturamento
    		            
                FROM  vw_dep_relatorio_liberacao
                INNER JOIN tb_dep_clientes ON tb_dep_clientes.id_cliente = vw_dep_relatorio_liberacao.id_cliente
                INNER JOIN tb_dep_depositos ON tb_dep_depositos.id_deposito = vw_dep_relatorio_liberacao.id_deposito
                
                WHERE vw_dep_relatorio_liberacao.id_cliente = {0}
                
            GROUP BY  vw_dep_relatorio_liberacao.id_cliente
                    , vw_dep_relatorio_liberacao.id_deposito
                    , YEAR(vw_dep_relatorio_liberacao.data_liberacao)
                    , MONTH(vw_dep_relatorio_liberacao.data_liberacao)
                    , tb_dep_clientes.nome
                    , tb_dep_depositos.descricao
                    
            ORDER BY  YEAR(vw_dep_relatorio_liberacao.data_liberacao) 
                    , MONTH(vw_dep_relatorio_liberacao.data_liberacao)", base._idCliente);

            var depositos = new List<GraficoModel>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow item in tbRes.Rows)
                    {
                        var dep = new GraficoModel();
                        dep.Deposito.clienteDeposito.idCliente = (int)item["id_cliente"];
                        dep.Deposito.clienteDeposito.nomeCliente = item["nome_cliente"].ToString();
                        dep.Deposito.descricaoClienteDeposito = string.Empty;
                        dep.Deposito.descricaoDeposito = item["nome_deposito"].ToString();
                        dep.Deposito.idDeposito = (int)item["id_deposito"];

                        string mesExtenso = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(int.Parse(item["mes"].ToString())).ToUpper().Substring(0, 3);

                        Coordenadas Coordenada = new Coordenadas();
                        Coordenada.ROTULO_X = mesExtenso + " - " + item["ano"].ToString();
                        Coordenada.Y = double.Parse(item["faturamento"].ToString());

                        dep.CoordenadasGrafico.Add(Coordenada);


                        depositos.Add(dep);
                    }
                }
            }
            catch
            {
                throw;
            }

            return depositos;
        }

    }
}
