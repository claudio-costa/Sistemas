using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobLink.WebLeilao.Web.Controllers
{
    public class ConfiguracoesController : Controller
    {
        // GET: Configuracoes
        public ActionResult Leilao()
        {
            return View();
        }

        public ActionResult Sistema()
        {
            return View();
        }
    }
}