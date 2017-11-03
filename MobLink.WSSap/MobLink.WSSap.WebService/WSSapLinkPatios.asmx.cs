using AutoMapper;
using MobLink.WSSap.Repositorio;
using System;
using System.Net;
using System.Web.Services;

namespace MobLink.WSSap.WebService
{
    /// <summary>
    /// Descrição resumida de WSSapLinkPatios
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "http://linkpatios/")]
    
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WSSapLinkPatios : System.Web.Services.WebService
    {
        public WSSapLinkPatios()
        {
            SapMapperProfile.Iniciar();
        }

#if (DEBUG)
        [WebMethod]
        public string teste()
        {
            var SERVICO_SAP_ORDEM_VENDA = new MobLink.WSSap.Repositorio.si_ordemvenda_requestService.si_ordemvenda_requestService();

            CookieContainer _cookie = new CookieContainer();
            SERVICO_SAP_ORDEM_VENDA.CookieContainer = _cookie;

            CredentialCache credentialCache = new CredentialCache();

            var p = Framework.Setup.WebServices.WsSap(Framework.Util.DetectarAmbiente());
            credentialCache.Add(new Uri(SERVICO_SAP_ORDEM_VENDA.Url), "Basic", new NetworkCredential(p.Usuario, p.Senha));

            SERVICO_SAP_ORDEM_VENDA.Credentials = credentialCache;
            SERVICO_SAP_ORDEM_VENDA.PreAuthenticate = true;

            var OBJETO_SAP_ORDEM_VENDA = new MobLink.WSSap.Repositorio.si_ordemvenda_requestService.dt_ordemvenda_request()
            {

            };

            try
            {
                SERVICO_SAP_ORDEM_VENDA.si_ordemvenda_request(OBJETO_SAP_ORDEM_VENDA);
            }
            catch (Exception)
            {

                throw;
            }

            return "";
        }
#endif

        [WebMethod(Description = "Cadastra FB70")]
        public Dominio.Retorno FB70(Dominio.OrdemInternaFB70 Entrada)
        {
            var classe_sap = Mapper.Map<Dominio.OrdemInternaFB70, Repositorio.si_ordem_interna_requestService.dt_titulo_ord_int_fb70>(Entrada);
            return new Repositorio.OrdemInternaRepositorio().Inserir(classe_sap);
        }

        [WebMethod(Description = "Cadastra ou modifica um cliente no SAP")]
        public Dominio.Retorno CadastrarClienteSAP(Dominio.ClienteSap Cliente)
        {
            return new Repositorio.ClienteRepositorio().InserirClienteSap(Cliente);
        }
        
        [WebMethod(Description = "Cadastra ordem de venda no SAP")]
        public Dominio.Retorno CadastrarOrdemVendaSAP(Dominio.OrdemVendaSAP OrdemVenda)
        {
            return new Repositorio.OrdemVendaRepositorio().Inserir(OrdemVenda);
        }

        [WebMethod(Description = "Método exclusivo para consumo do SAP")]
        public Dominio.Retorno IngressaTransacaoSAP(Dominio.Transacao Transacao)
        {
            return new Repositorio.SapRepositorio().InserirRetornoSap(Transacao);
        }
    }
}
