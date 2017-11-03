using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MobLink.WebLeilao.Repositorio
{
    public class GRVRepositorio : DbSqlServer, ICrud<GRV, int>
    {
        protected internal GRVRepositorio() : base(Util.DetectarConexao())
        {

        }
                                
        public int Inserir(GRV entidade, int Leilao)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO dbo.tb_leilao_lotes (");
            SQL.AppendLine("id_leilao,");
            SQL.AppendLine("numero_lote,");
            SQL.AppendLine("placa,");
            SQL.AppendLine("chassi,");
            SQL.AppendLine("id_status_lote,");
            SQL.AppendLine("marca_modelo,");
            SQL.AppendLine("cor,");            
            SQL.AppendLine("tipo_veiculo,");
            SQL.AppendLine("patio,");
            SQL.AppendLine("numero_formulario_grv,");
            SQL.AppendLine("id_grv");
            SQL.AppendLine(", municipio");
            SQL.AppendLine(", uf");


            SQL.AppendLine(") VALUES (");

            SQL.AppendFormat("{0}", Leilao);
            
            SQL.AppendFormat(",'{0}'", entidade.lote);
            SQL.AppendFormat(",'{0}'", entidade.placa);
            SQL.AppendFormat(",'{0}'", entidade.chassi);

            var idStatus = RecuperarStatusLote(entidade.status);

            if (!string.IsNullOrEmpty(idStatus))
                SQL.AppendFormat(",'{0}'", idStatus);
            else
                SQL.AppendLine(",'17'");                            //STATUS IMPORTADO
            
            SQL.AppendFormat(",'{0}'", entidade.marca_modelo);
            SQL.AppendFormat(",'{0}'", entidade.cor);
            SQL.AppendFormat(",'{0}'", entidade.tipo_veiculo);
            SQL.AppendFormat(",'{0}'", entidade.id_deposito);
            SQL.AppendFormat(",'{0}'", entidade.numero_formulario_grv);
            SQL.AppendFormat(",'{0}'", entidade.id_grv);
            SQL.AppendFormat(",'{0}'", entidade.municipio);
            SQL.AppendFormat(",'{0}')", entidade.uf);


            return ExecutaSQL(SQL.ToString());
        }

        private string RecuperarStatusLote(string DescricaoStatus)
        {
            if (string.IsNullOrEmpty(DescricaoStatus))
            {
                return string.Empty;
            }

            StringBuilder SQL = new StringBuilder();

            SQL.AppendFormat("SELECT id FROM tb_lotes_status WHERE correlacao_dsin = '{0}'", DescricaoStatus.Trim().ToUpper());

            var tb = ConsultaSQL(SQL.ToString());

            if (tb.Rows.Count > 0)
            {
                return tb.Rows[0][0].ToString();
            }
            else
                return string.Empty;
        }
        
        public IList<GRV> SelecionarTudo(int cliente = 0, string deposito = "", int numLotes = 0, int numDias = 0, int idGrv = 0, string data = "", string grvs = "")
        {
            if (cliente == 0 && deposito == "" && numLotes == 0 && numDias == 0 && idGrv == 0 && grvs == "")
            {
                return new List<GRV>();
            }

            #region CONSULTA
            StringBuilder SQL = new StringBuilder();

            #region QUERY ALTERADA CRISTINEY - 15/05/2017
            //SQL.AppendLine("         SELECT tb_dep_grv.numero_formulario_grv, tb_dep_grv.placa, tb_dep_grv.chassi, ");
            //SQL.AppendLine("		        db_link_patios_ws_prod.dbo.tb_detran_marca_modelo.descricao AS marca_modelo, ");
            //SQL.AppendLine("	            tb_dep_tipo_veiculos.descricao AS tipo_veiculo, db_global.dbo.tb_glo_sys_cores.descricao AS cor, ");
            //SQL.AppendLine("				tb_dep_grv.flag_comboio, tb_dep_reboques.placa AS reboque, tb_dep_grv.data_hora_remocao, ");
            //SQL.AppendLine("				tb_dep_grv.data_hora_guarda, tb_dep_grv.id_status_operacao AS status, tb_dep_clientes.nome AS nome_cliente, ");
            //SQL.AppendLine("	            tb_dep_depositos.descricao AS nome_deposito, tb_dep_grv.id_grv, tb_dep_grv.id_tarifa_tipo_veiculo, ");
            //SQL.AppendLine("				tb_dep_grv.id_cliente, tb_dep_grv.id_deposito, tb_dep_grv.id_reboquista, tb_dep_grv.id_reboque, ");
            //SQL.AppendLine("				tb_dep_grv.id_autoridade_responsavel, tb_dep_grv.id_cor, tb_dep_grv.id_detran_marca_modelo, ");
            //SQL.AppendLine("	            tb_dep_grv.data_cadastro");
            //SQL.AppendLine("           FROM dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv");
            //SQL.AppendLine("     INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_clientes ON tb_dep_grv.id_cliente = tb_dep_clientes.id_cliente ");
            //SQL.AppendLine("LEFT OUTER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_reboques ON tb_dep_grv.id_reboque = tb_dep_reboques.id_reboque ");
            //SQL.AppendLine("     INNER JOIN db_link_patios_ws_prod.dbo.tb_detran_marca_modelo ON tb_dep_grv.id_detran_marca_modelo = db_link_patios_ws_prod.dbo.tb_detran_marca_modelo.id_detran_marca_modelo ");
            //SQL.AppendLine("     INNER JOIN db_global.dbo.tb_glo_sys_cores ON tb_dep_grv.id_cor = db_global.dbo.tb_glo_sys_cores.id_cor ");
            //SQL.AppendLine("     INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_depositos ON tb_dep_grv.id_deposito = tb_dep_depositos.id_deposito ");


            //SQL.AppendLine("     LEFT JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_tarifas_tipo_veiculos ON tb_dep_grv.id_tarifa_tipo_veiculo = tb_dep_tarifas_tipo_veiculos.id_tarifa_tipo_veiculo ");
            //SQL.AppendLine("     LEFT JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_tipo_veiculos ON tb_dep_tarifas_tipo_veiculos.id_tipo_veiculo = tb_dep_tipo_veiculos.id_tipo_veiculo");

            //SQL.AppendLine("WHERE 1 = 1");

            //if (!string.IsNullOrWhiteSpace(grvs))
            //    SQL.AppendFormat("AND tb_dep_grv.id_grv in ({0})", grvs);
            //else
            //    if (idGrv != 0) SQL.AppendFormat("AND tb_dep_grv.id_grv = {0}", idGrv);

            ////IMPEDIR UTILIZAÇÃO DE LOTES JÁ CADASTRADOS
            ////SQL.AppendLine("AND tb_dep_grv.placa NOT IN(SELECT placa FROM tb_leilao_lotes)");
            ////SQL.AppendLine("AND tb_dep_grv.chassi NOT IN(SELECT chassi FROM tb_leilao_lotes)");

            //SQL.AppendLine("      AND tb_dep_grv.id_grv NOT IN(SELECT id_grv                                                                                        ");
            //SQL.AppendLine("                                     FROM tb_leilao_lotes                                                                               ");
            //SQL.AppendLine("                                    INNER JOIN tb_leilao ON tb_leilao_lotes.id_leilao = tb_leilao.id AND id_status IN(1, 2, 3, 4, 5, 6) ");
            //SQL.AppendLine("                                    WHERE id_grv = tb_dep_grv.id_grv)                                                                   ");

            //SQL.AppendLine("GROUP BY tb_dep_grv.numero_formulario_grv, tb_dep_grv.placa, tb_dep_grv.chassi, db_link_patios_ws_prod.dbo.tb_detran_marca_modelo.descricao, ");
            //SQL.AppendLine(" 		 tb_dep_tipo_veiculos.descricao, db_global.dbo.tb_glo_sys_cores.descricao, tb_dep_grv.flag_comboio, tb_dep_reboques.placa, tb_dep_grv.data_hora_remocao, ");
            //SQL.AppendLine("		 tb_dep_grv.data_hora_guarda, tb_dep_grv.id_status_operacao, tb_dep_clientes.nome, tb_dep_depositos.descricao, tb_dep_grv.id_grv, ");
            //SQL.AppendLine("		 tb_dep_grv.id_tarifa_tipo_veiculo, tb_dep_grv.id_cliente, tb_dep_grv.id_deposito, tb_dep_grv.id_reboquista, tb_dep_grv.id_reboque, ");
            //SQL.AppendLine("		 tb_dep_grv.id_autoridade_responsavel, tb_dep_grv.id_cor, tb_dep_grv.id_detran_marca_modelo, tb_dep_grv.data_cadastro ");

            //if (cliente != 0 && deposito != "" && numDias != 0)
            //{
            //    SQL.AppendFormat("HAVING (tb_dep_grv.id_cliente = {0})", cliente);
            //    SQL.AppendFormat("   AND (tb_dep_grv.id_deposito in ({0}))", deposito);
            //    SQL.AppendLine("                 AND (tb_dep_grv.id_status_operacao in('G', 'P','L','M', 'T') )");
            //    //SQL.AppendFormat(string.Format("   AND DATEDIFF( DAY , data_hora_remocao, getdate() ) <= {0} ", numDias));
            //    //SQL.AppendFormat(string.Format("   AND DATEDIFF( DAY , data_hora_remocao, '{0}' ) >= {1} ", Convert.ToDateTime(data).ToString("MM/dd/yyyy"), numDias));
            //    SQL.AppendFormat(string.Format("   AND DATEDIFF( DAY , data_hora_remocao, '{0}' ) >= {1} ", Convert.ToDateTime(data).ToString("MM/dd/yyyy"), numDias));
            //}

            //SQL.AppendLine("ORDER BY tb_dep_grv.data_hora_remocao DESC");
            #endregion

            #region ALTERAÇÃO DE CONSULTA - 07/06/2017 - DUPLICAÇÃO DE REGISTROS
            //SQL.AppendLine("            SELECT tb_dep_grv.numero_formulario_grv, tb_dep_grv.placa, tb_dep_grv.chassi,                                             ");
            //SQL.AppendLine("	                ve.marca_modelo, ve.tipo_veiculo, ve.cor, tb_dep_grv.flag_comboio,                                                ");
            //SQL.AppendLine("					tb_dep_grv.data_hora_remocao, tb_dep_grv.data_hora_guarda, tb_dep_grv.id_status_operacao AS status, 			  ");
            //SQL.AppendLine("					tb_dep_grv.id_grv, tb_dep_grv.id_tarifa_tipo_veiculo,                                                             ");
            //SQL.AppendLine("					tb_dep_grv.id_cliente, tb_dep_grv.id_deposito, tb_dep_grv.id_reboquista, tb_dep_grv.id_reboque,                   ");
            //SQL.AppendLine("					tb_dep_grv.id_autoridade_responsavel, tb_dep_grv.id_cor, tb_dep_grv.id_detran_marca_modelo,                       ");
            //SQL.AppendLine("					tb_dep_grv.data_cadastro                                                                                          ");
            //SQL.AppendLine("                                                                                                                                      ");
            //SQL.AppendLine("			   FROM dbMobLinkDepositoPublicoProducao.dbo.vw_estoque_veiculos ve                                                       ");
            //SQL.AppendLine("			   JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv_enquadramento_infracoes                                           ");
            //SQL.AppendLine("			     ON tb_dep_grv_enquadramento_infracoes.id_grv = ve.id_grv                                                             ");
            //SQL.AppendLine("			   JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv tb_dep_grv                                                        ");
            //SQL.AppendLine("			     ON tb_dep_grv.id_grv = tb_dep_grv_enquadramento_infracoes.id_grv                                                     ");
            //SQL.AppendLine("			   JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes                                                      ");
            //SQL.AppendLine("			     ON tb_dep_status_operacoes.id_status_operacao = tb_dep_grv.id_status_operacao                                        ");
            //SQL.AppendLine("			  WHERE ve.status IN ('G', 'P', 'L', 'M', 'T', '2','3', '4', '5', '6')                                                    ");
            //SQL.AppendFormat("			    AND tb_dep_grv.id_deposito IN ({0})", deposito);
            //SQL.AppendFormat("			    AND tb_dep_grv.id_cliente IN ({0})", cliente);
            //SQL.AppendLine("			    AND DATEDIFF(dd, tb_dep_grv.data_hora_remocao, CONVERT(datetime, '22/06/2017', 103)) >= 60                            ");
            //SQL.AppendLine("			    AND dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes.id_status_operacao <> '7' -- 'Leilão - Entregue'     ");
            #endregion

            SQL.AppendLine("    SELECT tb_dep_grv.numero_formulario_grv                                                                                    ");
            SQL.AppendLine("           , tb_dep_grv.placa                                                                                                  ");
            SQL.AppendLine("           , tb_dep_grv.chassi                                                                                                 ");
            SQL.AppendLine("           , ve.marca_modelo                                                                                                   ");
            SQL.AppendLine("           , ve.tipo_veiculo                                                                                                   ");
            SQL.AppendLine("           , ve.cor                                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.flag_comboio                                                                                           ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_remocao                                                                                      ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_guarda                                                                                       ");
            SQL.AppendLine("           , tb_dep_grv.id_status_operacao AS status                                                                           ");
            SQL.AppendLine("           , tb_dep_grv.id_grv                                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_tarifa_tipo_veiculo                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_cliente                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.id_deposito                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.id_reboquista                                                                                          ");
            SQL.AppendLine("           , tb_dep_grv.id_reboque                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.id_autoridade_responsavel                                                                              ");
            SQL.AppendLine("           , tb_dep_grv.id_cor                                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_detran_marca_modelo                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.data_cadastro                                                                                          ");
            SQL.AppendLine("           , tb_dep_grv.municipio  ");
            SQL.AppendLine("           , tb_dep_grv.uf   ");
            
           
            SQL.AppendLine("      FROM dbMobLinkDepositoPublicoProducao.dbo.vw_estoque_veiculos ve                                                         ");
            SQL.AppendLine("INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv_enquadramento_infracoes                                             ");
            SQL.AppendLine("        ON tb_dep_grv_enquadramento_infracoes.id_grv = ve.id_grv                                                               ");
            SQL.AppendLine("INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv tb_dep_grv                                                          ");
            SQL.AppendLine("        ON tb_dep_grv.id_grv = tb_dep_grv_enquadramento_infracoes.id_grv                                                       ");
            SQL.AppendLine("INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes                                                        ");
            SQL.AppendLine("        ON tb_dep_status_operacoes.id_status_operacao = tb_dep_grv.id_status_operacao                                          ");
            SQL.AppendLine("     WHERE ve.status IN ('G', 'P', 'L', 'M', 'T', '2','3', '4', '5', '6')                                                      ");

            if (cliente != 0 && deposito != "" && numDias != 0)
            {
                SQL.AppendFormat("       AND tb_dep_grv.id_cliente IN ({0})", cliente);
                SQL.AppendFormat("       AND tb_dep_grv.id_deposito IN ({0})", deposito);
                SQL.AppendFormat("       AND DATEDIFF(dd, tb_dep_grv.data_hora_remocao, CONVERT(datetime, '{0}', 103)) >= {1}", data, numDias);
            }

            if (!string.IsNullOrWhiteSpace(grvs))
                SQL.AppendFormat("AND tb_dep_grv.id_grv in ({0})", grvs);
            else if (idGrv != 0)
                SQL.AppendFormat("AND tb_dep_grv.id_grv = {0}", idGrv);

            SQL.AppendLine("       AND dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes.id_status_operacao <> '7' -- 'Leilão - Entregue'       ");
            SQL.AppendLine("  GROUP BY tb_dep_grv.numero_formulario_grv                                                                                    ");
            SQL.AppendLine("           , tb_dep_grv.placa                                                                                                  ");
            SQL.AppendLine("           , tb_dep_grv.chassi                                                                                                 ");
            SQL.AppendLine("           , ve.marca_modelo                                                                                                   ");
            SQL.AppendLine("           , ve.tipo_veiculo                                                                                                   ");
            SQL.AppendLine("           , ve.cor                                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.flag_comboio                                                                                           ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_remocao                                                                                      ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_guarda                                                                                       ");
            SQL.AppendLine("           , tb_dep_grv.id_status_operacao                                                                                     ");
            SQL.AppendLine("           , tb_dep_grv.id_grv                                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_tarifa_tipo_veiculo                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_cliente                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.id_deposito                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.id_reboquista                                                                                          ");
            SQL.AppendLine("           , tb_dep_grv.id_reboque                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.id_autoridade_responsavel                                                                              ");
            SQL.AppendLine("           , tb_dep_grv.id_cor                                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_detran_marca_modelo                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.data_cadastro                                                                                          ");
            SQL.AppendLine("           , tb_dep_grv.municipio  ");
            SQL.AppendLine("           , tb_dep_grv.uf   ");
            SQL.AppendLine("  ORDER BY tb_dep_grv.numero_formulario_grv                                                                                    ");

            



            #endregion
            
            if (numLotes != 0)
            {
                var lista = ConsultaSQL(SQL.ToString()).ConverterParaLista<GRV>().Take(numLotes).ToList();
                return lista;
            }

            var lista2 = ConsultaSQL(SQL.ToString()).ConverterParaLista<GRV>();
            return lista2; 
        }

        public IList<GRV> SelecionarTudoFormularioGRV(int cliente = 0, string deposito = "", int numLotes = 0, int numDias = 0, int idGrv = 0, string data = "", string grvs = "")
        {
            if (cliente == 0 && deposito == "" && numLotes == 0 && numDias == 0 && idGrv == 0 && grvs == "")
            {
                return new List<GRV>();
            }

            #region CONSULTA
            StringBuilder SQL = new StringBuilder();

            #region QUERY ALTERADA CRISTINEY - 15/05/2017
            //SQL.AppendLine("         SELECT tb_dep_grv.numero_formulario_grv, tb_dep_grv.placa, tb_dep_grv.chassi, ");
            //SQL.AppendLine("		        db_link_patios_ws_prod.dbo.tb_detran_marca_modelo.descricao AS marca_modelo, ");
            //SQL.AppendLine("	            tb_dep_tipo_veiculos.descricao AS tipo_veiculo, db_global.dbo.tb_glo_sys_cores.descricao AS cor, ");
            //SQL.AppendLine("				tb_dep_grv.flag_comboio, tb_dep_reboques.placa AS reboque, tb_dep_grv.data_hora_remocao, ");
            //SQL.AppendLine("				tb_dep_grv.data_hora_guarda, tb_dep_grv.id_status_operacao AS status, tb_dep_clientes.nome AS nome_cliente, ");
            //SQL.AppendLine("	            tb_dep_depositos.descricao AS nome_deposito, tb_dep_grv.id_grv, tb_dep_grv.id_tarifa_tipo_veiculo, ");
            //SQL.AppendLine("				tb_dep_grv.id_cliente, tb_dep_grv.id_deposito, tb_dep_grv.id_reboquista, tb_dep_grv.id_reboque, ");
            //SQL.AppendLine("				tb_dep_grv.id_autoridade_responsavel, tb_dep_grv.id_cor, tb_dep_grv.id_detran_marca_modelo, ");
            //SQL.AppendLine("	            tb_dep_grv.data_cadastro");
            //SQL.AppendLine("           FROM dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv");
            //SQL.AppendLine("     INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_clientes ON tb_dep_grv.id_cliente = tb_dep_clientes.id_cliente ");
            //SQL.AppendLine("LEFT OUTER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_reboques ON tb_dep_grv.id_reboque = tb_dep_reboques.id_reboque ");
            //SQL.AppendLine("     INNER JOIN db_link_patios_ws_prod.dbo.tb_detran_marca_modelo ON tb_dep_grv.id_detran_marca_modelo = db_link_patios_ws_prod.dbo.tb_detran_marca_modelo.id_detran_marca_modelo ");
            //SQL.AppendLine("     INNER JOIN db_global.dbo.tb_glo_sys_cores ON tb_dep_grv.id_cor = db_global.dbo.tb_glo_sys_cores.id_cor ");
            //SQL.AppendLine("     INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_depositos ON tb_dep_grv.id_deposito = tb_dep_depositos.id_deposito ");


            //SQL.AppendLine("     LEFT JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_tarifas_tipo_veiculos ON tb_dep_grv.id_tarifa_tipo_veiculo = tb_dep_tarifas_tipo_veiculos.id_tarifa_tipo_veiculo ");
            //SQL.AppendLine("     LEFT JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_tipo_veiculos ON tb_dep_tarifas_tipo_veiculos.id_tipo_veiculo = tb_dep_tipo_veiculos.id_tipo_veiculo");

            //SQL.AppendLine("WHERE 1 = 1");

            //if (!string.IsNullOrWhiteSpace(grvs))
            //    SQL.AppendFormat("AND tb_dep_grv.id_grv in ({0})", grvs);
            //else
            //    if (idGrv != 0) SQL.AppendFormat("AND tb_dep_grv.id_grv = {0}", idGrv);

            ////IMPEDIR UTILIZAÇÃO DE LOTES JÁ CADASTRADOS
            ////SQL.AppendLine("AND tb_dep_grv.placa NOT IN(SELECT placa FROM tb_leilao_lotes)");
            ////SQL.AppendLine("AND tb_dep_grv.chassi NOT IN(SELECT chassi FROM tb_leilao_lotes)");

            //SQL.AppendLine("      AND tb_dep_grv.id_grv NOT IN(SELECT id_grv                                                                                        ");
            //SQL.AppendLine("                                     FROM tb_leilao_lotes                                                                               ");
            //SQL.AppendLine("                                    INNER JOIN tb_leilao ON tb_leilao_lotes.id_leilao = tb_leilao.id AND id_status IN(1, 2, 3, 4, 5, 6) ");
            //SQL.AppendLine("                                    WHERE id_grv = tb_dep_grv.id_grv)                                                                   ");

            //SQL.AppendLine("GROUP BY tb_dep_grv.numero_formulario_grv, tb_dep_grv.placa, tb_dep_grv.chassi, db_link_patios_ws_prod.dbo.tb_detran_marca_modelo.descricao, ");
            //SQL.AppendLine(" 		 tb_dep_tipo_veiculos.descricao, db_global.dbo.tb_glo_sys_cores.descricao, tb_dep_grv.flag_comboio, tb_dep_reboques.placa, tb_dep_grv.data_hora_remocao, ");
            //SQL.AppendLine("		 tb_dep_grv.data_hora_guarda, tb_dep_grv.id_status_operacao, tb_dep_clientes.nome, tb_dep_depositos.descricao, tb_dep_grv.id_grv, ");
            //SQL.AppendLine("		 tb_dep_grv.id_tarifa_tipo_veiculo, tb_dep_grv.id_cliente, tb_dep_grv.id_deposito, tb_dep_grv.id_reboquista, tb_dep_grv.id_reboque, ");
            //SQL.AppendLine("		 tb_dep_grv.id_autoridade_responsavel, tb_dep_grv.id_cor, tb_dep_grv.id_detran_marca_modelo, tb_dep_grv.data_cadastro ");

            //if (cliente != 0 && deposito != "" && numDias != 0)
            //{
            //    SQL.AppendFormat("HAVING (tb_dep_grv.id_cliente = {0})", cliente);
            //    SQL.AppendFormat("   AND (tb_dep_grv.id_deposito in ({0}))", deposito);
            //    SQL.AppendLine("                 AND (tb_dep_grv.id_status_operacao in('G', 'P','L','M', 'T') )");
            //    //SQL.AppendFormat(string.Format("   AND DATEDIFF( DAY , data_hora_remocao, getdate() ) <= {0} ", numDias));
            //    //SQL.AppendFormat(string.Format("   AND DATEDIFF( DAY , data_hora_remocao, '{0}' ) >= {1} ", Convert.ToDateTime(data).ToString("MM/dd/yyyy"), numDias));
            //    SQL.AppendFormat(string.Format("   AND DATEDIFF( DAY , data_hora_remocao, '{0}' ) >= {1} ", Convert.ToDateTime(data).ToString("MM/dd/yyyy"), numDias));
            //}

            //SQL.AppendLine("ORDER BY tb_dep_grv.data_hora_remocao DESC");
            #endregion

            #region ALTERAÇÃO DE CONSULTA - 07/06/2017 - DUPLICAÇÃO DE REGISTROS
            //SQL.AppendLine("            SELECT tb_dep_grv.numero_formulario_grv, tb_dep_grv.placa, tb_dep_grv.chassi,                                             ");
            //SQL.AppendLine("	                ve.marca_modelo, ve.tipo_veiculo, ve.cor, tb_dep_grv.flag_comboio,                                                ");
            //SQL.AppendLine("					tb_dep_grv.data_hora_remocao, tb_dep_grv.data_hora_guarda, tb_dep_grv.id_status_operacao AS status, 			  ");
            //SQL.AppendLine("					tb_dep_grv.id_grv, tb_dep_grv.id_tarifa_tipo_veiculo,                                                             ");
            //SQL.AppendLine("					tb_dep_grv.id_cliente, tb_dep_grv.id_deposito, tb_dep_grv.id_reboquista, tb_dep_grv.id_reboque,                   ");
            //SQL.AppendLine("					tb_dep_grv.id_autoridade_responsavel, tb_dep_grv.id_cor, tb_dep_grv.id_detran_marca_modelo,                       ");
            //SQL.AppendLine("					tb_dep_grv.data_cadastro                                                                                          ");
            //SQL.AppendLine("                                                                                                                                      ");
            //SQL.AppendLine("			   FROM dbMobLinkDepositoPublicoProducao.dbo.vw_estoque_veiculos ve                                                       ");
            //SQL.AppendLine("			   JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv_enquadramento_infracoes                                           ");
            //SQL.AppendLine("			     ON tb_dep_grv_enquadramento_infracoes.id_grv = ve.id_grv                                                             ");
            //SQL.AppendLine("			   JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv tb_dep_grv                                                        ");
            //SQL.AppendLine("			     ON tb_dep_grv.id_grv = tb_dep_grv_enquadramento_infracoes.id_grv                                                     ");
            //SQL.AppendLine("			   JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes                                                      ");
            //SQL.AppendLine("			     ON tb_dep_status_operacoes.id_status_operacao = tb_dep_grv.id_status_operacao                                        ");
            //SQL.AppendLine("			  WHERE ve.status IN ('G', 'P', 'L', 'M', 'T', '2','3', '4', '5', '6')                                                    ");
            //SQL.AppendFormat("			    AND tb_dep_grv.id_deposito IN ({0})", deposito);
            //SQL.AppendFormat("			    AND tb_dep_grv.id_cliente IN ({0})", cliente);
            //SQL.AppendLine("			    AND DATEDIFF(dd, tb_dep_grv.data_hora_remocao, CONVERT(datetime, '22/06/2017', 103)) >= 60                            ");
            //SQL.AppendLine("			    AND dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes.id_status_operacao <> '7' -- 'Leilão - Entregue'     ");
            #endregion

            SQL.AppendLine("    SELECT tb_dep_grv.numero_formulario_grv                                                                                    ");
            SQL.AppendLine("           , tb_dep_grv.placa                                                                                                  ");
            SQL.AppendLine("           , tb_dep_grv.chassi                                                                                                 ");
            SQL.AppendLine("           , ve.marca_modelo                                                                                                   ");
            SQL.AppendLine("           , ve.tipo_veiculo                                                                                                   ");
            SQL.AppendLine("           , ve.cor                                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.flag_comboio                                                                                           ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_remocao                                                                                      ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_guarda                                                                                       ");
            SQL.AppendLine("           , tb_dep_grv.id_status_operacao AS status                                                                           ");
            SQL.AppendLine("           , tb_dep_grv.id_grv                                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_tarifa_tipo_veiculo                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_cliente                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.id_deposito                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.id_reboquista                                                                                          ");
            SQL.AppendLine("           , tb_dep_grv.id_reboque                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.id_autoridade_responsavel                                                                              ");
            SQL.AppendLine("           , tb_dep_grv.id_cor                                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_detran_marca_modelo                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.data_cadastro                                                                                          ");
            SQL.AppendLine("      FROM dbMobLinkDepositoPublicoProducao.dbo.vw_estoque_veiculos ve                                                         ");
            SQL.AppendLine("INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv_enquadramento_infracoes                                             ");
            SQL.AppendLine("        ON tb_dep_grv_enquadramento_infracoes.id_grv = ve.id_grv                                                               ");
            SQL.AppendLine("INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv tb_dep_grv                                                          ");
            SQL.AppendLine("        ON tb_dep_grv.id_grv = tb_dep_grv_enquadramento_infracoes.id_grv                                                       ");
            SQL.AppendLine("INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes                                                        ");
            SQL.AppendLine("        ON tb_dep_status_operacoes.id_status_operacao = tb_dep_grv.id_status_operacao                                          ");
            //SQL.AppendLine("     WHERE ve.status IN ('G', 'P', 'L', 'M', 'T', '2','3', '4', '5', '6')                                                      ");
            SQL.AppendLine("     WHERE ve.status IN ('G', 'P', 'L', 'M', 'T', '2','3', '4', '5', '6', '1')                                                      ");

            if (cliente != 0 && deposito != "" && numDias != 0)
            {
                SQL.AppendFormat("       AND tb_dep_grv.id_cliente IN ({0})", cliente);
                SQL.AppendFormat("       AND tb_dep_grv.id_deposito IN ({0})", deposito);
                SQL.AppendFormat("       AND DATEDIFF(dd, tb_dep_grv.data_hora_remocao, CONVERT(datetime, '{0}', 103)) >= {1}", data, numDias);
            }

            if (!string.IsNullOrWhiteSpace(grvs))
                SQL.AppendFormat("AND tb_dep_grv.numero_formulario_grv = '{0}'", grvs);

            else if (idGrv != 0)
                SQL.AppendFormat("AND tb_dep_grv.numero_formulario_grv = {0}", idGrv);

            SQL.AppendLine("       AND dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes.id_status_operacao <> '7' -- 'Leilão - Entregue'       ");
            SQL.AppendLine("  GROUP BY tb_dep_grv.numero_formulario_grv                                                                                    ");
            SQL.AppendLine("           , tb_dep_grv.placa                                                                                                  ");
            SQL.AppendLine("           , tb_dep_grv.chassi                                                                                                 ");
            SQL.AppendLine("           , ve.marca_modelo                                                                                                   ");
            SQL.AppendLine("           , ve.tipo_veiculo                                                                                                   ");
            SQL.AppendLine("           , ve.cor                                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.flag_comboio                                                                                           ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_remocao                                                                                      ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_guarda                                                                                       ");
            SQL.AppendLine("           , tb_dep_grv.id_status_operacao                                                                                     ");
            SQL.AppendLine("           , tb_dep_grv.id_grv                                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_tarifa_tipo_veiculo                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_cliente                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.id_deposito                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.id_reboquista                                                                                          ");
            SQL.AppendLine("           , tb_dep_grv.id_reboque                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.id_autoridade_responsavel                                                                              ");
            SQL.AppendLine("           , tb_dep_grv.id_cor                                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.id_detran_marca_modelo                                                                                 ");
            SQL.AppendLine("           , tb_dep_grv.data_cadastro                                                                                          ");
            SQL.AppendLine("  ORDER BY tb_dep_grv.numero_formulario_grv                                                                                    ");





            #endregion

            if (numLotes != 0)
            {
                var lista = ConsultaSQL(SQL.ToString()).ConverterParaLista<GRV>().Take(numLotes).ToList();
                return lista;
            }

            var lista2 = ConsultaSQL(SQL.ToString()).ConverterParaLista<GRV>();
            return lista2;
        }

        public IList<GRV> SelecionarTudo()
        {
            throw new NotImplementedException();
        }

        public IList<GRV> SelecionarTudo(GRV Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<GRV> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }

        public GRV SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public int Inserir(GRV Entidade)
        {
            throw new NotImplementedException();
        }

        public int Alterar(GRV Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(GRV Entidade)
        {
            throw new NotImplementedException();
        }
        
        public int AtualizarStatusOperacao(int id_grv, string status_operacao)
        {
            try
            {
                string sql =
                    string.Format("update tb_dep_grv set id_status_operacao = '{0}' where id_grv = {1}", status_operacao, id_grv);
                return RepositorioGlobal.Util.ExecutaSqlGenerico(Util.LerConfiguracao("CONEXAO_DP"), sql);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
