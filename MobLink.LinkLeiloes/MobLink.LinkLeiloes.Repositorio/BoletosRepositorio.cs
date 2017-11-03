using System;
using System.Text;

namespace MobLink.LinkLeiloes.Repositorio
{
    public class BoletosRepositorio : Framework.Database.DbSqlServer
    {
        public BoletosRepositorio() : base(Framework.Util.LerConfiguracao("CONEXAO_BOLETOS"))
        {
        }

        public System.Data.DataTable GetList_arrematante_recebimento(int id, string opcao_filtro = "") //id_leilao
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("      SELECT                                                                                                                   ");
            sql.AppendLine("           view_boletos_creditados.identificacao AS id_boleto,                                                                 ");
            sql.AppendLine("           view_boletos_creditados.boleto_vencimento,                                                                          ");
            sql.AppendLine("           view_boletos_creditados.valor_cobrado,                                                                              ");
            sql.AppendLine("           view_boletos_creditados.boleto_banco,                                                                               ");
            sql.AppendLine("           view_boletos_creditados.data_documento,                                                                             ");
            sql.AppendLine("           view_boletos_creditados.boleto_data_arrecadado AS data_credito_boleto,                                              ");
            sql.AppendLine("           dbLeilao.dbo.tb_leilao.descricao AS leilao,                                                                         ");
            sql.AppendLine("           tb_boleto_lotes.lote_leilao,                                                                                        ");
            sql.AppendLine("           dbLeilao.dbo.tb_leilao_lotes.placa,                                                                                 ");
            sql.AppendLine("           dbLeilao.dbo.tb_leilao_lotes.chassi,                                                                                ");
            sql.AppendLine("           dbLeilao.dbo.tb_leilao_lotes.marca_modelo,                                                                          ");
            sql.AppendLine("           dbLeilao.dbo.tb_leilao_lotes.cor,                                                                                   ");
            sql.AppendLine("           dbLeilao.dbo.tb_leilao_lotes.tipo_veiculo,                                                                          ");
            sql.AppendLine("           dbLeilao.dbo.tb_lotes_status.descricao AS id_status_lote,                                                              ");
            sql.AppendLine("           dbLeilao.dbo.tb_leilao_lotes.valor_avaliacao,                                                                       ");
            sql.AppendLine("           tb_boleto_lotes.valor_arrematacao,                                                                                  ");
            sql.AppendLine("           tb_boleto_lotes.taxa_administrativa,                                                                                ");
            sql.AppendLine("           tb_boleto_lotes.outras_taxas,                                                                                       ");
            sql.AppendLine("           tb_boleto_lotes.comissao,                                                                                           ");
            sql.AppendLine("           dbLeilao.dbo.tb_leiloeiros.comissao AS taxa_comissao_leiloeiro,                                                     ");
            sql.AppendLine("           3.7 AS taxa_bancaria,                                                                                               ");
            sql.AppendLine("           view_boletos_creditados.valor_pago,                                                                                 ");
            sql.AppendLine("           tb_boletos.sacado_cpfCnpj,                                                                                          ");
            sql.AppendLine("           tb_boletos.sacado_nome                                                                                              ");
            sql.AppendLine("       FROM view_boletos_creditados                                                                                            ");
            sql.AppendLine("       LEFT JOIN tb_boleto_lotes ON tb_boleto_lotes.id_boleto = view_boletos_creditados.identificacao                          ");
            sql.AppendLine("       JOIN tb_boletos ON tb_boletos.id_boleto = view_boletos_creditados.identificacao                                         ");
            sql.AppendLine("       JOIN dbLeilao.dbo.tb_leilao_lotes ON dbLeilao.dbo.tb_leilao_lotes.numero_lote = tb_boleto_lotes.lote_leilao             ");
            sql.AppendLine("       JOIN dbLeilao.dbo.tb_leilao ON dbLeilao.dbo.tb_leilao_lotes.id_leilao = dbLeilao.dbo.tb_leilao.id                       ");
            sql.AppendLine("       JOIN dbLeilao.dbo.tb_lotes_status ON dbLeilao.dbo.tb_lotes_status.id = dbLeilao.dbo.tb_leilao_lotes.id_status_lote         ");
            sql.AppendLine("       JOIN dbLeilao.dbo.tb_leiloeiros ON dbLeilao.dbo.tb_leiloeiros.id = dbLeilao.dbo.tb_leilao.id_leiloeiro                  ");
            sql.AppendLine("       WHERE 1 = 1                                                                                                             ");
            sql.AppendLine("       --boleto_data_arrecadado IN ('04/09/2017', '01/09/2017', '02/09/2017')                                                  ");
            sql.AppendLine("       --AND view_boletos_creditados.boleto_banco = '0237'                                                                     ");
            sql.AppendLine("       --AND TB_BOLETOS.id_interface_usuario = 3                                                                               ");
            sql.AppendFormat("       AND dbLeilao.dbo.tb_leilao_lotes.id_leilao = {0}                                                                        ", id);

            if (opcao_filtro != "")
            {
                if (opcao_filtro == "0")
                {
                    sql.AppendFormat(" AND boleto_data_arrecadado = '{0}'", DateTime.Now.ToShortDateString());
                }

                if (opcao_filtro == "1")
                {
                    sql.AppendFormat(" AND CONVERT(DATE, boleto_data_arrecadado, 103) > CONVERT(DATE, '{0}', 103)", DateTime.Now.AddDays(-7).ToShortDateString());
                }

                if (opcao_filtro == "2")
                {
                    sql.AppendFormat(" AND CONVERT(DATE, boleto_data_arrecadado, 103) > CONVERT(DATE, '{0}', 103)", DateTime.Now.AddDays(-30).ToShortDateString());
                }
            }

            sql.AppendLine("            AND boleto_data_arrecadado NOT IN ('01/01/0001')        ");
            sql.AppendLine("       ORDER BY view_boletos_creditados.boleto_data_arrecadado DESC "); 

            return ConsultaSQL(sql.ToString());
        }



    }
}
