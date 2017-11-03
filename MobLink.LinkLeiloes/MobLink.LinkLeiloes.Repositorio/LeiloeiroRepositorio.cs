using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class LeiloeiroRepositorio : DbSqlServer, ICrud<Leiloeiro, int>
    {
        protected internal LeiloeiroRepositorio(): base(Util.DetectarConexao())
        {

        }

        public int Alterar(Leiloeiro Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Leiloeiro Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Leiloeiro Entidade)
        {
            throw new NotImplementedException();
        }

        public Leiloeiro SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Leiloeiro> SelecionarTudo()
        {
            string sql = @"
                        SELECT *, u.login NomeUsuarioCadastro
                          FROM tb_leiloeiros l
                          JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios u 
                            ON l.id_usuario_cadastro = u.id_usuario ";

            return ConsultaSQL(sql).ConverterParaLista<Leiloeiro>();
        }

        public IList<Leiloeiro> SelecionarTudo(Leiloeiro Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Leiloeiro> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
