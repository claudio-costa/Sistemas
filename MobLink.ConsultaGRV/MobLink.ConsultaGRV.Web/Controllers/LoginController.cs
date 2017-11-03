using Business.Usuarios;
using Funcoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MobLink.ConsultaGRV.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string login, string senha)
        {
            RetornoAutenticacao ret = Login(login, senha);

            if (ret.Autenticado)
            {
                FormsAuthentication.SetAuthCookie(login.ToUpper().Trim(), false);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Erro = ret.MensagemAutenticacao;
            return View();
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private struct RetornoAutenticacao
        {
            public string MensagemAutenticacao { get; set; }
            public bool Autenticado { get; set; }
        }

        private RetornoAutenticacao Login(string login, string senha)
        {
            if (string.IsNullOrEmpty(login))
                return new RetornoAutenticacao() { Autenticado = false, MensagemAutenticacao = "Digite o seu login para continuar" };

            if (string.IsNullOrEmpty(senha))
                return new RetornoAutenticacao() { Autenticado = false, MensagemAutenticacao = "Digite sua senha para continuar" };

            login = login.Trim().ToUpper();
            senha = senha.Trim().ToUpper();

            try
            {

                MSDAL.ConnectionFactory.connectionString = MobLink.Framework.Util.LerConfiguracao("CONEXAO");
                MSDAL.ConnectionFactory.ConnectDataBase();

                using (DataTable consulta = Usuarios.ConsultarAcesso(login, senha))
                {
                    if (consulta == null)
                    {
                        return new RetornoAutenticacao() { Autenticado = false, MensagemAutenticacao = "Usuário não encontrado ou senha incorreta" };
                    }
                    else if (DataBase.GetChar(consulta, "flag_ativo") == 'N')
                    {
                        return new RetornoAutenticacao() { Autenticado = false, MensagemAutenticacao = "Usuário desativado no Sistema" };
                    }

                    Business.Sistema.Configuracoes.id_usuario = DataBase.GetInt(consulta, "id_usuario");

                    Business.Sistema.Configuracoes.login_usuario = login;

                    Business.Sistema.Configuracoes.id_empresa = DataBase.GetInt(consulta, "id_empresa");

                    Business.Sistema.Configuracoes.modelUsuarios = Business.Usuarios.Usuarios.Retornar(Business.Sistema.Configuracoes.id_usuario);

                    return new RetornoAutenticacao() { Autenticado = true };
                }
            }
            catch (Exception ex)
            {
                return new RetornoAutenticacao() { Autenticado = false, MensagemAutenticacao = "Ocorreu um erro ao consultar o Usuário. " + ex.Message };
            }
            finally
            {
                MSDAL.ConnectionFactory.DisconnectDataBase();
            }
        }
    }
}