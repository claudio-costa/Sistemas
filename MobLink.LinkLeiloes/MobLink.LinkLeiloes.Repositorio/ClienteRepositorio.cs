using MobLink.Framework;
using MobLink.Framework.CoreBusiness;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.WebLeilao.Repositorio
{
    public class ClienteRepositorio : DbSqlServer, ICrud<Cliente, int>
    {
        protected internal ClienteRepositorio() : base(Util.LerConfiguracao("CONEXAO_DP"))
        {
        }

        public int Alterar(Cliente Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Cliente Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Cliente Entidade)
        {
            throw new NotImplementedException();
        }

        public Cliente SelecionarPorId(int id)
        {
            return ConsultaSQL(string.Format(@"
                        SELECT CLI.id_cliente, CLI.nome, CLI.endereco_completo
                          FROM dbo.vw_dep_clientes CLI
                         WHERE CLI.flag_ativo = 'S' 
                           AND CLI.id_cliente = {0}", id)).Rows[0].ConverterParaEntidade<Cliente>();
        }

        public IList<Cliente> SelecionarTudo()
        {
            return ConsultaSQL(@"
                        SELECT CLI.id_cliente, CLI.nome
                          FROM dbo.tb_dep_clientes CLI
                         WHERE CLI.flag_ativo = 'S' ").ConverterParaLista<Cliente>();
        }

        public IList<Cliente> SelecionarTudo(Cliente Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Cliente> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}