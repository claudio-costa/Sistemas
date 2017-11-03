using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;
using System.Text;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class StatusLeilaoRepositorio : DbSqlServer, ICrud<StatusLeilao, int>
    {
        protected internal StatusLeilaoRepositorio() : base(Util.DetectarConexao())
        {

        }

        public int Alterar(StatusLeilao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(StatusLeilao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(StatusLeilao Entidade)
        {
            throw new NotImplementedException();
        }

        public StatusLeilao SelecionarPorId(int id)
        {
            return ConsultaSQL(string.Format("select * from tb_leilao_status where id = {0}", id))
                .ConverterParaEntidade<StatusLeilao>();
        }

        public IList<StatusLeilao> SelecionarTudo()
        {
            return ConsultaSQL(@"SELECT * FROM tb_leilao_status")
                .ConverterParaLista<StatusLeilao>();
        }

        public IList<StatusLeilao> SelecionarTudo(StatusLeilao Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<StatusLeilao> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }

        public IList<StatusLeilao> SelecionarTudoEmUso()
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("  SELECT tb_leilao_status.id, tb_leilao_status.descricao , count(*) ContagemLeiloes ");
            SQL.AppendLine("    FROM tb_leilao_status                                                           ");
            SQL.AppendLine("    JOIN tb_leilao ON tb_leilao_status.id = tb_leilao.id_status                     ");
            SQL.AppendLine("GROUP BY tb_leilao_status.id, tb_leilao_status.descricao                            ");

            return ConsultaSQL(SQL.ToString())
                .ConverterParaLista<StatusLeilao>();
        }

    }
}
