using CCFW;
using MobLink.Framework.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Relatorios.Extrator.Negocio
{
    public class PRFRepositorio : DbSqlServer
    {
        public PRFRepositorio()
            : base(Framework.Util.LerConfiguracao("CONEXAO_DP"))
        {

        }

        private List<Modelos.RelatorioPRF> GerarRelatorioPRF(int id, string depositos)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("    SELECT tb_dep_grv.numero_formulario_grv                                                                                      ");
            SQL.AppendLine("           , tb_dep_grv.placa                                                                                                    ");
            SQL.AppendLine("           , tb_dep_grv.chassi                                                                                                   ");
            SQL.AppendLine("           , ve.marca_modelo                                                                                                     ");
            SQL.AppendLine("           , ve.tipo_veiculo                                                                                                     ");
            SQL.AppendLine("           , ve.cor                                                                                                              ");
            SQL.AppendLine("           , tb_dep_grv.flag_comboio                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_remocao                                                                                        ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_guarda                                                                                         ");
            SQL.AppendLine("           , tb_dep_grv.id_status_operacao AS status                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.id_grv                                                                                                   ");
            SQL.AppendLine("           , tb_dep_grv.id_tarifa_tipo_veiculo                                                                                   ");
            SQL.AppendLine("           , tb_dep_grv.id_cliente                                                                                               ");
            SQL.AppendLine("           , tb_dep_grv.id_deposito                                                                                              ");
            SQL.AppendLine("           , tb_dep_grv.id_reboquista                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.id_reboque                                                                                               ");
            SQL.AppendLine("           , tb_dep_grv.id_autoridade_responsavel                                                                                ");
            SQL.AppendLine("           , tb_dep_grv.id_cor                                                                                                   ");
            SQL.AppendLine("           , tb_dep_grv.id_detran_marca_modelo                                                                                   ");
            SQL.AppendLine("           , tb_dep_grv.data_cadastro                                                                                            ");
            SQL.AppendLine("      FROM dbMobLinkDepositoPublicoProducao.dbo.vw_estoque_veiculos ve                                                           ");
            SQL.AppendLine("INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv_enquadramento_infracoes                                               ");
            SQL.AppendLine("        ON tb_dep_grv_enquadramento_infracoes.id_grv = ve.id_grv                                                                 ");
            SQL.AppendLine("INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_grv tb_dep_grv                                                            ");
            SQL.AppendLine("        ON tb_dep_grv.id_grv = tb_dep_grv_enquadramento_infracoes.id_grv                                                         ");
            SQL.AppendLine("INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes                                                          ");
            SQL.AppendLine("        ON tb_dep_status_operacoes.id_status_operacao = tb_dep_grv.id_status_operacao                                            ");
            SQL.AppendLine("     WHERE ve.status IN ('G', 'P', 'L', 'M', 'T', '2','3', '4', '5', '6')                                                        ");
            SQL.AppendFormat("       AND tb_dep_grv.id_cliente IN ({0})       AND tb_dep_grv.id_deposito IN ({1})                                            ", id, depositos);
            SQL.AppendLine("	   AND DATEDIFF(dd, tb_dep_grv.data_hora_remocao, CONVERT(datetime, '01/01/2019', 103)) >= 61                               ");
            SQL.AppendLine("	   AND dbMobLinkDepositoPublicoProducao.dbo.tb_dep_status_operacoes.id_status_operacao <> '7' -- 'Leilão - Entregue'         ");
            SQL.AppendLine("	                                                                                                                             ");
            SQL.AppendLine("  GROUP BY tb_dep_grv.numero_formulario_grv                                                                                      ");
            SQL.AppendLine("           , tb_dep_grv.placa                                                                                                    ");
            SQL.AppendLine("           , tb_dep_grv.chassi                                                                                                   ");
            SQL.AppendLine("           , ve.marca_modelo                                                                                                     ");
            SQL.AppendLine("           , ve.tipo_veiculo                                                                                                     ");
            SQL.AppendLine("           , ve.cor                                                                                                              ");
            SQL.AppendLine("           , tb_dep_grv.flag_comboio                                                                                             ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_remocao                                                                                        ");
            SQL.AppendLine("           , tb_dep_grv.data_hora_guarda                                                                                         ");
            SQL.AppendLine("           , tb_dep_grv.id_status_operacao                                                                                       ");
            SQL.AppendLine("           , tb_dep_grv.id_grv                                                                                                   ");
            SQL.AppendLine("           , tb_dep_grv.id_tarifa_tipo_veiculo                                                                                   ");
            SQL.AppendLine("           , tb_dep_grv.id_cliente                                                                                               ");
            SQL.AppendLine("           , tb_dep_grv.id_deposito                                                                                              ");
            SQL.AppendLine("           , tb_dep_grv.id_reboquista                                                                                            ");
            SQL.AppendLine("           , tb_dep_grv.id_reboque                                                                                               ");
            SQL.AppendLine("           , tb_dep_grv.id_autoridade_responsavel                                                                                ");
            SQL.AppendLine("           , tb_dep_grv.id_cor                                                                                                   ");
            SQL.AppendLine("           , tb_dep_grv.id_detran_marca_modelo                                                                                   ");
            SQL.AppendLine("           , tb_dep_grv.data_cadastro                                                                                            ");
            SQL.AppendLine("  ORDER BY tb_dep_grv.numero_formulario_grv                                                                                      ");
            
            var ret = ConsultaSQL(SQL.ToString());

            var relatorio = ret.ConverterParaLista<Modelos.RelatorioPRF>().ToList();

            decimal totaltotal = 0;

            foreach (var item in relatorio)
            {
                Model.GRV.GRV ModelGRV = new Model.GRV.GRV();

                ModelGRV.id_grv = Convert.ToInt32(item.id_grv);

                Business.Sistema.GlobalDataBase.SetConnectionString(MobLink.Framework.Util.LerConfiguracao("CONEXAO_DP"));

                var listViewFaturamentoServicosGRV = Business.Faturamento.Servicos.ViewFaturamentoServicosGRV.Listar(ModelGRV.id_grv, "DEP");

                if (listViewFaturamentoServicosGRV == null) continue;

                decimal total = 0;
                decimal subtotal = 0;
                string servico_descricao = string.Empty;

                foreach (var s in listViewFaturamentoServicosGRV)
                {
                    if (servico_descricao == s.servico_descricao)
                    {
                        continue;
                    }
                    else
                    {
                        servico_descricao = s.servico_descricao;
                    }

                    if (s.flag_realizar_cobranca == 'N')
                    {
                        continue;
                    }

                    if (s.flag_rebocada == 'S' && ModelGRV.flag_comboio == 'S')
                    {
                        continue;
                    }

                    if (s.tipo_cobranca == 'D')
                    {
                        Model.GRV.CalculoDiarias modelCalculoDiarias = new Model.GRV.CalculoDiarias();

                        var consulta = Business.GRV.GRV.ConsultarRelacionado(ModelGRV, null, null, Business.Sistema.Configuracoes.id_usuario);

                        modelCalculoDiarias.id_cliente = Funcoes.DataBase.GetInt(consulta, "id_cliente");

                        modelCalculoDiarias.id_deposito = Funcoes.DataBase.GetInt(consulta, "id_deposito");

                        modelCalculoDiarias.maximo_diarias_para_cobranca = Funcoes.DataBase.GetInt(consulta, "cliente_maximo_diarias_para_cobranca");

                        modelCalculoDiarias.maximo_dias_vencimento = Funcoes.DataBase.GetInt(consulta, "cliente_maximo_dias_vencimento");

                        modelCalculoDiarias.hora_diaria = Funcoes.DataBase.GetString(consulta, "cliente_hora_diaria");

                        modelCalculoDiarias.flag_cliente_realiza_faturamento_arrecadacao = Funcoes.DataBase.GetChar(consulta, "cliente_flag_cliente_realiza_faturamento_arrecadacao");

                        modelCalculoDiarias.flag_usar_hora_diaria = Funcoes.DataBase.GetChar(consulta, "cliente_flag_usar_hora_diaria");

                        modelCalculoDiarias.flag_cobrar_diarias_dias_corridos = Funcoes.DataBase.GetChar(consulta, "cliente_flag_cobrar_diarias_dias_corridos");

                        modelCalculoDiarias.data_hora_guarda = Funcoes.DataBase.GetDatetime(consulta, "data_hora_guarda");

                        modelCalculoDiarias.diarias = Business.Calculos.Diarias.Calcular(modelCalculoDiarias);

                        //ViewBag.diarias = modelCalculoDiarias.diarias;

                        subtotal = s.preco_padrao * modelCalculoDiarias.diarias;
                        total += s.preco_padrao * modelCalculoDiarias.diarias;
                        totaltotal += total;
                    }
                    else if (s.tipo_cobranca == 'Q')
                    {
                        if (s.flag_rebocada == 'S')
                        {
                            s.valor = 1;
                        }

                        if (s.valor == 0)
                        {
                            continue;
                        }

                        decimal valor = s.valor;

                        if (s.valor == 0 && s.flag_servico_obrigatorio == 'S')
                        {
                            valor = 1;
                        }

                        subtotal = s.preco_padrao * valor;
                        total += s.preco_padrao * valor;
                        totaltotal += total;
                    }
                }

                item.ValorTotalAteHoje = total.ToString("c");
            }

            var TOTAL = totaltotal.ToString("c");

            return relatorio;
        }

        private int Inserir(Modelos.RelatorioPRF ent)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO dbLeilao.dbo.tb_relatorio_prf (numero_formulario_grv ");
            sql.AppendLine("								, placa                          ");
            sql.AppendLine("								, chassi                         ");
            sql.AppendLine("								, marca_modelo                   ");
            sql.AppendLine("								, tipo_veiculo                   ");
            sql.AppendLine("								, cor                            ");
            sql.AppendLine("								, flag_comboio                   ");
            sql.AppendLine("								, data_hora_remocao              ");
            sql.AppendLine("								, data_hora_guarda               ");
            sql.AppendLine("								, status                         ");
            sql.AppendLine("								, id_grv                         ");
            sql.AppendLine("								, id_tarifa_tipo_veiculo         ");
            sql.AppendLine("								, id_cliente                     ");
            sql.AppendLine("								, id_deposito                    ");
            sql.AppendLine("								, id_reboquista                  ");
            sql.AppendLine("								, id_reboque                     ");
            sql.AppendLine("								, id_autoridade_responsavel      ");
            sql.AppendLine("								, id_cor                         ");
            sql.AppendLine("								, id_detran_marca_modelo         ");
            sql.AppendLine("								, data_cadastro                  ");
            sql.AppendLine("								, ValorTotalAteHoje)             ");

            sql.AppendLine(string.Format("VALUES (  '{0}'      ", ent.numero_formulario_grv));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.placa));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.chassi));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.marca_modelo));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.tipo_veiculo));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.cor));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.flag_comboio));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.data_hora_remocao));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.data_hora_guarda));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.status));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.id_grv));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.id_tarifa_tipo_veiculo));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.id_cliente));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.id_deposito));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.id_reboquista));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.id_reboque));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.id_autoridade_responsavel));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.id_cor));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.id_detran_marca_modelo));
            sql.AppendLine(string.Format("        , '{0}'      ", ent.data_cadastro));
            sql.AppendLine(string.Format("        , '{0}' )    ", ent.ValorTotalAteHoje));

            return ExecutaSQL(sql.ToString());
        }

        private void ExcluirTabela()
        {
            var sql = "delete dbLeilao.dbo.tb_relatorio_prf";
            ExecutaSQL(sql);
        }

        public void GravarRelatorioPRF()
        {
            ExcluirTabela();

            var listaClienteDepositoPRF = ConsultaSQL("SELECT id_cliente, id_deposito, *FROM dbMobLinkDepositoPublicoProducao.dbo.vw_dep_clientes_depositos WHERE id_cliente IN (13, 14)");
            
            foreach (DataRow item in listaClienteDepositoPRF.Rows)
            {
                int cliente;
                string deposito;

                try
                {
                    cliente = (int)item[0];
                    deposito = item[1].ToString();
                }
                catch
                {
                    throw new Exception("Erro na conversão");
                }

                var tabela = new PRFRepositorio().GerarRelatorioPRF(cliente, deposito);

                foreach (var c in tabela)
                {
                    Inserir(c);
                    Console.WriteLine(c.marca_modelo);
                }

            }
        }
    }
}
