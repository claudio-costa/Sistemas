using System;
using System.Collections.Generic;
using MobLink.LinkLeiloes.Dominio;
using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;

namespace MobLink.LinkLeiloes.Repositorio
{
    public class NotificaoLeilaoRepositorio : DbSqlServer, ICrud<NotificacaoLeilao, int>
    {
        public NotificaoLeilaoRepositorio(): base(Util.DetectarConexao())
        {
        }

        public int Alterar(NotificacaoLeilao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(NotificacaoLeilao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(NotificacaoLeilao Entidade)
        {
            throw new NotImplementedException();
        }

        public NotificacaoLeilao SelecionarPorId(int id)
        {
            string sql = string.Format(@"
            SELECT tb_leilao_notificacoes.*, tb_leilao.descricao descricao_leilao, u.login, tb_notificacao.descricao
              FROM tb_leilao_notificacoes
             INNER JOIN tb_notificacao ON tb_notificacao.id = tb_leilao_notificacoes.id_notificacao
             INNER JOIN tb_leilao ON tb_leilao.id = tb_leilao_notificacoes.id_leilao
             INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios u
                     ON tb_leilao_notificacoes.id_usuario = u.id_usuario
                  WHERE tb_leilao.id = {0}", id);

            var not = ConsultaSQL(sql);

            return not.Rows.Count > 0 ? not.Rows[0].ConverterParaEntidade<NotificacaoLeilao>() : new NotificacaoLeilao();
        }

        public IList<NotificacaoLeilao> SelecionarTudo()
        {
            throw new NotImplementedException();
        }

        public IList<NotificacaoLeilao> SelecionarTudo(NotificacaoLeilao Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<NotificacaoLeilao> SelecionarTudo(int id)
        {
            string sql = string.Format(@"
            SELECT tb_leilao_notificacoes.*, tb_leilao.descricao descricao_leilao, u.login, tb_notificacao.descricao
              FROM tb_leilao_notificacoes
             INNER JOIN tb_notificacao ON tb_notificacao.id = tb_leilao_notificacoes.id_notificacao
             INNER JOIN tb_leilao ON tb_leilao.id = tb_leilao_notificacoes.id_leilao
             INNER JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios u
                     ON tb_leilao_notificacoes.id_usuario = u.id_usuario
                  WHERE tb_leilao.id = {0}", id);

            var not = ConsultaSQL(sql);

            return ConsultaSQL(sql).ConverterParaLista<NotificacaoLeilao>();
        }
    }
}
