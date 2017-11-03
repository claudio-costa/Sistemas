using System;
using System.Collections.Generic;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.Framework;

namespace ConsoleApp1
{
    public class LeilaoRepositorio : DbSqlServer, ICrud<Leilao, int>
    {
        public LeilaoRepositorio(string ConnectionString) : base(Util.DetectarConexao())
        {
        }

        public int Alterar(Leilao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Leilao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Leilao Entidade)
        {
            throw new NotImplementedException();
        }

        public Leilao SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Leilao> SelecionarTudo()
        {
            return ConsultaSQL("select * from tb_dep_orgao_executivo_transito").ConverterParaLista<Leilao>();
        }

        public IList<Leilao> SelecionarTudo(Leilao Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Leilao> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
