using MobLink.Framework;
using MobLink.WebLeilao.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestesServicos
{
    class Program
    {
        static void Main(string[] args)
        {
            //LOTES DE LEILÕES COM POSSIBILIDADE DE PAGAMENTO
            string sql_lotes = @"
            SELECT NUMERO_LOTE
					      FROM tb_leilao_lotes 
					     WHERE id_leilao IN (SELECT id
					                           FROM tb_leilao 
					                          WHERE id_status IN (SELECT id 
					                                                FROM tb_leilao_status 
					                                               WHERE ativo = 'P')) ";

            var consulta_lotes = RepositorioGlobal.Util.ConsultaGenerica(Util.LerConfiguracao("CONEXAO_LEILAO"), sql_lotes);

            string sql_pagamentos = @"
            SELECT *
              FROM dbMoblinkBoletos.dbo.tb_boleto_lotes
              JOIN dbMoblinkBoletos.dbo.view_boletos_creditados ON dbMoblinkBoletos.dbo.view_boletos_creditados.identificacao = dbMoblinkBoletos.dbo.tb_boleto_lotes.id_boleto
             WHERE leilao = 'TRGD02.17'
               AND RTRIM(LTRIM(lote_leilao)) = '888'
               AND boleto_data_arrecadado<> '01/01/0001'";

            var consulta_pagamentos = RepositorioGlobal.Util.ConsultaGenerica(Util.LerConfiguracao("CONEXAO_BOLETOS"), sql_pagamentos);

            //SE HOUVER RETORNO DA CONSULTA ACIMA, O LOTE POSSUI UM PAGAMENTO

            //CADASTRAR FLAGS DE CONTROLE DE CRIAÇÃO DE CLIENTE E ORDEM FB70
            
        }
    }
}
