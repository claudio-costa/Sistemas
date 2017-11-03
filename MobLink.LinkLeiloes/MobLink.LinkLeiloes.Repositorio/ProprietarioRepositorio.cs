using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;
using System.Text;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class ProprietarioRepositorio : DbSqlServer, ICrud<Transacao005, int>
    {
        protected internal ProprietarioRepositorio(): base(Util.DetectarConexao())
        {

        }

        public int Alterar(Transacao005 entidade)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("  UPDATE dbo.tb_leilao_lotes_proprietarios               ");
            sql.AppendLine("  SET                                                    ");
            sql.AppendLine(string.Format("   bairro_comunicado_venda             = '{0}' ", entidade.BairroComunicadoVenda));
            sql.AppendLine(string.Format(" , bairro_financiamento_efet           = '{0}' ", entidade.BairroFinanciamentoEfet));
            sql.AppendLine(string.Format(" , bairro_proprietario                 = '{0}' ", entidade.BairroProprietario));
            sql.AppendLine(string.Format(" , cep_comunicado_venda                = '{0}' ", entidade.CepComunicadoVenda));
            sql.AppendLine(string.Format(" , cep_endereco_proprietario           = '{0}' ", entidade.CepEnderecoProprietario));
            sql.AppendLine(string.Format(" , cep_financiamento_efet              = '{0}' ", entidade.CepFinanciamentoEfet));
            sql.AppendLine(string.Format(" , complemento_comunicado_venda        = '{0}' ", entidade.ComplementoComunicadoVenda));
            sql.AppendLine(string.Format(" , complemento_endereco_proprietario   = '{0}' ", entidade.ComplementoEnderecoProprietario));
            sql.AppendLine(string.Format(" , complemento_financiamento_efet      = '{0}' ", entidade.ComplementoFinanciamentoEfet));
            sql.AppendLine(string.Format(" , cpf_cnpj_comunicado_venda           = '{0}' ", entidade.CpfCnpjComunicadoVenda));
            sql.AppendLine(string.Format(" , cpf_cnpj_financiamento_efet         = '{0}' ", entidade.CpfCnpjFinanciamentoEfet));
            sql.AppendLine(string.Format(" , descricao_municipio_endereco        = '{0}' ", entidade.DescricaoMunicipioEndereco));
            sql.AppendLine(string.Format(" , endereco_comunicado_venda           = '{0}' ", entidade.EnderecoComunicadoVenda));
            sql.AppendLine(string.Format(" , endereco_financiamento_efet         = '{0}' ", entidade.EnderecoFinanciamentoEfet));
            sql.AppendLine(string.Format(" , endereco_proprietario               = '{0}' ", entidade.EnderecoProprietario));
            sql.AppendLine(string.Format(" , municipio_comunicado_venda          = '{0}' ", entidade.MunicipioComunicadoVenda));
            sql.AppendLine(string.Format(" , municipio_financiamento_efet        = '{0}' ", entidade.MunicipioFinanciamentoEfet));
            sql.AppendLine(string.Format(" , nome_comunicado_venda               = '{0}' ", entidade.NomeComunicacaoVenda));
            sql.AppendLine(string.Format(" , nome_financiamento_efet             = '{0}' ", entidade.NomeFinanciamentoEfet));
            sql.AppendLine(string.Format(" , nome_proprietario                   = '{0}' ", entidade.NomeProprietario));
            sql.AppendLine(string.Format(" , numero_comunicado_venda             = '{0}' ", entidade.NumeroComunicadoVenda));
            sql.AppendLine(string.Format(" , numero_cpf_cnpj                     = '{0}' ", entidade.NumeroCpfCnpj));
            sql.AppendLine(string.Format(" , numero_endereco_proprietario        = '{0}' ", entidade.NumeroEnderecoProprietario));
            sql.AppendLine(string.Format(" , numero_financiamento_efet           = '{0}' ", entidade.NumeroFinanciamentoEfet));
            sql.AppendLine(string.Format(" , uf_comunicado_venda                 = '{0}' ", entidade.UfComunicadoVenda));
            sql.AppendLine(string.Format(" , uf_financiamento_efet               = '{0}' ", entidade.UfFinanciamentoEfet));
            sql.AppendLine(string.Format(" , uf_proprietario                     = '{0}' ", entidade.UfProprietario));
            sql.AppendLine(string.Format(" , flag_notificar_proprietario         = '{0}' ", entidade.flag_notificar_proprietario));
            sql.AppendLine(string.Format(" , flag_notificar_financeira           = '{0}' ", entidade.flag_notificar_financeira));
            sql.AppendLine(string.Format(" , flag_notificar_comunicado           = '{0}' ", entidade.flag_notificar_comunicado));
            
            sql.AppendLine(string.Format("  WHERE id_lote = {0}                          ", entidade.Id_lote));

            try
            {
                return ExecutaSQL(sql.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Alterar(Proprietario entidade)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("  UPDATE dbo.tb_leilao_lotes_proprietarios               ");
            sql.AppendLine("  SET                                                    ");
            sql.AppendLine(string.Format("   bairro_comunicado_venda             = '{0}' ", entidade.bairro_comunicado_venda));
            sql.AppendLine(string.Format(" , bairro_financiamento_efet           = '{0}' ", entidade.bairro_financiamento_efet));
            sql.AppendLine(string.Format(" , bairro_proprietario                 = '{0}' ", entidade.bairro_proprietario));
            sql.AppendLine(string.Format(" , cep_comunicado_venda                = '{0}' ", entidade.cep_comunicado_venda));
            sql.AppendLine(string.Format(" , cep_endereco_proprietario           = '{0}' ", entidade.cep_endereco_proprietario));
            sql.AppendLine(string.Format(" , cep_financiamento_efet              = '{0}' ", entidade.cep_financiamento_efet));
            sql.AppendLine(string.Format(" , complemento_comunicado_venda        = '{0}' ", entidade.complemento_comunicado_venda));
            sql.AppendLine(string.Format(" , complemento_endereco_proprietario   = '{0}' ", entidade.complemento_endereco_proprietario));
            sql.AppendLine(string.Format(" , complemento_financiamento_efet      = '{0}' ", entidade.complemento_financiamento_efet));
            sql.AppendLine(string.Format(" , cpf_cnpj_comunicado_venda           = '{0}' ", entidade.cpf_cnpj_comunicado_venda));
            sql.AppendLine(string.Format(" , cpf_cnpj_financiamento_efet         = '{0}' ", entidade.cpf_cnpj_financiamento_efet));
            sql.AppendLine(string.Format(" , descricao_municipio_endereco        = '{0}' ", entidade.descricao_municipio_endereco));
            sql.AppendLine(string.Format(" , endereco_comunicado_venda           = '{0}' ", entidade.endereco_comunicado_venda));
            sql.AppendLine(string.Format(" , endereco_financiamento_efet         = '{0}' ", entidade.endereco_financiamento_efet));
            sql.AppendLine(string.Format(" , endereco_proprietario               = '{0}' ", entidade.endereco_proprietario));
            sql.AppendLine(string.Format(" , municipio_comunicado_venda          = '{0}' ", entidade.municipio_comunicado_venda));
            sql.AppendLine(string.Format(" , municipio_financiamento_efet        = '{0}' ", entidade.municipio_financiamento_efet));
            sql.AppendLine(string.Format(" , nome_comunicado_venda               = '{0}' ", entidade.nome_comunicado_venda));
            sql.AppendLine(string.Format(" , nome_financiamento_efet             = '{0}' ", entidade.nome_financiamento_efet));
            sql.AppendLine(string.Format(" , nome_proprietario                   = '{0}' ", entidade.nome_proprietario));
            sql.AppendLine(string.Format(" , numero_comunicado_venda             = '{0}' ", entidade.numero_comunicado_venda));
            sql.AppendLine(string.Format(" , numero_cpf_cnpj                     = '{0}' ", entidade.numero_cpf_cnpj));
            sql.AppendLine(string.Format(" , numero_endereco_proprietario        = '{0}' ", entidade.numero_endereco_proprietario));
            sql.AppendLine(string.Format(" , numero_financiamento_efet           = '{0}' ", entidade.numero_financiamento_efet));
            sql.AppendLine(string.Format(" , uf_comunicado_venda                 = '{0}' ", entidade.uf_comunicado_venda));
            sql.AppendLine(string.Format(" , uf_financiamento_efet               = '{0}' ", entidade.uf_financiamento_efet));
            sql.AppendLine(string.Format(" , uf_proprietario                     = '{0}' ", entidade.uf_proprietario));
            sql.AppendLine(string.Format(" , flag_notificar_proprietario         = '{0}' ", entidade.flag_notificar_proprietario));
            sql.AppendLine(string.Format(" , flag_notificar_financeira           = '{0}' ", entidade.flag_notificar_financeira));
            sql.AppendLine(string.Format(" , flag_notificar_comunicado           = '{0}' ", entidade.flag_notificar_comunicado));
            sql.AppendLine(string.Format(" , flag_normalizado                    = '{0}' ", entidade.flag_normalizado));

            sql.AppendLine(string.Format("  WHERE id_lote = {0}                          ", entidade.id_lote));

            try
            {
                return ExecutaSQL(sql.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Transacao005 Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Transacao005 Transacao005)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("    INSERT INTO dbo.tb_leilao_lotes_proprietarios          ");
            sql.AppendLine("    	(id_lote                                           ");
            sql.AppendLine("    	, retorno                                          ");
            sql.AppendLine("    	, chassi                                           ");
            sql.AppendLine("    	, chassi_remarcado                                 ");
            sql.AppendLine("    	, placa                                            ");
            sql.AppendLine("    	, placa_anterior                                   ");
            sql.AppendLine("    	, placa_nova                                       ");
            sql.AppendLine("    	, ano_fabricacao                                   ");
            sql.AppendLine("    	, ano_modelo                                       ");
            sql.AppendLine("    	, renavam                                          ");
            sql.AppendLine("    	, descricao_cor                                    ");
            sql.AppendLine("    	, descricao_marca                                  ");
            sql.AppendLine("    	, descricao_tipo                                   ");
            sql.AppendLine("    	, descricao_categoria                              ");
            sql.AppendLine("    	, descricao_combustivel                            ");
            sql.AppendLine("    	, descricao_especie                                ");
            sql.AppendLine("    	, descricao_municipio_endereco                     ");
            sql.AppendLine("    	, descricao_municipio_emplacamento                 ");
            sql.AppendLine("    	, capacidade_passageiros                           ");
            sql.AppendLine("    	, capacidade_carga                                 ");
            sql.AppendLine("    	, data_limite_restricao                            ");
            sql.AppendLine("    	, peso_bruto_total                                 ");
            sql.AppendLine("    	, descricao_serie                                  ");
            sql.AppendLine("    	, numero_motor                                     ");
            sql.AppendLine("    	, dia_juliano                                      ");
            sql.AppendLine("    	, sequencial                                       ");
            sql.AppendLine("    	, transacao                                        ");
            sql.AppendLine("    	, indicacao_multas_renainf                         ");
            sql.AppendLine("    	, indicacao_divida_ativa                           ");
            sql.AppendLine("    	, indicacao_veiculo_baixado                        ");
            sql.AppendLine("    	, indicacao_roubo_furto                            ");
            sql.AppendLine("    	, indicacao_financiamento                          ");
            sql.AppendLine("    	, tipo_documento                                   ");
            sql.AppendLine("    	, numero_cpf_cnpj                                  ");
            sql.AppendLine("    	, cep_endereco_proprietario                        ");
            sql.AppendLine("    	, nome_proprietario                                ");
            sql.AppendLine("    	, endereco_proprietario                            ");
            sql.AppendLine("    	, numero_endereco_proprietario                     ");
            sql.AppendLine("    	, complemento_endereco_proprietario                ");
            sql.AppendLine("    	, nome_proprietario_anterior                       ");
            sql.AppendLine("    	, tipo_documento_comunicado_venda                  ");
            sql.AppendLine("    	, nome_comunicado_venda                            ");
            sql.AppendLine("    	, endereco_comunicado_venda                        ");
            sql.AppendLine("    	, numero_comunicado_venda                          ");
            sql.AppendLine("    	, complemento_comunicado_venda                     ");
            sql.AppendLine("    	, bairro_comunicado_venda                          ");
            sql.AppendLine("    	, municipio_comunicado_venda                       ");
            sql.AppendLine("    	, uf_comunicado_venda                              ");
            sql.AppendLine("    	, cpf_cnpj_comunicado_venda                        ");
            sql.AppendLine("    	, cep_comunicado_venda                             ");
            sql.AppendLine("    	, data_venda_comunicado_venda                      ");
            sql.AppendLine("    	, tipo_documento_financiamento_efet                ");
            sql.AppendLine("    	, cpf_cnpj_financiamento_efet                      ");
            sql.AppendLine("    	, cep_financiamento_efet                           ");
            sql.AppendLine("    	, data_financiamento_efet                          ");
            sql.AppendLine("    	, hora_financiamento_efet                          ");
            sql.AppendLine("    	, nome_financiamento_efet                          ");
            sql.AppendLine("    	, endereco_financiamento_efet                      ");
            sql.AppendLine("    	, numero_financiamento_efet                        ");
            sql.AppendLine("    	, complemento_financiamento_efet                   ");
            sql.AppendLine("    	, municipio_financiamento_efet                     ");
            sql.AppendLine("    	, tipo_documento_financiado_sng                    ");
            sql.AppendLine("    	, cpf_cnpj_financiado_sng                          ");
            sql.AppendLine("    	, data_financiado_sng                              ");
            sql.AppendLine("    	, hora_financiado_sng                              ");
            sql.AppendLine("    	, nome_financiado_sng                              ");
            sql.AppendLine("    	, tipo_documento_agente_financeiro                 ");
            sql.AppendLine("    	, cpf_cnpj_agente_financeiro                       ");
            sql.AppendLine("    	, nome_agente_financeiro                           ");
            sql.AppendLine("    	, observacoes)                                     ");
            sql.AppendLine("    	                                                   ");
            sql.AppendLine("    VALUES                                                 ");

            sql.AppendLine(string.Format(" ({0} ", Transacao005.Id_lote));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.Retorno));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.Chassi));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.ChassiRemarcado));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.Placa));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.PlacaAnterior));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.PlacaNova));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.AnoFabricacao));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.AnoModelo));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.Renavam));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DescricaoCor));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DescricaoMarca));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DescricaoTipo));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DescricaoCategoria));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DescricaoCombustivel));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DescricaoEspecie));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DescricaoMunicipioEndereco));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DescricaoMunicipioEmplacamento));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.CapacidadePassageiros));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.CapacidadeCarga));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DataLimiteRestricao));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.PesoBrutoTotal));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DescricaoSerie));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NumeroMotor));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DiaJuliano));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.Sequencial));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.Transacao));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.IndicacaoMultasRenainf));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.IndicacaoDividaAtiva));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.IndicacaoVeiculoBaixado));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.IndicacaoRouboFurto));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.IndicacaoFinanciamento));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.TipoDocumento));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NumeroCpfCnpj));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.CepEnderecoProprietario));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NomeProprietario));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.EnderecoProprietario));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NumeroEnderecoProprietario));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.ComplementoEnderecoProprietario));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NomeProprietarioAnterior));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.TipoDocumentoComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NomeComunicacaoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.EnderecoComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NumeroComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.ComplementoComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.BairroComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.MunicipioComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.UfComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.CpfCnpjComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.CepComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DataVendaComunicadoVenda));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.TipoDocumentoFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.CpfCnpjFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.CepFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DataFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.HoraFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NomeFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.EnderecoFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NumeroFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.ComplementoFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.MunicipioFinanciamentoEfet));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.TipoDocumentoFinanciadoSng));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.CpfCnpjFinanciadoSng));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.DataFinanciadoSng));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.HoraFinanciadoSng));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NomeFinanciadoSng));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.TipoDocumentoAgenteFinanceiro));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.CpfCnpjAgeteFinanceiro));
            sql.AppendLine(string.Format(",'{0}'", Transacao005.NomeAgenteFinanceiro));
            sql.AppendLine(string.Format(",'{0}')", Transacao005.Observacoes));

            try
            {
                return ExecutaSQL_ScopeIdentity(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public int Inserir(Transacao005 Entidade)
        //{
        //    //StringBuilder sql = new StringBuilder();

        //    //sql.AppendLine("    INSERT INTO dbo.tb_leilao_lotes_proprietarios          ");
        //    //sql.AppendLine("    	(id_lote                                           ");
        //    //sql.AppendLine("    	, retorno                                          ");
        //    //sql.AppendLine("    	, chassi                                           ");
        //    //sql.AppendLine("    	, chassi_remarcado                                 ");
        //    //sql.AppendLine("    	, placa                                            ");
        //    //sql.AppendLine("    	, placa_anterior                                   ");
        //    //sql.AppendLine("    	, placa_nova                                       ");
        //    //sql.AppendLine("    	, ano_fabricacao                                   ");
        //    //sql.AppendLine("    	, ano_modelo                                       ");
        //    //sql.AppendLine("    	, renavam                                          ");
        //    //sql.AppendLine("    	, descricao_cor                                    ");
        //    //sql.AppendLine("    	, descricao_marca                                  ");
        //    //sql.AppendLine("    	, descricao_tipo                                   ");
        //    //sql.AppendLine("    	, descricao_categoria                              ");
        //    //sql.AppendLine("    	, descricao_combustivel                            ");
        //    //sql.AppendLine("    	, descricao_especie                                ");
        //    //sql.AppendLine("    	, descricao_municipio_endereco                     ");
        //    //sql.AppendLine("    	, descricao_municipio_emplacamento                 ");
        //    //sql.AppendLine("    	, capacidade_passageiros                           ");
        //    //sql.AppendLine("    	, capacidade_carga                                 ");
        //    //sql.AppendLine("    	, data_limite_restricao                            ");
        //    //sql.AppendLine("    	, peso_bruto_total                                 ");
        //    //sql.AppendLine("    	, descricao_serie                                  ");
        //    //sql.AppendLine("    	, numero_motor                                     ");
        //    //sql.AppendLine("    	, dia_juliano                                      ");
        //    //sql.AppendLine("    	, sequencial                                       ");
        //    //sql.AppendLine("    	, transacao                                        ");
        //    //sql.AppendLine("    	, indicacao_multas_renainf                         ");
        //    //sql.AppendLine("    	, indicacao_divida_ativa                           ");
        //    //sql.AppendLine("    	, indicacao_veiculo_baixado                        ");
        //    //sql.AppendLine("    	, indicacao_roubo_furto                            ");
        //    //sql.AppendLine("    	, indicacao_financiamento                          ");
        //    //sql.AppendLine("    	, tipo_documento                                   ");
        //    //sql.AppendLine("    	, numero_cpf_cnpj                                  ");
        //    //sql.AppendLine("    	, cep_endereco_proprietario                        ");
        //    //sql.AppendLine("    	, nome_proprietario                                ");
        //    //sql.AppendLine("    	, endereco_proprietario                            ");
        //    //sql.AppendLine("    	, numero_endereco_proprietario                     ");
        //    //sql.AppendLine("    	, complemento_endereco_proprietario                ");
        //    //sql.AppendLine("    	, nome_proprietario_anterior                       ");
        //    //sql.AppendLine("    	, tipo_documento_comunicado_venda                  ");
        //    //sql.AppendLine("    	, nome_comunicado_venda                            ");
        //    //sql.AppendLine("    	, endereco_comunicado_venda                        ");
        //    //sql.AppendLine("    	, numero_comunicado_venda                          ");
        //    //sql.AppendLine("    	, complemento_comunicado_venda                     ");
        //    //sql.AppendLine("    	, bairro_comunicado_venda                          ");
        //    //sql.AppendLine("    	, municipio_comunicado_venda                       ");
        //    //sql.AppendLine("    	, uf_comunicado_venda                              ");
        //    //sql.AppendLine("    	, cpf_cnpj_comunicado_venda                        ");
        //    //sql.AppendLine("    	, cep_comunicado_venda                             ");
        //    //sql.AppendLine("    	, data_venda_comunicado_venda                      ");
        //    //sql.AppendLine("    	, tipo_documento_financiamento_efet                ");
        //    //sql.AppendLine("    	, cpf_cnpj_financiamento_efet                      ");
        //    //sql.AppendLine("    	, cep_financiamento_efet                           ");
        //    //sql.AppendLine("    	, data_financiamento_efet                          ");
        //    //sql.AppendLine("    	, hora_financiamento_efet                          ");
        //    //sql.AppendLine("    	, nome_financiamento_efet                          ");
        //    //sql.AppendLine("    	, endereco_financiamento_efet                      ");
        //    //sql.AppendLine("    	, numero_financiamento_efet                        ");
        //    //sql.AppendLine("    	, complemento_financiamento_efet                   ");
        //    //sql.AppendLine("    	, municipio_financiamento_efet                     ");
        //    //sql.AppendLine("    	, tipo_documento_financiado_sng                    ");
        //    //sql.AppendLine("    	, cpf_cnpj_financiado_sng                          ");
        //    //sql.AppendLine("    	, data_financiado_sng                              ");
        //    //sql.AppendLine("    	, hora_financiado_sng                              ");
        //    //sql.AppendLine("    	, nome_financiado_sng                              ");
        //    //sql.AppendLine("    	, tipo_documento_agente_financeiro                 ");
        //    //sql.AppendLine("    	, cpf_cnpj_agente_financeiro                       ");
        //    //sql.AppendLine("    	, nome_agente_financeiro                           ");
        //    //sql.AppendLine("    	, observacoes)                                     ");
        //    //sql.AppendLine("    	                                                   ");
        //    //sql.AppendLine("    VALUES                                                 ");

        //    //sql.AppendLine(string.Format(" ({0} ",  Entidade.id_lote));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.retorno));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.chassi));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.chassi_remarcado));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.placa));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.placa_anterior));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.placa_nova));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.ano_fabricacao));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.ano_modelo));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.renavam));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.descricao_cor));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.descricao_marca));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.descricao_tipo));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.descricao_categoria));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.descricao_combustivel));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.descricao_especie));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.descricao_municipio_endereco));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.descricao_municipio_emplacamento));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.capacidade_passageiros));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.capacidade_carga));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.data_limite_restricao));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.peso_bruto_total));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.descricao_serie));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.numero_motor));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.dia_juliano));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.sequencial));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.transacao));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.indicacao_multas_renainf));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.indicacao_divida_ativa));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.indicacao_veiculo_baixado));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.indicacao_roubo_furto));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.indicacao_financiamento));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.tipo_documento));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.numero_cpf_cnpj));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.cep_endereco_proprietario));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.nome_proprietario));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.endereco_proprietario));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.numero_endereco_proprietario));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.complemento_endereco_proprietario));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.nome_proprietario_anterior));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.tipo_documento_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.nome_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.endereco_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.numero_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.complemento_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.bairro_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.municipio_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.uf_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.cpf_cnpj_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.cep_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.data_venda_comunicado_venda));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.tipo_documento_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.cpf_cnpj_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.cep_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.data_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.hora_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.nome_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.endereco_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.numero_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.complemento_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.municipio_financiamento_efet));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.tipo_documento_financiado_sng));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.cpf_cnpj_financiado_sng));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.data_financiado_sng));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.hora_financiado_sng));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.nome_financiado_sng));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.tipo_documento_agente_financeiro));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.cpf_cnpj_agente_financeiro));
        //    //sql.AppendLine(string.Format(",'{0}'",  Entidade.nome_agente_financeiro));
        //    //sql.AppendLine(string.Format(",'{0}')", Entidade.observacoes));

        //    //try
        //    //{
        //    //    ExecutaSQL(sql.ToString());
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw ex;
        //    //}

        //    return -1;
        //}

        public Transacao005 SelecionarPorId(int id)
        {
            if (id == 0)
            {
                return new Transacao005();
            }

            string sql = @" SELECT * FROM dbo.tb_leilao_lotes_proprietarios WHERE id = " + id;

            var dt = ConsultaSQL(sql);

            return dt.Rows.Count <= 0 ? new Transacao005() : dt
                .Rows[0]
                .ConverterParaEntidade<Transacao005>();
        }

        public Transacao005 SelecionarProprietarioPorLote(int idLote)
        {
            if (idLote == 0)
            {
                return new Transacao005();
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("	 SELECT id, id_lote,                                                          ");
            sql.AppendFormat("			nome_comunicado_venda NomeComunicacaoVenda,                           ");
            sql.AppendFormat("			nome_proprietario NomeProprietario,                                   ");
            sql.AppendFormat("			nome_financiamento_efet NomeFinanciamentoEfet,                        ");
            sql.AppendFormat("			numero_cpf_cnpj NumeroCpfCnpj,                                        ");
            sql.AppendFormat("			cpf_cnpj_comunicado_venda CpfCnpjComunicadoVenda,                     ");
            sql.AppendFormat("			cpf_cnpj_financiamento_efet CpfCnpjFinanciamentoEfet,                 ");
            sql.AppendFormat("			cep_endereco_proprietario CepEnderecoProprietario,                    ");
            sql.AppendFormat("			cep_comunicado_venda CepComunicadoVenda,                              ");
            sql.AppendFormat("			cep_financiamento_efet CepFinanciamentoEfet,                          ");
            sql.AppendFormat("			endereco_proprietario EnderecoProprietario,                           ");
            sql.AppendFormat("			endereco_comunicado_venda EnderecoComunicadoVenda,                    ");
            sql.AppendFormat("			endereco_financiamento_efet EnderecoFinanciamentoEfet,                ");
            sql.AppendFormat("			numero_endereco_proprietario NumeroEnderecoProprietario,              ");
            sql.AppendFormat("			numero_comunicado_venda NumeroComunicadoVenda,                        ");
            sql.AppendFormat("			numero_financiamento_efet NumeroFinanciamentoEfet,                    ");
            sql.AppendFormat("			complemento_endereco_proprietario ComplementoEnderecoProprietario,    ");
            sql.AppendFormat("			complemento_comunicado_venda ComplementoComunicadoVenda,              ");
            sql.AppendFormat("			complemento_financiamento_efet ComplementoFinanciamentoEfet,          ");
            sql.AppendFormat("			bairro_comunicado_venda BairroComunicadoVenda,                        ");
            sql.AppendFormat("			bairro_financiamento_efet BairroFinanciamentoEfet,                    ");
            sql.AppendFormat("			bairro_proprietario BairroProprietario,                               ");
            sql.AppendFormat("			municipio_comunicado_venda MunicipioComunicadoVenda,                  ");
            sql.AppendFormat("			municipio_financiamento_efet MunicipioFinanciamentoEfet,              ");
            sql.AppendFormat("			descricao_municipio_endereco DescricaoMunicipioEndereco,              ");
            sql.AppendFormat("			uf_proprietario UfProprietario,                                       ");
            sql.AppendFormat("			uf_comunicado_venda UfComunicadoVenda,                                ");
            sql.AppendFormat("			uf_financiamento_efet UfFinanciamentoEfet,                            ");
            sql.AppendFormat("			flag_notificar_proprietario,                                          ");
            sql.AppendFormat("			flag_notificar_financeira,                                            ");
            sql.AppendFormat("			flag_notificar_comunicado                                             ");
            sql.AppendFormat("	   FROM dbo.tb_leilao_lotes_proprietarios                                     ");
            sql.AppendFormat("    WHERE id_lote = {0}                                                         ", idLote);

            var dt = ConsultaSQL(sql.ToString());

            var prop = dt.Rows.Count > 0 ? dt
                .Rows[0]
                .ConverterParaEntidade<Transacao005>() : new Transacao005();

            return prop;
        }

        public IList<Transacao005> SelecionarTudo()
        {
            throw new NotImplementedException();
        }

        public IList<Transacao005> SelecionarTudo(Transacao005 Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Transacao005> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Transacao005> SelecionarTudoLeilao(int IdLeilao)
        {
            if (IdLeilao == 0)
            {
                return new List<Transacao005>();
            }

            //string sql = @" SELECT p.*, l.*
            //                    FROM dbo.tb_leilao_lotes_proprietarios p
            //              INNER JOIN tb_leilao_lotes l ON l.id = p.id_lote
            //                   WHERE l.id_leilao = " + IdLeilao;


            string sql = string.Format(@"
      SELECT p.*, l.data_hora_apreensao, l.numero_formulario_grv, c.id_cliente
        FROM dbo.tb_leilao_lotes_proprietarios p
        JOIN tb_leilao_lotes l ON l.id = p.id_lote
        JOIN tb_leilao lei ON l.id_leilao = lei.id
        JOIN tb_comitentes c ON c.id = lei.id_comitente
       WHERE l.id_leilao = {0}", IdLeilao);

            //string sql = string.Format(@"
            //SELECT * FROM (

            //   SELECT  'PR' TIPO
            //          , p.placa PLACA
            //          , p.nome_proprietario NOME
            //          , p.endereco_proprietario ENDERECO
            //          , p.numero_endereco_proprietario NUMERO
            //          , P.complemento_endereco_proprietario COMPLEMENTO
            //          , p.bairro_proprietario BAIRRO
            //          , p.descricao_municipio_endereco MUNICIPIO
            //          , P.uf_proprietario UF
            //          , p.cep_endereco_proprietario CEP          
            //     FROM dbo.tb_leilao_lotes_proprietarios p
            //iNNER JOIN tb_leilao_lotes l ON l.id = p.id_lote
            //    WHERE l.id_leilao = {0}

            //UNION

            //   SELECT 'CV'  
            //       , p.placa
            //          , p.nome_comunicado_venda
            //          , P.endereco_comunicado_venda
            //          , p.numero_comunicado_venda
            //          , P.complemento_comunicado_venda
            //          , P.bairro_comunicado_venda 
            //          , P.municipio_comunicado_venda 
            //          , P.uf_comunicado_venda 
            //          , P.cep_comunicado_venda           
            //     FROM dbo.tb_leilao_lotes_proprietarios p
            //iNNER JOIN tb_leilao_lotes l ON l.id = p.id_lote
            //    WHERE l.id_leilao = {0}

            //UNION

            //   SELECT 'FI'  
            //       , p.placa
            //          , P.nome_financiamento_efet 
            //          , P.endereco_financiamento_efet 
            //          , P.numero_financiamento_efet 
            //          , P.complemento_financiamento_efet 
            //          , P.bairro_financiamento_efet 
            //          , P.municipio_financiamento_efet 
            //          , P.uf_financiamento_efet 
            //          , P.cep_financiamento_efet           
            //     FROM dbo.tb_leilao_lotes_proprietarios p
            //iNNER JOIN tb_leilao_lotes l ON l.id = p.id_lote
            //    WHERE l.id_leilao = {0}
            //      ) sub

            //    WHERE NOME <> ''
            //      AND CEP <> 0

            //ORDER BY PLACA
            //", IdLeilao);

            var dt = ConsultaSQL(sql);

            return dt.Rows.Count > 0 ? dt
                .ConverterParaLista<Transacao005>() : new List<Transacao005>();
        }

        public IList<Proprietario> SelecionarNotificacoes(int id_leilao, bool flag_normalizado = true)
        {
            string sql = string.Format(@"
              SELECT tb_leilao_lotes_proprietarios.*
                FROM tb_leilao_lotes_proprietarios
                JOIN tb_leilao_lotes ON tb_leilao_lotes.id = tb_leilao_lotes_proprietarios.id_lote
               WHERE tb_leilao_lotes.id_leilao = {0}", id_leilao);

            if (flag_normalizado)
            {
                sql += " AND tb_leilao_lotes_proprietarios.flag_normalizado = 'S' ";
            }

            //sql += " AND tb_leilao_lotes.id_grv IS NOT NULL";

            var dt = ConsultaSQL(sql);

            return dt.Rows.Count > 0 ? dt
                .ConverterParaLista<Proprietario>() : new List<Proprietario>();
        }
    }
}