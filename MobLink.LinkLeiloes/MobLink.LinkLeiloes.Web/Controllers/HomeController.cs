using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MobLink.LinkLeiloes.Repositorio;
using MobLink.LinkLeiloes.Web.Security;

namespace MobLink.LinkLeiloes.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Inicial()
        {
            return View();
        }

        [PermissoesFiltro(Roles = "DASHBOARD")]
        public ActionResult Index(int? Id)
        {
            List<Dominio.Leilao> leiloes;

            //LeilaoRepositorio lr = new LeilaoRepositorio();
            //StatusLeilaoRepositorio slr = new StatusLeilaoRepositorio();
            //ComitenteRepositorio cr = new ComitenteRepositorio();

            if (Id.HasValue)
            {
                leiloes = RepositorioGlobal.Leilao.SelecionarTudo().Where(p => p.id_status == Id).ToList();
            }
            else
                leiloes = RepositorioGlobal.Leilao.SelecionarTudo().ToList();

            foreach (var item in leiloes)
            {
                item.comitente = RepositorioGlobal.Comitente.SelecionarPorId(item.id_comitente);
            }

            ViewBag.Status = RepositorioGlobal.StatusLeilao.SelecionarTudoEmUso();

            var user = HttpContext.User.Identity;

            UsuarioController uc = new UsuarioController();

            return View(leiloes);
        }

        [AllowAnonymous]
        public ActionResult AcessoNegado()
        {
            return View();
        }
    }
}