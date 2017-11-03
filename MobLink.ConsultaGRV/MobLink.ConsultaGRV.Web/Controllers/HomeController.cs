using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CCFW;

namespace MobLink.ConsultaGRV.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Consulta GRV - MobLink";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string placa, string chassi, string GRV, FormCollection f)
        {
            ViewBag.Title = "Consulta GRV - MobLink";

            placa = string.IsNullOrEmpty(placa) ? string.Empty : placa.Trim();

            chassi = string.IsNullOrEmpty(chassi) ? string.Empty : chassi.Trim();

            GRV = string.IsNullOrEmpty(GRV) ? string.Empty : GRV.Replace('⁠', ' ').Trim();


            var EMITE_BOLETO = Convert.ToBoolean(f["EMITE_BOLETO"]);
            var ENVIA_EMAIL = Convert.ToBoolean(f["ENVIA_EMAIL"]);
            var ATUALIZA_BOLETO = Convert.ToBoolean(f["ATUALIZA_BOLETO"]);
            var NOVA_CONSULTA = Convert.ToBoolean(f["NOVA_CONSULTA"]);

            if (NOVA_CONSULTA)
            {
                return View();
            }

            if (ENVIA_EMAIL)
            {
                EnviarBoletoPorEMail(GRV);
                return View();
            }

            if (ATUALIZA_BOLETO)
            {
                var IdFaturamento = GerarFaturamento(GRV);
                var Boleto = GerarBoleto(IdFaturamento);
                Boleto.IdGRV = Convert.ToInt32(GRV);
                EnviarBoletoPorEMail(GRV);

                return View();
            }

            if (EMITE_BOLETO)
            {
                #region PREENCHE DADOS ATENDIMENTO

                var Atendimento = new Model.Atendimento.Atendimento();

                Atendimento.nota_fiscal_bairro = f["BAIRRO"];
                Atendimento.nota_fiscal_cep = f["CEP"].Replace("-", "");
                Atendimento.nota_fiscal_complemento = f["COMPLEMENTO"];
                Atendimento.nota_fiscal_cpf = f["DOC"].Replace("-", "").Replace(".", "").Replace("/", "");
                Atendimento.nota_fiscal_ddd = f["DDD"].Replace("(", "").Replace(")", "");
                Atendimento.nota_fiscal_email = f["EMAIL"];
                Atendimento.nota_fiscal_endereco = f["ENDERECO"];
                Atendimento.nota_fiscal_municipio = f["MUNICIPIO"];
                Atendimento.nota_fiscal_nome = f["NOME"];
                Atendimento.nota_fiscal_numero = f["NUMERO"];
                Atendimento.nota_fiscal_telefone = f["TELEFONE"].Replace("-", "");
                Atendimento.nota_fiscal_uf = f["UF"];
                Atendimento.proprietario_id_tipo_documento = f["TIPO_DOC"] == "0" ? 2 : 1;
                #endregion

                int IdAtendimento = GerarAtendimento(GRV, Atendimento);

                int IdFaturamento = 0;

                try
                {
                    IdFaturamento = GerarFaturamento(GRV);
                }
                catch (Exception e)
                {

                    throw;
                }
                


                var Boleto = GerarBoleto(IdFaturamento);
                Boleto.IdGRV = Convert.ToInt32(GRV);

                return View("../_ImprimirBoletoAtendimento", Boleto);
            }

            #region CONSULTA

            if (string.IsNullOrEmpty(placa) && string.IsNullOrEmpty(chassi) && GRV == null)
            {
                ViewBag.Erro = "É NECESSÁRIO INFORMAR AO MENOS UM PARAMETRO PARA A PESQUISA.";
                return View();
            }

            Model.GRV.GRV ModelGRV = new Model.GRV.GRV();

            try
            {
                var modelCalculoDiarias = new Model.GRV.CalculoDiarias();

                MSDAL.ConnectionFactory.connectionString = MobLink.Framework.Util.LerConfiguracao("CONEXAO");
                MSDAL.ConnectionFactory.ConnectDataBase();

                List<Model.GRV.GRV> ListaGRV = Business.GRV.GRV.Listar(
                    new Model.GRV.GRV()
                    {
                        placa = placa.Trim(),
                        chassi = chassi.Trim(),
                        numero_formulario_grv = GRV.ToString()
                    });



                if (ListaGRV == null)
                {
                    ViewBag.Erro = "GRV NÃO LOCALIZADO";
                    return View();
                }

                ModelGRV = ListaGRV.FirstOrDefault();

                //MAPEAR O HTML DE PROCEDIMENTOS DE LIBERAÇÃO EM _ProcedimentosLiberacao.cshtml
                ViewBag.id_cliente = ModelGRV.id_cliente;

                if (new[] { "G", "L" }.Contains(ModelGRV.id_status_operacao))
                {
                    if (ModelGRV.id_status_operacao == "L") //AGUARDANDO PAGAMENTO
                    {
                        Model.Atendimento.Atendimento ModelAtendimento = new Model.Atendimento.Atendimento() { id_grv = ModelGRV.id_grv };
                        var atendimento = Business.Atendimento.Atendimento.Listar(ModelAtendimento).FirstOrDefault();

                        Model.Faturamento.Faturamento ModelFaturamento = new Model.Faturamento.Faturamento() { id_atendimento = atendimento.id_atendimento };
                        var faturamento = Business.Faturamento.Faturamento.Listar(ModelFaturamento).LastOrDefault();

                        Model.Faturamento.FaturamentoBoletos ModelFaturamentoBoletos = new Model.Faturamento.FaturamentoBoletos();
                        var boletos = Business.Faturamento.FaturamentoBoletos.Consultar(faturamento.id_faturamento);

                        var vboleto = Business.Report.ViewBoleto.Retornar(faturamento.id_faturamento);

                        ViewBag.Msg = "BOLETO ENCONTRADO";

                        ViewBag.atendimento = atendimento;
                        ViewBag.faturamento = faturamento;
                        ViewBag.boleto = vboleto;
                    }
                }
                else
                {
                    //MOSTRAR O STATUS NA VIEWBAG
                    StringBuilder SQL = new StringBuilder();
                    SQL.AppendFormat("select * from tb_dep_status_operacoes where id_status_operacao = '{0}'", char.Parse(ListaGRV.FirstOrDefault().id_status_operacao));
                    var dtStatus = Business.Sistema.GlobalDataBase.Select(SQL);

                    var desc = dtStatus
                        .AsEnumerable()
                        .FirstOrDefault()
                        .ConverterParaEntidade<Model.GRV.StatusOperacoes>()
                        .descricao
                        .ToUpper();

                    ViewBag.Erro = "VEÍCULO / GRV NÃO PODE SER CONSULTADO. MOTIVO: " + desc;
                    return View();
                }

                ViewBag.placa = ModelGRV.placa;
                ViewBag.chassi = ModelGRV.chassi;
                ViewBag.renavam = ModelGRV.renavam;
                ViewBag.data_hora_guarda = ModelGRV.data_hora_guarda;
                ViewBag.numero_formulario_grv = ModelGRV.numero_formulario_grv;

                var marcaModelo = Business.Detran.DetranMarcaModelo.Listar(new Model.Detran.DetranMarcaModelo() { id_detran_marca_modelo = ModelGRV.id_detran_marca_modelo });
                ViewBag.marcaModeloDesc = marcaModelo.FirstOrDefault().descricao;

                var tipoVeiculo = Business.Veiculos.TipoVeiculos.ConsultarRelacionado(new Model.Veiculos.TipoVeiculos() { id_tipo_veiculo = ModelGRV.id_tipo_veiculo });
                ViewBag.tipoVeiculoDesc = tipoVeiculo.Rows[0]["descricao"].ToString();

                var listViewFaturamentoServicosGRV = Business.Faturamento.Servicos.ViewFaturamentoServicosGRV.Listar(ModelGRV.id_grv, "DEP");
                ViewBag.modelo = listViewFaturamentoServicosGRV;

                var dep = Business.Depositos.DepositosController.ConsultarRelacionado(ModelGRV.id_deposito);

                ViewBag.deposito = Funcoes.DataBase.GetString(dep, "descricao") + " (" +
                    Funcoes.DataBase.GetString(dep, "cep_tipo_logradouro") + " " +
                    Funcoes.DataBase.GetString(dep, "cep_logradouro") + ", " +
                    Funcoes.DataBase.GetString(dep, "cliente_numero_logradouro") + " - " +
                    Funcoes.DataBase.GetString(dep, "cep_bairro") + " - " +
                    Funcoes.DataBase.GetString(dep, "cep_municipio") + " - " +
                    Funcoes.DataBase.GetString(dep, "cep_uf") + ")";

                #region CÁLCULO DE DIÁRIAS
                var consulta = Business.GRV.GRV.ConsultarRelacionado(ModelGRV, null, null, Business.Sistema.Configuracoes.id_usuario);

                modelCalculoDiarias.id_cliente = Funcoes.DataBase.GetInt(consulta, "id_cliente");

                modelCalculoDiarias.id_deposito = Funcoes.DataBase.GetInt(consulta, "id_deposito");

                modelCalculoDiarias.maximo_diarias_para_cobranca = Funcoes.DataBase.GetInt(consulta, "cliente_maximo_diarias_para_cobranca");

                modelCalculoDiarias.maximo_dias_vencimento = Funcoes.DataBase.GetInt(consulta, "cliente_maximo_dias_vencimento");

                modelCalculoDiarias.hora_diaria = Funcoes.DataBase.GetString(consulta, "cliente_hora_diaria");

                modelCalculoDiarias.flag_cliente_realiza_faturamento_arrecadacao = Funcoes.DataBase.GetChar(consulta, "cliente_flag_cliente_realiza_faturamento_arrecadacao");

                modelCalculoDiarias.flag_usar_hora_diaria = Funcoes.DataBase.GetChar(consulta, "cliente_flag_usar_hora_diaria");

                modelCalculoDiarias.flag_cobrar_diarias_dias_corridos = Funcoes.DataBase.GetChar(consulta, "cliente_flag_cobrar_diarias_dias_corridos");

                modelCalculoDiarias.data_hora_guarda = Funcoes.DataBase.GetDatetime(consulta, "data_hora_guarda");

                modelCalculoDiarias.diarias = Business.Calculos.Diarias.Calcular(modelCalculoDiarias);

                ViewBag.diarias = modelCalculoDiarias.diarias;

                #endregion CÁLCULO DE DIÁRIAS
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }
            finally
            {
                MSDAL.ConnectionFactory.DisconnectDataBase();
            }

            #endregion  

            return View(ModelGRV);
        }



        private void EnviarBoletoPorEMail(string GRV)
        {
            var atendimento = Business.Atendimento.Atendimento
                    .Listar(new Model.Atendimento.Atendimento() { id_grv = Convert.ToInt32(GRV) })
                    .FirstOrDefault();

            var faturamento = Business.Faturamento.Faturamento
                .Listar(new Model.Faturamento.Faturamento() { id_atendimento = atendimento.id_atendimento })
                .LastOrDefault();

            var view = Business.Report.ViewBoleto.Consultar(faturamento.id_faturamento)
                .AsEnumerable()
                .FirstOrDefault().ConverterParaEntidade<Model.Report.ViewBoleto>();

            var boleto = PegarSegundaViaBoleto(faturamento.id_faturamento);

            Stream attach = new MemoryStream(boleto.ImagemBoleto);

            var subject = string.Format("BOLETO {0} - PROCESSO: {1}",
                view.cedente_nome, view.numero_documento);

            var body = string.Format("Prezado(a) {0},\n\nSegue em anexo o boleto solicitado no valor de R${1}.\n\n" +
                                     "SALIENTAMOS QUE ESTE BOLETO SÓ É VÁLIDO NA DATA DE HOJE.\n\n" +
                                     "Caso seja sua preferência, pode utilizar a linha digitável a seguir com e sem formatação para efetuar o pagamento.\n\n" +
                                     "Linha formatada:\n{2}\n\nLinha sem formatação:\n{3}\n\n\n" +
                                     "Obrigado por utilizar nosso sistema!\n\n\n\n\nEquipe\n{4}",
                                     view.sacado_nome.Split(' ')[0],
                                     view.valor_boleto,
                                     boleto.LinhaDigitavel,
                                     boleto.LinhaDigitavel.Replace(" ", "").Replace(".", ""),
                                     view.cedente_nome);


            var teste = System.Web.HttpContext.Current.Server.MapPath("." + "\\EmailTemplates\\Boleto\\" + "email-boleto.html");

            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("." + "\\EmailTemplates\\Boleto\\" + "email-boleto.html")))
            {
                body = sr.ReadToEnd();
            }

            body = body.Replace("[SACADO_NOME]", view.sacado_nome);
            body = body.Replace("[VALOR_BOLETO]", view.valor_boleto);
            body = body.Replace("[CODIGO_BARRAS_FORMAT]", boleto.LinhaDigitavel);
            body = body.Replace("[CODIGO_BARRAS_N_FORMAT]", boleto.LinhaDigitavel.Replace(" ", "").Replace(".", ""));
            body = body.Replace("[CEDENTE_NOME]", view.cedente_nome);







            //"cristineysoares@mob-link.net.br"
            //"claudio.costa@live.com"
            var resEnvio = MobLink.Framework.CoreBusiness.Email.Enviar(atendimento.nota_fiscal_email,
                                                                subject,
                                                                body,
                                                                attach,
                                                                boleto.IdBoleto.ToString() + ".pdf");
            ViewBag.Msg = resEnvio.ToUpper();
        }

        public string TesteBoleto()
        {
            var subject = string.Format("BOLETO {0} - PROCESSO: {1}", "TRANSGUARD", "99999");

            string body;

            using (var sr = new StreamReader(Server.MapPath("\\EmailTemplates\\Boleto\\" + "email-boleto.html")))
            {
                body = sr.ReadToEnd();
            }

            //body = string.Format("Prezado(a) {0},\n\nSegue em anexo o boleto solicitado no valor de R${1}.\n\n" +
            //                             "SALIENTAMOS QUE ESTE BOLETO SÓ É VÁLIDO NA DATA DE HOJE.\n\n" +
            //                             "Caso seja sua preferência, pode utilizar a linha digitável a seguir com e sem formatação para efetuar o pagamento.\n\n" +
            //                             "Linha formatada:\n{2}\n\nLinha sem formatação:\n{3}\n\n\n" +
            //                             "Obrigado por utilizar nosso sistema!\n\n\n\n\nEquipe\n{4}",
            //                             "NOME",
            //                             "VALOR",
            //                             "LINHA DIGITAVEL",
            //                             "LINHA DIGITAVEL SEM FORMATAÇÃO",
            //                             "CEDENTE NOME");

            //return MobLink.Framework.CoreBusiness.Email.Enviar("cristineysoares@gmail.com", subject, body);
            //return MobLink.Framework.Comum.Email.Enviar("claudio.costa@live.com", subject, body);

            return MobLink.Framework.CoreBusiness.Email.Enviar("cristineysoares@gmail.com", subject, body);
        }


        private int GerarFaturamento(string IdGRV)
        {
            #region DECLARAÇÃO DE VARIÁVEIS
            var modelGRV = new Model.GRV.GRV();
            var modelParametrosCalculoFaturamento = new Model.Faturamento.CalcularFaturamentoParametros();
            var modelFaturamento = new Model.Faturamento.Faturamento();
            var modelCalculoFaturamento = new Model.Faturamento.CalculoFaturamento();
            Business.Sistema.Configuracoes.id_usuario = 1;
            #endregion

            #region CONSULTAR GRV
            modelGRV.id_grv = Convert.ToInt32(IdGRV);
            var dtGRV = Business.GRV.GRV.ConsultarRelacionado(modelGRV, null, null, Business.Sistema.Configuracoes.id_usuario);
            #endregion

            #region INICIAR TRANSAÇÃO
            try
            {
                Business.Sistema.GlobalDataBase.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao iniciar a transação com o Banco de Dados." + ex.Message);
            }
            #endregion INICIAR TRANSAÇÃO

            #region CADASTRAR FATURAMENTO
            try
            {
                modelParametrosCalculoFaturamento.id_grv = Convert.ToInt32(IdGRV);

                modelParametrosCalculoFaturamento.id_tipo_meio_cobranca = Funcoes.DataBase.GetInt(dtGRV, "id_tipo_meio_cobranca");

                modelParametrosCalculoFaturamento.id_usuario = Business.Sistema.Configuracoes.id_usuario;

                modelParametrosCalculoFaturamento.origemTipoComposicao = 'P'; // P = PÁTIO, L = LEILÃO

                modelParametrosCalculoFaturamento.flag_cadastrar_faturamento = true;


                
                modelParametrosCalculoFaturamento.dataHoraGuarda = Funcoes.DateTimeClass.SetSecond(Funcoes.DataBase.GetDatetime(dtGRV, "data_hora_guarda"), 0);
                //modelParametrosCalculoFaturamento.dataHoraGuarda = Funcoes.DataBase.GetDatetime(dtGRV, "data_hora_guarda");

                modelParametrosCalculoFaturamento.dataLiberacao = Funcoes.DateTimeClass.SetSecond(modelParametrosCalculoFaturamento.dataLiberacao, 0);
                //modelParametrosCalculoFaturamento.dataLiberacao = modelParametrosCalculoFaturamento.dataLiberacao;

                modelParametrosCalculoFaturamento.dataLiberacao = DateTime.Now;

                modelParametrosCalculoFaturamento.listFaturamentoDesconto = new List<Model.Faturamento.FaturamentoDesconto>();

                //modelCalculoFaturamento = Business.Faturamento.CalcularFaturamento.Calcular(modelParametrosCalculoFaturamento);
                modelCalculoFaturamento = Business.Calculos.Faturamento.Calcular(modelParametrosCalculoFaturamento);

                modelFaturamento = modelCalculoFaturamento.modelFaturamento;
            }
            catch (Exception ex)
            {
                Business.Sistema.GlobalDataBase.RollbackTransaction();

                throw new Exception("Ocorreu um erro ao cadastrar o Faturamento." + ex.Message);
            }
            #endregion

            #region FINALIZAR TRANSAÇÃO
            try
            {
                Business.Sistema.GlobalDataBase.CommitTransaction();
            }
            catch (Exception ex)
            {
                Business.Sistema.GlobalDataBase.RollbackTransaction();

                throw new Exception("Ocorreu um erro ao finalizar a transação com o Banco de Dados." + ex.Message);
            }
            #endregion FINALIZAR TRANSAÇÃO

            return modelFaturamento.id_faturamento;
        }

        public int GerarAtendimento(string GRV, Model.Atendimento.Atendimento Atendimento)
        {
            #region VERIFICAR SE JÁ EXISTE ATENDIMENTO
            Atendimento.id_atendimento =
                                    Funcoes
                                    .DataBase
                                    .GetInt(Business.Atendimento.Atendimento.Consultar(Convert.ToInt32(GRV)), "id_atendimento");

            if (Atendimento.id_atendimento != 0)
                return Atendimento.id_atendimento;
            #endregion

            #region DECLARAÇÃO DE VARIÁVEIS
            Business.Sistema.Configuracoes.id_usuario = 1;
            var modelClientesDepositos = new Model.Clientes.ClientesDepositosModel();
            var modelAtendimentoFotosResponsaveis = new Model.Atendimento.AtendimentoFotosResponsaveis();
            var modelGRV = new Model.GRV.GRV();
            var modelFaturamento = new Model.Faturamento.Faturamento();
            var modelCalculoFaturamento = new Model.Faturamento.CalculoFaturamento();
            var modelCalculoDiarias = new Model.GRV.CalculoDiarias();
            var modelParametrosCalculoFaturamento = new Model.Faturamento.CalcularFaturamentoParametros();
            //var Atendimento = new Model.Atendimento.Atendimento();
            #endregion DECLARAÇÃO DE VARIÁVEIS

            #region PREENCHIMENTO DA GRV            
            modelGRV.id_grv = Convert.ToInt32(GRV);
            var dtGRV = Business.GRV.GRV.ConsultarRelacionado(modelGRV, null, null, Business.Sistema.Configuracoes.id_usuario);
            modelGRV.id_status_operacao = Funcoes.DataBase.GetString(dtGRV, "id_status_operacao");
            modelGRV.id_cliente = Funcoes.DataBase.GetInt(dtGRV, "id_cliente");
            modelGRV.id_deposito = Funcoes.DataBase.GetInt(dtGRV, "id_deposito");
            modelGRV.data_hora_guarda = Funcoes.DataBase.GetString(dtGRV, "data_hora_guarda");
            modelGRV.numero_formulario_grv = Funcoes.DataBase.GetString(dtGRV, "numero_formulario_grv");
            #endregion

            if (modelGRV.id_status_operacao.Equals("G"))
            {
                #region PREENCHIMENTO DO ATENDIMENTO
                Atendimento.id_grv = Convert.ToInt32(GRV);
                Atendimento.id_usuario_cadastro = 1;
                Atendimento.id_qualificacao_responsavel = 1; //PRÓPRIO
                Atendimento.id_usuario_cadastro = Business.Sistema.Configuracoes.id_usuario;   //ROOT
                Atendimento.responsavel_bairro = Atendimento.nota_fiscal_bairro;
                Atendimento.responsavel_cep = Atendimento.nota_fiscal_cep;
                Atendimento.responsavel_complemento = Atendimento.nota_fiscal_complemento;
                Atendimento.responsavel_ddd = Atendimento.nota_fiscal_ddd;
                Atendimento.responsavel_documento = Atendimento.nota_fiscal_cpf;
                Atendimento.responsavel_endereco = Atendimento.nota_fiscal_endereco;
                Atendimento.responsavel_municipio = Atendimento.nota_fiscal_municipio;
                Atendimento.responsavel_nome = Atendimento.nota_fiscal_nome;
                Atendimento.responsavel_numero = Atendimento.nota_fiscal_numero;
                Atendimento.responsavel_telefone = Atendimento.nota_fiscal_telefone;
                Atendimento.responsavel_uf = Atendimento.nota_fiscal_uf;
                Atendimento.proprietario_bairro = Atendimento.nota_fiscal_bairro;
                Atendimento.proprietario_cep = Atendimento.nota_fiscal_cep;
                Atendimento.proprietario_complemento = Atendimento.nota_fiscal_complemento;
                Atendimento.proprietario_ddd = Atendimento.nota_fiscal_ddd;
                Atendimento.proprietario_documento = Atendimento.nota_fiscal_cpf;
                Atendimento.proprietario_endereco = Atendimento.nota_fiscal_endereco;
                Atendimento.proprietario_municipio = Atendimento.nota_fiscal_municipio;
                Atendimento.proprietario_nome = Atendimento.nota_fiscal_nome;
                Atendimento.proprietario_numero = Atendimento.nota_fiscal_numero;
                Atendimento.proprietario_telefone = Atendimento.nota_fiscal_telefone;
                Atendimento.proprietario_uf = Atendimento.nota_fiscal_uf;
                //Atendimento.data_alteracao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //Atendimento.data_cadastro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //Atendimento.data_impressao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //Atendimento.data_hora_inicio_atendimento = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                Atendimento.data_alteracao = DateTime.Now.ToString(); ;
                Atendimento.data_cadastro = DateTime.Now.ToString(); ;
                Atendimento.data_impressao = DateTime.Now.ToString(); ;
                Atendimento.data_hora_inicio_atendimento = DateTime.Now.ToString();

                Atendimento.total_impressoes++;
                #endregion

                #region PREENCHIMENTO DO CALCULO DE DIARIAS
                modelCalculoDiarias.data_hora_guarda = Funcoes.DataBase.GetDatetime(dtGRV, "data_hora_guarda");
                #endregion

                #region TRATAMENTO DO SAP
                try
                {
                    modelClientesDepositos = new Model.Clientes.ClientesDepositosModel()
                    {
                        id_cliente = modelGRV.id_cliente,

                        id_deposito = modelGRV.id_deposito
                    };

                    using (DataTable subConsulta = Business.Clientes.ClientesDepositosController.Consultar(modelClientesDepositos))
                    {
                        if (subConsulta == null)
                        {
                            ViewBag.Erro = "O CLIENTE ASSOCIADO AO GRV NÃO POSSUI DEPÓSITO ASSOCIADO.";
                        }

                        modelClientesDepositos.id_cliente = modelGRV.id_cliente;

                        modelClientesDepositos.id_deposito = modelGRV.id_deposito;

                        modelClientesDepositos.codigo_sap = Funcoes.DataBase.GetString(subConsulta, "codigo_sap");

                        modelClientesDepositos.codigo_sap_ordem_vendas = Funcoes.DataBase.GetString(subConsulta, "codigo_sap_ordem_vendas");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Erro = "OCORREU UM ERRO AO SELECIONAR OS DADOS DA ASSOCIAÇÃO ENTRE CLIENTE E DEPÓSITO." + ex.Message;
                    return -1;
                }

                // N = NÃO POSSUI CADASTRO SAP, 
                // P = PENDENTE DE CADASTRO, 
                // S = SOLICITAÇÃO DE CADASTRO ENVIADA, 
                // F = CADASTRADO FINALIZADO

                var modelClientes = Business.Clientes.ClientesController.Consultar(modelGRV.id_cliente);

                if (string.IsNullOrWhiteSpace(modelClientesDepositos.codigo_sap))
                {
                    Atendimento.status_cadastro_sap = 'N';

                    Atendimento.status_cadastro_ordens_venda_sap = 'N';
                }

                else if (modelClientes.flag_emissao_nota_fiscal_sap == 'S')
                {
                    Atendimento.status_cadastro_sap = 'P';

                    Atendimento.status_cadastro_ordens_venda_sap = 'P';
                }

                else
                {
                    Atendimento.status_cadastro_sap = 'N';

                    Atendimento.status_cadastro_ordens_venda_sap = 'N';
                }
                #endregion SAP

                #region INICIAR TRANSAÇÃO
                try
                {
                    Business.Sistema.GlobalDataBase.BeginTransaction();
                }
                catch (Exception ex)
                {
                    ViewBag.Erro = "OCORREU UM ERRO AO INICIAR A TRANSAÇÃO COM O BANCO DE DADOS." + ex.Message;
                    return -1;
                }
                #endregion INICIAR TRANSAÇÃO

                #region CADASTRAR ATENDIMENTO
                try
                {
                    Atendimento.id_atendimento = Business.Atendimento.Atendimento.Cadastrar(Atendimento);
                }
                catch (Exception ex)
                {
                    Business.Sistema.GlobalDataBase.RollbackTransaction();

                    ViewBag.Erro = "OCORREU UM ERRO AO CADASTRAR O ATENDIMENTO." + ex.Message;

                    return -1;
                }
                #endregion CADASTRAR ATENDIMENTO

                #region CADASTRAR FATURAMENTO
                //try
                //{
                //    modelFaturamento.id_tipo_meio_cobranca = Funcoes.DataBase.GetInt(dtGRV, "id_tipo_meio_cobranca");

                //    modelParametrosCalculoFaturamento.id_grv = modelGRV.id_grv;

                //    modelParametrosCalculoFaturamento.id_tipo_meio_cobranca = modelFaturamento.id_tipo_meio_cobranca;

                //    modelParametrosCalculoFaturamento.id_usuario = Business.Sistema.Configuracoes.id_usuario;

                //    modelParametrosCalculoFaturamento.origemTipoComposicao = 'P'; // P = PÁTIO, L = LEILÃO

                //    modelParametrosCalculoFaturamento.flag_cadastrar_faturamento = true;

                //    modelParametrosCalculoFaturamento.dataHoraGuarda = Funcoes.DateTimeClass.SetSecond(modelCalculoDiarias.data_hora_guarda, 0);

                //    modelParametrosCalculoFaturamento.dataLiberacao = Funcoes.DateTimeClass.SetSecond(modelParametrosCalculoFaturamento.dataLiberacao, 0);

                //    modelParametrosCalculoFaturamento.listFaturamentoDesconto = new List<Model.Faturamento.FaturamentoDesconto>();

                //    modelCalculoFaturamento = Business.Faturamento.CalcularFaturamento.Calcular(modelParametrosCalculoFaturamento);

                //    modelFaturamento = modelCalculoFaturamento.modelFaturamento;
                //}
                //catch (Exception ex)
                //{
                //    Business.Sistema.GlobalDataBase.RollbackTransaction();

                //    ViewBag.Erro = "OCORREU UM ERRO AO CADASTRAR O FATURAMENTO." + ex.Message;

                //    return -1;
                //}
                #endregion

                #region ATUALIZAR RENAVAM
                try
                {
                    modelGRV.id_grv = Atendimento.id_grv;

                    //TODO: BUSCAR RENAVAM
                    //modelGRV.renavam = txtProprietarioRenavam.Text;

                    Business.GRV.GRV.AtualizarRenavam(modelGRV.id_grv, modelGRV.renavam, Business.Sistema.Configuracoes.id_usuario);
                }
                catch (Exception ex)
                {
                    Business.Sistema.GlobalDataBase.RollbackTransaction();

                    ViewBag.Erro = "OCORREU UM ERRO AO ATUALIZAR O RENAVAM." + ex.Message;

                    return -1;
                }
                #endregion

                #region ATUALIZAR O STATUS DO GRV PARA AGUARDANDO PAGAMENTO
                try
                {
                    modelGRV.id_grv = Atendimento.id_grv;

                    modelGRV.id_status_operacao = "L";

                    Business.GRV.GRV.AtualizarStatusOperacao(modelGRV.id_grv, modelGRV.id_status_operacao, 1);
                }
                catch (Exception ex)
                {
                    Business.Sistema.GlobalDataBase.RollbackTransaction();

                    ViewBag.Erro = "OCORREU UM ERRO AO ATUALIZAR O STATUS DA OPERAÇÃO." + ex.Message;

                    return -1;
                }
                #endregion ATUALIZAR O STATUS DO GRV PARA AGUARDANDO PAGAMENTO

                #region FINALIZAR TRANSAÇÃO
                try
                {
                    Business.Sistema.GlobalDataBase.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Business.Sistema.GlobalDataBase.RollbackTransaction();

                    ViewBag.Erro = "OCORREU UM ERRO AO FINALIZAR A TRANSAÇÃO COM O BANCO DE DADOS." + ex.Message;

                    return -1;
                }
                #endregion FINALIZAR TRANSAÇÃO
            }

            try
            {
                return Atendimento.id_atendimento;
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "OCORREU UM ERRO AO GERAR O ATENDIMENTO." + ex.Message;
                return -1;
            }
        }

        private Models.Boleto GerarBoleto(int id_faturamento)
        {
            MobLink.Framework.WebServices.WSBoleto ws = new Framework.WebServices.WSBoleto(MobLink.Framework.Util.DetectarAmbiente());
            var b = Business.Report.ViewBoleto.Retornar(id_faturamento);

            var boletoTodos = new Framework.WebServices._WSBoleto.BoletoTodos()
            {
                banco = b.cedente_codigo_febraban,
                carteira = b.sacado_carteira,
                numeroDocumento = b.numero_documento,
                valor_boleto = b.valor_boleto,
                vencimento = b.vencimento,

                cedente_agencia = b.cedente_agencia,
                cedente_codigo = b.cedente_codigo,
                cedente_conta = b.cedente_conta_corrente,
                cedente_cpfCnpj = b.cedente_documento,
                cedente_digitoConta = b.cedente_dv,
                cedente_nome = b.cedente_nome,
                cedente_nossoNumeroBoleto = b.cedente_nosso_numero,

                instrucoes = b.sacado_instrucoes,
                sacado_bairro = b.sacado_bairro,
                sacado_cep = b.sacado_cep,
                sacado_cidade = b.sacado_cidade,
                sacado_cpfCnpj = b.sacado_documento,
                sacado_endereco = b.sacado_endereco,
                sacado_nome = b.sacado_nome,
                sacado_uf = b.sacado_uf
            };

            int id_boleto = 0;
            string linha_digitavel = string.Empty;

            var ambiente = Framework.Util.LerConfiguracao("AMBIENTE");

            var boleto = ws.BoletoBancosRetornoLinha(boletoTodos, "pdf", out linha_digitavel, out id_boleto, IsDev: ambiente == "D");

            var modelBoleto = new Models.Boleto()
            {
                IdBoleto = id_boleto,
                ImagemBoleto = boleto,
                LinhaDigitavel = linha_digitavel,
                Valor = b.valor_boleto,
                Vencimento = b.vencimento
            };

            var idFaturamentoBoleto = Business.Faturamento.FaturamentoBoletos.Cadastrar(id_faturamento,
                                                              modelBoleto.IdBoleto,
                                                              Business.Sistema.Configuracoes.id_usuario,
                                                              Convert.ToDecimal(modelBoleto.Valor),
                                                              modelBoleto.LinhaDigitavel);

            Business.Faturamento.FaturamentoBoletosImagens.Cadastrar(idFaturamentoBoleto, modelBoleto.ImagemBoleto);

            return modelBoleto;
        }

        public Models.Boleto PegarSegundaViaBoleto(int id_faturamento)
        {
            Model.Faturamento.FaturamentoBoletos boleto =
                new Model.Faturamento.FaturamentoBoletos();

            List<Model.Faturamento.FaturamentoBoletos> boletos =
                    Business.Faturamento.FaturamentoBoletos.Listar(id_faturamento);

            if (boletos != null)
            {
                boleto = boletos.LastOrDefault();
            }
            else
            {
                return new Models.Boleto();
            }

            var imagem =
                Business.Faturamento.FaturamentoBoletosImagens.DownloadByteArray(boleto.id_faturamento_boleto);

            var figura =
                Business.Faturamento.FaturamentoBoletosImagens.DownloadMemoryStream(boleto.id_faturamento_boleto);

            var retorno = new Models.Boleto()
            {
                IdBoleto = boleto.id_boleto,
                ImagemBoleto = imagem,
                LinhaDigitavel = boleto.linha,
                Valor = Convert.ToString(boleto.valor)
            };

            return retorno;

            #region ANTIGO
            //#region DECLARAÇÃO DE VARIÁVEIS
            //DateTime data_vencimento = DateTime.Now;
            //#endregion DECLARAÇÃO DE VARIÁVEIS

            //#region PRIMEIRO VERIFICO SE HOUVE AVANÇO NO STATUS DO GRV, PARA NÃO REEMPRIMIR A SEGUNDA-VIA INDEVIDAMENTE.
            //try
            //{
            //    using (var consulta = Business.GRV.GRV.ConsultarRelacionado(modelGRV, null, null, Business.Sistema.Configuracoes.id_usuario))
            //    {
            //        if (consulta != null)
            //        {
            //            modelGRV.id_grv = Funcoes.DataBase.GetInt(consulta, "id_grv");

            //            modelGRV.numero_formulario_grv = Funcoes.DataBase.GetString(consulta, "numero_formulario_grv");

            //            modelGRV.id_deposito = Funcoes.DataBase.GetInt(consulta, "id_deposito");

            //            modelGRV.id_status_operacao = Funcoes.DataBase.GetString(consulta, "id_status_operacao");

            //            modelGRV.data_hora_guarda = Funcoes.DataBase.GetString(consulta, "data_hora_guarda");

            //            if (!new[] { "L" }.Contains(modelGRV.id_status_operacao))
            //            {
            //                //Global.MessageBoxExclamation(this, "Houve avanço no Status deste GRV.\n\nStatus atual: " + DataBase.GetString(consulta, "status_operacao_descricao"));

            //                //btnImprimir.Enabled = false;

            //                //txtGRV.Focus(); return;
            //            }
            //        }
            //        else
            //        {
            //            //Global.MessageBoxExclamation(this, "Você não possui permissão para selecionar este GRV, este GRV não está apto para Atendimento ou este GRV não existe.");

            //            //txtGRV.Focus(); return;

            //            return new Model.Faturamento.Faturamento();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //Global.MessageBoxError(this, "Ocorreu um erro ao selecionar o GRV.", ex);
            //    //return;
            //    return new Model.Faturamento.Faturamento();
            //}
            //#endregion PRIMEIRO VERIFICO SE HOUVE AVANÇO NO STATUS DO GRV, PARA NÃO REEMPRIMIR A SEGUNDA-VIA INDEVIDAMENTE.

            //#region ÚLTIMO FATURAMENTO
            //var modelFaturamento = new Model.Faturamento.Faturamento();

            //modelFaturamento.id_atendimento = modelAtendimento.id_atendimento;

            //using (var consultaFaturamento = Business.Faturamento.Faturamento.ConsultarUltimoFaturamento(modelFaturamento))
            //{
            //    if (consultaFaturamento != null)
            //    {
            //        modelFaturamento.id_faturamento = Funcoes.DataBase.GetInt(consultaFaturamento, "id_faturamento");

            //        modelFaturamento.id_tipo_meio_cobranca = Funcoes.DataBase.GetInt(consultaFaturamento, "id_tipo_meio_cobranca");
            //    }
            //    else
            //    {
            //        //Global.MessageBoxExclamation(this, "Faturamento não encontrado."); return;
            //        return new Model.Faturamento.Faturamento();
            //    }
            //}
            //#endregion ÚLTIMO FATURAMENTO

            //#region DEPOIS VERIFICO A DATA DE VENCIMENTO
            //try
            //{
            //    modelFaturamento = Business.Faturamento.Faturamento.Consultar(modelFaturamento.id_faturamento);

            //    if (modelFaturamento == null)
            //    {
            //        //Global.MessageBoxExclamation(this, "Faturamento não encontrado."); return;
            //        return new Model.Faturamento.Faturamento();
            //    }

            //    data_vencimento = modelFaturamento.data_vencimento;
            //}
            //catch (Exception ex)
            //{
            //    //Global.MessageBoxError(this, "Ocorreu um erro ao selecionar o Faturamento.", ex); //return;
            //    return new Model.Faturamento.Faturamento();
            //}
            //#endregion DEPOIS VERIFICO A DATA DE VENCIMENTO

            //#region CADASTRAR NOVO FATURAMENTO

            ////if (!Global.MessageBoxQuestion(this, "Atenção, o valor do pagamento será recalculado e uma nova data de vencimento será gerada.\n\nDeseja realmente realizar a impressão da segunda via deste Atendimento?"))
            ////{
            ////    return;
            ////}

            //#region AÍ VERIFICO DE NOVO, USUÁRIO É FODA!
            //try
            //{
            //    using (var consulta = Business.GRV.GRV.ConsultarRelacionado(modelGRV, null, null, Business.Sistema.Configuracoes.id_usuario))
            //    {
            //        if (consulta != null)
            //        {
            //            modelGRV.id_grv = Funcoes.DataBase.GetInt(consulta, "id_grv");

            //            modelGRV.numero_formulario_grv = Funcoes.DataBase.GetString(consulta, "numero_formulario_grv");

            //            modelGRV.id_deposito = Funcoes.DataBase.GetInt(consulta, "id_deposito");

            //            modelGRV.id_status_operacao = Funcoes.DataBase.GetString(consulta, "id_status_operacao");

            //            modelGRV.data_hora_guarda = Funcoes.DataBase.GetString(consulta, "data_hora_guarda");

            //            if (!new[] { "L" }.Contains(modelGRV.id_status_operacao))
            //            {
            //                //Global.MessageBoxExclamation(this, "Houve avanço no Status deste GRV.\n\nStatus atual: " + DataBase.GetString(consulta, "status_operacao_descricao"));

            //                //btnImprimir.Enabled = false;

            //                //txtGRV.Focus(); return;
            //                return new Model.Faturamento.Faturamento();
            //            }
            //        }
            //        else
            //        {
            //            //Global.MessageBoxExclamation(this, "Você não possui permissão para selecionar este GRV, este GRV não está apto para Atendimento ou este GRV não existe.");

            //            //txtGRV.Focus(); return;
            //            return new Model.Faturamento.Faturamento();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //Global.MessageBoxError(this, "Ocorreu um erro ao selecionar o GRV.", ex); return;
            //    return new Model.Faturamento.Faturamento();
            //}
            //#endregion AÍ VERIFICO DE NOVO, USUÁRIO É FODA!

            //#region INICIAR TRANSAÇÃO
            //try
            //{
            //    Business.Sistema.GlobalDataBase.BeginTransaction();
            //}
            //catch (Exception ex)
            //{
            //    //Global.MessageBoxError(this, "Ocorreu um erro ao iniciar a transação com o Banco de Dados.", ex); return;
            //    return new Model.Faturamento.Faturamento();
            //}
            //#endregion INICIAR TRANSAÇÃO

            //#region CADASTRAR O FATURAMENTO
            //try
            //{
            //    var modelParametrosCalculoFaturamento = new Model.Faturamento.CalcularFaturamentoParametros();

            //    modelParametrosCalculoFaturamento.id_grv = modelGRV.id_grv;

            //    modelParametrosCalculoFaturamento.id_usuario = Business.Sistema.Configuracoes.id_usuario;

            //    modelParametrosCalculoFaturamento.origemTipoComposicao = 'P'; // P = PÁTIO, L = LEILÃO

            //    modelParametrosCalculoFaturamento.flag_cadastrar_faturamento = true;

            //    modelParametrosCalculoFaturamento.dataHoraGuarda = modelCalculoDiarias.data_hora_guarda;

            //    modelParametrosCalculoFaturamento.listFaturamentoDesconto = new List<Model.Faturamento.FaturamentoDesconto>();

            //    modelFaturamento = Business.Faturamento.CalcularFaturamento.Calcular(modelParametrosCalculoFaturamento).modelFaturamento;


            //}
            //catch (Exception ex)
            //{
            //    Business.Sistema.GlobalDataBase.RollbackTransaction();

            //    //Global.MessageBoxError(this, "Ocorreu um erro ao cadastrar o Faturamento.", ex); return;
            //    return new Model.Faturamento.Faturamento();
            //}
            //#endregion CADASTRAR O FATURAMENTO

            //#region FINALIZAR TRANSAÇÃO
            //try
            //{
            //    Business.Sistema.GlobalDataBase.CommitTransaction();
            //}
            //catch (Exception ex)
            //{
            //    Business.Sistema.GlobalDataBase.RollbackTransaction();

            //    //Global.MessageBoxError(this, "Ocorreu um erro ao finalizar a transação com o Banco de Dados.", ex); return;
            //    return new Model.Faturamento.Faturamento();
            //}
            //#endregion FINALIZAR TRANSAÇÃO

            //#endregion CADASTRAR NOVO FATURAMENTO

            //return modelFaturamento;
            #endregion
        }

        public ActionResult RelatorioPRF()
        {
            ViewBag.clientes = new SelectList(new Repositorio.PRFRepositorio().ClientesPRF(), "id", "descricao", null);
            ViewBag.depositos = new SelectList(new Repositorio.PRFRepositorio().DepositosPRF(), "id", "descricao", null);
            return View(new List<Models.Relatorio>());
        }

        [HttpPost]
        public ActionResult RelatorioPRF(FormCollection form)
        {
            var cli = form["CLIENTE"];
            var dep = form["DEPOSITO"];

            ViewBag.clientes = new SelectList(new Repositorio.PRFRepositorio().ClientesPRF(), "id", "descricao", cli);
            ViewBag.depositos = new SelectList(new Repositorio.PRFRepositorio().DepositosPRF(), "id", "descricao", dep);

            var dados = new Repositorio.PRFRepositorio().DadosRelatorioPRF(cli, dep);

            ViewBag.count = dados.Count;

            if (form["EXCEL"] == "1")
                ExcelXLSX(dados);

            return View(dados);
        }

        public void ExcelXLSX<T>(List<T> Modelo)
        {
            var model = Modelo;

            Export export = new Export();
            export.ToExcel_XLSX(Response, model);
        }
    }
}