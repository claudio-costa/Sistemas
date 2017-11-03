using MobLink.Framework;
using MobLink.Framework.CoreBusiness;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobLink.LinkLeiloes.Repositorio
{
    public class ArrematanteRepositorio : DbSqlServer, ICrud<Arrematante, int>
    {
        public ArrematanteRepositorio() : base(Framework.Util.DetectarConexao())
        {
            ArrematanteMapperProfile.Iniciar();
        }

        public int Alterar(Arrematante Entidade)
        {
            string sql = string.Empty;

            sql = "UPDATE dbo.tb_leilao_lotes_arrematantes SET";

            sql += string.Format("\n" + "	lote = 										'{0}', ", Entidade.lote);
            sql += string.Format("\n" + "	numero_processo = 							'{0}', ", Entidade.numero_processo);
            sql += string.Format("\n" + "	leilao = 									'{0}', ", Entidade.leilao);
            sql += string.Format("\n" + "	placa = 									'{0}', ", Entidade.placa);
            sql += string.Format("\n" + "	chassi = 									'{0}', ", Entidade.chassi);
            sql += string.Format("\n" + "	nome_arrematante = 							'{0}', ", Entidade.nome_arrematante);
            sql += string.Format("\n" + "	cpf = 										'{0}', ", Entidade.cpf);
            sql += string.Format("\n" + "	cnpj = 										'{0}', ", Entidade.cnpj);
            sql += string.Format("\n" + "	fone_1 = 									'{0}', ", Entidade.fone_1);
            sql += string.Format("\n" + "	fone_2 = 									'{0}', ", Entidade.fone_2);
            sql += string.Format("\n" + "	email = 									'{0}', ", Entidade.email);
            sql += string.Format("\n" + "	logradouro = 								'{0}', ", Entidade.logradouro);
            sql += string.Format("\n" + "	numero = 									'{0}', ", Entidade.numero);
            sql += string.Format("\n" + "	complemento = 								'{0}', ", Entidade.complemento);
            sql += string.Format("\n" + "	bairro = 									'{0}', ", Entidade.bairro);
            sql += string.Format("\n" + "	cidade = 									'{0}', ", Entidade.cidade);
            sql += string.Format("\n" + "	estado = 									'{0}', ", Entidade.estado);
            sql += string.Format("\n" + "	cep = 										'{0}', ", Entidade.cep);
            sql += string.Format("\n" + "	numero_boleto = 							'{0}', ", Entidade.numero_boleto);
            sql += string.Format("\n" + "	linha_digitavel = 							'{0}', ", Entidade.linha_digitavel);
            
            sql += string.Format("\n" + "	data_emissao_boleto = 						'{0}', ", Entidade.data_emissao_boleto);
            sql += string.Format("\n" + "	data_vencimento_boleto = 					'{0}', ", Entidade.data_vencimento_boleto);
            sql += string.Format("\n" + "	data_pagamento_boleto = 					'{0}', ", Entidade.data_pagamento_boleto);
            sql += string.Format("\n" + "	status_boleto = 							'{0}', ", Entidade.status_boleto);
            sql += string.Format("\n" + "	avaliacao = 								'{0}', ", Entidade.avaliacao);
            sql += string.Format("\n" + "	arrematacao = 								'{0}', ", Entidade.arrematacao);
            sql += string.Format("\n" + "	descontos = 								'{0}', ", Entidade.descontos);
            sql += string.Format("\n" + "	taxa_administrativa = 						'{0}', ", Entidade.taxa_administrativa);
            sql += string.Format("\n" + "	outras_taxas = 								'{0}', ", Entidade.outras_taxas);
            sql += string.Format("\n" + "	tarifa_bancaria = 							'{0}', ", Entidade.tarifa_bancaria);
            sql += string.Format("\n" + "	valor_total = 								'{0}', ", Entidade.valor_total);
            sql += string.Format("\n" + "	valor_pago = 								'{0}', ", Entidade.valor_pago);
            sql += string.Format("\n" + "	iss = 										'{0}', ", Entidade.iss);
            sql += string.Format("\n" + "	comissao = 									'{0}', ", Entidade.comissao);
            sql += string.Format("\n" + "	cartela = 									'{0}', ", Entidade.cartela);
            sql += string.Format("\n" + "	status_cadastro_cliente_sap = 				'{0}', ", Entidade.status_cadastro_cliente_sap);
            sql += string.Format("\n" + "	status_cadastro_fb70_sap = 					'{0}', ", Entidade.status_cadastro_fb70_sap);
            sql += string.Format("\n" + "	id_documento_cliente_sap = 					'{0}', ", Entidade.id_documento_cliente_sap);
            sql += string.Format("\n" + "	id_documento_fb70_sap = 					'{0}', ", Entidade.id_documento_fb70_sap);
            sql += string.Format("\n" + "	id_grv = 				                	'{0}'  ", Entidade.id_grv);

            sql += string.Format("\n" + "	WHERE id = 					                 {0}   ", Entidade.id);

            return ExecutaSQL(sql);
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Arrematante Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Arrematante Entidade)
        {
            try
            {
                StringBuilder DbSqlServer = new StringBuilder();

                DbSqlServer.AppendLine("INSERT INTO dbo.tb_leilao_lotes_arrematantes (	                  ");
                DbSqlServer.AppendLine("						 Lote                        		      ");
                DbSqlServer.AppendLine("						,leilao                                   ");
                DbSqlServer.AppendLine("						,Placa                                    ");
                DbSqlServer.AppendLine("						,Chassi                                   ");
                DbSqlServer.AppendLine("						,Nome_Arrematante                         ");
                DbSqlServer.AppendLine("						,Cpf                                      ");
                DbSqlServer.AppendLine("						,cnpj                                     ");
                DbSqlServer.AppendLine("						,Fone_1                                   ");
                DbSqlServer.AppendLine("						,Fone_2                                   ");
                DbSqlServer.AppendLine("						,Email                                    ");
                DbSqlServer.AppendLine("						,Logradouro                               ");
                DbSqlServer.AppendLine("						,Numero                                   ");
                DbSqlServer.AppendLine("						,Complemento                              ");
                DbSqlServer.AppendLine("						,Bairro                                   ");
                DbSqlServer.AppendLine("						,Cidade                                   ");
                DbSqlServer.AppendLine("						,Estado                                   ");
                DbSqlServer.AppendLine("						,CEP                                      ");
                DbSqlServer.AppendLine("						,Avaliacao                                ");
                DbSqlServer.AppendLine("						,Arrematacao                              ");
                DbSqlServer.AppendLine("						,Descontos                                ");
                DbSqlServer.AppendLine("						,ISS                                      ");
                DbSqlServer.AppendLine("						,Taxa_Administrativa                      ");
                DbSqlServer.AppendLine("						,Outras_Taxas                             ");
                DbSqlServer.AppendLine("						,Comissao                                 ");
                DbSqlServer.AppendLine("						,Tarifa_Bancaria                          ");
                DbSqlServer.AppendLine("						,Valor_Total                              ");
                DbSqlServer.AppendLine("						,Valor_Pago                               ");
                DbSqlServer.AppendLine("						,Numero_Boleto                            ");
                DbSqlServer.AppendLine("						,Linha_Digitavel                          ");
                
                DbSqlServer.AppendLine("						,Data_Emissao_Boleto                      ");
                DbSqlServer.AppendLine("						,Data_Vencimento_Boleto                   ");
                DbSqlServer.AppendLine("						,Data_Pagamento_Boleto                    ");
                DbSqlServer.AppendLine("						,Status_Boleto                            ");
                DbSqlServer.AppendLine("						,Numero_Processo                          ");
                DbSqlServer.AppendLine("						,cartela                                  ");
                DbSqlServer.AppendLine("						,status_cadastro_cliente_sap              ");
                DbSqlServer.AppendLine("						,status_cadastro_fb70_sap                 ");
                DbSqlServer.AppendLine("						,id_grv)                                  ");



                DbSqlServer.AppendLine("VALUES (                                                         ");

                DbSqlServer.AppendFormat("						 {0},   ", Entidade.lote);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.leilao);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.placa);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.chassi);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.nome_arrematante.Replace('\'', ' ').Trim());
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.cpf);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.cnpj);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.fone_1);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.fone_2);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.email);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.logradouro.Replace('\'', ' '));
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.numero);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.complemento);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.bairro.Replace('\'', ' '));
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.cidade);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.estado);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.cep);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.avaliacao);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.arrematacao);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.descontos);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.iss);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.taxa_administrativa);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.outras_taxas);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.comissao);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.tarifa_bancaria);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.valor_total);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.valor_pago);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.numero_boleto);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.linha_digitavel);
                
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.data_emissao_boleto);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.data_vencimento_boleto);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.data_pagamento_boleto);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.status_boleto);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.numero_processo);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.cartela);
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.status_cadastro_cliente_sap ?? "N");
                DbSqlServer.AppendFormat("						'{0}',  ", Entidade.status_cadastro_fb70_sap ?? "N");
                DbSqlServer.AppendFormat("						 {0})   ", Entidade.id_grv ?? 0);

                return ExecutaSQL(DbSqlServer.ToString());
            }
            catch (Exception EX)
            {
                throw;
            }
        }

        public Arrematante SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Arrematante> SelecionarTudo()
        {
            throw new NotImplementedException();
        }

        public IList<Arrematante> SelecionarTudo(Arrematante Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Arrematante> SelecionarTudo(int id)
        {
            string sql = string.Format(@"
            SELECT tb_leilao_lotes_arrematantes.* 
               FROM tb_leilao_lotes_arrematantes
              INNER JOIN tb_leilao ON tb_leilao.descricao = tb_leilao_lotes_arrematantes.leilao
            WHERE tb_leilao.id = {0} ", id);

            //StringBuilder DbSqlServer = new StringBuilder();

            //DbSqlServer.AppendLine(" SELECT * ");
            //DbSqlServer.AppendLine("   FROM tb_leilao_lotes_arrematantes");
            //DbSqlServer.AppendLine("  INNER JOIN tb_leilao_lotes ON tb_leilao_lotes_arrematantes.numero_processo = tb_leilao_lotes.numero_formulario_grv");
            //DbSqlServer.AppendLine("  INNER JOIN tb_leilao ON tb_leilao_lotes.id_leilao = tb_leilao.id");
            //DbSqlServer.AppendFormat("WHERE tb_leilao.id = {0}", id);

            var lista = ConsultaSQL(sql).ConverterParaLista<Arrematante>();
            return lista;
        }

        public List<Arrematante> FinanceiroDetalhamento(string descricaoLeilao)
        {
            try
            {
                Framework.WebServices.WSVipBoleto ws = new Framework.WebServices.WSVipBoleto(Util.DetectarAmbiente());

                var retorno = ws.FinanceiroDetalhamento(descricaoLeilao);

                return AutoMapper.Mapper.Map
                    <List<Framework.WebServices.FinanceiroDetalhamento_Output>, List<Dominio.Arrematante>>
                    (retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArrematantePagamento> ObterListaPagamentos(int id_leilao)
        {
            string sql = string.Format(@"
            SELECT a.valor_cobrado, a.valor_pago, a.data_documento, a.boleto_data_arrecadado,
                   b.id_boleto, b.comissao, b.desconto, b.outras_taxas, b.taxa_administrativa, b.valor_arrematacao, b.valor_caucao,
                   c.id, c.descricao, c.ordem_interna_matriz, c.ordem_interna_leilao, c.id_comitente,
                   d.linha_digitavel, d.boleto_vencimento

              FROM dbMoblinkBoletos.dbo.view_boletos_creditados a
              JOIN dbMoblinkBoletos.dbo.tb_boleto_lotes b ON a.identificacao = b.id_boleto
              JOIN dbLeilao.dbo.tb_leilao c ON b.leilao = c.descricao
              JOIN dbMoblinkBoletos.dbo.tb_boletos d ON d.id_boleto = b.id_boleto
             WHERE c.id = {0}", id_leilao);

            return ConsultaSQL(sql).ConverterParaLista<ArrematantePagamento>();
        }

    }
}
