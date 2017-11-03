using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPatios.Business
{
    public class DiretoriaBLL : BaseBLL
    {
        public DiretoriaBLL()
        {

        }

        public DiretoriaBLL(Model.Deposito filtroDeposito)
        {
            base._idCliente = filtroDeposito.clienteDeposito.idCliente;
            base._idDeposito = filtroDeposito.idDeposito;
        }


        public struct EvolucaoPorEstadoModel
        {
            public int ANO { get; set; }
            public int MES { get; set; }
            public double FATURAMENTO { get; set; }
            public string UF { get; set; }
        }

        //PATIO, TOTAL

        public struct EvolucaoPorPatio
        {
            public int ID_CLIENTE { get; set; }
            public int ID_DEPOSITO { get; set; }
            public int ANO { get; set; }
            public int MES { get; set; }
            public string PATIO { get; set; }
            public double FATURAMENTO { get; set; }
        }

        public List<EvolucaoPorEstadoModel> GraficoEvolucaoMensalEstado()
        {
            var SQL = new StringBuilder();
            
            SQL.AppendLine("            SELECT uf, ano, mes, count(*) qtd_depositos, sum(pagamento) total FROM                                                                             ");
            SQL.AppendLine("			(                                                                                                                                                  ");
            SQL.AppendLine("			SELECT         vw_dep_relatorio_liberacao.id_cliente                                                                                               ");
            SQL.AppendLine("						 , vw_dep_relatorio_liberacao.id_deposito                                                                                              ");
            SQL.AppendLine("						 , YEAR(vw_dep_relatorio_liberacao.data_liberacao) AS ano                                                                              ");
            SQL.AppendLine("						 , MONTH(vw_dep_relatorio_liberacao.data_liberacao) AS mes                                                                             ");
            SQL.AppendLine("						 , SUM(vw_dep_relatorio_liberacao.valor_pagamento) AS pagamento                                                                        ");
            SQL.AppendLine("						 , db_global.dbo.tb_glo_loc_municipios.uf                                                                                              ");
            SQL.AppendLine("                                                                                                                                                               ");
            SQL.AppendLine("                                                                                                                                                               ");
            SQL.AppendLine("				  FROM  vw_dep_relatorio_liberacao                                                                                                             ");
            SQL.AppendLine("			INNER JOIN tb_dep_clientes ON vw_dep_relatorio_liberacao.id_cliente = tb_dep_clientes.id_cliente                                                   ");
            SQL.AppendLine("			INNER JOIN db_global.dbo.tb_glo_loc_cep ON tb_dep_clientes.id_cep = db_global.dbo.tb_glo_loc_cep.id_cep                                            ");
            SQL.AppendLine("			INNER JOIN db_global.dbo.tb_glo_loc_municipios ON db_global.dbo.tb_glo_loc_cep.id_municipio = db_global.dbo.tb_glo_loc_municipios.id_municipio     ");

            SQL.AppendLine("      WHERE YEAR(vw_dep_relatorio_liberacao.data_liberacao) = YEAR(getdate())");
            SQL.AppendLine("        AND MONTH(vw_dep_relatorio_liberacao.data_liberacao) >= MONTH(GETDATE()) - 3");

            SQL.AppendLine("			GROUP BY                                                                                                                                           ");
            SQL.AppendLine("                                                                                                                                                               ");
            SQL.AppendLine("						vw_dep_relatorio_liberacao.id_cliente                                                                                                  ");
            SQL.AppendLine("					  , vw_dep_relatorio_liberacao.id_deposito                                                                                                 ");
            SQL.AppendLine("					  , YEAR(vw_dep_relatorio_liberacao.data_liberacao)                                                                                        ");
            SQL.AppendLine("					  , MONTH(vw_dep_relatorio_liberacao.data_liberacao)                                                                                       ");
            SQL.AppendLine("					  , db_global.dbo.tb_glo_loc_municipios.uf                                                                                                 ");
            SQL.AppendLine("                                                                                                                                                               ");
            SQL.AppendLine("			HAVING(db_global.dbo.tb_glo_loc_municipios.uf <> 'RJ')                                                                                             ");
            SQL.AppendLine("			--ORDER BY db_global.dbo.tb_glo_loc_municipios.uf, ano, mes                                                                                        ");
            SQL.AppendLine("			) tb                                                                                                                                               ");
            SQL.AppendLine("			GROUP BY uf, ano, mes                                                                                                                              ");
            SQL.AppendLine("			ORDER BY uf                                                                                                                                        ");

            var RETORNO = new List<EvolucaoPorEstadoModel>();

            var tbRES = ConsultaSQL(SQL.ToString());

            if (tbRES.Rows.Count > 0)
            {
                foreach (System.Data.DataRow fat in tbRES.Rows)
                {
                    EvolucaoPorEstadoModel EEM = new EvolucaoPorEstadoModel();

                    EEM.ANO = CCFW.Conversoes.ConversaoSegura<int>(fat["ANO"], 0);
                    EEM.FATURAMENTO = CCFW.Conversoes.ConversaoSegura<double>(fat["TOTAL"], 0);
                    EEM.MES = CCFW.Conversoes.ConversaoSegura<int>(fat["MES"], 0);
                    EEM.UF = CCFW.Conversoes.ConversaoSegura<string>(fat["UF"], "");


                    RETORNO.Add(EEM);

                }
            }

            return RETORNO;
        }

        public List<EvolucaoPorPatio> GraficoEvolucaoMensalPatio()
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("            select    id_cliente                                                                                                                         ");
            SQL.AppendLine("            		, id_deposito                                                                                                                        ");
            SQL.AppendLine("            		, ano                                                                                                                                ");
            SQL.AppendLine("            		, mes                                                                                                                                ");
            SQL.AppendLine("            		, cliente_nome +' - ' + deposito_nome patio                                                                                          ");
            SQL.AppendLine("                    , sum(pagamento) FATURAMENTO                                                                                                         ");
            SQL.AppendLine("                                                                                                                                                         ");
            SQL.AppendLine("              from	(                                                                                                                                    ");
            SQL.AppendLine("   			select   vw_dep_relatorio_liberacao.id_cliente                                                                                               ");
            SQL.AppendLine("   				   , vw_dep_relatorio_liberacao.id_deposito                                                                                              ");
            SQL.AppendLine("   				   , year(vw_dep_relatorio_liberacao.data_liberacao) as ano                                                                              ");
            SQL.AppendLine("   				   , month(vw_dep_relatorio_liberacao.data_liberacao) as mes                                                                             ");
            SQL.AppendLine("   				   , sum(vw_dep_relatorio_liberacao.valor_pagamento) as pagamento                                                                        ");
            SQL.AppendLine("   				   , db_global.dbo.tb_glo_loc_municipios.uf                                                                                              ");
            SQL.AppendLine("   				   , vw_dep_clientes_depositos.deposito_nome                                                                                             ");
            SQL.AppendLine("   				   , vw_dep_clientes_depositos.cliente_nome                                                                                              ");
            SQL.AppendLine("   																																						 ");
            SQL.AppendLine("   																																						 ");
            SQL.AppendLine("   			  from  vw_dep_relatorio_liberacao                                                                                                           ");
            SQL.AppendLine("   		inner join tb_dep_clientes on vw_dep_relatorio_liberacao.id_cliente = tb_dep_clientes.id_cliente                                                 ");
            SQL.AppendLine("   		inner join vw_dep_clientes_depositos on vw_dep_relatorio_liberacao.id_cliente = vw_dep_clientes_depositos.id_cliente                             ");
            SQL.AppendLine("   			   and vw_dep_relatorio_liberacao.id_deposito = vw_dep_clientes_depositos.id_deposito                                                        ");
            SQL.AppendLine("   		inner join db_global.dbo.tb_glo_loc_cep on tb_dep_clientes.id_cep = db_global.dbo.tb_glo_loc_cep.id_cep                                          ");
            SQL.AppendLine("   		inner join db_global.dbo.tb_glo_loc_municipios on db_global.dbo.tb_glo_loc_cep.id_municipio = db_global.dbo.tb_glo_loc_municipios.id_municipio   ");
            SQL.AppendLine("   		                                                                                                                                                 ");
            SQL.AppendLine("   		  group by	vw_dep_relatorio_liberacao.id_cliente                                                                                                ");
            SQL.AppendLine("   				  , vw_dep_relatorio_liberacao.id_deposito                                                                                               ");
            SQL.AppendLine("   				  , year(vw_dep_relatorio_liberacao.data_liberacao)                                                                                      ");
            SQL.AppendLine("   				  , month(vw_dep_relatorio_liberacao.data_liberacao)                                                                                     ");
            SQL.AppendLine("   				  , db_global.dbo.tb_glo_loc_municipios.uf                                                                                               ");
            SQL.AppendLine("   				  , vw_dep_clientes_depositos.deposito_nome                                                                                              ");
            SQL.AppendLine("   				  , vw_dep_clientes_depositos.cliente_nome                                                                                               ");
            SQL.AppendLine("   		                                                                                                                                                 ");
            SQL.AppendLine("   																																						 ");
            SQL.AppendLine("   		having(db_global.dbo.tb_glo_loc_municipios.uf <> 'rj')) tb                                                                                       ");

            SQL.AppendLine("   where ano = 2017                                                                                                                                      ");
            SQL.AppendLine("      and mes = 5                                                                                                                                        ");
            
            SQL.AppendLine("   group by ano, mes, id_deposito, deposito_nome, id_cliente, cliente_nome                                                                               ");
            SQL.AppendLine("   order by FATURAMENTO  DESC                                                                                                                            ");

            var RETORNO = new List<EvolucaoPorPatio>();

            var tbRES = ConsultaSQL(SQL.ToString());

            if (tbRES.Rows.Count > 0)
            {
                foreach (System.Data.DataRow fat in tbRES.Rows)
                {
                    EvolucaoPorPatio EP = new EvolucaoPorPatio();

                    EP.ID_CLIENTE = CCFW.Conversoes.ConversaoSegura<int>(fat["ID_CLIENTE"], 0);
                    EP.ID_DEPOSITO = CCFW.Conversoes.ConversaoSegura<int>(fat["ID_DEPOSITO"], 0);
                    EP.ANO = CCFW.Conversoes.ConversaoSegura<int>(fat["ANO"], 0);
                    EP.MES = CCFW.Conversoes.ConversaoSegura<int>(fat["MES"], 0);
                    EP.PATIO = CCFW.Conversoes.ConversaoSegura<string>(fat["PATIO"], "");
                    EP.FATURAMENTO = CCFW.Conversoes.ConversaoSegura<double>(fat["FATURAMENTO"], 0);

                    RETORNO.Add(EP);
                }
            }

            return RETORNO;
        }
    }
}
