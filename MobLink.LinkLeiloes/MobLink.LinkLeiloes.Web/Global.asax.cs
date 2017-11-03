using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobLink.LinkLeiloes.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public override void Dispose()
        {
            base.Dispose();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
