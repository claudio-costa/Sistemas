using MobLink.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportarExcel.VipLeiloes
{
    public class VipRepositorio : MobLink.Framework.Database.DbSqlServer
    {
        public VipRepositorio() : base(MobLink.Framework.Util.LerConfiguracao("CONEXAO_VIP"))
        {

        }

        public static void Complementar_tb_boleto_lotes(string descricao_leilao)
        {
            VipRepositorio vipRepositorio = new VipRepositorio();

            MobLink.Framework.WebServices.WSVipBoleto wSVipBoleto = new MobLink.Framework.WebServices.WSVipBoleto(MobLink.Framework.Ambientes.Desenvolvimento);
            var retorno = wSVipBoleto.FinanceiroDetalhamento(descricao_leilao);

            var reg = retorno.FirstOrDefault();

            foreach (var item in retorno)
            {
                Console.WriteLine(item.numero_boleto);

                if (item.numero_boleto == "110704")
                {
                    
                    
                }

                //Preciso consultar aqui a tabela tb_boleto_lotes
                string sql = string.Format("SELECT id FROM dbo.tb_boleto_lotes WHERE leilao = 'TRGD02.17' AND lote_leilao = '{0}'", item.lote);
                var consulta = vipRepositorio.ConsultaSQL(sql);

                if (consulta.Rows.Count == 0)
                {
                    //105, 83, 762, 165, 349, 118, 1001
                }

                foreach (DataRow row in consulta.Rows)
                {
                    var sqlUpdate = string.Format(@"
                            UPDATE dbo.tb_boleto_lotes
                            SET   comissao = '{0}'
	                            , taxa_administrativa = '{1}'
	                            , valor_arrematacao = '{2}'
                            WHERE id in ({3});", item.comissao_v.ToString(), item.taxasfixas.ToString(), item.arrematacao.ToString(), row[0].ToString());

                    vipRepositorio.ExecutaSQL(sqlUpdate);
                }
            }
        }
    }
}
