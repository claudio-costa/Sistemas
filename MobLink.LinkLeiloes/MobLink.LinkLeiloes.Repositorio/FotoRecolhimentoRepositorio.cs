using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;
using System.Text;


namespace MobLink.WebLeilao.Repositorio
{
    public class FotoRecolhimentoRepositorio : DbSqlServer, ICrud<FotoRecolhimento, int>
    {
        protected internal FotoRecolhimentoRepositorio() : base(Util.DetectarConexao())
        {

        }

        public int Alterar(FotoRecolhimento Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(FotoRecolhimento Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(FotoRecolhimento Entidade)
        {
            throw new NotImplementedException();
        }

        public FotoRecolhimento SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<FotoRecolhimento> SelecionarTudo(int IdLote)
        {
            StringBuilder DbSqlServer = new StringBuilder();
            DbSqlServer.AppendLine(" select * from tb_leilao_lotes_fotos ");
            DbSqlServer.AppendFormat(" where id_lote = {0} ", IdLote);
            
            var lista = ConsultaSQL(DbSqlServer.ToString()).ConverterParaLista<FotoRecolhimento>();
            return lista;
        }

        public IList<FotoRecolhimento> SelecionarTudo()
        {
            throw new NotImplementedException();
        }

        public IList<FotoRecolhimento> SelecionarTudo(FotoRecolhimento Entidade)
        {
            throw new NotImplementedException();
        }
    }
}
