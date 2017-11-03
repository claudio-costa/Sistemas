using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.WebLeilao.Repositorio
{
    public class EditalRepositorio : DbSqlServer, ICrud<Edital, int>
    {
        protected internal EditalRepositorio() : base(Util.DetectarConexao())
        {
        }

        public int Alterar(Edital Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Edital Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Edital Entidade)
        {
            string sql = string.Format(@"
            INSERT INTO dbo.tb_leilao_editais (id_leilao, id_usuario_geracao)
            VALUES ({0}, {1})
            ", Entidade.id_leilao, Entidade.id_usuario_geracao);

            return ExecutaSQL(sql);
        }

        public Edital SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Edital> SelecionarTudo()
        {
            throw new NotImplementedException();
        }

        public IList<Edital> SelecionarTudo(Leilao Leilao)
        {
            string sql = string.Format(@"SELECT * FROM tb_leilao_editais WHERE id_leilao = {0}", Leilao.id);
            return ConsultaSQL(sql).ConverterParaLista<Edital>();
        }

        public IList<Edital> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Edital> SelecionarTudo(Edital Entidade)
        {
            throw new NotImplementedException();
        }
    }
}
