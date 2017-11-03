using MobLink.Utilitarios.Repositorio.ConsultaCEP;
using System.Web.Services;

namespace MobLink.WSSap.WebService
{
    [WebService(Namespace = "http://linkpatios/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WSUtilitariosLinkPatios : System.Web.Services.WebService
    {
        [WebMethod(Description = "Consulta Dados por CEP")]
        public WebCEP GetCepFromCorreios(WebCEP modelCEP)
        {
            WebCEP _WebCEP = new WebCEP();
            var ret = _WebCEP.GetCepFromCorreios(modelCEP);

            return ret;
        }
    }
}
