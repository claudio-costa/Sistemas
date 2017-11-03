using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class NotificacaoRepositorio : DbSqlServer, ICrud<Notificacao, int>
    {
        protected internal NotificacaoRepositorio(): base(Util.DetectarConexao())
        {

        }

        public IList<NotificacaoTipos> SelecionarTiposNotificacao(bool somente_ativos = true)
        {
            string sql = "SELECT * FROM dbo.tb_notificacoes_tipos";

            if (somente_ativos)
            {
                sql += " WHERE flg_status = 'S' ";
            }

            return ConsultaSQL(sql).ConverterParaLista<NotificacaoTipos>();
        }

        public IList<Notificacao> SelecionarTudo()
        {
            string sql = "SELECT * FROM dbo.tb_notificacao";
            return ConsultaSQL(sql).ConverterParaLista<Notificacao>();
        }

        public IList<Notificacao> SelecionarTudo(Notificacao Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Notificacao> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }

        public Notificacao SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Notificacao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Alterar(Notificacao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Notificacao Entidade)
        {
            throw new NotImplementedException();
        }
    }
}
