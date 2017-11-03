using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class LoteRepositorio : DbSqlServer, ICrud<Lote, int>
    {
        protected internal LoteRepositorio(): base(Util.DetectarConexao())
        {

        }

        public Rel_PC_Lote RelatorioPrestacaoContasLote(int id)
        {
            string sql = string.Format(@"

            SELECT tb_leilao.id id_leilao , tb_leilao.id_comitente,tb_leilao.descricao, tb_leilao.data_leilao, tb_leiloeiros.nome, tb_leiloeiros.documento,

                   tb_leilao_lotes.placa, tb_leilao_lotes.chassi, tb_leilao_lotes.renavan, tb_leilao_lotes.marca_modelo,
                   tb_leilao_lotes.ano_fabricacao, ano_modelo, tb_leilao_lotes.id_grv, datediff(dd, getdate(), tb_leilao_lotes.data_hora_apreensao) * -1 numero_dias_patio,
                   tb_leilao_lotes.numero_lote, tb_leilao_lotes.tipo_veiculo,
                   tb_leilao_lotes_arrematantes.avaliacao, tb_leilao_lotes_arrematantes.arrematacao,
                   tb_comitentes_taxas.valor taxa_comitente
              FROM tb_leilao_lotes
              JOIN tb_leilao ON tb_leilao_lotes.id_leilao = tb_leilao.id
              JOIN tb_leiloeiros ON tb_leiloeiros.id = tb_leilao.id_leiloeiro
              LEFT JOIN tb_leilao_lotes_arrematantes ON tb_leilao_lotes.placa = tb_leilao_lotes_arrematantes.placa
              left JOIN tb_comitentes_taxas ON tb_leilao.id_comitente = tb_comitentes_taxas.id_comitente
             WHERE tb_leilao_lotes.id = {0}", id);

            var cabecalho = ConsultaSQL(sql)
                .Rows[0]
                .ConverterParaEntidade<Rel_PC_Lote>();

            sql = string.Format(@"
            SELECT tb_leilao_despesas.id, tb_leilao_despesas.id_leilao, tb_leilao_despesas.id_despesa, tb_leilao_despesas.valor,
                   
                   dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos.descricao DescricaoDespesa, 
                   (SELECT COUNT(*) FROM tb_leilao_lotes WHERE id_leilao = tb_leilao_despesas.id_leilao) qtd_lotes,
                   tb_leilao_despesas.valor / (SELECT COUNT(*) FROM tb_leilao_lotes WHERE id_leilao = tb_leilao_despesas.id_leilao) Rateio
              FROM tb_leilao_despesas
              JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos
                ON dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos.id_faturamento_servico_tipo = tb_leilao_despesas.id_despesa
             WHERE id_leilao = {0}
             ORDER BY dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos.ordem_impressao", cabecalho.id_leilao);

            var teste = ConsultaSQL(sql);

            var despesasLeilao = teste.ConverterParaLista<Despesa_Leilao>();

            sql = string.Format(@"
            SELECT dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos.descricao DescricaoDespesa, 
                   tb_leilao_lotes_despesas.valor Valor
              FROM tb_leilao_lotes_despesas 
              JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos
                ON dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos.id_faturamento_servico_tipo = tb_leilao_lotes_despesas.id_despesa
             WHERE id_lote = {0}
             ORDER BY dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos.ordem_impressao", id);

            var despesasLote = ConsultaSQL(sql).ConverterParaLista<Despesa_Lote>();

            Rel_PC_Lote res = new Rel_PC_Lote();

            res = cabecalho;
            res.DespesasLeilao = despesasLeilao;
            res.DespesasLote = despesasLote;

            return res;
        }

        public IList<Lote> SelecionarTudoPatio()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT id, id_leilao, numero_lote, placa, chassi, numero_formulario_grv, marca_modelo, cor, tipo_veiculo, localizacao, situacao_lote, situacao_placa, situacao_chassi, situacao_gnv, situacao_veiculo, id_grv, conferido_patio, cor_ostentada FROM tb_leilao_lotes l where conferido_patio = 'N'");

            var lote = ConsultaSQL(sb.ToString()).ConverterParaLista<Lote>();

            return lote;
        }

        public int AlterarPatio(Lote entidade)
        {
            StringBuilder sqlUpd = new StringBuilder();

            sqlUpd.AppendLine(string.Format("UPDATE dbo.tb_leilao_lotes SET marca_modelo = '{0}'", entidade.marca_modelo));
            sqlUpd.AppendLine(string.Format("	, cor =                                    '{0}'", entidade.cor));
            sqlUpd.AppendLine(string.Format("	, cor_ostentada =                          '{0}'", entidade.cor_ostentada));
            sqlUpd.AppendLine(string.Format("	, situacao_lote =                          '{0}'", entidade.situacao_lote));
            sqlUpd.AppendLine(string.Format("	, situacao_placa =                         '{0}'", entidade.situacao_placa));
            sqlUpd.AppendLine(string.Format("	, situacao_chassi =                        '{0}'", entidade.situacao_chassi));
            sqlUpd.AppendLine(string.Format("	, situacao_gnv =                           '{0}'", entidade.situacao_GNV));
            sqlUpd.AppendLine(string.Format("	, situacao_veiculo =                       '{0}'", entidade.situacao_veiculo));
            sqlUpd.AppendLine(string.Format("	, localizacao =                            '{0}'", entidade.localizacao));
            sqlUpd.AppendLine(string.Format("	, conferido_patio =                        '{0}'", entidade.Conferido_Patio));
            sqlUpd.AppendLine(string.Format("WHERE id =                                     {0}", entidade.id));

            return ExecutaSQL(sqlUpd.ToString());
        }

        public int Alterar(Lote entidade)
        {
            StringBuilder sqlUpd = new StringBuilder();

            sqlUpd.AppendLine("UPDATE tb_leilao_lotes ");
            sqlUpd.AppendLine(string.Format("SET situacao_lote = '{0}'", entidade.situacao_lote));
            sqlUpd.AppendLine(string.Format(", situacao_placa = '{0}'", entidade.situacao_placa));
            sqlUpd.AppendLine(string.Format(", situacao_chassi = '{0}'", entidade.situacao_chassi));
            sqlUpd.AppendLine(string.Format(", situacao_gnv = '{0}'", entidade.situacao_GNV));
            sqlUpd.AppendLine(string.Format(", situacao_veiculo = '{0}'", entidade.situacao_veiculo));
            sqlUpd.AppendLine(string.Format(", id_status_lote = {0}", entidade.id_status_lote));
            sqlUpd.AppendLine(string.Format(", cor = '{0}'", entidade.cor));
            sqlUpd.AppendLine(string.Format(", marca_modelo = '{0}'", entidade.marca_modelo));
            sqlUpd.AppendLine(string.Format(", tipo_veiculo = '{0}'", entidade.tipo_veiculo));
            sqlUpd.AppendLine(string.Format(", municipio = '{0}'", entidade.municipio));
            sqlUpd.AppendLine(string.Format(", localizacao = '{0}'", entidade.localizacao));
            sqlUpd.AppendLine(string.Format(", responsavel_remocao = '{0}'", entidade.responsavel_remocao));
            sqlUpd.AppendLine(string.Format(", observacoes = '{0}'", entidade.observacoes));
            sqlUpd.AppendLine(string.Format(", reboque = '{0}'", entidade.reboque));
            sqlUpd.AppendLine(string.Format(", tipo_combustivel = '{0}'", entidade.tipo_combustivel));
            sqlUpd.AppendLine(string.Format(", procedencia_veiculo = '{0}'", entidade.procedencia_veiculo));
            sqlUpd.AppendLine(string.Format(", chave = '{0}'", entidade.chave));
            sqlUpd.AppendLine(string.Format(", valor_avaliacao = '{0}'", entidade.valor_avaliacao));
            sqlUpd.AppendLine(string.Format(", numero_motor = '{0}'", entidade.numero_motor));
            sqlUpd.AppendLine(string.Format(", lance_minimo = '{0}'", entidade.lance_minimo));
            sqlUpd.AppendLine(string.Format(", quilometragem = '{0}'", entidade.quilometragem));
            sqlUpd.AppendLine(string.Format(", cambio = '{0}'", entidade.cambio));
            sqlUpd.AppendLine(string.Format(", ar_condicionado = '{0}'", entidade.ar_condicionado));
            sqlUpd.AppendLine(string.Format(", direcao = '{0}'", entidade.direcao));
            sqlUpd.AppendLine(string.Format(", vidro_eletrico = '{0}'", entidade.vidro_eletrico));
            sqlUpd.AppendLine(string.Format(", trava_eletrica = '{0}'", entidade.trava_eletrica));
            sqlUpd.AppendLine(string.Format(", status_pericia = '{0}'", entidade.status_pericia));
            sqlUpd.AppendLine(string.Format(", renavan = '{0}'", entidade.renavan));
            sqlUpd.AppendLine(string.Format(", ano_fabricacao = '{0}'", entidade.ano_fabricacao));
            sqlUpd.AppendLine(string.Format(", ano_modelo = '{0}'", entidade.ano_modelo));
            sqlUpd.AppendLine(string.Format(", situacao_veiculo_pericia = '{0}'", entidade.situacao_veiculo_pericia));
            sqlUpd.AppendLine(string.Format(", uf = '{0}'", entidade.uf));
            sqlUpd.AppendLine(string.Format(", numero_lote = '{0}'", entidade.numero_lote));
            sqlUpd.AppendLine(string.Format(", flag_transacao = '{0}'", entidade.Flag_Transacao));
            sqlUpd.AppendLine(string.Format(", flag_agendado = '{0}'", entidade.Flag_Agendado));
            sqlUpd.AppendLine(string.Format(", informacao_roubo = '{0}'", entidade.informacao_roubo));
            sqlUpd.AppendLine(string.Format(", restricao_estelionato = '{0}'", entidade.restricao_estelionato));
            sqlUpd.AppendLine(string.Format(", log_recolhimento = {0}", entidade.Log_Recolhimento ?? 0));
            sqlUpd.AppendLine(string.Format(", obs_transacao = '{0}'", entidade.obs_transacao ?? ""));
            
            sqlUpd.AppendLine(string.Format("WHERE id = {0}", entidade.id));

            return ExecutaSQL(sqlUpd.ToString());
        }

        public int InserirLoteDSIN(LoteDSIN LoteDSIN, int IdLeilao)
        {
            foreach (var e in LoteDSIN.REGISTROS)
            {
                StringBuilder sqlUpd = new StringBuilder();

                sqlUpd.AppendLine(@" INSERT INTO dbo.tb_leilao_lotes 
                                 (ano_fabricacao
                                , ano_modelo
                                , chassi
                                , chave
                                , tipo_combustivel
                                , cor
                                , data_hora_apreensao
                                , lance_minimo
                                , localizacao
                                , marca_modelo
                                , municipio
                                , numero_motor
                                , observacoes
                                , patio
                                , placa
                                , numero_formulario_grv
                                , renavan
                                , responsavel_remocao
                                , tipo_veiculo
                                , uf
                                , reboque
                                , id_leilao
                                , numero_lote
                                , id_status_lote) ");


                sqlUpd.AppendFormat(" VALUES ('{0}' ", e.ANOFABRICACAO);
                sqlUpd.AppendFormat("        ,'{0}' ", e.ANOMODELO);
                sqlUpd.AppendFormat("        ,'{0}' ", e.CHASSI);
                sqlUpd.AppendFormat("        ,'{0}' ", e.CHAVE);
                sqlUpd.AppendFormat("        ,'{0}' ", e.COMBUSTIVEL);
                sqlUpd.AppendFormat("        ,'{0}' ", e.COR);
                sqlUpd.AppendFormat("        ,'{0}' ", Convert.ToDateTime(e.DATAAPREENSAO + " " + e.HORAAPREENSAO));
                sqlUpd.AppendFormat("        ,'{0}' ", e.LANCEMINIMO);
                sqlUpd.AppendFormat("        ,'{0}' ", string.IsNullOrEmpty(e.LOCALIZACAO) ? "" : e.LOCALIZACAO.Replace("'", ""));
                sqlUpd.AppendFormat("        ,'{0}' ", e.MARCA + "/" + e.MODELO);
                sqlUpd.AppendFormat("        ,'{0}' ", string.IsNullOrEmpty(e.MUNVEICULO) ? "" : e.MUNVEICULO.Replace("'", ""));
                sqlUpd.AppendFormat("        ,'{0}' ", e.NROMOTOR);
                sqlUpd.AppendFormat("        ,'{0}' ", e.OBSERVACAO);
                sqlUpd.AppendFormat("        ,'{0}' ", e.PATIO);
                sqlUpd.AppendFormat("        ,'{0}' ", e.PLACA);
                sqlUpd.AppendFormat("        ,'{0}' ", e.PROCESSO.Replace("_", ""));
                sqlUpd.AppendFormat("        ,'{0}' ", e.RENAVAM);
                sqlUpd.AppendFormat("        ,'{0}' ", e.RESPREMOCAO);
                sqlUpd.AppendFormat("        ,'{0}' ", e.TIPOVEICULODESCRICAO);
                sqlUpd.AppendFormat("        ,'{0}' ", e.UFVEICULO);
                sqlUpd.AppendFormat("        ,'{0}' ", e.VEICREBOCADO);
                sqlUpd.AppendFormat("        , {0}  ", IdLeilao);

                if (e.LOTE != null)
                {
                    try
                    {
                        sqlUpd.AppendFormat("  ,'{0}'  ", Int32.Parse(e.LOTE));
                    }
                    catch
                    {
                        sqlUpd.AppendFormat("  ,'{0}'  ", string.Empty);
                    }
                }

                sqlUpd.AppendFormat("  ,{0})  ", 17);

                try
                {
                    ExecutaSQL(sqlUpd.ToString());
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }

            return -1;
        }

        public Lote SelecionarPorProcesso(string numero_formulario_grv)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM tb_leilao_lotes l");
            sb.AppendFormat("WHERE l.numero_formulario_grv = '{0}'", numero_formulario_grv);

            return ConsultaSQL(sb.ToString())
                .Rows[0]
                .ConverterParaEntidade<Lote>();
        }

        public Lote SelecionarPorId(int id)
        {
            string sql = string.Format(@"

             SELECT l.*,

                    s.descricao as descricao_status, tb_leilao.descricao as nome_leilao,

                    l.placa,        
                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN
                    				(SELECT TOP 1 placa
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      WHERE placa = l.placa OR chassi = l.chassi) 
                    END _placa,

                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN
                    			(SELECT TOP 1 chassi
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      WHERE chassi = l.chassi OR placa = l.placa) 
                    END _chassi,

                    l.tipo_veiculo, 
                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN
                    			(SELECT TOP 1 descricao_tipo
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      WHERE chassi = l.chassi OR placa = l.placa) 
                    END _tipo_veiculo,

                    l.marca_modelo,                     
                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN                    
                    				(SELECT TOP 1 descricao
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      INNER JOIN db_link_patios_ws_prod.dbo.tb_detran_marca_modelo
                                         ON tb_detran_marca_modelo.id_detran_marca_modelo = tb_detran_veiculos_ws.id_detran_marca_modelo
                                      WHERE chassi = l.chassi OR placa = l.placa)                                       
					END _marca_modelo,


                    l.cor,			
                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN                    
                    			(SELECT TOP 1 descricao
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      INNER JOIN db_global.dbo.tb_glo_sys_cores
                                         ON tb_detran_veiculos_ws.id_cor = db_global.dbo.tb_glo_sys_cores.id_cor
                                       WHERE chassi = l.chassi OR placa = l.placa) 
                    END _cor 

               FROM tb_leilao_lotes l

              INNER JOIN tb_lotes_status s ON l.id_status_lote = s.id
              INNER JOIN tb_leilao ON tb_leilao.id = l.id_leilao
              WHERE l.id = {0} ", id);

            var lote = ConsultaSQL(sql)
                .Rows[0]
                .ConverterParaEntidade<Lote>();

            lote.Leilao = new LeilaoRepositorio().SelecionarPorId(lote.id_leilao);
            lote.Proprietario = new ProprietarioRepositorio().SelecionarProprietarioPorLote(lote.id);
            lote.Restricoes = new RestricaoRepositorio().SelecionarTudo(lote.id).ToList();
            lote.Transacoes = RepositorioGlobal.Transacoes.Selecionar_Transacoes_Lote(lote.id);

            return lote;
        }

        public IList<Lote> SelecionarTudo()
        {
            return ConsultaSQL("SELECT * FROM dbo.tb_leilao_lotes").ConverterParaLista<Lote>();
        }

        public IList<Lote> SelecionarTudo(int IdLeilao)
        {
            string sql = string.Format(@"

             SELECT l.id, l.id_leilao, leilao.descricao nome_leilao, leilao.data_leilao, l.numero_lote, l.numero_formulario_grv, l.id_status_lote, s.descricao as descricao_status,
                    l.situacao_lote, l.situacao_placa, l.situacao_chassi, l.situacao_gnv, l.situacao_veiculo,

                    CASE 
                    WHEN ((SELECT nome_proprietario FROM tb_leilao_lotes_proprietarios WHERE id_lote = l.id) IS NULL)
                    THEN 0 
                    ELSE 1 END Dados_Proprietario,

                    l.placa,        
                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN
                    				(SELECT TOP 1 placa
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      WHERE placa = l.placa OR chassi = l.chassi) 
                    END _placa,
                    

                    l.chassi,       
                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN
                    			(SELECT TOP 1 chassi
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      WHERE chassi = l.chassi OR placa = l.placa) 
                    END _chassi,

                    l.tipo_veiculo, 
                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN
                    			(SELECT TOP 1 descricao_tipo
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      WHERE chassi = l.chassi OR placa = l.placa) 
                    END _tipo_veiculo,

                    l.marca_modelo,                     
                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN                    
                    				(SELECT TOP 1 descricao
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      INNER JOIN db_link_patios_ws_prod.dbo.tb_detran_marca_modelo
                                         ON tb_detran_marca_modelo.id_detran_marca_modelo = tb_detran_veiculos_ws.id_detran_marca_modelo
                                      WHERE chassi = l.chassi OR placa = l.placa)                                       
					END _marca_modelo,


                    l.cor,			
                    CASE WHEN l.chassi <> '' AND l.placa <> '' THEN                    
                    			(SELECT TOP 1 descricao
                                       FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws
                                      INNER JOIN db_global.dbo.tb_glo_sys_cores
                                         ON tb_detran_veiculos_ws.id_cor = db_global.dbo.tb_glo_sys_cores.id_cor
                                       WHERE chassi = l.chassi OR placa = l.placa) 
                    END _cor, 
                    
                    l.uf,
                    (SELECT COUNT(*) FROM tb_leilao_lotes_fotos WHERE id_lote = l.id) qtd_fotos,
                    (SELECT COUNT(*) FROM tb_leilao_lotes_despesas WHERE id_lote = l.id) qtd_despesas,
                    (SELECT COUNT(*) FROM tb_leilao_lotes_restricoes WHERE id_lote = l.id) qtd_restricoes,

                    l.flag_transacao,
                    l.flag_agendado,
                    l.obs_transacao

               FROM tb_leilao_lotes l

              INNER JOIN tb_lotes_status s ON l.id_status_lote = s.id
              INNER JOIN tb_leilao leilao ON l.id_leilao = leilao.id

              WHERE l.id_leilao = {0} ", IdLeilao);

            var lotes = ConsultaSQL(sql).ConverterParaLista<Lote>();

            LeilaoRepositorio lr = new LeilaoRepositorio();
            var leilaoAssociado = lr.SelecionarPorId(IdLeilao);

            foreach (var item in lotes)
            {
                item.Leilao = leilaoAssociado;
            }

            return lotes;
        }

        public IList<Lote> SelecionarLotesNaoNormalizados()
        {
            StringBuilder DbSqlServer = new StringBuilder();

            DbSqlServer.AppendLine("SELECT id, placa, chassi, cor, marca_modelo, tipo_veiculo FROM TB_LEILAO_LOTES WHERE flag_normalizado = 'N'");

            var lotes = ConsultaSQL(DbSqlServer.ToString()).ConverterParaLista<Lote>();

            return lotes;
        }

        public List<Dominio.TabelaGenerica> SelecionarLocalizacao()
        {
            StringBuilder DbSqlServer = new StringBuilder();
            DbSqlServer.AppendLine("SELECT distinct(upper(localizacao)) descricao");
            DbSqlServer.AppendLine("FROM tb_leilao_lotes");
            DbSqlServer.AppendLine("WHERE localizacao IS NOT NULL");
            DbSqlServer.AppendLine("AND ltrim(rtrim(localizacao)) <> ''");

            var Localizacoes = ConsultaSQL(DbSqlServer.ToString()).ConverterParaLista<Dominio.TabelaGenerica>();

            return Localizacoes;
        }

        public int NormalizarLote(Lote Lote)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("UPDATE dbo.tb_leilao_lotes");
            sb.AppendFormat("   SET chassi = '{0}'", Lote.chassi);
            sb.AppendFormat("   , cor = '{0}'", Lote.cor);
            sb.AppendFormat("   , marca_modelo = '{0}'", Lote.marca_modelo);
            sb.AppendFormat("   , tipo_veiculo = '{0}'", Lote.tipo_veiculo);
            sb.AppendFormat("   , placa = '{0}'", Lote.placa);
            sb.AppendFormat("   , flag_normalizado = 'S'");
            sb.AppendFormat("WHERE id = {0}", Lote.id);

            return ExecutaSQL(sb.ToString());
        }

        public Lote ConsultarLote(string Placa = "", string Chassi = "", string Processo = "", bool SomenteDivergentes = false, int? IdLote = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT l.id, l.id_leilao, leilao.descricao nome_leilao, l.numero_lote, l.numero_formulario_grv,");
            sb.AppendLine("       l.id_status_lote, s.descricao as descricao_status,");
            sb.AppendLine("       l.situacao_lote, l.situacao_placa, l.situacao_chassi, l.situacao_gnv, l.situacao_veiculo,");
            sb.AppendLine("       l.tipo_combustivel, l.procedencia_veiculo, l.chave, l.valor_avaliacao, ");
            sb.AppendLine("       l.numero_motor, l.lance_minimo, l.quilometragem, l.cambio, l.ar_condicionado, l.direcao, ");
            sb.AppendLine("       l.vidroEletrico, l.travaEletrica, l.flag_normalizado, l.status_pericia, l.renavan, l.ano, municipio, observacoes, ");
            sb.AppendLine("       l.localizacao, l.reboque, l.responsavel_remocao, l.situacao_veiculo_pericia, l.uf,");



            sb.AppendLine("       l.placa,        (SELECT TOP 1 placa");
            sb.AppendLine("                          FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws");
            sb.AppendLine("                         WHERE placa = l.placa OR chassi = l.chassi) _placa,");
            sb.AppendLine("       l.chassi,       (SELECT TOP 1 chassi");
            sb.AppendLine("                          FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws");
            sb.AppendLine("                         WHERE chassi = l.chassi OR placa = l.placa) _chassi,");
            sb.AppendLine("       l.tipo_veiculo, (SELECT TOP 1 descricao_tipo");
            sb.AppendLine("                          FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws");
            sb.AppendLine("                         WHERE chassi = l.chassi OR placa = l.placa) _tipo_veiculo,");
            sb.AppendLine("       l.marca_modelo, (SELECT TOP 1 descricao");
            sb.AppendLine("                          FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws");
            sb.AppendLine("                         INNER JOIN db_link_patios_ws_prod.dbo.tb_detran_marca_modelo");
            sb.AppendLine("                            ON tb_detran_marca_modelo.id_detran_marca_modelo = tb_detran_veiculos_ws.id_detran_marca_modelo");
            sb.AppendLine("                         WHERE chassi = l.chassi OR placa = l.placa) _marca_modelo,");
            sb.AppendLine("                l.cor, (SELECT TOP 1 descricao");
            sb.AppendLine("                          FROM db_link_patios_ws_prod.dbo.tb_detran_veiculos_ws");
            sb.AppendLine("                         INNER JOIN db_global.dbo.tb_glo_sys_cores");
            sb.AppendLine("                            ON tb_detran_veiculos_ws.id_cor = db_global.dbo.tb_glo_sys_cores.id_cor");
            sb.AppendLine("                         WHERE chassi = l.chassi OR placa = l.placa) _cor");
            sb.AppendLine("  FROM tb_leilao_lotes l");
            sb.AppendLine(" LEFT JOIN tb_lotes_status s ON l.id_status_lote = s.id");
            sb.AppendLine(" INNER JOIN tb_leilao leilao ON l.id_leilao = leilao.id");
            sb.AppendLine(" WHERE 1=1");

            if (Placa != "")
            {
                sb.AppendFormat(" AND l.placa like '%{0}%'", Placa.Trim().ToUpper());
            }

            if (Chassi != "")
            {
                sb.AppendFormat(" AND l.chassi like '%{0}%'", Chassi.Trim().ToUpper());
            }

            if (Processo != "")
            {
                sb.AppendFormat(" AND l.numero_formulario_grv = '{0}'", Processo.Trim().ToUpper());
            }

            if (IdLote.HasValue)
            {
                sb.AppendFormat(" AND l.id = {0}", IdLote.Value);
            }

            var lote = ConsultaSQL(sb.ToString());

            if (lote != null)
            {
                Lote mLote = lote
                    .Rows[0]
                    .ConverterParaEntidade<Lote>();

                if (SomenteDivergentes)
                {
                    if (mLote.placa == mLote._placa &&
                        mLote.chassi == mLote._chassi &&
                        mLote.tipo_veiculo == mLote._tipo_veiculo &&
                        mLote.marca_modelo == mLote._marca_modelo &&
                        mLote.cor == mLote._cor)
                    {
                        return null;
                    }
                }

                return mLote;
            }
            else
            {
                return null;
            }
        }

        public string ConsultaDadosLotesLeilao(string DescricaoLeilao)
        {
            var ws = new Framework.WebServices.WSDsin(Util.DetectarAmbiente()); //TODO: PARAMETRIZAR!

            return ws.ConsultaDadosLotesLeilao(DescricaoLeilao);
        }

        public string ConsultaDadosLotes(string dataLeilao, int qtdeLotes, int qtdeDiasPatio, string depositos)
        {
            var ws = new Framework.WebServices.WSDsin(Util.DetectarAmbiente());

            return ws.ConsultaDadosLotes(dataLeilao, qtdeLotes, qtdeDiasPatio, depositos);
        }

        public IList<Lote> SelecionarTudo(Lote Entidade)
        {
            throw new NotImplementedException();
        }
        
        public int Inserir(Lote Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Lote Entidade)
        {
            throw new NotImplementedException();
        }

        public int AtribuirNumeracaoAutomaticaLeilao(int id)
        {
            
            var numeroLote = 0;

            var lotes = SelecionarTudo(id);

            foreach (var item in lotes)
            {
                numeroLote++;
                item.numero_lote = numeroLote;
                RepositorioGlobal.Lote.Alterar(item);
            }

            return -1;
        }

    }
}
