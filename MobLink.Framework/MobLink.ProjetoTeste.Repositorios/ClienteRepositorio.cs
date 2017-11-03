using System;
using System.Collections.Generic;
using System.Linq;
using MobLink.ProjetoTeste.Dominios;
using System.Data;
using MobLink.Framework.Comum.AdoNet;
using MobLink.Framework.Comum.Repositorio;

namespace MobLink.ProjetoTeste.Repositorios
{
    public class ClienteRepositorio : RepositorioSqlServer, IRepositorioGenerico<Cliente, int>
    {
        public ClienteRepositorio() : base(ConexoesProntas.dbLeilao)
        {
            var result = SelecionarTudo();

            Cliente ret = SelecionarPorId(110);

            var teste = string.Empty;
        }

        public IList<Cliente> SelecionarTudo()
        {   
            ComandoSQL.Add("select * from tb_leilao");
            return RetornarConsulta().ConverterParaLista<Cliente>();
        }

        public Cliente SelecionarPorId(int id)
        {
            ComandoSQL.Add("select * from tb_leilao where id = " + id);
            return RetornarConsulta().AsEnumerable().FirstOrDefault().ConverterParaEntidade<Cliente>();
        }

        public void Inserir(Cliente entidade)
        {
            throw new NotImplementedException();
        }

        public void Alterar(Cliente entidade)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Cliente entidade)
        {
            throw new NotImplementedException();
        }

        public void ExcluirPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
