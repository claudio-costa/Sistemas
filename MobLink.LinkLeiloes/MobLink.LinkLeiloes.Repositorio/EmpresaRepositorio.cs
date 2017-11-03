using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.WebLeilao.Repositorio
{
    public class EmpresaRepositorio : DbSqlServer, ICrud<Empresa, int>
    {
        protected internal EmpresaRepositorio() : base(Util.LerConfiguracao("CONEXAO_GLOBAL"))
        {

        }

        public int Alterar(Empresa Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Empresa Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Empresa Entidade)
        {
            throw new NotImplementedException();
        }

        public Empresa SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Empresa> SelecionarTudo()
        {
            string sql = @"
                 SELECT id_empresa as id, codigo_sap as codigosap, nome as descricao 
                   FROM db_global.dbo.tb_glo_emp_empresas
                    WHERE codigo_sap IS NOT NULL AND id_empresa_classificacao = 1
                    ORDER BY nome ";

            var lista = ConsultaSQL(sql).ConverterParaLista<Empresa>();

            return lista;
        }

        public IList<Empresa> SelecionarTudo(Empresa Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Empresa> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
