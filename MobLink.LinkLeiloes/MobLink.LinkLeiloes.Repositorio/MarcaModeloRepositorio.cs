using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class MarcaModeloRepositorio : DbSqlServer, ICrud<MarcaModelo, int>
    {
        protected internal MarcaModeloRepositorio(): base(Util.LerConfiguracao("CONEXAO_DP"))
        {

        }

        public int Alterar(MarcaModelo Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(MarcaModelo Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(MarcaModelo Entidade)
        {
            throw new NotImplementedException();
        }

        public MarcaModelo SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<MarcaModelo> SelecionarTudo()
        {
            string sql = @"
                SELECT id_marca_modelo, descricao
                  FROM dbo.tb_dep_marcas_modelos
                  WHERE status = 'S'";

            var lista = ConsultaSQL(sql).ConverterParaLista<MarcaModelo>();
            return lista;
        }

        public IList<MarcaModelo> SelecionarTudo(MarcaModelo Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<MarcaModelo> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
