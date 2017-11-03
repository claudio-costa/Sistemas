using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class StatusLoteRepositorio : DbSqlServer, ICrud<StatusLote, int>
    {
        protected internal StatusLoteRepositorio() : base(Util.DetectarConexao())
        {

        }

        public int Alterar(StatusLote Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(StatusLote Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(StatusLote Entidade)
        {
            throw new NotImplementedException();
        }

        public StatusLote SelecionarPorId(int id)
        {
            return ConsultaSQL(string.Format("select * from tb_lotes_status where id = {0}", id))
                .Rows[0]
                .ConverterParaEntidade<StatusLote>();
        }

        public IList<StatusLote> SelecionarTudo()
        {
            return ConsultaSQL("SELECT * FROM tb_lotes_status ORDER BY descricao")
                .ConverterParaLista<StatusLote>();
        }

        public IList<StatusLote> SelecionarTudo(StatusLote Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<StatusLote> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
