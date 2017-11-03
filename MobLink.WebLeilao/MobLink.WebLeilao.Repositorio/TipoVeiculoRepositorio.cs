using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.WebLeilao.Repositorio
{
    public class TipoVeiculoRepositorio : DbSqlServer, ICrud<TipoVeiculo, int>
    {
        protected internal TipoVeiculoRepositorio(): base(Util.LerConfiguracao("CONEXAO_DP"))
        {

        }

        public int Alterar(TipoVeiculo Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(TipoVeiculo Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(TipoVeiculo Entidade)
        {
            throw new NotImplementedException();
        }

        public TipoVeiculo SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<TipoVeiculo> SelecionarTudo()
        {
            return ConsultaSQL(@"
                SELECT id_tipo_veiculo id, descricao
                  FROM tb_dep_tipo_veiculos
                  WHERE flag_ativo = 'S'")
                 .ConverterParaLista<TipoVeiculo>();
        }

        public IList<TipoVeiculo> SelecionarTudo(TipoVeiculo Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<TipoVeiculo> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
