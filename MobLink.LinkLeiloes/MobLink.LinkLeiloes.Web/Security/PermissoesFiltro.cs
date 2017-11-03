using System.Web.Mvc;

namespace MobLink.WebLeilao.Web.Security
{
    public class PermissoesFiltro : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.HttpContext.Response.Redirect("/Home/AcessoNegado");
            }
        }
    }
}