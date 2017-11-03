using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobLink.WebLeilao.Repositorio
{
    public class RestricaoRepositorio : DbSqlServer, ICrud<Restricao, int>
    {
        protected internal RestricaoRepositorio() : base(Util.DetectarConexao())
        {

        }

        public int Alterar(Restricao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Restricao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Restricao entidade)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO dbo.tb_leilao_lotes_restricoes ");
             
            sql.AppendLine("   (id_lote                                  ");
            sql.AppendLine(entidade.codigo == null ? "" : "   , codigo   ");
            sql.AppendLine("   , restricao                               ");
            sql.AppendLine("   , sub_restricao                           ");
            sql.AppendLine("   , observacoes                             ");
            sql.AppendLine("   , origem)                                 ");

            sql.AppendLine("VALUES                                       ");

            sql.AppendLine(string.Format(" ({0}  ", entidade.id_lote));
            sql.AppendLine(entidade.codigo == null ? "" : string.Format(", {0}  ", entidade.codigo));
            sql.AppendLine(string.Format(",'{0}' ", entidade.restricao));
            sql.AppendLine(string.Format(",'{0}' ", entidade.sub_restricao));
            sql.AppendLine(string.Format(",'{0}' ", entidade.observacoes));
            sql.AppendLine(string.Format(",'{0}')", entidade.origem));

            try
            {
                return ExecutaSQL(sql.ToString());
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public Restricao SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Restricao> SelecionarTudo(int id)
        {
            if (id == 0)
            {
                return new List<Restricao>();
            }

            var dt = ConsultaSQL(string.Format(@"SELECT * FROM dbo.tb_leilao_lotes_restricoes where id_lote = {0}", id));
            
            var ret = dt != null ? dt.ConverterParaLista<Restricao>() : new List<Restricao>();

            return ret;
        }

        public IList<Restricao> SelecionarTudo()
        {
            throw new NotImplementedException();
        }

        public IList<Restricao> SelecionarTudo(Restricao Entidade)
        {
            throw new NotImplementedException();
        }
    }
}
