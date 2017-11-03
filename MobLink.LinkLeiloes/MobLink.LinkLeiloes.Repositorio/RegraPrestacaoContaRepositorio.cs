using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Repositorio
{
    public class RegraPrestacaoContaRepositorio : DbSqlServer, ICrud<RegraPrestacaoConta, int>
    {
        public RegraPrestacaoContaRepositorio() : base(Util.LerConfiguracao("CONEXAO"))
        {
        }

        public int Alterar(RegraPrestacaoConta Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(RegraPrestacaoConta Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(RegraPrestacaoConta Entidade)
        {
            throw new NotImplementedException();
        }

        public RegraPrestacaoConta SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<RegraPrestacaoConta> SelecionarTudo()
        {
            string sql = "select * from dbo.tb_leilao_regras_prestacao_contas";
            return ConsultaSQL(sql).ConverterParaLista<RegraPrestacaoConta>();
        }

        public IList<RegraPrestacaoConta> SelecionarTudo(RegraPrestacaoConta Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<RegraPrestacaoConta> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
