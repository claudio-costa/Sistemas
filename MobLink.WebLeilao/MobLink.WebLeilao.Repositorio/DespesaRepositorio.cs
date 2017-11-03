using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;
using System.Text;


namespace MobLink.WebLeilao.Repositorio
{
    public class DespesaRepositorio : DbSqlServer, ICrud<Despesa, int>
    {
        protected internal DespesaRepositorio() : base(Util.DetectarConexao())
        {

        }
        
        public int ExcluirDespesaLeilao(int IdDespesa)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("DELETE dbo.tb_leilao_despesas");
            SQL.AppendFormat("WHERE id = {0}", IdDespesa);
            return ExecutaSQL(SQL.ToString());
        }

        public int InserirDespesaLeilao(Despesa_Leilao entidade)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO dbo.tb_leilao_despesas(id_leilao, id_despesa, valor)");
            SQL.AppendFormat("VALUES({0}, {1}, {2})", entidade.Id_Leilao, entidade.Id_Despesa, entidade.Valor.ToString().Replace(",", "."));
            return ExecutaSQL(SQL.ToString());
        }

        public int InserirDespesaLote(Despesa_Lote entidade)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO dbo.tb_leilao_lotes_despesas(id_lote, id_despesa, valor)");
            SQL.AppendFormat("VALUES({0}, {1}, {2})", entidade.Id_Lote, entidade.Id_Despesa, entidade.Valor.ToString().Replace(",", "."));
            return ExecutaSQL(SQL.ToString());
        }
        
        public IList<Despesa> SelecionarTudo()
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("  SELECT id_faturamento_servico_tipo id, descricao, ordem_impressao ordem, tipo_cobranca");

            sql.AppendLine("    FROM dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos");

            sql.AppendLine("   WHERE faturamento_produto_codigo = 'LEP'");

            sql.AppendLine("     AND flag_ativo = 'S'");

            sql.AppendLine("ORDER BY ordem_impressao");

            return ConsultaSQL(sql.ToString()).ConverterParaLista<Despesa>();
        }

        public IList<Despesa_Lote> SelecionarDespesasLote(int id)
        {
            if (id == 0)
            {
                return new List<Despesa_Lote>();
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("            SELECT dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos.descricao DescricaoDespesa,                           ");
            sql.AppendLine("        tb_leilao_lotes_despesas.valor                                                                                                                 ");
            sql.AppendLine("   FROM tb_leilao_lotes_despesas                                                                                                                       ");
            sql.AppendLine("   JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos                                                                  ");
            sql.AppendLine("     ON dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos.id_faturamento_servico_tipo = tb_leilao_lotes_despesas.id_despesa");
            sql.AppendLine("                                                                                                                                                       ");
            sql.AppendLine("                                                                                                                                                       ");
            sql.AppendLine(string.Format("   WHERE id_lote = {0}                                                                                                                   ", id));
            sql.AppendLine("   ORDER BY dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos.ordem_impressao                                              ");
            
            return ConsultaSQL(sql.ToString()).ConverterParaLista<Despesa_Lote>();
        }

        public IList<Despesa_Leilao> SelecionarDespesasLeilao(int IdLeilao)
        {
            StringBuilder SQL = new StringBuilder();
            //SQL.AppendLine("  SELECT despesas.*, servicos.descricao DescricaoDespesa, leilao.descricao DescricaoLeilao        ");
            //SQL.AppendLine("    FROM tb_leilao_despesas despesas                                                              ");
            //SQL.AppendLine("    JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos servicos   ");
            //SQL.AppendLine("      ON despesas.id_despesa = servicos.id_faturamento_servico_tipo                               ");
            //SQL.AppendLine("    JOIN tb_leilao leilao                                                                         ");
            //SQL.AppendLine("      ON despesas.id_leilao = leilao.id                                                           ");
            //SQL.AppendFormat("   WHERE despesas.id_leilao = {0}", IdLeilao);
            //SQL.AppendLine("ORDER BY servicos.ordem_impressao                                                                 ");

            SQL.AppendLine("  SELECT despesas.*, servicos.descricao DescricaoDespesa, leilao.descricao DescricaoLeilao     ");
            SQL.AppendLine("    FROM tb_leilao_despesas despesas                                                           ");
            SQL.AppendLine("    JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_faturamento_servicos_tipos servicos       ");
            SQL.AppendLine("      ON despesas.id_despesa = servicos.id_faturamento_servico_tipo                            ");
            SQL.AppendLine("    JOIN tb_leilao leilao                                                                      ");
            SQL.AppendLine("      ON despesas.id_leilao = leilao.id                                                        ");
            SQL.AppendFormat(" WHERE despesas.id_leilao = {0}", IdLeilao);
            SQL.AppendLine("     AND servicos.faturamento_produto_codigo = 'LEP'                                           ");
            SQL.AppendLine("     AND servicos.flag_ativo = 'S'                                                             ");
            SQL.AppendLine("   ORDER BY servicos.ordem_impressao                                                           ");

            return ConsultaSQL(SQL.ToString()).ConverterParaLista<Despesa_Leilao>();
        }

        public IList<Despesa> SelecionarTudo(Despesa Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Despesa> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }

        public Despesa SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Despesa Entidade)
        {
            throw new NotImplementedException();
        }

        public int Alterar(Despesa Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Despesa Entidade)
        {
            throw new NotImplementedException();
        }
    }
}
