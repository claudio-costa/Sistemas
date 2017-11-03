using MobLink.LinkLeiloes.Dominio;
using MobLink.LinkLeiloes.Repositorio;
using System.Web.Mvc;
using System.Web.Security;

namespace MobLink.LinkLeiloes.Web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Usuario Usuario, bool Lembrar = false)
        {
            var retUsuario = RepositorioGlobal.Usuario.AutenticarUsuario(Usuario.Login, Usuario.Senha);

            if (retUsuario.Id == 0)
            {
                ViewBag.Erro = "Usuário ou senha inválidos!";
                return View(Usuario);
            }

            Constantes.UsuarioLogado = retUsuario;

            FormsAuthentication.SetAuthCookie(retUsuario.Login.ToUpper().Trim(), Lembrar);

            var possuiDashboard = retUsuario.Modulos.Find(x => x.Descricao.Contains("DASHBOARD")) == null ? false : true;

            if (possuiDashboard)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Inicial", "Home");
            }
           
        }

    }
}