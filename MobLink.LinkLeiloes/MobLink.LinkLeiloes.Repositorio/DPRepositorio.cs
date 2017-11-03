using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobLink.Framework;

namespace MobLink.LinkLeiloes.Repositorio
{
    public class DPRepositorio : Framework.Database.DbSqlServer
    {
        public DPRepositorio() : base(Framework.Util.LerConfiguracao("CONEXAO_DP"))
        {
        }

        public List<Dominio.GRV> SelecionarPorProcesso(string numero_formulario_grv)
        {
            string sql = string.Format(@"
            SELECT * FROM dbo.tb_dep_grv WHERE numero_formulario_grv = '{0}'", numero_formulario_grv);

            return ConsultaSQL(sql).ConverterParaLista<Dominio.GRV>();
        }
    }
}
