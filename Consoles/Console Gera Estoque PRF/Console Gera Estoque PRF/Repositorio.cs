using MobLink.Framework;
using MobLink.Framework.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Repositorio : DbSqlServer
    {
        public Repositorio() : base(Util.DetectarConexao())
        {

        }

        public System.Data.DataTable PegarEstoque(int id_deposito)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(string.Format(@"
                SELECT g.numero_formulario_grv, g.placa, g.chassi, m.descricao marca_modelo, d.descricao, g.data_hora_guarda
                  FROM tb_dep_grv g
                  LEFT JOIN tb_dep_marcas_modelos m ON g.id_detran_marca_modelo = m.id_marca_modelo
                  LEFT JOIN tb_dep_depositos d ON d.id_deposito = g.id_deposito
                 WHERE g.id_deposito IN ({0})
                   AND g.id_status_operacao IN ('G','L','T')
                 ORDER BY g.data_hora_guarda;", id_deposito));

            

            return ConsultaSQL(sql.ToString());
        }

        public string PegarRestricoes(string placa)
        {
            MobLink.Framework.WebServices.WSPatioxDetran ws = new MobLink.Framework.WebServices.WSPatioxDetran(Ambientes.Producao);

            var ret = ws.ConsultaVeiculo(placa, "ROOT");

            var consulta = Newtonsoft.Json.JsonConvert.DeserializeObject<LRestricao>(ret);

            if(consulta.Retorno == "Dados da Consulta Inválidos.")
            {
                return consulta.Retorno.ToUpper();
            }

            string s = string.Empty;

            foreach (var ra in consulta.RestricoesAdministrativas)
            {
                s = s + " RESTR ADMIN (" + "CÓDIGO " + ra.codigo + " RESTRIÇÃO " + ra.restricao + ")";
            }

            foreach (var rj in consulta.RestricoesJuridicas)
            {
                s = s + " RESTR JUR (" + "CÓDIGO " + rj.codigo + " RESTRIÇÃO " + rj.restricao + ")";
            }

            if (consulta.InformacaoRoubo != "")
            {
                s = s + " INFO ROUBO (" + consulta.InformacaoRoubo + ")";
            }

            if (consulta.RestricaoEstelionato != "")
            {
                s = s + " RESTR ESTEL (" + consulta.RestricaoEstelionato + ")";
            }

            return s.Trim();
        }
    }
}
