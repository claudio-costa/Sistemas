using MobLink.Framework;
using MobLink.Framework.CoreBusiness;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;
using System.Text;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class ExpositorRepositorio : DbSqlServer, ICrud<Expositor, int>
    {
        protected internal ExpositorRepositorio() : base(Util.DetectarConexao())
        {

        }

        public int Alterar(Expositor Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Expositor Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Expositor Entidade)
        {
            throw new NotImplementedException();
        }

        public Expositor SelecionarPorId(int id)
        {
            StringBuilder DbSqlServer = new StringBuilder();
            DbSqlServer.AppendLine("   select *              ");
            DbSqlServer.AppendLine("     from tb_expositores ");
            DbSqlServer.AppendFormat("  where id = {0}       ", id);

            return ConsultaSQL(DbSqlServer.ToString()).Rows[0]
                .ConverterParaEntidade<Expositor>();            
        }

        public IList<Expositor> SelecionarTudo()
        {
            return ConsultaSQL("select * from tb_expositores").ConverterParaLista<Expositor>();
        }

        public IList<Expositor> SelecionarTudo(Expositor Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Expositor> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
