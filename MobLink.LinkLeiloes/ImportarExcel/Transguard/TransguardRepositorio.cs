using System;
using System.Linq;
using System.Text;
using System.Data;
using MobLink.WebLeilao.Repositorio;
using MobLink.WebLeilao.Dominio;
using MobLink.Framework.Database;
using MobLink.Framework;

namespace ImportarExcel
{
    public class TransguardRepositorio : DbSqlServer
    {
        public TransguardRepositorio() : base(Util.DetectarConexao())
        {

        }

        public void Importar_PROPRIETARIOS()
        {
            AutoMapperProfiles.TransguardProfile.AtivarProfiles();

            string sql = @"
                SELECT *
                  FROM tb_leilao_lotes
                  WHERE id_leilao = 65";

            //LER TABELA DE LOTES
            var lotes = ConsultaSQL(sql).ConverterParaLista<MobLink.WebLeilao.Dominio.Lote>();

            //INSERIR PROPRIETARIOS
            foreach (var lote in lotes)
            {
                sql = string.Format("SELECT * FROM CADASTRADOS WHERE PLACA = '{0}' OR CHASSI = '{1}'", lote.placa, lote.chassi);

                var dtLotes = ConsultaSQL(sql);

                CADASTRADOS correspondencia;

                if (dtLotes.Rows.Count > 0)
                {
                    correspondencia = ConsultaSQL(sql).AsEnumerable().FirstOrDefault().ConverterParaEntidade<CADASTRADOS>();
                }
                else
                {
                    //não achou o lote
                    continue; 
                }

                var proprietario = AutoMapper.Mapper.Map<CADASTRADOS, MobLink.WebLeilao.Dominio.Transacao005>(correspondencia);

                //COMPLEMENTAÇÃO DO LOTE
                lote.numero_motor = proprietario.NumeroMotor;
                lote.tipo_combustivel = proprietario.DescricaoCombustivel;
                lote.placa = proprietario.Placa;
                lote.chassi = proprietario.Chassi;
                lote.id_status_lote = 1;

                RepositorioGlobal.Lote.Alterar(lote);

                try
                {
                    proprietario.Id_lote = lote.id;

                    RepositorioGlobal.Proprietario.Inserir(proprietario);

                    var id = RepositorioGlobal.Transacoes.InserirTransacao(new Transacao()
                    {
                        id_lote = lote.id,
                        retorno = "OK",
                        transacao = "005"
                    });
                }
                catch (Exception e)
                {
                    throw;
                }                
            }

            //INSERIR RESTRIÇÕES
            foreach (var lote in lotes)
            {
                sql = string.Format("SELECT * FROM RESTRICOES WHERE PLACA = '{0}' OR CHASSI = '{1}'", lote.placa, lote.chassi);

                var dtLotes = ConsultaSQL(sql);

                RESTRICOES correspondencia;

                if (dtLotes.Rows.Count > 0)
                {
                    correspondencia = ConsultaSQL(sql).AsEnumerable().FirstOrDefault().ConverterParaEntidade<RESTRICOES>();
                }
                else
                {
                    //não achou o lote
                    continue;
                }

                var restricao = AutoMapper.Mapper.Map<RESTRICOES, MobLink.WebLeilao.Dominio.Restricao>(correspondencia);

                //COMPLEMENTAÇÃO DO LOTE
                
                lote.id_status_lote = 26;
                RepositorioGlobal.Lote.Alterar(lote);

                try
                {
                    restricao.id_lote = lote.id;
                    restricao.origem = "005";

                    RepositorioGlobal.Restricao.Inserir(restricao);
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            //INSERIR ROUBO/FURTO
            foreach (var lote in lotes)
            {
                sql = string.Format("SELECT * FROM SITUACAO_ROUBOFURTO_BIN WHERE PLACA = '{0}' OR CHASSI = '{1}'", lote.placa, lote.chassi);

                var dtLotes = ConsultaSQL(sql);

                SITUACAO_ROUBOFURTO_BIN correspondencia;

                if (dtLotes.Rows.Count > 0)
                {
                    correspondencia = ConsultaSQL(sql).AsEnumerable().FirstOrDefault().ConverterParaEntidade<SITUACAO_ROUBOFURTO_BIN>();
                }
                else
                {
                    //não achou o lote
                    continue;
                }

                var roubofurto = AutoMapper.Mapper.Map<SITUACAO_ROUBOFURTO_BIN, MobLink.WebLeilao.Dominio.Restricao>(correspondencia);

                //COMPLEMENTAÇÃO DO LOTE

                try
                {
                    lote.id_status_lote = 4;
                    lote.informacao_roubo = correspondencia.SITUACAO;
                    RepositorioGlobal.Lote.Alterar(lote);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        public int Inserir_CADASTRADOS(CADASTRADOS ent)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO dbo.CADASTRADOS (PLACA                                ");
            sql.AppendLine("                          , CHASSI                                ");
            sql.AppendLine("						  , RENAVAM                               ");
            sql.AppendLine("						  , ANO_MODELO                            ");
            sql.AppendLine("						  , ANO_FABRIC                            ");
            sql.AppendLine("						  , COD_TIPO                              ");
            sql.AppendLine("						  , DESCRICAO_TIPO                        ");
            sql.AppendLine("						  , COD_MARCA                             ");
            sql.AppendLine("						  , DESC_MARCA                            ");
            sql.AppendLine("						  , TVE_CODIGO_COR                        ");
            sql.AppendLine("						  , DESCRICAO_COR                         ");
            sql.AppendLine("						  , TVE_CODIGO_COMB                       ");
            sql.AppendLine("						  , DESCRICAO_COMBUSTIVEL                 ");
            sql.AppendLine("						  , TIPO_CIC                              ");
            sql.AppendLine("						  , CIC                                   ");
            sql.AppendLine("						  , PROPRIETARIO                          ");
            sql.AppendLine("						  , LOGRAD_END_PROPR                      ");
            sql.AppendLine("						  , BAIRRO                                ");
            sql.AppendLine("						  , COMPLEM_END_PROPR                     ");
            sql.AppendLine("						  , NUMERO_END_PROPR                      ");
            sql.AppendLine("						  , CEP_END_PROPR                         ");
            sql.AppendLine("						  , COD_MUN_END_PROPR                     ");
            sql.AppendLine("						  , DESCR_MUN_END_PROPR                   ");
            sql.AppendLine("						  , UF                                    ");
            sql.AppendLine("						  , STATUS                                ");
            sql.AppendLine("						  , CIC_GRAVAME                           ");
            sql.AppendLine("						  , GRAVAME                               ");
            sql.AppendLine("						  , LOGRAD_GRAVAME                        ");
            sql.AppendLine("						  , BAIRRO_GRAVAME                        ");
            sql.AppendLine("						  , COMPLEM_GRAVAME                       ");
            sql.AppendLine("						  , NUMERO_GRAVAME                        ");
            sql.AppendLine("						  , CEP_GRAVAME                           ");
            sql.AppendLine("						  , COD_MUN_GRAVAME                       ");
            sql.AppendLine("						  , DESCR_MUN_END_GRAVAME                 ");
            sql.AppendLine("						  , UF_GRAVAME                            ");
            sql.AppendLine("						  , TIPO_CIC_CV                           ");
            sql.AppendLine("						  , CIC_CV                                ");
            sql.AppendLine("						  , DT_VENDA_CV                           ");
            sql.AppendLine("						  , NOME_CV                               ");
            sql.AppendLine("						  , LOGRAD_END_CV                         ");
            sql.AppendLine("						  , BAIRRO_END_CV                         ");
            sql.AppendLine("						  , COMPLEM_END_CV                        ");
            sql.AppendLine("						  , NUMERO_END_CV                         ");
            sql.AppendLine("						  , CEP_END_CV                            ");
            sql.AppendLine("						  , COD_MUN_END_CV                        ");
            sql.AppendLine("						  , DESCR_MUN_END_CV                      ");
            sql.AppendLine("						  , UF_END_CV                             ");
            sql.AppendLine("						  , TIPO_CIC_LEILAO                       ");
            sql.AppendLine("						  , CIC_LEILAO                            ");
            sql.AppendLine("						  , MENSAGEM                              ");
            sql.AppendLine("						  , NOME_AGENTE                           ");
            sql.AppendLine("						  , CGC_AGENTE                            ");
            sql.AppendLine("						  , NOME_FINANCIADO                       ");
            sql.AppendLine("						  , CPF_CGC_FINANCIADO                    ");
            sql.AppendLine("						  , QT_AI11                               ");
            sql.AppendLine("						  , QT_AI_DESC11                          ");
            sql.AppendLine("						  , VL_AI11                               ");
            sql.AppendLine("						  , DEB_MULTAS                            ");
            sql.AppendLine("						  , VL_UFIR                               ");
            sql.AppendLine("						  , DIVIDA_IPVA                           ");
            sql.AppendLine("						  , DIVIDA_TAXAS                          ");
            sql.AppendLine("						  , SITUACAO_IPVA                         ");
            sql.AppendLine("						  , DESC_SITUACAO_IPVA                    ");
            sql.AppendLine("						  , ANO_ULTIMO_LIC                        ");
            sql.AppendLine("						  , QTD_MULTAS                            ");
            sql.AppendLine("						  , VLR_MULTAS_UFIR                       ");
            sql.AppendLine("						  , VLR_MULTAS_REAL                       ");
            sql.AppendLine("						  , NUMERO_DO_MOTOR                       ");
            sql.AppendLine("						  , DT_RECOL                              ");
            sql.AppendLine("						  , STATUS_RECOL                          ");
            sql.AppendLine("						  , DESC_STATUS_RECOL                     ");
            sql.AppendLine("						  , CATEGORIA                             ");
            sql.AppendLine("						  , TVE_PLACA                             ");
            sql.AppendLine("						  , PLACA_ANTERIOR                        ");
            sql.AppendLine("						  , TVE_NUMERO_GRV_RECOLHEDOR             ");
            sql.AppendLine("						  , MSG_SNG                               ");
            sql.AppendLine("						  , TVE_CASO                              ");
            sql.AppendLine("						  , DESC_CASO)                            ");
            sql.AppendLine(string.Format("VALUES ('{0}'           ", ent.PLACA));                         //PLACA                             
            sql.AppendLine(string.Format("      , '{0}'           ", ent.CHASSI));                         //CHASSI                             
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.RENAVAM));                         //RENAVAM                            
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.ANO_MODELO));                         //ANO_MODELO                         
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.ANO_FABRIC));                         //ANO_FABRIC                         
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.COD_TIPO));                         //COD_TIPO                           
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DESCRICAO_TIPO));                         //DESCRICAO_TIPO                     
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.COD_MARCA));                         //COD_MARCA                          
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DESC_MARCA));                         //DESC_MARCA                         
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.TVE_CODIGO_COR));                         //TVE_CODIGO_COR                     
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DESCRICAO_COR));                         //DESCRICAO_COR                      
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.TVE_CODIGO_COMB));                         //TVE_CODIGO_COMB                    
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DESCRICAO_COMBUSTIVEL));                         //DESCRICAO_COMBUSTIVEL              
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.TIPO_CIC));                         //TIPO_CIC                           
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CIC));                         //CIC                                
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.PROPRIETARIO));                         //PROPRIETARIO                       
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.LOGRAD_END_PROPR));                         //LOGRAD_END_PROPR                   
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.BAIRRO));                         //BAIRRO                             
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.COMPLEM_END_PROPR));                         //COMPLEM_END_PROPR                  
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.NUMERO_END_PROPR));                         //NUMERO_END_PROPR                   
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CEP_END_PROPR));                         //CEP_END_PROPR                      
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.COD_MUN_END_PROPR));                         //COD_MUN_END_PROPR                  
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DESCR_MUN_END_PROPR));                         //DESCR_MUN_END_PROPR                
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.UF));                         //UF                                 
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.STATUS));                         //STATUS                             
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CIC_GRAVAME));                         //CIC_GRAVAME                        
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.GRAVAME));                         //GRAVAME                            
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.LOGRAD_GRAVAME));                         //LOGRAD_GRAVAME                     
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.BAIRRO_GRAVAME));                         //BAIRRO_GRAVAME                     
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.COMPLEM_GRAVAME));                         //COMPLEM_GRAVAME                    
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.NUMERO_GRAVAME));                         //NUMERO_GRAVAME                     
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CEP_GRAVAME));                         //CEP_GRAVAME                        
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.COD_MUN_GRAVAME));                         //COD_MUN_GRAVAME                    
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DESCR_MUN_END_GRAVAME));                         //DESCR_MUN_END_GRAVAME              
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.UF_GRAVAME));                         //UF_GRAVAME                         
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.TIPO_CIC_CV));                         //TIPO_CIC_CV                        
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CIC_CV));                         //CIC_CV                             
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DT_VENDA_CV));                         //DT_VENDA_CV                        
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.NOME_CV));                         //NOME_CV                            
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.LOGRAD_END_CV));                         //LOGRAD_END_CV                      
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.BAIRRO_END_CV));                         //BAIRRO_END_CV                      
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.COMPLEM_END_CV));                         //COMPLEM_END_CV                     
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.NUMERO_END_CV));                         //NUMERO_END_CV                      
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CEP_END_CV));                         //CEP_END_CV                         
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.COD_MUN_END_CV));                         //COD_MUN_END_CV                     
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DESCR_MUN_END_CV));                         //DESCR_MUN_END_CV                   
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.UF_END_CV));                         //UF_END_CV                          
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.TIPO_CIC_LEILAO));                         //TIPO_CIC_LEILAO                    
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CIC_LEILAO));                         //CIC_LEILAO                         
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.MENSAGEM));                         //MENSAGEM                           
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.NOME_AGENTE));                         //NOME_AGENTE                        
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CGC_AGENTE));                         //CGC_AGENTE                         
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.NOME_FINANCIADO));                         //NOME_FINANCIADO                    
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CPF_CGC_FINANCIADO));                         //CPF_CGC_FINANCIADO                 
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.QT_AI11));                         //QT_AI11                            
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.QT_AI_DESC11));                         //QT_AI_DESC11                       
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.VL_AI11));                         //VL_AI11                            
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DEB_MULTAS));                         //DEB_MULTAS                         
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.VL_UFIR));                         //VL_UFIR                            
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DIVIDA_IPVA));                         //DIVIDA_IPVA                        
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DIVIDA_TAXAS));                         //DIVIDA_TAXAS                       
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.SITUACAO_IPVA));                         //SITUACAO_IPVA                      
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DESC_SITUACAO_IPVA));                         //DESC_SITUACAO_IPVA                 
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.ANO_ULTIMO_LIC));                         //ANO_ULTIMO_LIC                     
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.QTD_MULTAS));                         //QTD_MULTAS                         
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.VLR_MULTAS_UFIR));                         //VLR_MULTAS_UFIR                    
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.VLR_MULTAS_REAL));                         //VLR_MULTAS_REAL                    
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.NUMERO_DO_MOTOR));                         //NUMERO_DO_MOTOR                    
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DT_RECOL));                         //DT_RECOL                           
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.STATUS_RECOL));                         //STATUS_RECOL                       
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.DESC_STATUS_RECOL));                         //DESC_STATUS_RECOL                  
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.CATEGORIA));                         //CATEGORIA                          
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.TVE_PLACA));                         //TVE_PLACA                          
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.PLACA_ANTERIOR));                         //PLACA_ANTERIOR                     
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.TVE_NUMERO_GRV_RECOLHEDOR));                         //TVE_NUMERO_GRV_RECOLHEDOR          
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.MSG_SNG));                         //MSG_SNG                            
            sql.AppendLine(string.Format("	    , '{0}'           ", ent.TVE_CASO));                         //TVE_CASO                           
            sql.AppendLine(string.Format("	    , '{0}')           ", ent.DESC_CASO));                         //DESC_CASO                   

            return ExecutaSQL(sql.ToString());
        }

        public int Inserir_NAO_CADASTRADOS_DETRAN_RJ(NAO_CADASTRADOS_DETRAN_RJ ent)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO dbo.NAO_CADASTRADOS_DETRAN_RJ (PLACA                      ");
            sql.AppendLine("                                         , CHASSI                     ");
            sql.AppendLine("										 , UF                         ");
            sql.AppendLine("										 , CD_RENAVAM                 ");
            sql.AppendLine("										 , NU_MOTOR                   ");
            sql.AppendLine("										 , SITUACAO_VEICULO           ");
            sql.AppendLine("										 , CD_TIPO                    ");
            sql.AppendLine("										 , DESC_TIPO                  ");
            sql.AppendLine("										 , COD_MARCA_MODELO           ");
            sql.AppendLine("										 , DESC_MARCA_MODELO          ");
            sql.AppendLine("										 , COD_COR                    ");
            sql.AppendLine("										 , DESC_COR                   ");
            sql.AppendLine("										 , ANO_MODELO                 ");
            sql.AppendLine("										 , ANO_FABRICACAO             ");
            sql.AppendLine("										 , CMT_VEICULO                ");
            sql.AppendLine("										 , PBT_VEICULO                ");
            sql.AppendLine("										 , CAPAC_CARGA                ");
            sql.AppendLine("										 , NOME_PROCED_VEICULO        ");
            sql.AppendLine("										 , MENSAGEM                   ");
            sql.AppendLine("										 , NOME_AGENTE                ");
            sql.AppendLine("										 , CGC_AGENTE                 ");
            sql.AppendLine("										 , NOME_FINANCIADO            ");
            sql.AppendLine("										 , CPF_CGC_FINANCIADO         ");
            sql.AppendLine("										 , UF_DESTINO_FATURAMENTO)    ");
            sql.AppendLine(string.Format("VALUES ('{0}'   ", ent.PLACA));     //PLACA                 
            sql.AppendLine(string.Format("      , '{0}'   ", ent.CHASSI));     //CHASSI                
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.UF));     //UF                    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.CD_RENAVAM));     //CD_RENAVAM            
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.NU_MOTOR));     //NU_MOTOR              
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.SITUACAO_VEICULO));     //SITUACAO_VEICULO      
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.CD_TIPO));     //CD_TIPO               
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DESC_TIPO));     //DESC_TIPO             
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.COD_MARCA_MODELO));     //COD_MARCA_MODELO      
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DESC_MARCA_MODELO));     //DESC_MARCA_MODELO     
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.COD_COR));     //COD_COR               
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DESC_COR));     //DESC_COR              
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.ANO_MODELO));     //ANO_MODELO            
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.ANO_FABRICACAO));     //ANO_FABRICACAO        
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.CMT_VEICULO));     //CMT_VEICULO           
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.PBT_VEICULO));     //PBT_VEICULO           
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.CAPAC_CARGA));     //CAPAC_CARGA           
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.NOME_PROCED_VEICULO));     //NOME_PROCED_VEICULO   
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.MENSAGEM));     //MENSAGEM              
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.NOME_AGENTE));     //NOME_AGENTE           
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.CGC_AGENTE));     //CGC_AGENTE            
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.NOME_FINANCIADO));     //NOME_FINANCIADO       
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.CPF_CGC_FINANCIADO));     //CPF_CGC_FINANCIADO    
            sql.AppendLine(string.Format("	    , '{0}')  ", ent.UF_DESTINO_FATURAMENTO));     //UF_DESTINO_FATURAMENTO

            return ExecutaSQL(sql.ToString());
        }

        public int Inserir_SITUACAO_ROUBOFURTO_BIN(SITUACAO_ROUBOFURTO_BIN ent)
        {
            string sql = string.Format(@"
            INSERT INTO dbo.SITUACAO_ROUBOFURTO_BIN(PLACA, CHASSI, SITUACAO)
            VALUES('{0}','{1}','{2}')", ent.PLACA, ent.CHASSI, ent.SITUACAO);
            
            return ExecutaSQL(sql);
        }

        public int Inserir_RESTRICOES(RESTRICOES ent)
        {
            string sql = string.Format(@"
            INSERT INTO dbo.RESTRICOES(PLACA, CHASSI, RESTRICAO, SUB_RESTRICAO, DESC_RESTRICAO)
            VALUES('{0}','{1}','{2}','{3}','{4}')", 
            ent.PLACA, 
            ent.CHASSI, 
            ent.RESTRICAO,
            ent.SUB_RESTRICAO, 
            ent.DESC_RESTRICAO);

            return ExecutaSQL(sql);
        }

        public int Inserir_TRIBUTOS(TRIBUTOS ent)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO dbo.TRIBUTOS (PLACA               ");
            sql.AppendLine("                        , RENAVAM             ");
            sql.AppendLine("						, IPVA2012            ");
            sql.AppendLine("						, IPVA2013            ");
            sql.AppendLine("						, IPVA2014            ");
            sql.AppendLine("						, IPVA2015            ");
            sql.AppendLine("						, IPVA2016            ");
            sql.AppendLine("						, IPVA2017            ");
            sql.AppendLine("						, DAD2015             ");
            sql.AppendLine("						, DAD2016             ");
            sql.AppendLine("						, DAD2017             ");
            sql.AppendLine("						, DPVAT2015           ");
            sql.AppendLine("						, DPVAT2016           ");
            sql.AppendLine("						, DPVAT2017           ");
            sql.AppendLine("						, VIST2015            ");
            sql.AppendLine("						, VIST2016            ");
            sql.AppendLine("						, VIST2017            ");
            sql.AppendLine("						, PARC2012            ");
            sql.AppendLine("						, PARC2013            ");
            sql.AppendLine("						, PARC2014            ");
            sql.AppendLine("						, PARC2015            ");
            sql.AppendLine("						, PARC2016            ");
            sql.AppendLine("						, PARC2017            ");
            sql.AppendLine("						, RES_IPVA2012        ");
            sql.AppendLine("						, RES_IPVA2013        ");
            sql.AppendLine("						, RES_IPVA2014        ");
            sql.AppendLine("						, RES_IPVA2015        ");
            sql.AppendLine("						, RES_IPVA2016        ");
            sql.AppendLine("						, RES_IPVA2017        ");
            sql.AppendLine("						, DIV_ATIVA1          ");
            sql.AppendLine("						, DIV_ATIVA2          ");
            sql.AppendLine("						, DIV_ATIVA3          ");
            sql.AppendLine("						, DIV_ATIVA4          ");
            sql.AppendLine("						, DIV_ATIVA5          ");
            sql.AppendLine("						, DIV_ATIVA6          ");
            sql.AppendLine("						, DIV_ATIVA7          ");
            sql.AppendLine("						, DIV_ATIVA8          ");
            sql.AppendLine("						, DIV_ATIVA9          ");
            sql.AppendLine("						, DIV_ATIVA10         ");
            sql.AppendLine("						, DIV_ATIVA11         ");
            sql.AppendLine("						, DIV_ATIVA12         ");
            sql.AppendLine("						, DIV_ATIVA13         ");
            sql.AppendLine("						, DIV_ATIVA14)        ");
            sql.AppendLine(string.Format("VALUES ('{0}'   ", ent.PLACA));    //PLACA       
            sql.AppendLine(string.Format("      , '{0}'   ", ent.RENAVAM));    //RENAVAM     
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.IPVA2012));    //IPVA2012    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.IPVA2013));    //IPVA2013    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.IPVA2014));    //IPVA2014    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.IPVA2015));    //IPVA2015    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.IPVA2016));    //IPVA2016    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.IPVA2017));    //IPVA2017    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DAD2015));    //DAD2015     
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DAD2016));    //DAD2016     
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DAD2017));    //DAD2017     
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DPVAT2015));    //DPVAT2015   
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DPVAT2016));    //DPVAT2016   
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DPVAT2017));    //DPVAT2017   
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.VIST2015));    //VIST2015    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.VIST2016));    //VIST2016    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.VIST2017));    //VIST2017    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.PARC2012));    //PARC2012    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.PARC2013));    //PARC2013    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.PARC2014));    //PARC2014    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.PARC2015));    //PARC2015    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.PARC2016));    //PARC2016    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.PARC2017));    //PARC2017    
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.RES_IPVA2012));    //RES_IPVA2012
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.RES_IPVA2013));    //RES_IPVA2013
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.RES_IPVA2014));    //RES_IPVA2014
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.RES_IPVA2015));    //RES_IPVA2015
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.RES_IPVA2016));    //RES_IPVA2016
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.RES_IPVA2017));    //RES_IPVA2017
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA1));    //DIV_ATIVA1  
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA2));    //DIV_ATIVA2  
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA3));    //DIV_ATIVA3  
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA4));    //DIV_ATIVA4  
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA5));    //DIV_ATIVA5  
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA6));    //DIV_ATIVA6  
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA7));    //DIV_ATIVA7  
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA8));    //DIV_ATIVA8  
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA9));    //DIV_ATIVA9  
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA10));    //DIV_ATIVA10 
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA11));    //DIV_ATIVA11 
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA12));    //DIV_ATIVA12 
            sql.AppendLine(string.Format("	    , '{0}'   ", ent.DIV_ATIVA13));    //DIV_ATIVA13 
            sql.AppendLine(string.Format("	    , '{0}')  ", ent.DIV_ATIVA14));    //DIV_ATIVA14)

            return ExecutaSQL(sql.ToString());
        }
        
        public int Inserir_MULTAS(MULTAS ent)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO dbo.MULTAS (NUM_AIT            ");
            sql.AppendLine("                      , NUM_AUTO_RENAINF   ");
            sql.AppendLine("					  , DT_INFRACAO        ");
            sql.AppendLine("					  , HORA_INFRACAO      ");
            sql.AppendLine("					  , DT_VENCIMENTO      ");
            sql.AppendLine("					  , CAPIT_INFRACAO     ");
            sql.AppendLine("					  , COD_INFRACAO       ");
            sql.AppendLine("					  , DESC_INFRACAO      ");
            sql.AppendLine("					  , STATUS_INFRACAO    ");
            sql.AppendLine("					  , VLR_MULTA          ");
            sql.AppendLine("					  , DT_INCLUSAO        ");
            sql.AppendLine("					  , MUNICIPIO_INFRACAO ");
            sql.AppendLine("					  , LOCAL_INFRACAO     ");
            sql.AppendLine("					  , PTS_INFRACAO       ");
            sql.AppendLine("					  , DESCRICAO_PAGO     ");
            sql.AppendLine("					  , PLACA_MULTA        ");
            sql.AppendLine("					  , RENAVAM_MULTA      ");
            sql.AppendLine("					  , CPF_INFRACAO       ");
            sql.AppendLine("					  , VLR_MULTA_SMTR     ");
            sql.AppendLine("					  , AUTO_PARCELAMENTO  ");
            sql.AppendLine("					  , ORGAO_AUTUADOR     ");
            sql.AppendLine("					  , AGENTE_AUTUADOR)   ");
            sql.AppendLine(string.Format("VALUES ('{0}'        ", ent.NUM_AIT));
            sql.AppendLine(string.Format("      , '{0}'        ", ent.NUM_AUTO_RENAINF));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.DT_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.HORA_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.DT_VENCIMENTO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.CAPIT_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.COD_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.DESC_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.STATUS_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.VLR_MULTA));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.DT_INCLUSAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.MUNICIPIO_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.LOCAL_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.PTS_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.DESCRICAO_PAGO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.PLACA_MULTA));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.RENAVAM_MULTA));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.CPF_INFRACAO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.VLR_MULTA_SMTR));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.AUTO_PARCELAMENTO));
            sql.AppendLine(string.Format("	    , '{0}'        ", ent.ORGAO_AUTUADOR));
            sql.AppendLine(string.Format("	    , '{0}')       ", ent.AGENTE_AUTUADOR));

            return ExecutaSQL(sql.ToString());
        }

        public int Inserir_DETALHE_RENAINF(DETALHE_RENAINF ent)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO dbo.DETALHE_RENAINF (CD_INF                 ");
            sql.AppendLine("                               , DV_CD_INF              ");
            sql.AppendLine("							   , DESD_CD_INF            ");
            sql.AppendLine("							   , DS_INF_TAB             ");
            sql.AppendLine("							   , AUTO                   ");
            sql.AppendLine("							   , DT_INF                 ");
            sql.AppendLine("							   , DT_PGT_INF             ");
            sql.AppendLine("							   , VALOR                  ");
            sql.AppendLine("							   , SITUACAO               ");
            sql.AppendLine("							   , STATUS                 ");
            sql.AppendLine("							   , STATUS2                ");
            sql.AppendLine("							   , BASE                   ");
            sql.AppendLine("							   , IDENT                  ");
            sql.AppendLine("							   , NM_PROP_INIC           ");
            sql.AppendLine("							   , NM_LOG_PROP_INIC       ");
            sql.AppendLine("							   , NU_END_PROP_INIC       ");
            sql.AppendLine("							   , CP_END_PROP_INIC       ");
            sql.AppendLine("							   , NU_CEP_ENDPROP_INIC    ");
            sql.AppendLine("							   , CD_MUN_END_PROP_INIC   ");
            sql.AppendLine("							   , DESCRICAO_MUNICIPIO    ");
            sql.AppendLine("							   , CD_ORG_AUT             ");
            sql.AppendLine("							   , DS_ORG_AUT             ");
            sql.AppendLine("							   , TP                     ");
            sql.AppendLine("							   , CD_REN_VEI_INF         ");
            sql.AppendLine("							   , PL_VEI_INF_            ");
            sql.AppendLine("							   , CD_STATUS_AUT_SM       ");
            sql.AppendLine("							   , DESC_STATUS_SM         ");
            sql.AppendLine("							   , DT_STATUS_AUT_SM       ");
            sql.AppendLine("							   , NU_PROC_CAN_SM         ");
            sql.AppendLine("							   , IND_PRESCRITO          ");
            sql.AppendLine("							   , UF_ORG_AUT             ");
            sql.AppendLine("							   , NUM_AUT_INFORMADO      ");
            sql.AppendLine("							   , COD_RENAINF)           ");
            sql.AppendLine(string.Format("VALUES ('{0}'    ", ent.CD_INF));
            sql.AppendLine(string.Format("      , '{0}'    ", ent.DV_CD_INF));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.DESD_CD_INF));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.DS_INF_TAB));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.AUTO));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.DT_INF));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.DT_PGT_INF));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.VALOR));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.SITUACAO));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.STATUS));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.STATUS2));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.BASE));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.IDENT));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.NM_PROP_INIC));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.NM_LOG_PROP_INIC));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.NU_END_PROP_INIC));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.CP_END_PROP_INIC));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.NU_CEP_ENDPROP_INIC));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.CD_MUN_END_PROP_INIC));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.DESCRICAO_MUNICIPIO));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.CD_ORG_AUT));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.DS_ORG_AUT));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.TP));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.CD_REN_VEI_INF));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.PL_VEI_INF_));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.CD_STATUS_AUT_SM));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.DESC_STATUS_SM));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.DT_STATUS_AUT_SM));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.NU_PROC_CAN_SM));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.IND_PRESCRITO));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.UF_ORG_AUT));
            sql.AppendLine(string.Format("	    , '{0}'    ", ent.NUM_AUT_INFORMADO));
            sql.AppendLine(string.Format("	    , '{0}')   ", ent.COD_RENAINF));

            return ExecutaSQL(sql.ToString());
        }

    }
}
