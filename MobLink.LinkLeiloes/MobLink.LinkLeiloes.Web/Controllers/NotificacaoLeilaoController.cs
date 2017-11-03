using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobLink.LinkLeiloes.Web.Controllers
{
    public class NotificacaoLeilaoController : Controller
    {
        // GET: NotificacaoLeilao
        public ActionResult Index(int id)
        {
            var notificacoes = Repositorio.RepositorioGlobal.NotificaoLeilao.SelecionarTudo(id);

            return View(notificacoes);
        }

        public ActionResult Inserir(FormCollection form)
        {
            string s = string.Empty;

            s = form[0];
            s = form[1];
            

            return View();
        }
    }
}