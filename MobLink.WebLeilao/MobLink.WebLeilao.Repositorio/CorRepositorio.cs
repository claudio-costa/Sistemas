using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.WebLeilao.Repositorio
{
    public class CorRepositorio : DbSqlServer, ICrud<Cor, int>
    {
        protected internal CorRepositorio() : base(Util.LerConfiguracao("CONEXAO_GLOBAL"))
        {

        }

        public int Alterar(Cor Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Cor Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Cor Entidade)
        {
            throw new NotImplementedException();
        }

        public Cor SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Cor> SelecionarTudo()
        {
            return ConsultaSQL(@"

                    SELECT id_cor, descricao, descricao_secundaria
                      FROM db_global.dbo.tb_glo_sys_cores
                    WHERE flag_ativo = 'S'

                 ").ConverterParaLista<Cor>();
        }

        public IList<Cor> SelecionarTudo(Cor Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Cor> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
