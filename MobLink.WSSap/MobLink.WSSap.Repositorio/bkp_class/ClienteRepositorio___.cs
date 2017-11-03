using System;
using System.Net;
using System.Text;

namespace MobLink.WSSap.Repositorio
{
    /// <summary>
    /// 
    /// </summary>
    public class ClienteRepositorio___ : SapRepositorio___
    {
        //CLIENTE
        protected si_cliente_requestService.si_cliente_requestService SERVICO_SAP_CLIENTE_INC;
        protected si_cliente_requestService.dt_cliente_request OBJETO_SAP_CLIENTE_INC;

        //CLIENTE MODIFICAÇÃO
        protected si_modificacao_parceiroService.si_modificacao_parceiroService SERVICO_SAP_CLIENTE_MODIF;
        protected si_modificacao_parceiroService.dt_modificacao_parceiro OBJETO_SAP_CLIENTE_MODIF;
                
        private string ConsultarCliente(Dominio.ClienteSap Cliente)
        {
            #region VALIDAÇÕES
            if (string.IsNullOrEmpty(Cliente.CPF) && string.IsNullOrEmpty(Cliente.CNPJ)) { return string.Empty; }
            #endregion

            #region CONSULTA SQL
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("   SELECT tb_sap_retorno.id_documento , MAX(tb_sap_clientes.data_cadastro)    ");
            sql.AppendLine("     FROM tb_sap_clientes                                                                                          ");
            sql.AppendLine("LEFT JOIN tb_sap_retorno                                                                                              ");
            sql.AppendLine("       ON tb_sap_retorno.id_transacao_sap = tb_sap_clientes.id_transacao_sap     ");
            sql.AppendLine("    WHERE 1 = 1                                                               ");

            if (!string.IsNullOrEmpty(Cliente.CPF))
                sql.AppendFormat(" AND tb_sap_clientes.cpf = '{0}'", Cliente.CPF.Trim());
            else
                sql.AppendFormat(" AND tb_sap_clientes.cnpj = '{0}'", Cliente.CNPJ.Trim());

            //AMPLIAÇÃO
            if (!string.IsNullOrEmpty(Cliente.Codigo_Empresa))
                sql.AppendFormat(" AND codigo_empresa = '{0}' ", Cliente.Codigo_Empresa);

            //sql.AppendLine("AND tb_sap_retorno.id_documento IS NOT NULL");
            //sql.AppendLine("AND tb_sap_retorno.id_documento <> ''");
            sql.AppendLine("GROUP BY tb_sap_retorno.id_documento");
            #endregion

            try
            {
                var res = ConsultaSQL(sql.ToString());

                //SE RES = NULL CRIAR
                //SE RES <> NULL E ROWCOUNT > 0 E ID_DOCUMENTO = NULL APAGAR REGISTROS DO MESMO DOCUMENTO E SOLICITAR CRIAÇÃO
                //SE RES <> NULL E ROWCOUNT > 0 E ID_DOCUMENTO <> NULL SOLICITAR ALTERAÇÃO

                if (res != null && res.Rows.Count > 0)
                {
                    var doc_sap = Funcoes.DataBase.GetString(res, "id_documento", doTrim: true);

                    if (string.IsNullOrEmpty(doc_sap)) //ja houveram tentativas mas sem retorno
                    {
                        return "SR";
                    }

                    return doc_sap;
                }   
                else
                    return string.Empty;    //Criar Cliente, pois não existe
            }
            catch
            {
                throw;
            }
        }

        private int InserirCliente(Dominio.ClienteSap Cliente)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO dbo.tb_sap_clientes ");
            SQL.AppendLine("( id_transacao_sap              ");
            SQL.AppendLine(", codigo_empresa                ");
            SQL.AppendLine(", organizacao_vendas            ");
            SQL.AppendLine(", nome_cliente                  ");
            SQL.AppendLine(", endereco_rua                  ");
            SQL.AppendLine(", endereco_numero               ");
            SQL.AppendLine(", edificio                      ");
            SQL.AppendLine(", andar                         ");
            SQL.AppendLine(", bairro                        ");
            SQL.AppendLine(", cep                           ");
            SQL.AppendLine(", cidade                        ");
            SQL.AppendLine(", regiao                        ");
            SQL.AppendLine(", contato_telefone              ");
            SQL.AppendLine(", contato_email                 ");
            SQL.AppendLine(", cnpj                          ");
            SQL.AppendLine(", cpf                           ");
            SQL.AppendLine(", inscricao_estadual            ");
            SQL.AppendLine(", inscricao_municipal           ");
            SQL.AppendLine(", forma_pagamento               ");
            SQL.AppendLine(", condicao_pagamento            ");
            SQL.AppendLine(", id_grv                        ");
            SQL.AppendLine(", tipo_parceiro                 ");
            SQL.AppendLine(", cod_parceiro                  ");
            SQL.AppendLine(", cod_org)                      ");

            SQL.AppendLine("VALUES");

            SQL.AppendFormat(" ({0}  ", Cliente.Transacao_SAP);
            SQL.AppendFormat(",'{0}' ", Cliente.Codigo_Empresa);
            SQL.AppendFormat(",'{0}' ", Cliente.Codigo_Organizacao_Parceiro);
            SQL.AppendFormat(",'{0}' ", Cliente.Nome_Cliente);

            SQL.AppendFormat(",'{0}' ", Cliente.Endereco_Rua);
            SQL.AppendFormat(",'{0}' ", Cliente.Endereco_Numero);
            SQL.AppendFormat(",'{0}' ", Cliente.Endereco_Edificio);
            SQL.AppendFormat(",'{0}' ", Cliente.Endereco_Edificio_Andar);
            SQL.AppendFormat(",'{0}' ", Cliente.Endereco_Bairro);
            SQL.AppendFormat(",'{0}' ", Cliente.Endereco_CEP);

            SQL.AppendFormat(",'{0}' ", Cliente.Endereco_Cidade);
            SQL.AppendFormat(",'{0}' ", Cliente.Endereco_Regiao);
            SQL.AppendFormat(",'{0}' ", Cliente.Telefone);
            SQL.AppendFormat(",'{0}' ", Cliente.Email);
            SQL.AppendFormat(",'{0}' ", Cliente.CNPJ);

            SQL.AppendFormat(",'{0}' ", Cliente.CPF);
            SQL.AppendFormat(",'{0}' ", Cliente.Inscricao_Estadual);
            SQL.AppendFormat(",'{0}' ", Cliente.Inscricao_Municipal);
            SQL.AppendFormat(",'{0}' ", Cliente.Forma_Pagamento);
            SQL.AppendFormat(",'{0}' ", Cliente.Condicao_Pagamento);
            SQL.AppendFormat(", {0}  ", Cliente.Id_GRV);
            SQL.AppendFormat(",'{0}' ", Cliente.Tipo_Parceiro);
            SQL.AppendFormat(",'{0}' ", Cliente.Codigo_Parceiro);
            SQL.AppendFormat(",'{0}') ", Cliente.Codigo_Organizacao_Parceiro);
            try
            {
                return ExecutaSQL(SQL.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                
            }

        }

        private int ExcluirCliente(Dominio.ClienteSap Cliente)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("DELETE dbo.tb_sap_clientes ");
            SQL.AppendFormat("WHERE cpf = '{0}' OR cnpj = '{1}'", Cliente.CPF, Cliente.CNPJ);
            
            try
            {
                return ExecutaSQL(SQL.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                
            }

        }

        private int AlterarCliente(Dominio.ClienteSap Cliente)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendFormat("    UPDATE dbo.tb_sap_clientes          ");
            SQL.AppendFormat("       SET id_grv              = '{0}'", Cliente.Id_GRV);
            SQL.AppendFormat("          ,codigo_empresa      = '{0}'", Cliente.Codigo_Empresa);
            SQL.AppendFormat("          ,organizacao_vendas  = '{0}'", string.IsNullOrEmpty(Cliente.Organizacao_Vendas) ? Cliente.Codigo_Organizacao_Parceiro : Cliente.Organizacao_Vendas);
            SQL.AppendFormat("          ,nome_cliente        = '{0}'", Cliente.Nome_Cliente);
            SQL.AppendFormat("          ,endereco_rua        = '{0}'", Cliente.Endereco_Rua);
            SQL.AppendFormat("          ,endereco_numero     = '{0}'", Cliente.Endereco_Numero);
            SQL.AppendFormat("          ,edificio            = '{0}'", Cliente.Endereco_Edificio);
            SQL.AppendFormat("          ,andar               = '{0}'", Cliente.Endereco_Edificio_Andar);
            SQL.AppendFormat("          ,bairro              = '{0}'", Cliente.Endereco_Bairro);
            SQL.AppendFormat("          ,cep                 = '{0}'", Cliente.Endereco_CEP);
            SQL.AppendFormat("          ,cidade              = '{0}'", Cliente.Endereco_Cidade);
            SQL.AppendFormat("          ,regiao              = '{0}'", Cliente.Endereco_Regiao);
            SQL.AppendFormat("          ,contato_telefone    = '{0}'", Cliente.Telefone);
            SQL.AppendFormat("          ,contato_email       = '{0}'", Cliente.Email);
            SQL.AppendFormat("          ,cnpj                = '{0}'", Cliente.CNPJ);
            SQL.AppendFormat("          ,cpf                 = '{0}'", Cliente.CPF);
            SQL.AppendFormat("          ,inscricao_estadual  = '{0}'", Cliente.Inscricao_Estadual);
            SQL.AppendFormat("          ,inscricao_municipal = '{0}'", Cliente.Inscricao_Municipal);
            SQL.AppendFormat("          ,forma_pagamento     = '{0}'", Cliente.Forma_Pagamento);
            SQL.AppendFormat("          ,condicao_pagamento  = '{0}'", Cliente.Condicao_Pagamento);
            SQL.AppendFormat("          ,tipo_parceiro       = '{0}'", Cliente.Tipo_Parceiro);
            SQL.AppendFormat("          ,cod_org             = '{0}'", Cliente.Codigo_Organizacao_Parceiro);
            SQL.AppendFormat("          ,cod_parceiro        = '{0}'", Cliente.Codigo_Parceiro);
            SQL.AppendFormat("    WHERE cpf = '{0}' OR cnpj  = '{1}'", Cliente.CPF, Cliente.CNPJ);

            try
            {
                return ExecutaSQL(SQL.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                
            }
        }

        private void EnviarTransacaoInclusaoSap(Dominio.ClienteSap Cliente)
        {
            OBJETO_SAP_CLIENTE_INC = new si_cliente_requestService.dt_cliente_request()
            {
                IDTRANSACAO = Cliente.Transacao_SAP,
                BUKRS = Cliente.Codigo_Empresa,
                VKORG = Cliente.Codigo_Empresa,                 //Solicitado por Cristiney, em 09/11/2016 - Informou que a regra não vai mudar
                NAME1 = Cliente.Nome_Cliente,
                TELF1 = Cliente.Telefone,
                SMTP_ADDR = Cliente.Email,
                ZWELS = Cliente.Forma_Pagamento,
                ZTERM = Cliente.Condicao_Pagamento,
                STRAS = Cliente.Endereco_Rua,
                HOUSE_NUM1 = Cliente.Endereco_Numero,
                ORT02 = Cliente.Endereco_Bairro,
                PSTLZ = Cliente.Endereco_CEP,
                MCOD3 = Cliente.Endereco_Cidade,
                REGIO = Cliente.Endereco_Regiao,
                LAND1 = Cliente.Endereco_Pais,
                BUILDING = Cliente.Endereco_Edificio,
                FLOOR = Cliente.Endereco_Edificio_Andar,
                STCD1 = Cliente.CNPJ,
                STCD2 = Cliente.CPF,
                STCD3 = Cliente.Inscricao_Estadual,
                STCD4 = Cliente.Inscricao_Municipal
            };
            SERVICO_SAP_CLIENTE_INC.si_cliente_request(OBJETO_SAP_CLIENTE_INC);
        }

        private void EnviarTransacaoModificacaoSap(Dominio.ClienteSap Cliente)
        {
            OBJETO_SAP_CLIENTE_MODIF = new si_modificacao_parceiroService.dt_modificacao_parceiro()
            {
                IDTRANSACAO = Cliente.Transacao_SAP,
                TIPO_PARCEIRO = Cliente.Tipo_Parceiro,
                COD_PARCEIRO = Cliente.Codigo_Parceiro,
                COD_ORG = Cliente.Codigo_Organizacao_Parceiro,
                STRAS = Cliente.Endereco_Rua,
                HOUSE_NUM1 = Cliente.Endereco_Numero,
                BUILDING = Cliente.Endereco_Edificio,
                FLOOR = Cliente.Endereco_Edificio_Andar,
                ORT02 = Cliente.Endereco_Bairro,
                PSTLZ = Cliente.Endereco_CEP,
                MCOD3 = Cliente.Endereco_Cidade,
                REGIO = Cliente.Endereco_Regiao,
                TELF1 = Cliente.Telefone,
                SMTP_ADDR = Cliente.Email,
                ZTERM = Cliente.Condicao_Pagamento
            };
            SERVICO_SAP_CLIENTE_MODIF.si_modificacao_parceiro(OBJETO_SAP_CLIENTE_MODIF);
        }

        private Dominio.Retorno CadastraClienteSAP(Dominio.ClienteSap Cliente)
        {
            try
            {
                #region VALIDAÇÕES
                if (!string.IsNullOrEmpty(Cliente.CNPJ))
                {
                    Cliente.CNPJ = Cliente.CNPJ.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                    if (!Funcoes.Documentos.ValidarCNPJ(Cliente.CNPJ)) { return new Dominio.Retorno() { Resultado = false, Mensagem = "CNPJ Inválido" }; }
                }

                if (!string.IsNullOrEmpty(Cliente.CPF))
                {
                    Cliente.CPF = Cliente.CPF.Trim().Replace(".", "").Replace("-", "").Replace("-", "");
                    if (!Funcoes.Documentos.ValidarCPF(Cliente.CPF)) { return new Dominio.Retorno() { Resultado = false, Mensagem = "CPF Inválido" }; }
                }
                #endregion

                var CodigoParceiro = ConsultarCliente(Cliente);

                switch (CodigoParceiro)
                {
                    case "SR": //EXCLUIR E CRIAR O CLIENTE
                        ExcluirCliente(Cliente);

                        Cliente.Transacao_SAP = GetTransacaoSap();
                        InserirCliente(Cliente);
                        InserirSolicitacao(Cliente.Transacao_SAP, "CRIAR CLIENTE", id_grv: Cliente.Id_GRV);
                        EnviarTransacaoInclusaoSap(Cliente);
                        break;
                    case "": //CRIA O CLIENTE
                        Cliente.Transacao_SAP = GetTransacaoSap();
                        InserirCliente(Cliente);
                        InserirSolicitacao(Cliente.Transacao_SAP, "CRIAR CLIENTE", id_grv: Cliente.Id_GRV);
                        EnviarTransacaoInclusaoSap(Cliente);
                        break;
                    default: //ALTERA O CLIENTE  
                        Cliente.Transacao_SAP = GetTransacaoSap();
                        Cliente.Codigo_Parceiro = CodigoParceiro;
                        Cliente.Tipo_Parceiro = "C";
                        AlterarCliente(Cliente);
                        InserirSolicitacao(Cliente.Transacao_SAP, "MODIFICAR CLIENTE", id_grv: Cliente.Id_GRV);
                        EnviarTransacaoModificacaoSap(Cliente);
                        break;
                }

                return new Dominio.Retorno() { Resultado = true, Mensagem = "" };
            }
            catch (Exception Except)
            {
                Log(Except, "CadastraClienteSAP", int.Parse(Cliente.Transacao_SAP));
                return new Dominio.Retorno() { Mensagem = Except.ToString(), Resultado = false };
            }
        }

        public ClienteRepositorio___()
        {
            CredentialCache Credenciais;

            SERVICO_SAP_CLIENTE_INC = new si_cliente_requestService.si_cliente_requestService();

            Credenciais = new CredentialCache
            {
                {
                    new Uri(SERVICO_SAP_CLIENTE_INC.Url),
                    "Basic",
                    new NetworkCredential()
                    {
                        UserName = Framework.Setup.WebServices.WsSap(Ambiente).Usuario,
                        Password = Framework.Setup.WebServices.WsSap(Ambiente).Senha
                    }
                }
            };

            SERVICO_SAP_CLIENTE_INC.Credentials = Credenciais;
            SERVICO_SAP_CLIENTE_INC.PreAuthenticate = true;

            OBJETO_SAP_CLIENTE_INC = new si_cliente_requestService.dt_cliente_request();

            SERVICO_SAP_CLIENTE_MODIF = new si_modificacao_parceiroService.si_modificacao_parceiroService();

            Credenciais = new CredentialCache
            {
                {
                    new Uri(SERVICO_SAP_CLIENTE_MODIF.Url),
                    "Basic",
                    new NetworkCredential()
                    {
                        UserName = Framework.Setup.WebServices.WsSap(this.Ambiente).Usuario,
                        Password = Framework.Setup.WebServices.WsSap(this.Ambiente).Senha
                    }
                }
            };
            SERVICO_SAP_CLIENTE_MODIF.Credentials = Credenciais;
            SERVICO_SAP_CLIENTE_MODIF.PreAuthenticate = true;

            OBJETO_SAP_CLIENTE_MODIF = new si_modificacao_parceiroService.dt_modificacao_parceiro();
        }

        public static Dominio.Retorno CadastraCliente(Dominio.ClienteSap Cliente)
        {
            ClienteRepositorio___ ClienteRepositorio = new ClienteRepositorio___();
            return ClienteRepositorio.CadastraClienteSAP(Cliente);
        }

    }
}
