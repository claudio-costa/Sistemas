using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPatios.Dashboard.Controllers
{
    public class AutenticacaoController : Controller
    {
        // GET: Autenticacao
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        private String GetIP()
        {
            String ip = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ip;
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            Model.Usuario user = new Business.UsuarioBLL().login(form["UserName"], form["Password"], GetIP());
            
            if (user.idUsuario != 0)
            {
                //System.Web.Security.FormsAuthentication.SetAuthCookie(user.clienteUsuario.FirstOrDefault().nomeCliente, false);
                System.Web.Security.FormsAuthentication.SetAuthCookie(form["UserName"], false);

                HttpContext.Application["USUARIO"] = user;
                HttpContext.Application["FILTRO"] = user.depositosUsuario.FirstOrDefault();
                user.ipUsuario = GetIP();
                Business.LogBLL log = new Business.LogBLL();
                log.InserirLogAcesso(user);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Usuário ou senha inválidos! Tente novamente.";
            return View();
        }

        public ActionResult Logoff()
        {
            HttpContext.Application.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}