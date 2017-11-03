using MobLink.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransmissaoFB70
{
    public class PagamentosRepositorio : MobLink.Framework.Database.DbSqlServer
    {
        protected PagamentosRepositorio() : base(MobLink.Framework.Util.LerConfiguracao(""))
        {

        }

        internal List<Pagamento> GetList(int id_leilao)
        {
            string sql = @"
            SELECT a.valor_cobrado, a.valor_pago, a.data_documento, a.boleto_data_arrecadado,
                   b.id_boleto, b.comissao, b.desconto, b.outras_taxas, b.taxa_administrativa, b.valor_arrematacao, b.valor_caucao,
                   c.id, c.descricao, c.ordem_interna_matriz, c.ordem_interna_leilao, c.id_comitente
              FROM dbMoblinkBoletos.dbo.view_boletos_creditados a
              JOIN dbMoblinkBoletos.dbo.tb_boleto_lotes b ON a.identificacao = b.id_boleto
              JOIN dbLeilao.dbo.tb_leilao c ON b.leilao = c.descricao
              AND c.id = 65";

            return ConsultaSQL(sql).ConverterParaLista<Pagamento>();
        }
    }
}
